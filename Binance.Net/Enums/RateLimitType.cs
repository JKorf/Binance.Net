using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of rate limit
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RateLimitType>))]
    public enum RateLimitType
    {
        /// <summary>
        /// ["<c>REQUEST_WEIGHT</c>"] Request weight
        /// </summary>
        [Map("REQUEST_WEIGHT")]
        RequestWeight,
        /// <summary>
        /// ["<c>ORDERS</c>"] Order amount
        /// </summary>
        [Map("ORDERS")]
        Orders,
        /// <summary>
        /// ["<c>RAW_REQUESTS</c>"] Raw requests
        /// </summary>
        [Map("RAW_REQUESTS")]
        RawRequests,
        /// <summary>
        /// ["<c>CONNECTIONS</c>"] Connections
        /// </summary>
        [Map("CONNECTIONS")]
        Connections
    }
}

