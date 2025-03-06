namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountTransferWrapper
    {
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("transfers")]
        public BinanceSubAccountTransfer[]? Transfers { get; set; }
    }

    /// <summary>
    /// Sub account transfer info
    /// </summary>
    public record BinanceSubAccountTransfer
    {
        /// <summary>
        /// From which email the transfer originated
        /// </summary>
        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;
        /// <summary>
        /// To which email the transfer was to
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
        /// Transaction Id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
    }
}
