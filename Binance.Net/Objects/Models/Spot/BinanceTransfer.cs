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
        /// The asset which was transferred
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity transferred
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonPropertyName("type")]
        public UniversalTransferType Type { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The transfer identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long Id { get; set; }
        /// <summary>
        /// The transfer timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
