namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dividend record
    /// </summary>
    public record BinanceDividendRecord
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
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
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Info
        /// </summary>
        [JsonPropertyName("enInfo")]
        public string? Info { get; set; }
    }
}
