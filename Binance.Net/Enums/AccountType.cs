using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of account
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// ["<c>SPOT</c>"] Spot account type
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>MARGIN</c>"] Margin account type
        /// </summary>>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// ["<c>FUTURES</c>"] Futures account type
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// ["<c>LEVERAGED</c>"] Leveraged account type
        /// </summary>
        [Map("LEVERAGED")]
        Leveraged
    }
}

