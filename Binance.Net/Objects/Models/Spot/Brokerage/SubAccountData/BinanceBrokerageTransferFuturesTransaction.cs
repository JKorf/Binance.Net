using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Futures Transactions
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageTransferFuturesTransactions
    {
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>futuresType</c>"] Futures type
        /// </summary>
        [JsonPropertyName("futuresType")]
        public FuturesAccountType FuturesType { get; set; }

        /// <summary>
        /// ["<c>transfer</c>"] Transfer
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
        /// ["<c>tranId</c>"] Transaction Id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>clientTranId</c>"] Client Transfer Id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>time</c>"] Date
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }
    }
}