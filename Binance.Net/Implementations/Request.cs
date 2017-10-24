using Binance.Net.Interfaces;
using System.Net;

namespace Binance.Net.Implementations
{
    public class Request : IRequest
    {
        private WebRequest request;

        public Request(WebRequest request)
        {
            this.request = request;
        }

        public WebHeaderCollection Headers
        {
            get { return request.Headers; }
            set { request.Headers = value; }
        }
        public string Method
        {
            get { return request.Method; }
            set { request.Method = value; }
        }

        public IResponse GetResponse()
        {
            return new Response(request.GetResponse());
        }
    }
}
