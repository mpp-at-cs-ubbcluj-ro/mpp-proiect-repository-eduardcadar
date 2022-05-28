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
    public class ServerRpcAMSProxy : IServicesAMS
    {
        private readonly string _host;
        private readonly int _port;

        //private IObserver _client;

        private NetworkStream _stream;

        private IFormatter _formatter;
        private TcpClient _connection;
        private volatile bool _finished;

        private Queue<Response> _responses;
        private EventWaitHandle _waitHandle;

        public ServerRpcAMSProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<Response>();
        }

        private void SendRequest(Request request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                throw new ServiceException("Error sending object " + e);
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

        public void Login(Agency agency)
        {
            InitializeConnection();
            Request request = new Request.Builder().Type(RequestType.LOGIN).Data(agency).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.OK)
            {
                //_client = observer;
                return;
            }
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                CloseConnection();
                throw new ServiceException(err);
            }
        }

        public void Logout(Agency agency)
        {
            Request request = new Request.Builder().Type(RequestType.LOGOUT).Data(agency).Build();
            SendRequest(request);
            Response response = ReadResponse();
            CloseConnection();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new ServiceException(err);
            }
        }

        public IEnumerable<Agency> GetAgencies()
        {
            Request request = new Request.Builder().Type(RequestType.GET_AGENCIES).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new ServiceException(err);
            }
            Agency[] agencies = (Agency[])response.Data;
            return agencies.ToArray();
        }

        public IEnumerable<Reservation> GetReservations()
        {
            Request request = new Request.Builder().Type(RequestType.GET_RESERVATIONS).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new ServiceException(err);
            }
            Reservation[] reservations = (Reservation[])response.Data;
            return reservations.ToArray();
        }

        public IEnumerable<Trip> GetTrips(string destination, TimeSpan startTime, TimeSpan endTime)
        {
            TripFilterDTO filterDTO = new() { Destination = destination, StartTime = startTime, EndTime = endTime };
            Request request = new Request.Builder().Type(RequestType.GET_TRIPS).Data(filterDTO).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new ServiceException(err);
            }
            Trip[] trips = (Trip[])response.Data;
            return trips.ToArray();
        }

        public void SaveReservation(Reservation reservation)
        {
            Request request = new Request.Builder().Type(RequestType.SAVE_RESERVATION).Data(reservation).Build();
            SendRequest(request);
            Response response = ReadResponse();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new ServiceException(err);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                _connection.Close();
                //_client = null;
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

        private static bool IsUpdate(Response response)
        {
            return response.Type == ResponseType.NEW_RESERVATION;
        }

        public virtual void Run()
        {
            while (!_finished)
            {
                try
                {
                    object response = _formatter.Deserialize(_stream);
                    Console.WriteLine("Response received: " + response);
                    if (IsUpdate((Response)response))
                    {

                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((Response)response);
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
