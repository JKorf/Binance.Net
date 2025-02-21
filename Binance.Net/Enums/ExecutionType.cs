using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The type of execution
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExecutionType>))] public  enum ExecutionType
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// Replaced
        /// </summary>
        [Map("REPLACED")]
        Replaced,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// Trade
        /// </summary>
        [Map("TRADE")]
        Trade,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// Amendment
        /// </summary>
        [Map("AMENDMENT")]
        Amendment,
        /// <summary>
        /// Self trade prevented
        /// </summary>
        [Map("TRADE_PREVENTION")]
        TradePrevention
    }
}
