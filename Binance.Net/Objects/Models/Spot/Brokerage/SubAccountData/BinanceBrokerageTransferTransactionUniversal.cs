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
        [JsonPropertyName("txnId")]
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// Client Transfer Id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
        
        /// <summary>
        /// To id
        /// </summary>
        public string ToId { get; set; } = string.Empty;
        
        /// <summary>
        /// From account type
        /// </summary>
        public BrokerageAccountType FromAccountType { get; set; }
        
        /// <summary>
        /// To account type
        public BrokerageAccountType ToAccountType { get; set; }
        
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
        /// Date
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Status
        /// </summary>
        public BrokerageTransferTransactionStatus Status { get; set; }
    }
}