using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Flexible Borrow Status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FlexibleBorrowStatus>))]
    public enum FlexibleBorrowStatus
    {
        /// <summary>
        /// ["<c>Succeeds</c>"] Successful execution
        /// </summary>
        [Map("Succeeds")]
        Succeeds,
        /// <summary>
        /// ["<c>Failed</c>"] Execution failed
        /// </summary>
        [Map("Failed")]
        Failed,
        /// <summary>
        /// ["<c>Processing</c>"] Processing
        /// </summary>
        [Map("Processing")]
        Processing
    }
}

