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
        /// ["<c>amount</c>"] Redeemed quantity.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>originalAmount</c>"] Original quantity 
        /// </summary>
        [JsonPropertyName("originalAmount")]
        public decimal OriginalQuantity { get; set; }
        /// <summary>
        /// ["<c>lossAmount</c>"] Loss quantity 
        /// </summary>
        [JsonPropertyName("lossAmount")]
        public decimal LossQuantity { get; set; }
        /// <summary>
        /// ["<c>isComplete</c>"] Is complete 
        /// </summary>
        [JsonPropertyName("isComplete")]
        public bool Complete { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Product asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rewardAsset</c>"] Reward asset
        /// </summary>
        [JsonPropertyName("rewardAsset")]
        public string RewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rewardAmt</c>"] Reward amt 
        /// </summary>
        [JsonPropertyName("rewardAmt")]
        public decimal RewardAmt { get; set; }
        /// <summary>
        /// ["<c>extraRewardAsset</c>"] Extra reward asset
        /// </summary>
        [JsonPropertyName("extraRewardAsset")]
        public string extraRewardAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>estExtraRewardAmt</c>"] Estimate extra reward asset
        /// </summary>
        [JsonPropertyName("estExtraRewardAmt")]
        public decimal EstimateExtraRewardAmt { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId"), JsonConverter(typeof(NumberStringConverter))]
        public string PositionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>redeemId</c>"] Redeem id
        /// </summary>
        [JsonPropertyName("redeemId")]
        public long RedeemId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>lockPeriod</c>"] Lock period
        /// </summary>
        [JsonPropertyName("lockPeriod")]
        public int LockPeriod { get; set; }
        /// <summary>
        /// ["<c>deliverDate</c>"] Delivery date
        /// </summary>
        [JsonPropertyName("deliverDate"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public RedemptionType Type { get; set; }
    }
}

