namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo order result
    /// </summary>
    [SerializationModel]
    public record BinanceAlgoResult : BinanceResult
    {
        /// <summary>
        /// ["<c>algoId</c>"] Algo order id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// ["<c>clientAlgoId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        public string? ClientAlgoId { get; set; }
        /// <summary>
        /// ["<c>success</c>"] Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}

