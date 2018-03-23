using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class OrderResponseTypeConverter: BaseConverter<OrderResponseType>
    {
        public OrderResponseTypeConverter(): this(true) { }
        public OrderResponseTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<OrderResponseType, string> Mapping => new Dictionary<OrderResponseType, string>()
        {
            { OrderResponseType.Acknowledge, "ACK" },
            { OrderResponseType.Result, "RESULT" },
            { OrderResponseType.Full, "FULL" }
        };
    }
}
