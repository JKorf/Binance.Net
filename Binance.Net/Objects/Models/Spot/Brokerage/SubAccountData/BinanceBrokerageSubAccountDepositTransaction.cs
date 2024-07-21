namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Deposit Transaction
    /// </summary>
    public record BinanceBrokerageSubAccountDepositTransaction
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// Address Tag
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonPropertyName("insertTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// Network
        /// </summary>
        public string Network { get; set; } = string.Empty;
        
        /// <summary>
        /// Status
        /// </summary>
        public BinanceBrokerageSubAccountDepositStatus Status { get; set; }
        
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonPropertyName("txId")]
        public string TransactionId { get; set; } = string.Empty;
        
        /// <summary>
        /// Source Address
        /// </summary>
        public string SourceAddress { get; set; } = string.Empty;
        
        /// <summary>
        /// Confirm Times
        /// </summary>
        public string ConfirmTimes { get; set; } = string.Empty;
    }
}