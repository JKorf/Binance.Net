namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of rate limit
    /// </summary>
    public enum RateLimitType
    {
        /// <summary>
        /// Request weight
        /// </summary>
        RequestWeight,
        /// <summary>
        /// Order amount
        /// </summary>
        Orders,
        /// <summary>
        /// Raw requests
        /// </summary>
        RawRequests,
        /// <summary>
        /// Connections
        /// </summary>
        Connections
    }
}
