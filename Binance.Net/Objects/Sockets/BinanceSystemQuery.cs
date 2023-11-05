using Binance.Net.Objects.Internal;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Sockets
{
    internal class BinanceSystemQuery<T> : Query<T> where T: BinanceSocketQueryResponse
    {
        public BinanceSystemQuery(BinanceSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
        }

        public override CallResult<T> HandleResponse(ParsedMessage<T> message) => new CallResult<T>(message.Data);
        public override bool MessageMatchesQuery(ParsedMessage<T> message) => ((BinanceSocketRequest)Request).Id == message.Data.Id;
    }
}
