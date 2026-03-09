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
        /// ["<c>0</c>"] Email has been send
        /// </summary>
        [Map("0")]
        EmailSend,
        /// <summary>
        /// ["<c>1</c>"] Withdrawal has been canceled
        /// </summary>
        [Map("1")]
        Canceled,
        /// <summary>
        /// ["<c>2</c>"] Withdrawal is awaiting approval
        /// </summary>
        [Map("2")]
        AwaitingApproval,
        /// <summary>
        /// ["<c>3</c>"] Withdrawal has been rejected
        /// </summary>
        [Map("3")]
        Rejected,
        /// <summary>
        /// ["<c>4</c>"] Withdrawal is processing
        /// </summary>
        [Map("4")]
        Processing,
        /// <summary>
        /// ["<c>5</c>"] Withdrawal has failed
        /// </summary>
        [Map("5")]
        Failure,
        /// <summary>
        /// ["<c>6</c>"] Withdrawal has been processed
        /// </summary>
        [Map("6")]
        Completed
    }
}

