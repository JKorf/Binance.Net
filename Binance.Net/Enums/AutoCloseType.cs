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
        /// ["<c>ADL</c>"] ADL
        /// </summary>
        [Map("ADL")]
        ADL,

        /// <summary>
        /// ["<c>LIQUIDATION</c>"] Liquidation
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation
    }
}

