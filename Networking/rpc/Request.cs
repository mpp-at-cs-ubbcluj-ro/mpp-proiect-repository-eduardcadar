using System;

namespace Networking.rpc
{
    [Serializable]
    public class Request
    {
        public RequestType Type { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return "Request{type=" + Type + ", data=" + Data + "}";
        }

        public class Builder
        {
            private readonly Request _request = new();

            public Builder Type(RequestType type)
            {
                _request.Type = type;
                return this;
            }

            public Builder Data(object data)
            {
                _request.Data = data;
                return this;
            }

            public Request Build()
            {
                return _request;
            }
        }
    }
}
