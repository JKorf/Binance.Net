using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple Earn locked product redemption record
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnLockedRedemptionRecord
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Original quantity 
        /// </summary>
        [JsonPropertyName("originalAmount")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// Loss quantity 
        /// </summary>
        [JsonPropertyName("lossAmount")]
        public decimal LossQuantity { get; set; }
        /// <summary>
        /// Is complete 
        /// </summary>
        [JsonPropertyName("isComplete")]
        public bool Complete { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Reward amt 
        /// </summary>
        [JsonPropertyName("rewardAmt")]
        public decimal RewardAmt { get; set; }
        /// <summary>
        /// Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string extraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// Estimate extra reward asset
        /// </summary>
        [JsonPropertyName("estExtraRewardAmt")]
        public decimal EstimateExtraRewardAmt { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId"), JsonConverter(typeof(NumberStringConverter))]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// Redeem id
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Lock period
        /// </summary>
        [JsonPropertyName("lockPeriod")]
        public int LockPeriod { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonPropertyName("deliverDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public RedemptionType Type { get; set; }
    }
}
