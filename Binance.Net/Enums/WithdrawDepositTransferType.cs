using Binance.Net.Converters;
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
        /// Internal transfer
        /// </summary>
        [Map("1")]
        Internal,
        /// <summary>
        /// External transfer
        /// </summary>
        [Map("0")]
        External
    }
}
