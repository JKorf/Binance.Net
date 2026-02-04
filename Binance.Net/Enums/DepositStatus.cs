using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// The status of a deposit
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositStatus>))]
    public enum DepositStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        [Map("0")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("1")]
        Success,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("2")]
        Rejected,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("6")]
        Completed,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("7")]
        WrongDeposit,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("8")]
        WaitingUserConfirm,
    }
}
