namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The result of amending an order
    /// </summary>
    [SerializationModel]
    public record BinanceAmendedOrderResult
    {
        /// <summary>
        /// ["<c>transactTime</c>"] Transaction time
        /// </summary>
        [JsonPropertyName("transactTime")]
        public DateTime TransactTime { get; set; }

        /// <summary>
        /// ["<c>executionId</c>"] Execution ID
        /// </summary>
        [JsonPropertyName("executionId")]
        public long ExecutionId { get; set; }

        /// <summary>
        /// ["<c>amendedOrder</c>"] The amended order details
        /// </summary>
        [JsonPropertyName("amendedOrder")]
        public BinanceOrder AmendedOrder { get; set; } = default!;
    }
}
