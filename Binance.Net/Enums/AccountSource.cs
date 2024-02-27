using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Account source
    /// </summary>
    public enum AccountSource
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Fund
        /// </summary>
        [Map("FUND")]
        Fund,
        /// <summary>
        /// All
        /// </summary>
        [Map("ALL")]
        All
    }
}
