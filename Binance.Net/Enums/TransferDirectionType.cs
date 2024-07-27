using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Transfer direction
    /// </summary>
    public enum TransferDirectionType
    {
        /// <summary>
        /// From main account to margin account
        /// </summary>
        [Map("1")]
        MainToMargin,
        /// <summary>
        /// From margin account to main account
        /// </summary>
        [Map("2")]
        MarginToMain
    }
}
