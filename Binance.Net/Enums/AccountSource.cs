using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Account source
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountSource>))]
    public enum AccountSource
    {
        /// <summary>
        /// ["<c>SPOT</c>"] Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>FUND</c>"] Fund
        /// </summary>
        [Map("FUND")]
        Fund,
        /// <summary>
        /// ["<c>ALL</c>"] All
        /// </summary>
        [Map("ALL")]
        All
    }
}

