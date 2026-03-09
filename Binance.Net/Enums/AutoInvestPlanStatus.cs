using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Plan status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestPlanStatus>))]
    public enum AutoInvestPlanStatus
    {
        /// <summary>
        /// ["<c>ONGOING</c>"] Ongoing
        /// </summary>
        [Map("ONGOING")]
        Ongoing,
        /// <summary>
        /// ["<c>PAUSED</c>"] Paused
        /// </summary>
        [Map("PAUSED")]
        Paused,
        /// <summary>
        /// ["<c>REMOVED</c>"] Removed
        /// </summary>
        [Map("REMOVED")]
        Removed
    }
}

