namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Transfer Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageTransferResult
    {
        /// <summary>
        /// ["<c>txnId</c>"] Transaction Id
        /// </summary>
        [JsonPropertyName("txnId")]
        public long Id { get; set; }

        /// <summary>
        /// ["<c>clientTranId</c>"] Client Transfer Id
        /// </summary>
        [JsonPropertyName("clientTranId")]
        public string ClientTransferId { get; set; } = string.Empty;
    }
}