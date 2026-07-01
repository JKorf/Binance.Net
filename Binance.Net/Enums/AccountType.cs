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
        [Map("SPOT", "spot")]
        Spot,
        /// <summary>
        /// ["<c>MARGIN</c>"] Margin account type
        /// </summary>>
        [Map("MARGIN", "margin")]
        Margin,
        /// <summary>
        /// ["<c>FUTURES</c>"] Futures account type
        /// </summary>
        [Map("FUTURES", "futures")]
        Futures,
        /// <summary>
        /// ["<c>LEVERAGED</c>"] Leveraged account type
        /// </summary>
        [Map("LEVERAGED", "leveraged")]
        Leveraged
    }
}

