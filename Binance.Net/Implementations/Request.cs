using Binance.Net.Interfaces;
using System.Net;

namespace Binance.Net.Implementations
{
    public class Request : IRequest
    {
        private readonly WebRequest request;

        public Request(WebRequest request)
        {
            this.request = request;
        }

        public WebHeaderCollection Headers
        {
            get => request.Headers;
            set => request.Headers = value;
        }
        public string Method
        {
            get => request.Method;
            set => request.Method = value;
        }

        public IResponse GetResponse()
        {
            return new Response(request.GetResponse());
        }
    }
}
