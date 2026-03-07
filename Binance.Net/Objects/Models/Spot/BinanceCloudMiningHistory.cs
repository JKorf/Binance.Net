using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Cloud mining payment/refund history
    /// </summary>
    [SerializationModel]
    public record BinanceCloudMiningHistory
    {
        /// <summary>
        /// ["<c>createTime</c>"] The creation time.
        /// </summary>
        [JsonPropertyName("createTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>tranId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public CloudMiningPaymentStatus Type { get; set; }

        /// <summary>
        /// ["<c>asset</c>"] The asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}

