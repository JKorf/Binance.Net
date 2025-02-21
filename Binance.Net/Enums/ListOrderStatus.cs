using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// List order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ListOrderStatus>))] public  enum ListOrderStatus
    {
        /// <summary>
        /// Executing
        /// </summary>
        [Map("EXECUTING")]
        Executing,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECT")]
        Rejected,
        /// <summary>
        /// Done
        /// </summary>
        [Map("ALL_DONE")]
        Done
    }
}
