using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Transaction Universal
    /// </summary>
    [SerializationModel]
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
        [JsonPropertyName("toId")]
        public string ToId { get; set; } = string.Empty;

        /// <summary>
        /// From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public BrokerageAccountType FromAccountType { get; set; }

        /// <summary>
        /// To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public BrokerageAccountType ToAccountType { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
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
        [JsonPropertyName("status")]
        public BrokerageTransferTransactionStatus Status { get; set; }
    }
}