using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Flexible Borrow Status
    /// </summary>
    public enum FlexibleBorrowStatus
    {
        /// <summary>
        /// Successful execution
        /// </summary>
        [Map("Succeeds")]
        Succeeds,
        /// <summary>
        /// Execution failed
        /// </summary>
        [Map("Failed")]
        Failed,
        /// <summary>
        /// Processing
        /// </summary>
        [Map("Processing")]
        Processing
    }
}
