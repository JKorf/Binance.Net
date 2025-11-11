namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo order result
    /// </summary>
    [SerializationModel]
    public record BinanceAlgoResult : BinanceResult
    {
        /// <summary>
        /// Algo order id
        /// </summary>
        [JsonPropertyName("algoId")]
        public long AlgoId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        public string? ClientAlgoId { get; set; }
        /// <summary>
        /// Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
