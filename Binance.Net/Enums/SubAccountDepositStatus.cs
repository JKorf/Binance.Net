using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Sub Account Deposit Status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubAccountDepositStatus>))]
    public enum SubAccountDepositStatus
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
        /// ["<c>6</c>"] Credited but cannot withdraw
        /// </summary>
        [Map("6")]
        CreditedButCannotWithdraw,
    }
}
