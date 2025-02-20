using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Plan status
    /// </summary>
    public enum AutoInvestPlanStatus
    {
        /// <summary>
        /// Ongoing
        /// </summary>
        [Map("ONGOING")]
        Ongoing,
        /// <summary>
        /// Paused
        /// </summary>
        [Map("PAUSED")]
        Paused,
        /// <summary>
        /// Removed
        /// </summary>
        [Map("REMOVED")]
        Removed
    }
}
