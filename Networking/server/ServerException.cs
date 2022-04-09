using System;

namespace Networking.server
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message) { }
        public ServerException(string message, Exception ex) : base(message, ex) { }
    }
}
