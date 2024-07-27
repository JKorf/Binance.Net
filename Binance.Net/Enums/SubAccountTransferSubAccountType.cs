using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    public enum SubAccountTransferSubAccountType
    {
        /// <summary>
        /// From main spot account to sub account
        /// </summary>
        [Map("1")]
        TransferIn,
        /// <summary>
        /// From sub account to main spot account
        /// </summary>
        [Map("2")]
        TransferOut
    }
}
