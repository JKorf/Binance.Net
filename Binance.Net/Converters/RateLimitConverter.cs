using System.Collections.Generic;
using Binance.Net.Objects;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    public class RateLimitConverter: BaseConverter<RateLimitType>
    {
        public RateLimitConverter() : this(true) { }
        public RateLimitConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<RateLimitType, string> Mapping => new Dictionary<RateLimitType, string>()
        {
            { RateLimitType.Orders, "ORDERS" },
            { RateLimitType.RequestWeight, "REQUEST_WEIGHT" },
            { RateLimitType.RawRequests, "RAW_REQUEST" },
        };
    }
}
