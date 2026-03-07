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
        /// ["<c>txnId</c>"] Transaction Id
        /// </summary>
        [JsonPropertyName("txnId")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>clientTranId</c>"] Client Transfer Id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>toId</c>"] To id
        /// </summary>
        [JsonPropertyName("toId")]
        public string ToId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>fromAccountType</c>"] From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public BrokerageAccountType FromAccountType { get; set; }

        /// <summary>
        /// ["<c>toAccountType</c>"] To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public BrokerageAccountType ToAccountType { get; set; }

        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>qty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>time</c>"] Date
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public BrokerageTransferTransactionStatus Status { get; set; }
    }
}