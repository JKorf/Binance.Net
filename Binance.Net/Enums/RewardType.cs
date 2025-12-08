using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple Earn Reward type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RewardType>))]
    public enum RewardType
    {
        /// <summary>
        /// Bonus tiered APR
        /// </summary>
        [Map("BONUS")]
        BonusTieredApr,
        /// <summary>
        /// Realtime APR
        /// </summary>
        [Map("REALTIME")]
        RealtimeApr,
        /// <summary>
        /// Historical rewards
        /// </summary>
        [Map("REWARDS")]
        HistoricalRewards,
        /// <summary>
        /// All reward types
        /// </summary>
        [Map("ALL")]
        All
    }
}
