using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Type of account
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Spot account type
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Margin account type
        /// </summary>>
        [Map("MARGIN")]
        Margin,
        /// <summary>
        /// Futures account type
        /// </summary>
        [Map("FUTURES")]
        Futures,
        /// <summary>
        /// Leveraged account type
        /// </summary>
        [Map("LEVERAGED")]
        Leveraged       
    }
}
