using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of auto close
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoCloseType>))]
    public enum AutoCloseType
    {
        /// <summary>
        /// ADL
        /// </summary>
        [Map("ADL")]
        ADL,

        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation
    }
}
