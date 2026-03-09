using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer direction for isolated margin transfer
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IsolatedMarginTransferDirection>))]
    public enum IsolatedMarginTransferDirection
    {
        /// <summary>
        /// ["<c>SPOT</c>"] Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>ISOLATED_MARGIN</c>"] Isolated margin
        /// </summary>
        [Map("ISOLATED_MARGIN")]
        IsolatedMargin
    }
}

