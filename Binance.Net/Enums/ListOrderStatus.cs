using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// List order status
    /// </summary>
    public enum ListOrderStatus
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
