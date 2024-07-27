using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of Margin
    /// </summary>
    public enum FuturesMarginType
    {
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED")]
        Isolated,
        /// <summary>
        /// Crossed margin
        /// </summary>
        [Map("CROSSED", "cross")]
        Cross
    }
}
