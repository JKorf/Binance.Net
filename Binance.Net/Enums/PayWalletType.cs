using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Wallet type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PayWalletType>))]
    public enum PayWalletType
    {
        /// <summary>
        /// ["<c>1</c>"] Funding wallet
        /// </summary>
        [Map("1")]
        Funding,
        /// <summary>
        /// ["<c>2</c>"] Spot wallet
        /// </summary>
        [Map("2")]
        Spot,
        /// <summary>
        /// ["<c>3</c>"] Fiat wallet
        /// </summary>
        [Map("3")]
        Fiat,
        /// <summary>
        /// Card payment
        /// </summary>
        [Map("4", "6")]
        Card,
        /// <summary>
        /// ["<c>5</c>"] Earn wallet
        /// </summary>
        [Map("5")]
        Earn
    }
}

