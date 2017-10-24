using Binance.Net.Interfaces;
using System.Net;

namespace Binance.Net.Implementations
{
    public class RequestFactory : IRequestFactory
    {
        public IRequest Create(string uri)
        {
            return new Request(WebRequest.Create(uri));
        }
    }
}
