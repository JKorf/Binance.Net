using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.BSwap
{
    /// <summary>
    /// Rewards history
    /// </summary>
    public class BinanceBSwapRewardHistory
    {
        /// <summary>
        /// Pool id
        /// </summary>
        public long PoolId { get; set; }
        /// <summary>
        /// Pool name
        /// </summary>
        public string PoolName { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        public string AssetRewards { get; set; } = string.Empty;
        /// <summary>
        /// Claim timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ClaimTime { get; set; }
        /// <summary>
        /// Claim quanity
        /// </summary>
        [JsonProperty("claimAmount")]
        public decimal ClaimQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
    }
}
