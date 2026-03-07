using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer info
    /// </summary>
    [SerializationModel]
    public record BinanceTransfer
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset which was transferred
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity transferred
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Transfer type
        /// </summary>
        [JsonPropertyName("type")]
        public UniversalTransferType Type { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tranId</c>"] The transfer identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] The transfer timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}

