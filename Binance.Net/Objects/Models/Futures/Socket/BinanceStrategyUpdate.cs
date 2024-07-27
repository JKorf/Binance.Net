namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Strategy update
    /// </summary>
    public record BinanceStrategyUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// Update info
        /// </summary>
        [JsonPropertyName("su")]
        public BinanceStrategyInfo StrategyUpdate { get; set; } = null!;

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Strategy update info
    /// </summary>
    public record BinanceStrategyInfo
    {
        /// <summary>
        /// The strategy id
        /// </summary>
        [JsonPropertyName("si")]
        public int StrategyId { get; set; }
        /// <summary>
        /// Strategy type
        /// </summary>
        [JsonPropertyName("st")]
        public string StrategyType { get; set; } = string.Empty;
        /// <summary>
        /// Stategy status
        /// </summary>
        [JsonPropertyName("ss")]
        public string StrategyStatus { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("ut")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Op code
        /// </summary>
        [JsonPropertyName("c")]
        public int OpCode { get; set; }
    }
}
