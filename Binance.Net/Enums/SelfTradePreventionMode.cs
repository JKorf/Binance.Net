using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// Expire taker
        /// </summary>
        [Map("EXPIRE_TAKER")]
        ExpireTaker,
        /// <summary>
        /// Exire maker
        /// </summary>
        [Map("EXPIRE_MAKER")]
        ExireMaker,
        /// <summary>
        /// Exire both
        /// </summary>
        [Map("EXPIRE_BOTH")]
        ExpireBoth,
        /// <summary>
        /// None
        /// </summary>
        [Map("NONE")]
        None
    }
}
