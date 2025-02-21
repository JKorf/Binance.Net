using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Wallet type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WalletType>))] public  enum WalletType
    {
        /// <summary>
        /// Spot wallet
        /// </summary>
        [Map("0")]
        Spot,
        /// <summary>
        /// Funding wallet
        /// </summary>
        [Map("1")]
        Funding
    }
}
