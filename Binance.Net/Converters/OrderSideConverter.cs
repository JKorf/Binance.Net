using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class OrderSideConverter : BaseConverter<OrderSide>
    {
        public OrderSideConverter(): this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderSide, string>> Mapping => new List<KeyValuePair<OrderSide, string>>
        {
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "BUY"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "SELL")
        };
    }
}
