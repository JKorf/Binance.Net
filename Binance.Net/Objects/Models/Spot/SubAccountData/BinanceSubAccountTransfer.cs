namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account transfer info
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountTransfer
    {
        /// <summary>
        /// The source account email address.
        /// </summary>
        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;
        /// <summary>
        /// The destination account email address.
        /// </summary>
        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;
        /// <summary>
        /// The asset of the transfer
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the transfer
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The timestamp of the transfer
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Status of the transaction
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
    }
}
