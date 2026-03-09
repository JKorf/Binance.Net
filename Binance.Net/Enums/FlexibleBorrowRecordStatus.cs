using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Flexible Borrow Status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FlexibleBorrowRecordStatus>))]
    public enum FlexibleBorrowRecordStatus
    {
        /// <summary>
        /// ["<c>SUCCESS</c>"] Successful execution
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAILED</c>"] Execution failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// ["<c>PENDING</c>"] Pending to execute
        /// </summary>
        [Map("PENDING")]
        Pending
    }
}

