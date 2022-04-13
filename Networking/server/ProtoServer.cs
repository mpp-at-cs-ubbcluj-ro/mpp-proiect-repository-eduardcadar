using System;
using System.Net.Sockets;
using System.Threading;
using Networking.protobuf;
using Services;

namespace Networking.server
{
    public class ProtoServer : AbsConcurrentServer
    {
        private readonly IServices _server;
        public ProtoServer(string host, int port, IServices server) : base(host, port)
        {
            _server = server;
            Console.WriteLine("ProtoServer...");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            ProtoWorker worker = new(_server, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
