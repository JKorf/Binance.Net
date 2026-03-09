using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of execution
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExecutionType>))]
    public enum ExecutionType
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>CANCELED</c>"] Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>REPLACED</c>"] Replaced
        /// </summary>
        [Map("REPLACED")]
        Replaced,
        /// <summary>
        /// ["<c>REJECTED</c>"] Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// ["<c>TRADE</c>"] Trade
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>AMENDMENT</c>"] Amendment
        /// </summary>
        [Map("AMENDMENT")]
        Amendment,
        /// <summary>
        /// ["<c>TRADE_PREVENTION</c>"] Self trade prevented
        /// </summary>
        [Map("TRADE_PREVENTION")]
        TradePrevention
    }
}

