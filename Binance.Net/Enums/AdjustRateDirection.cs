using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Rate direction
    /// </summary>
    public enum AdjustRateDirection
    {
        /// <summary>
        /// Additional
        /// </summary>
        [Map("ADDITIONAL")]
        Additional,
        /// <summary>
        /// Reduced
        /// </summary>
        [Map("REDUCED")]
        Reduced
    }
}
