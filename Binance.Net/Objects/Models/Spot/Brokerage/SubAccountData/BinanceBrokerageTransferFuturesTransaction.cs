using Binance.Net.Enums;

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
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Futures type
        /// </summary>
        [JsonPropertyName("futuresType")]
        public FuturesAccountType FuturesType { get; set; }

        /// <summary>
        /// Transfer
        /// </summary>
        [JsonPropertyName("transfer")]
        public BinanceBrokerageTransferFuturesTransaction[] Transactions { get; set; } = Array.Empty<BinanceBrokerageTransferFuturesTransaction>();
    }

    /// <summary>
    /// Transfer Futures Transaction
    /// </summary>
    public record BinanceBrokerageTransferFuturesTransaction
    {
        /// <summary>
        /// From Id
        /// </summary>
        [JsonPropertyName("fromId")]
        public string FromId { get; set; } = string.Empty;

        /// <summary>
        /// To Id
        /// </summary>
        [JsonPropertyName("toId")]
        public string ToId { get; set; } = string.Empty;

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