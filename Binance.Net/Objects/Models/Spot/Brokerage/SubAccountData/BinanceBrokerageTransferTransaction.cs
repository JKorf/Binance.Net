using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Transaction
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageTransferTransaction
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
        /// ["<c>fromId</c>"] From Id
        /// </summary>
        [JsonPropertyName("fromId")]
        public string FromId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>toId</c>"] To Id
        /// </summary>
        [JsonPropertyName("toId")]
        public string ToId { get; set; } = string.Empty;

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