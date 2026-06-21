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
        /// ["<c>0</c>"] Pending
        /// </summary>
        [Map("0")]
        Pending,
        /// <summary>
        /// ["<c>1</c>"] Success
        /// </summary>
        [Map("1")]
        Success,
        /// <summary>
        /// ["<c>2</c>"] Rejected
        /// </summary>
        [Map("2")]
        Rejected,
        /// <summary>
        /// ["<c>6</c>"] Credited but cannot withdraw
        /// </summary>
        [Map("6")]
        Credited,
        /// <summary>
        /// ["<c>7</c>"] Wrong deposit
        /// </summary>
        [Map("7")]
        WrongDeposit,
        /// <summary>
        /// ["<c>8</c>"] Waiting user confirmation
        /// </summary>
        [Map("8")]
        WaitingUserConfirm,
    }
}

