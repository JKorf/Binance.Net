namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Strategy update
    /// </summary>
    [SerializationModel]
    public record BinanceGridUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Update info
        /// </summary>
        [JsonPropertyName("gu")]
        public BinanceGridInfo GridUpdate { get; set; } = null!;

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Strategy update info
    /// </summary>
    public record BinanceGridInfo
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
        /// Strategy status
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
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("r")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Unmatched average price
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnmatchedAveragePrice { get; set; }
        /// <summary>
        /// Unmatched quantity
        /// </summary>
        [JsonPropertyName("uq")]
        public decimal UnmatchedQuantity { get; set; }
        /// <summary>
        /// Unmatched fee
        /// </summary>
        [JsonPropertyName("uf")]
        public decimal UnmatchedFee { get; set; }
        /// <summary>
        /// Matched profit and loss
        /// </summary>
        [JsonPropertyName("mp")]
        public decimal MatchedPnl { get; set; }
    }
}
