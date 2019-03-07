using Binance.Net.Objects;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class OrderResponseTypeConverter: BaseConverter<OrderResponseType>
    {
        public OrderResponseTypeConverter(): this(true) { }
        public OrderResponseTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderResponseType, string>> Mapping => new List<KeyValuePair<OrderResponseType, string>>
        {
            new KeyValuePair<OrderResponseType, string>(OrderResponseType.Acknowledge, "ACK"),
            new KeyValuePair<OrderResponseType, string>(OrderResponseType.Result, "RESULT"),
            new KeyValuePair<OrderResponseType, string>( OrderResponseType.Full, "FULL")
        };
    }
}
