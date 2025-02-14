using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of rate limit
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<RateLimitType>))] public  enum RateLimitType
    {
        /// <summary>
        /// Request weight
        /// </summary>
        [Map("REQUEST_WEIGHT")]
        RequestWeight,
        /// <summary>
        /// Order amount
        /// </summary>
        [Map("ORDERS")]
        Orders,
        /// <summary>
        /// Raw requests
        /// </summary>
        [Map("RAW_REQUESTS")]
        RawRequests,
        /// <summary>
        /// Connections
        /// </summary>
        [Map("CONNECTIONS")]
        Connections
    }
}
