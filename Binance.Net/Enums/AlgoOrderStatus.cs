using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Status of an algo order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AlgoOrderStatus>))]
    public enum AlgoOrderStatus
    {
        /// <summary>
        /// ["<c>NEW</c>"] This status indicates that the conditional order was successfully placed into the Algo Service but has not yet been triggered
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>CANCELED</c>"] This status signifies that the conditional order has been canceled
        /// </summary>>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>TRIGGERING</c>"] This status suggests that the order has met the triggering condition and has been forwarded to the matching engine
        /// </summary>
        [Map("TRIGGERING")]
        Triggering,
        /// <summary>
        /// ["<c>TRIGGERED</c>"] This status means that the order has been successfully placed into the matching engine
        /// </summary>
        [Map("TRIGGERED")]
        Triggered,
        /// <summary>
        /// ["<c>FINISHED</c>"] This status shows that the triggered conditional order has been filled or canceled in the matching engine
        /// </summary>
        [Map("FINISHED")]
        Finished,
        /// <summary>
        /// ["<c>REJECTED</c>"] This status signifies that the conditional order has been denied by the matching engine, such as in scenarios of margin check failures
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// ["<c>EXPIRED</c>"] This status denotes that the conditional order has been canceled by the system. An example would be when a user places a GTE_GTC Time-In-Force conditional order but then closes all positions on that symbol, resulting in system-led cancellation of the conditional order
        /// </summary>
        [Map("EXPIRED")]
        Expired,

    }
}

