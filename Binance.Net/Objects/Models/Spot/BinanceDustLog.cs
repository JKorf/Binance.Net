namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Dust log response details
    /// </summary>
    [SerializationModel]
    public record BinanceDustLogList
    {
        /// <summary>
        /// ["<c>total</c>"] Total counts of exchange
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>userAssetDribblets</c>"] Dust conversion logs.
        /// </summary>
        [JsonPropertyName("userAssetDribblets")]
        public BinanceDustLog[] UserAssetDribblets { get; set; } = Array.Empty<BinanceDustLog>();
    }

    /// <summary>
    /// Dust log details
    /// </summary>
    public record BinanceDustLog
    {
        /// <summary>
        /// ["<c>totalTransferedAmount</c>"] Total transferred
        /// </summary>
        [JsonPropertyName("totalTransferedAmount")]
        public decimal TransferredTotal { get; set; }
        /// <summary>
        /// ["<c>totalServiceChargeAmount</c>"] Total service charge
        /// </summary>
        [JsonPropertyName("totalServiceChargeAmount")]
        public decimal ServiceChargeTotal { get; set; }
        /// <summary>
        /// ["<c>transId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("transId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>userAssetDribbletDetails</c>"] Detailed conversion entries.
        /// </summary>
        [JsonPropertyName("userAssetDribbletDetails")]
        public BinanceDustLogDetails[] Logs { get; set; } = Array.Empty<BinanceDustLogDetails>();
        /// <summary>
        /// ["<c>operateTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
    }

    /// <summary>
    /// Dust log entry details
    /// </summary>
    public record BinanceDustLogDetails
    {
        /// <summary>
        /// ["<c>transId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("transId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>serviceChargeAmount</c>"] Service charge
        /// </summary>
        [JsonPropertyName("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>operateTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// ["<c>transferedAmount</c>"] Transferred quantity
        /// </summary>
        [JsonPropertyName("transferedAmount")]
        public decimal TransferredQuantity { get; set; }
        /// <summary>
        /// ["<c>fromAsset</c>"] Asset
        /// </summary>
        [JsonPropertyName("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
    }
}

