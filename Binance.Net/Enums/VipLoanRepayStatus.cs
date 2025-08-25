
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// VIP Loan repay status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<VipLoanRepayStatus>))]
    public enum VipLoanRepayStatus
    {
        /// <summary>
        /// Repaid
        /// </summary>
        [Map("Repaid")]
        Repaid,
        /// <summary>
        /// Repaying
        /// </summary>
        [Map("Repaying")]
        Repaying,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("Failed")]
        Failed
    }
}
