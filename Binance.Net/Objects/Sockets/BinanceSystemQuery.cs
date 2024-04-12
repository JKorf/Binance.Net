using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSystemQuery<T> : Query<T> where T: BinanceSocketQueryResponse
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public BinanceSystemQuery(BinanceSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { request.Id.ToString() };
        }
    }
}
