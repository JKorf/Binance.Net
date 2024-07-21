namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Result of dust transfer
    /// </summary>
    public record BinanceDustTransferResult
    {
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonPropertyName("totalServiceCharge")]
        public decimal TotalServiceCharge { get; set; }
        /// <summary>
        /// Total transferred
        /// </summary>
        [JsonPropertyName("totalTransfered")]
        public decimal TotalTransferred { get; set; }
        /// <summary>
        /// Transfer entries
        /// </summary>
        [JsonPropertyName("transferResult")]
        public IEnumerable<BinanceDustTransferResultEntry> TransferResult { get; set; } = Array.Empty<BinanceDustTransferResultEntry>();
    }

    /// <summary>
    /// Dust transfer entry
    /// </summary>
    public record BinanceDustTransferResultEntry
    {
        /// <summary>
        /// Quantity of dust
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp of conversion
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("operateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Service charge
        /// </summary>
        [JsonPropertyName("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// BNB result quantity
        /// </summary>
        [JsonPropertyName("transferedAmount")]
        public decimal TransferredQuantity { get; set; }
    }
}
