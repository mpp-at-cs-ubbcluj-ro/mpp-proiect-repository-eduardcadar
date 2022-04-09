using System;
using System.Net;
using System.Net.Sockets;

namespace Networking.server
{
    public abstract class AbstractServer
    {
        private readonly int _port;
        private readonly string _host;
        private TcpListener _server;
        
        public AbstractServer(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Start()
        {
            IPAddress adr = IPAddress.Parse(_host);
            IPEndPoint ep = new IPEndPoint(adr, _port);
            _server = new TcpListener(ep);
            _server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for clients...");
                TcpClient client = _server.AcceptTcpClient();
                Console.WriteLine("Client connected...");
                ProcessRequest(client);
            }
        }

        protected abstract void ProcessRequest(TcpClient client);
    }
}
