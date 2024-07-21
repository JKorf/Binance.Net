using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer direction for isolated margin transfer
    /// </summary>
    public enum IsolatedMarginTransferDirection
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("ISOLATED_MARGIN")]
        IsolatedMargin
    }
}
