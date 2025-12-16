using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The status of a withdrawal
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Email has been send
        /// </summary>
        [Map("0")]
        EmailSend,
        /// <summary>
        /// Withdrawal has been canceled
        /// </summary>
        [Map("1")]
        Canceled,
        /// <summary>
        /// Withdrawal is awaiting approval
        /// </summary>
        [Map("2")]
        AwaitingApproval,
        /// <summary>
        /// Withdrawal has been rejected
        /// </summary>
        [Map("3")]
        Rejected,
        /// <summary>
        /// Withdrawal is processing
        /// </summary>
        [Map("4")]
        Processing,
        /// <summary>
        /// Withdrawal has failed
        /// </summary>
        [Map("5")]
        Failure,
        /// <summary>
        /// Withdrawal has been processed
        /// </summary>
        [Map("6")]
        Completed
    }
}
