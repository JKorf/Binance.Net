namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Result of dust transfer
    /// </summary>
    [SerializationModel]
    public record BinanceDustTransferResult
    {
        /// <summary>
        /// ["<c>totalServiceCharge</c>"] Total service charge
        /// </summary>
        [JsonPropertyName("totalServiceCharge")]
        public decimal TotalServiceCharge { get; set; }
        /// <summary>
        /// ["<c>totalTransfered</c>"] Total transferred
        /// </summary>
        [JsonPropertyName("totalTransfered")]
        public decimal TotalTransferred { get; set; }
        /// <summary>
        /// ["<c>transferResult</c>"] Transfer entries
        /// </summary>
        [JsonPropertyName("transferResult")]
        public BinanceDustTransferResultEntry[] TransferResult { get; set; } = Array.Empty<BinanceDustTransferResultEntry>();
    }

    /// <summary>
    /// Dust transfer entry
    /// </summary>
    public record BinanceDustTransferResultEntry
    {
        /// <summary>
        /// ["<c>amount</c>"] Quantity of dust
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fromAsset</c>"] The source asset.
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>operateTime</c>"] Timestamp of conversion
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("operateTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>serviceChargeAmount</c>"] Service charge
        /// </summary>
        [JsonPropertyName("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// ["<c>tranId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>transferedAmount</c>"] BNB result quantity
        /// </summary>
        [JsonPropertyName("transferedAmount")]
        public decimal TransferredQuantity { get; set; }
    }
}

