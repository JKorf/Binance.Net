using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Flexible product subscription record
    /// </summary>
    public class BinanceSimpleEarnFlexibleRecord
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Purchase id
        /// </summary>
        [JsonProperty("purchaseId")]
        public long PurchaseId { get; set; }
        /// <summary>
        /// Subscription type
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(EnumConverter))]
        public SubscriptionType Type { get; set; }
        /// <summary>
        /// Source account
        /// </summary>
        [JsonProperty("sourceAccount"), JsonConverter(typeof(EnumConverter))]
        public AccountSource SourceAccount { get; set; }
        /// <summary>
        /// Quantity from spot
        /// </summary>
        [JsonProperty("amtFromSpot")]
        public decimal SpotQuantity { get; set; }
        /// <summary>
        /// Quantity from funding
        /// </summary>
        [JsonProperty("amtFromFunding")]
        public decimal FundingQuantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status"), JsonConverter(typeof(EnumConverter))]
        public SubscriptionStatus Status { get; set; }
    }
}
