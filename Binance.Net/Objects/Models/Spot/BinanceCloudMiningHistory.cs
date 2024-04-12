using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Cloud mining payment/refund history
    /// </summary>
    public class BinanceCloudMiningHistory
    {
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("createTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("type")]
        public CloudMiningPaymentStatus Type { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
