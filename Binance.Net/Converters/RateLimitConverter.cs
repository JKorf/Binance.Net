using Binance.Net.Enums;

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
            new KeyValuePair<RateLimitType, string>(RateLimitType.RawRequests, "RAW_REQUESTS"),
            new KeyValuePair<RateLimitType, string>(RateLimitType.Connections, "CONNECTIONS")
        };
    }
}
