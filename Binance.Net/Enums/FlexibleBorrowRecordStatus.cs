using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Flexible Borrow Status
    /// </summary>
    public enum FlexibleBorrowRecordStatus
    {
        /// <summary>
        /// Successful execution
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Execution failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// Pending to execute
        /// </summary>
        [Map("PENDING")]
        Pending
    }
}
