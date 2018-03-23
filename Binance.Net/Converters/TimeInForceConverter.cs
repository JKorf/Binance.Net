using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class TimeInForceConverter : BaseConverter<TimeInForce>
    {
        public TimeInForceConverter(): this(true) { }
        public TimeInForceConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<TimeInForce, string> Mapping => new Dictionary<TimeInForce, string>()
        {
            { TimeInForce.GoodTillCancel, "GTC" },
            { TimeInForce.ImmediateOrCancel, "IOC" },
            { TimeInForce.FillOrKill, "FOK" },
        };
    }
}
