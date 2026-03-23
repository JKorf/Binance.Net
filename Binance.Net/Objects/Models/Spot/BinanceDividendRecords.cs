using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dividend record
    /// </summary>
    [SerializationModel]
    public record BinanceDividendRecord
    {
        /// <summary>
        /// ["<c>id</c>"] The dividend record identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] The asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>divTime</c>"] Timestamp of the transaction
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("divTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>tranId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>enInfo</c>"] Additional info.
        /// </summary>
        [JsonPropertyName("enInfo")]
        public string? Info { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public DividendDirection Direction { get; set; }
    }
}

