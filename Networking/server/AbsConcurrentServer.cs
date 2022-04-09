using System.Net.Sockets;
using System.Threading;

namespace Networking.server
{
    public abstract class AbsConcurrentServer : AbstractServer
    {
        public AbsConcurrentServer(string host, int port) : base(host, port) { }

        protected override void ProcessRequest(TcpClient client)
        {
            Thread t = CreateWorker(client);
            t.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}
