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
    public class ProtoProxy : IServices
    {
        private readonly string _host;
        private readonly int _port;
        private IObserver _client;
        private NetworkStream _stream;

        private TcpClient _connection;
        private volatile bool _finished;

        private readonly Queue<Response> _responses;
        private EventWaitHandle _waitHandle;

        public ProtoProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<Response>();
        }

        public void Login(Model.Agency agency, IObserver observer)
        {
            InitializeConnection();
            SendRequest(ProtoUtils.CreateLoginRequest(agency));
            Response response = ReadResponse();
            if (response.Type == Response.Types.Type.Ok)
            {
                _client = observer;
                return;
            }
            if (response.Type == Response.Types.Type.Error)
            {
                string err = response.Error;
                CloseConnection();
                throw new ServiceException(err);
            }
        }

        public void Logout(Model.Agency agency, IObserver observer)
        {
            SendRequest(ProtoUtils.CreateLogoutRequest(agency));
            Response response = ReadResponse();
            CloseConnection();
            if (response.Type == Response.Types.Type.Error)
            {
                string err = response.Error;
                throw new ServiceException(err);
            }
        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                _connection.Close();
                _client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    response = _responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void SendRequest(Request request)
        {
            try
            {
                lock (_stream)
                {
                    request.WriteDelimitedTo(_stream);
                    _stream.Flush();
                }
                
            }
            catch (Exception e)
            {
                throw new ServiceException("Error sending object " + e);
            }
        }

        public IEnumerable<Model.Agency> GetAgencies()
        {
            SendRequest(ProtoUtils.CreateGetAgenciesRequest());
            Response response = ReadResponse();
            if (response.Type == Response.Types.Type.Error)
            {
                string err = response.Error;
                throw new ServiceException(err);
            }
            Model.Agency[] agencies = ProtoUtils.GetAgencies(response);
            return agencies.ToArray();
        }

        public IEnumerable<Model.Trip> GetTrips(string destination, TimeSpan startTime, TimeSpan endTime)
        {
            dto.TripFilterDTO filterDTO = new() { Destination = destination, StartTime = startTime, EndTime = endTime };
            Request request = ProtoUtils.CreateGetTripsRequest(filterDTO);
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == Response.Types.Type.Error)
            {
                string err = response.Error;
                throw new ServiceException(err);
            }
            Model.Trip[] trips = ProtoUtils.GetTrips(response);
            return trips.ToArray();
        }

        public IEnumerable<Model.Reservation> GetReservations()
        {
            Request request = ProtoUtils.CreateGetReservationsRequest();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == Response.Types.Type.Error)
            {
                string err = response.Error;
                throw new ServiceException(err);
            }
            Model.Reservation[] reservations = ProtoUtils.GetReservations(response);
            return reservations.ToArray();
        }

        public void SaveReservation(Model.Reservation reservation)
        {
            Request request = ProtoUtils.CreateSaveReservationRequest(reservation);
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == Response.Types.Type.Error)
            {
                string err = response.Error;
                throw new ServiceException(err);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartReader()
        {
            Thread tw = new(Run);
            tw.Start();
        }

        private void HandleUpdate(Response response)
        {
            if (response.Type == Response.Types.Type.NewReservation)
            {
                Model.Reservation reservation = ProtoUtils.GetReservation(response);
                try
                {
                    _client.ReservationSaved(reservation);
                }
                catch (ServiceException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private static bool IsUpdate(Response response)
        {
            return response.Type == Response.Types.Type.NewReservation;
        }

        public virtual void Run()
        {
            while (!_finished)
            {
                try
                {
                    Response response = Response.Parser.ParseDelimitedFrom(_stream);
                    Console.WriteLine("Response received: " + response);
                    if (IsUpdate(response))
                        HandleUpdate(response);
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue(response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error: " + e);
                }
            }
        }
    }
}
