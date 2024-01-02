using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSpotQuery<T> : Query<T> where T : BinanceResponse
    {
        public override List<string> StreamIdentifiers { get; set; }

        public BinanceSpotQuery(BinanceSocketQuery request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            StreamIdentifiers = new List<string> { request.Id.ToString() };
        }

    }
}
