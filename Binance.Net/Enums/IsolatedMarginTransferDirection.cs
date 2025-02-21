using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer direction for isolated margin transfer
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<IsolatedMarginTransferDirection>))] public  enum IsolatedMarginTransferDirection
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
