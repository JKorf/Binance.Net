namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Strategy update
    /// </summary>
    [SerializationModel]
    public record BinanceStrategyUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>su</c>"] Update info
        /// </summary>
        [JsonPropertyName("su")]
        public BinanceStrategyInfo StrategyUpdate { get; set; } = null!;

        /// <summary>
        /// ["<c>T</c>"] Transaction time
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
        /// ["<c>si</c>"] The strategy id
        /// </summary>
        [JsonPropertyName("si")]
        public int StrategyId { get; set; }
        /// <summary>
        /// ["<c>st</c>"] Strategy type
        /// </summary>
        [JsonPropertyName("st")]
        public string StrategyType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ss</c>"] Strategy status
        /// </summary>
        [JsonPropertyName("ss")]
        public string StrategyStatus { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>s</c>"] The symbol.
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ut</c>"] Update time
        /// </summary>
        [JsonPropertyName("ut")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Op code
        /// </summary>
        [JsonPropertyName("c")]
        public int OpCode { get; set; }
    }
}

