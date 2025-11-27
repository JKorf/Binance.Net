using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSystemQuery<T> : Query<T> where T : BinanceSocketQueryResponse
    {
        public BinanceSystemQuery(BinanceSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageRouter = MessageRouter.Create<T>(request.Id.ToString());
            MessageMatcher = MessageMatcher.Create<T>(request.Id.ToString());
        }
    }
}
