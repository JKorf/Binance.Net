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
        Second,
        /// <summary>
        /// Minutes
        /// </summary>
        Minute,
        /// <summary>
        /// Days
        /// </summary>
        Day
    }
}
