using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSystemQuery<T> : Query<T> where T: BinanceSocketQueryResponse
    {
        public override List<string> StreamIdentifiers { get; set; }

        public BinanceSystemQuery(BinanceSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            StreamIdentifiers = new List<string> { request.Id.ToString() };
        }
    }
}
