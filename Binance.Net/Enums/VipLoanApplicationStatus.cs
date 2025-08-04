
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
        /// Accruing interest
        /// </summary>
        [Map("Accruing_Interest")]
        AccruingInterest,
        /// <summary>
        /// Overdue
        /// </summary>
        [Map("Overdue")]
        Overdue,
        /// <summary>
        /// Liquidating
        /// </summary>
        [Map("Liquidating")]
        Liquidating,
        /// <summary>
        /// Repaying
        /// </summary>
        [Map("Repaying")]
        Repaying,
        /// <summary>
        /// Repaying
        /// </summary>
        [Map("Repaid")]
        Repaid,
        /// <summary>
        /// Liquidated
        /// </summary>
        [Map("Liquidated")]
        Liquidated,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("Pending")]
        Pending,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("Failed")]
        Failed,
    }
}
