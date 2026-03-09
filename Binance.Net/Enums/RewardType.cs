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
        /// ["<c>BONUS</c>"] Bonus tiered APR
        /// </summary>
        [Map("BONUS")]
        BonusTieredApr,
        /// <summary>
        /// ["<c>REALTIME</c>"] Realtime APR
        /// </summary>
        [Map("REALTIME")]
        RealtimeApr,
        /// <summary>
        /// ["<c>REWARDS</c>"] Historical rewards
        /// </summary>
        [Map("REWARDS")]
        HistoricalRewards,
        /// <summary>
        /// ["<c>ALL</c>"] All reward types
        /// </summary>
        [Map("ALL")]
        All
    }
}

