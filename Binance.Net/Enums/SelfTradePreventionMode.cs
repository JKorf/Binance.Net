using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonConverter(typeof(PocAOTEnumConverter<SelfTradePreventionMode>))]
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// Expire taker
        /// </summary>
        [Map("EXPIRE_TAKER")]
        ExpireTaker,
        /// <summary>
        /// Expire maker
        /// </summary>
        [Map("EXPIRE_MAKER")]
        ExpireMaker,
        /// <summary>
        /// Expire both
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
