namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dividend record
    /// </summary>
    [SerializationModel]
    public record BinanceDividendRecord
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("divTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Info
        /// </summary>
        [JsonPropertyName("enInfo")]
        public string? Info { get; set; }
    }
}
