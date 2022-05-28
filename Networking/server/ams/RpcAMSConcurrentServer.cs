using Networking.rpc.ams;
using Services.ams;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Networking.server.ams
{
    public class RpcAMSConcurrentServer : AbsConcurrentServer
    {
        private readonly IServicesAMS _server;
        public RpcAMSConcurrentServer(string host, int port, IServicesAMS server)
            : base(host, port)
        {
            Console.WriteLine("RpcAMSConcurrentServer port " + port);
            _server = server;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            ClientRpcAMSWorker worker = new(_server, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
