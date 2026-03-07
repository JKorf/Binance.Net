using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer info
    /// </summary>
    [SerializationModel]
    public record BinanceSpotFuturesTransfer
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tranId</c>"] The transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity transferred
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] The transfer direction
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesTransferType Type { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] The transfer timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>status</c>"] The status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public FuturesTransferStatus Status { get; set; }
    }
}

