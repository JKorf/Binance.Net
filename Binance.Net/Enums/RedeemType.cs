using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Redeem type
    /// </summary>
    public enum RedeemType
    {
        /// <summary>
        /// Fast
        /// </summary>
        [Map("FAST")]
        Fast,
        /// <summary>
        /// Normal
        /// </summary>
        [Map("NORMAL")]
        Normal
    }
}
