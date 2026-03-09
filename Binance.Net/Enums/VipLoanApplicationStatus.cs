
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// VIP Loan application status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<VipLoanApplicationStatus>))]
    public enum VipLoanApplicationStatus
    {
        /// <summary>
        /// ["<c>Accruing_Interest</c>"] Accruing interest
        /// </summary>
        [Map("Accruing_Interest")]
        AccruingInterest,
        /// <summary>
        /// ["<c>Overdue</c>"] Overdue
        /// </summary>
        [Map("Overdue")]
        Overdue,
        /// <summary>
        /// ["<c>Liquidating</c>"] Liquidating
        /// </summary>
        [Map("Liquidating")]
        Liquidating,
        /// <summary>
        /// ["<c>Repaying</c>"] Repaying
        /// </summary>
        [Map("Repaying")]
        Repaying,
        /// <summary>
        /// ["<c>Repaid</c>"] Repaying
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
        Failed,
    }
}

