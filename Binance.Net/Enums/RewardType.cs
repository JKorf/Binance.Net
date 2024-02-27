using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Simple Earn Reward type
    /// </summary>
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
        HistoricalRewards
    }
}
