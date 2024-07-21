using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Project status
    /// </summary>
    public enum ProjectStatus
    {
        /// <summary>
        /// Holding
        /// </summary>
        [Map("HOLDING")] 
        Holding,
        /// <summary>
        /// Redeemed
        /// </summary>
        [Map("REDEEMED")]
        Redeemed
    }
}
