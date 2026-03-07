namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin dust transfer info
    /// </summary>
    [SerializationModel]
    public record BinanceMarginDustTransfer
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
        /// ["<c>transferResult</c>"] Transfer results
        /// </summary>
        [JsonPropertyName("transferResult")]
        public BinanceMarginDustTransferResult[] TransferResults { get; set; } = Array.Empty<BinanceMarginDustTransferResult>();
    }

    /// <summary>
    /// Transfer results
    /// </summary>
    public record BinanceMarginDustTransferResult
    {
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fromAsset</c>"] Source asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>operateTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// ["<c>serviceChargeAmount</c>"] Service charge quantity
        /// </summary>
        [JsonPropertyName("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// ["<c>tranId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>transferedAmount</c>"] Transferred quantity
        /// </summary>
        [JsonPropertyName("transferedAmount")]
        public decimal TransferredQuantity { get; set; }
    }
}

