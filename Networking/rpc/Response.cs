using System;

namespace Networking.rpc
{
    [Serializable]
    public class Response
    {
        public ResponseType Type { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return "Response{type=" + Type + ", data=" + Data + "}";
        }

        public class Builder
        {
            private readonly Response _response = new();

            public Builder Type(ResponseType type)
            {
                _response.Type = type;
                return this;
            }

            public Builder Data(object data)
            {
                _response.Data = data;
                return this;
            }

            public Response Build()
            {
                return _response;
            }
        }
    }
}
