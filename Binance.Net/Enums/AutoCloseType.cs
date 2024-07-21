using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of auto close
    /// </summary>
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
