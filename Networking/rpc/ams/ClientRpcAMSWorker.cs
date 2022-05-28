using Model;
using Networking.dto;
using Services;
using Services.ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Networking.rpc.ams
{
    public class ClientRpcAMSWorker
    {
        private readonly IServicesAMS _server;
        private readonly TcpClient _connection;
        private readonly NetworkStream _stream;
        private readonly IFormatter _formatter;
        private volatile bool _connected;

        public ClientRpcAMSWorker(IServicesAMS server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
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
                    object request = _formatter.Deserialize(_stream);
                    object response = HandleRequest((Request)request);
                    if (response != null)
                        SendResponse((Response)response);
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

        private static readonly Response _okResponse = new Response.Builder().Type(ResponseType.OK).Build();

        private object HandleRequest(Request request)
        {
            if (request.Type == RequestType.LOGIN) return HandleLogin(request);
            if (request.Type == RequestType.LOGOUT) return HandleLogout(request);
            if (request.Type == RequestType.GET_AGENCIES) return HandleGetAgencies(request);
            if (request.Type == RequestType.GET_TRIPS) return HandleGetTrips(request);
            if (request.Type == RequestType.GET_RESERVATIONS) return HandleGetReservations(request);
            if (request.Type == RequestType.SAVE_RESERVATION) return HandleSaveReservation(request);
            return null;
        }

        private object HandleLogin(Request request)
        {
            Console.WriteLine("Login request.." + request.Type);
            Agency agency = (Agency)request.Data;
            try
            {
                lock (_server)
                {
                    _server.Login(agency);
                }
                return _okResponse;
            }
            catch (ServiceException ex)
            {
                _connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(ex.Message).Build();
            }
        }

        private object HandleLogout(Request request)
        {
            Console.WriteLine("Logout request.." + request.Type);
            Agency agency = (Agency)request.Data;
            try
            {
                lock (_server)
                {
                    _server.Logout(agency);
                }
                _connected = false;
                return _okResponse;
            }
            catch (ServiceException ex)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(ex.Message).Build();
            }
        }

        private object HandleSaveReservation(Request request)
        {
            Console.WriteLine("Save reservation request.." + request.Type);
            Reservation reservation = (Reservation)request.Data;
            try
            {
                lock (_server)
                {
                    _server.SaveReservation(reservation);
                }
                return _okResponse;
            }
            catch (ServiceException e)
            {
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private object HandleGetReservations(Request request)
        {
            Console.WriteLine("Get reservations request.." + request.Type);
            try
            {
                IEnumerable<Reservation> reservationsCol;
                lock (_server)
                {
                    reservationsCol = _server.GetReservations();
                }
                Reservation[] reservations = reservationsCol.ToArray();

                return new Response.Builder().Type(ResponseType.GET_RESERVATIONS).Data(reservations).Build();
            }
            catch (ServiceException ex)
            {
                _connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(ex.Message).Build();
            }
        }

        private object HandleGetTrips(Request request)
        {
            Console.WriteLine("Get trips request.." + request.Type);
            TripFilterDTO fDTO = (TripFilterDTO)request.Data;
            try
            {
                IEnumerable<Trip> tripsCol;
                lock (_server)
                {
                    tripsCol = _server.GetTrips(fDTO.Destination, fDTO.StartTime, fDTO.EndTime);
                }
                Trip[] trips = tripsCol.ToArray();
                return new Response.Builder().Type(ResponseType.GET_TRIPS).Data(trips).Build();
            }
            catch (ServiceException ex)
            {
                _connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(ex.Message).Build();
            }
        }

        private object HandleGetAgencies(Request request)
        {
            Console.WriteLine("Get agencies request.." + request.Type);
            try
            {

                IEnumerable<Agency> agenciesCol;

                lock (_server)
                {
                    agenciesCol = _server.GetAgencies();
                }

                Agency[] agencies = agenciesCol.ToArray();
                return new Response.Builder().Type(ResponseType.GET_AGENCIES).Data(agencies).Build();
            }
            catch (ServiceException ex)
            {
                _connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(ex.Message).Build();
            }
        }

        private void SendResponse(Response response)
        {
            Console.WriteLine("Sending response " + response);
            lock (_stream)
            {
                _formatter.Serialize(_stream, response);
                _stream.Flush();
            }
        }
    }
}
