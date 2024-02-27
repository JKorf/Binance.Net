using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Locked product position info
    /// </summary>
    public class BinanceSimpleEarnLockedPosition
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position id
        /// </summary>
        [JsonProperty("positionId")]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Project id
        /// </summary>
        [JsonProperty("projectId")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Purchase time
        /// </summary>
        [JsonProperty("purchaseTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PurchaseTime { get; set; }
        /// <summary>
        /// Duration in days
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }
        /// <summary>
        /// Accrual days
        /// </summary>
        [JsonProperty("accrualDays")]
        public int AccrualDays { get; set; }
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonProperty("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// APY
        /// </summary>
        [JsonProperty("APY")]
        public decimal APY { get; set; }
        /// <summary>
        /// Is renewable
        /// </summary>
        [JsonProperty("isRenewable")]
        public bool IsRenewable { get; set; }
        /// <summary>
        /// Is auto renew enabled
        /// </summary>
        [JsonProperty("isAutoRenew")]
        public bool IsAutoRenew { get; set; }
        /// <summary>
        /// Redeem date
        /// </summary>
        [JsonProperty("redeemDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? RedeemDate { get; set; }
    }
}
