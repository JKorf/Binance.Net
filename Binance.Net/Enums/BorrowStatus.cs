using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Borrow status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BorrowStatus>))]
    public enum BorrowStatus
    {
        /// <summary>
        /// ["<c>Accuring_Interest</c>"] Accruing interest
        /// </summary>
        [Map("Accuring_Interest")]
        AccruingInterest,
        /// <summary>
        /// ["<c>Overdue</c>"] Overdue
        /// </summary>
        [Map("Overdue")]
        Overdue,
        /// <summary>
        /// ["<c>Liquidating</c>"] Currently liquidating
        /// </summary>
        [Map("Liquidating")]
        Liquidating,
        /// <summary>
        /// ["<c>Repaying</c>"] Repaying
        /// </summary>
        [Map("Repaying")]
        Repaying,
        /// <summary>
        /// ["<c>Repaid</c>"] Repaid
        /// </summary>
        [Map("Repaid")]
        Repaid,
        /// <summary>
        /// ["<c>Liquidated</c>"] Liquidated
        /// </summary>
        [Map("Liquidated")]
        Liquidated,
        /// <summary>
        /// ["<c>Pending</c>"] Pending
        /// </summary>
        [Map("Pending")]
        Pending,
        /// <summary>
        /// ["<c>Failed</c>"] Failed
        /// </summary>
        [Map("Failed")]
        Failed
    }
}

