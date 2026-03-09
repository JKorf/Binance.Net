using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// List order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ListOrderStatus>))]
    public enum ListOrderStatus
    {
        /// <summary>
        /// ["<c>EXECUTING</c>"] Executing
        /// </summary>
        [Map("EXECUTING")]
        Executing,
        /// <summary>
        /// ["<c>REJECT</c>"] Rejected
        /// </summary>
        [Map("REJECT")]
        Rejected,
        /// <summary>
        /// ["<c>ALL_DONE</c>"] Done
        /// </summary>
        [Map("ALL_DONE")]
        Done
    }
}

