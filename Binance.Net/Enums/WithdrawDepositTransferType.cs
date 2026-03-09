using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawDepositTransferType>))]
    public enum WithdrawDepositTransferType
    {
        /// <summary>
        /// ["<c>1</c>"] Internal transfer
        /// </summary>
        [Map("1")]
        Internal,
        /// <summary>
        /// ["<c>0</c>"] External transfer
        /// </summary>
        [Map("0")]
        External
    }
}

