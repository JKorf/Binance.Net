using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Index status
    /// </summary>
    public enum AutoInvestIndexStatus
    {
        /// <summary>
        /// Running
        /// </summary>
        [Map("RUNNING")]
        Running,
        /// <summary>
        /// Rebalancing
        /// </summary>
        [Map("REBALANCING")]
        Rebalancing,
        /// <summary>
        /// Paused
        /// </summary>
        [Map("PAUSED")]
        Paused
    }
}
