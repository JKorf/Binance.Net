using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Transaction Universal
    /// </summary>
    public record BinanceBrokerageTransferTransactionUniversal
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        [JsonProperty("txnId")]
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// Client Transfer Id
        /// </summary>
        [JsonProperty("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
        
        /// <summary>
        /// To id
        /// </summary>
        public string ToId { get; set; } = string.Empty;
        
        /// <summary>
        /// From account type
        /// </summary>
        [JsonConverter(typeof(BrokerageAccountTypeConverter))]
        public BrokerageAccountType FromAccountType { get; set; }
        
        /// <summary>
        /// To account type
        /// </summary>
        [JsonConverter(typeof(BrokerageAccountTypeConverter))]
        public BrokerageAccountType ToAccountType { get; set; }
        
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Status
        /// </summary>
        [JsonConverter(typeof(BrokerageTransferTransactionStatusConverter))]
        public BrokerageTransferTransactionStatus Status { get; set; }
    }
}