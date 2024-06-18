namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountTransferWrapper
    {
        [JsonProperty("msg")]
        public string? Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<BinanceSubAccountTransfer>? Transfers { get; set; }
    }

    /// <summary>
    /// Sub account transfer info
    /// </summary>
    public record BinanceSubAccountTransfer
    {
        /// <summary>
        /// From which email the transfer originated
        /// </summary>
        public string From { get; set; } = string.Empty;
        /// <summary>
        /// To which email the transfer was to
        /// </summary>
        public string To { get; set; } = string.Empty;
        /// <summary>
        /// The asset of the transfer
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the transfer
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The timestamp of the transfer
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Status of the transaction
        /// </summary>
        public string Status { get; set; } = "";
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
    }
}
