using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSpotQuery<T> : Query<T> where T : BinanceResponse
    {
        public override List<string> Identifiers { get; }

        public BinanceSpotQuery(BinanceSocketQuery request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            Identifiers = new List<string> { request.Id.ToString() };
        }
    }
}
