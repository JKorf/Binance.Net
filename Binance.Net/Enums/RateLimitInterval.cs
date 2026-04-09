using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Rate limit on what unit
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RateLimitInterval>))]
    public enum RateLimitInterval
    {
        /// <summary>
        /// Seconds
        /// </summary>
        [Map("SECOND")]
        Second,
        /// <summary>
        /// Minutes
        /// </summary>
        [Map("MINUTE")]
        Minute,
        /// <summary>
        /// Days
        /// </summary>
        [Map("DAY")]
        Day
    }
}
