using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Wallet type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WalletType>))]
    public enum WalletType
    {
        /// <summary>
        /// ["<c>0</c>"] Spot wallet
        /// </summary>
        [Map("0")]
        Spot,
        /// <summary>
        /// ["<c>1</c>"] Funding wallet
        /// </summary>
        [Map("1")]
        Funding
    }
}

