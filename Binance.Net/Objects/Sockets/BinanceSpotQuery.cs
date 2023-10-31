using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSpotQuery : Query
    {
        public BinanceSpotQuery(object request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
        }

        public override CallResult HandleResponse(ParsedMessage message) => throw new NotImplementedException();
        public override bool MessageMatchesQuery(ParsedMessage message) => throw new NotImplementedException();
    }
}
