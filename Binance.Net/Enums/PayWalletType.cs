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
        /// Funding wallet
        /// </summary>
        [Map("1")]
        Funding,
        /// <summary>
        /// Spot wallet
        /// </summary>
        [Map("2")]
        Spot,
        /// <summary>
        /// Fiat wallet
        /// </summary>
        [Map("3")]
        Fiat,
        /// <summary>
        /// Card payment
        /// </summary>
        [Map("4", "6")]
        Card,
        /// <summary>
        /// Earn wallet
        /// </summary>
        [Map("5")]
        Earn
    }
}
