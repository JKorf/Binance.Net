namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Strategy update
    /// </summary>
    [SerializationModel]
    public record BinanceGridUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>gu</c>"] Update info
        /// </summary>
        [JsonPropertyName("gu")]
        public BinanceGridInfo GridUpdate { get; set; } = null!;

        /// <summary>
        /// ["<c>T</c>"] Transaction time
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
        /// ["<c>r</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("r")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>up</c>"] Unmatched average price
        /// </summary>
        [JsonPropertyName("up")]
        public decimal UnmatchedAveragePrice { get; set; }
        /// <summary>
        /// ["<c>uq</c>"] Unmatched quantity
        /// </summary>
        [JsonPropertyName("uq")]
        public decimal UnmatchedQuantity { get; set; }
        /// <summary>
        /// ["<c>uf</c>"] Unmatched fee
        /// </summary>
        [JsonPropertyName("uf")]
        public decimal UnmatchedFee { get; set; }
        /// <summary>
        /// ["<c>mp</c>"] Matched profit and loss
        /// </summary>
        [JsonPropertyName("mp")]
        public decimal MatchedPnl { get; set; }
    }
}

