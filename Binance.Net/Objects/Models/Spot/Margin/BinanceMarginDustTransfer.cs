namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Margin dust transfer info
    /// </summary>
    public record BinanceMarginDustTransfer
    {
        /// <summary>
        /// Total service charge
        /// </summary>
        [JsonProperty("totalServiceCharge")]
        public decimal TotalServiceCharge { get; set; }
        /// <summary>
        /// Total transfered
        /// </summary>
        [JsonProperty("totalTransfered")]
        public decimal TotalTransfered { get; set; }
        /// <summary>
        /// Transfer results
        /// </summary>
        [JsonProperty("transferResult")]
        public IEnumerable<BinanceMargingDustTransferResult> TransferResults { get; set; } = Array.Empty<BinanceMargingDustTransferResult>();
    }

    /// <summary>
    /// Transfer results
    /// </summary>
    public record BinanceMargingDustTransferResult
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Source asset
        /// </summary>
        [JsonProperty("fromAsset")]
        public string FromAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("operateTime")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// Service charge quantity
        /// </summary>
        [JsonProperty("serviceChargeAmount")]
        public decimal ServiceChargeQuantity { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Transfered quantity
        /// </summary>
        [JsonProperty("transferedAmount")]
        public decimal TransferedQuantity { get; set; }
    }
}
