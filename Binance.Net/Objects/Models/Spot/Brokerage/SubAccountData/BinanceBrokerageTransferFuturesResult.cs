namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Futures Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageTransferFuturesResult
    {
        /// <summary>
        /// ["<c>txnId</c>"] Transaction Id
        /// </summary>
        [JsonPropertyName("txnId")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>clientTranId</c>"] Client Transfer Id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
    }
}