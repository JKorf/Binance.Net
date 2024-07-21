namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Futures Transactions
    /// </summary>
    public record BinanceBrokerageTransferFuturesTransactions
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Futures type
        /// </summary>
        public BinanceBrokerageFuturesType FuturesType { get; set; }
        
        /// <summary>
        /// Transfer
        /// </summary>
        [JsonPropertyName("transfer")]
        public IEnumerable<BinanceBrokerageTransferFuturesTransaction> Transactions { get; set; } = Array.Empty<BinanceBrokerageTransferFuturesTransaction>();
    }
    
    /// <summary>
    /// Transfer Futures Transaction
    /// </summary>
    public record BinanceBrokerageTransferFuturesTransaction
    {
        /// <summary>
        /// From Id
        /// </summary>
        public string FromId { get; set; } = string.Empty;
        
        /// <summary>
        /// To Id
        /// </summary>
        public string ToId { get; set; } = string.Empty;
        
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// Client Transfer Id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
    }
}