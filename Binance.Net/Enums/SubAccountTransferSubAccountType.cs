using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubAccountTransferSubAccountType>))]
    public enum SubAccountTransferSubAccountType
    {
        /// <summary>
        /// ["<c>1</c>"] From main spot account to sub account
        /// </summary>
        [Map("1")]
        TransferIn,
        /// <summary>
        /// ["<c>2</c>"] From sub account to main spot account
        /// </summary>
        [Map("2")]
        TransferOut
    }
}

