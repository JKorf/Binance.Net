using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// ["<c>EXPIRE_TAKER</c>"] Expire taker
        /// </summary>
        [Map("EXPIRE_TAKER")]
        ExpireTaker,
        /// <summary>
        /// ["<c>EXPIRE_MAKER</c>"] Expire maker
        /// </summary>
        [Map("EXPIRE_MAKER")]
        ExpireMaker,
        /// <summary>
        /// ["<c>EXPIRE_BOTH</c>"] Expire both
        /// </summary>
        [Map("EXPIRE_BOTH")]
        ExpireBoth,
        /// <summary>
        /// ["<c>DECREMENT</c>"] Decrement
        /// </summary>
        [Map("DECREMENT")]
        Decrement,
        /// <summary>
        /// ["<c>NONE</c>"] None
        /// </summary>
        [Map("NONE")]
        None,
        /// <summary>
        /// ["<c>TRANSFER</c>"] Transfer
        /// </summary>
        [Map("TRANSFER")]
        Transfer
    }
}

