﻿using System;
using System.Net.Sockets;
using System.Threading;
using Networking.rpc;
using Services;

namespace Networking.server
{
    public class RpcConcurrentServer : AbsConcurrentServer
    {
        private readonly IServices _server;

        public RpcConcurrentServer(string host, int port, IServices server) : base(host, port)
        {
            _server = server;
            Console.WriteLine("RpcConcurrentServer...");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            ClientRpcWorker worker = new(_server, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}
