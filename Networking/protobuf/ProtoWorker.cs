using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;
using Protobuf;
using Services;

namespace Networking.protobuf
{
    public class ProtoWorker : IObserver
    {
        private readonly IServices _server;
        private readonly TcpClient _connection;
        private readonly NetworkStream _stream;
        private volatile bool _connected;

        public ProtoWorker(IServices server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = _connection.GetStream();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    Request request = Request.Parser.ParseDelimitedFrom(_stream);
                    Response response = HandleRequest(request);
                    if (response != null)
                        SendResponse(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    Thread.Sleep(500);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                _stream.Close();
                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }

        private void SendResponse(Response response)
        {
            Console.WriteLine("Sending response " + response);
            lock (_stream)
            {
                response.WriteDelimitedTo(_stream);
                _stream.Flush();
            }
        }

        private Response HandleRequest(Request request)
        {
            return request.Type switch
            {
                Request.Types.Type.Login => HandleLogin(request),
                Request.Types.Type.Logout => HandleLogout(request),
                Request.Types.Type.GetAgencies => HandleGetAgencies(request),
                Request.Types.Type.GetTrips => HandleGetTrips(request),
                Request.Types.Type.GetReservations => HandleGetReservations(request),
                Request.Types.Type.SaveReservation => HandleSaveReservation(request),
                _ => null
            };
        }

        private Response HandleSaveReservation(Request request)
        {
            Console.WriteLine("Save reservation request.." + request.Type);
            Model.Reservation reservation = ProtoUtils.GetReservation(request);
            try
            {
                lock (_server)
                {
                    _server.SaveReservation(reservation);
                }
                return ProtoUtils.CreateOkResponse();
            }
            catch (ServiceException ex)
            {
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        private Response HandleGetReservations(Request request)
        {
            Console.WriteLine("Get reservations request.." + request.Type);
            try
            {
                IEnumerable<Model.Reservation> reservationsCol;
                lock (_server)
                {
                    reservationsCol = _server.GetReservations();
                }
                Model.Reservation[] reservations = reservationsCol.ToArray();

                return ProtoUtils.CreateGetReservationsResponse(reservations);
            }
            catch (ServiceException ex)
            {
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        private Response HandleGetTrips(Request request)
        {
            Console.WriteLine("Get trips request.." + request.Type);
            dto.TripFilterDTO fDTO = ProtoUtils.GetTripFilterDTO(request);
            try
            {
                IEnumerable<Model.Trip> tripsCol;
                lock (_server)
                {
                    tripsCol = _server.GetTrips(fDTO.Destination, fDTO.StartTime, fDTO.EndTime);
                }
                Model.Trip[] trips = tripsCol.ToArray();
                return ProtoUtils.CreateGetTripsResponse(trips);
            }
            catch (ServiceException ex)
            {
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        private Response HandleGetAgencies(Request request)
        {
            Console.WriteLine("Get agencies request.." + request.Type);
            try
            {
                IEnumerable<Model.Agency> agenciesCol;
                lock (_server)
                {
                    agenciesCol = _server.GetAgencies();
                }
                Model.Agency[] agencies = agenciesCol.ToArray();
                return ProtoUtils.CreateGetAgenciesResponse(agencies);
            }
            catch (ServiceException ex)
            {
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        private Response HandleLogout(Request request)
        {
            Console.WriteLine("Logout request.." + request.Type);
            Model.Agency agency = ProtoUtils.GetAgency(request);
            try
            {
                lock (_server)
                {
                    _server.Logout(agency, this);
                }
                _connected = false;
                return ProtoUtils.CreateOkResponse();
            }
            catch (ServiceException ex)
            {
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        private Response HandleLogin(Request request)
        {
            Console.WriteLine("Login request..");
            Model.Agency agency = ProtoUtils.GetAgency(request);
            try
            {
                lock (_server)
                {
                    _server.Login(agency, this);
                }
                return ProtoUtils.CreateOkResponse();
            }
            catch (ServiceException ex)
            {
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        public void ReservationSaved(Model.Reservation reservation)
        {
            Console.WriteLine("Reservation saved: " + reservation);
            try
            {
                SendResponse(ProtoUtils.CreateNewReservationResponse(reservation));
            }
            catch (Exception e)
            {
                throw new ServiceException("Reservation saved error: ", e);
            }
        }
    }
}
