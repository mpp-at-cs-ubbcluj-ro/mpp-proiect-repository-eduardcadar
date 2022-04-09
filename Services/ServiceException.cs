using System;

namespace Services
{
    public class ServiceException : Exception
    {
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, Exception ex) : base(message, ex) { }
    }
}
