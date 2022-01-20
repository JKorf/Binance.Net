using System.Collections.Generic;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Converters
{
    internal class RateLimitConverter: BaseConverter<RateLimitType>
    {
        public RateLimitConverter() : this(true) { }
        public RateLimitConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<RateLimitType, string>> Mapping => new List<KeyValuePair<RateLimitType, string>>
        {
            new KeyValuePair<RateLimitType, string>(RateLimitType.Orders, "ORDERS"),
            new KeyValuePair<RateLimitType, string>(RateLimitType.RequestWeight, "REQUEST_WEIGHT"),
            new KeyValuePair<RateLimitType, string>(RateLimitType.RawRequests, "RAW_REQUESTS")
        };
    }
}
