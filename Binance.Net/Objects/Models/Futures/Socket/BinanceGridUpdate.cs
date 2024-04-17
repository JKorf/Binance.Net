namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Strategy update
    /// </summary>
    public class BinanceGridUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Update info
        /// </summary>
        [JsonProperty("gu")]
        public BinanceGridInfo GridUpdate { get; set; } = null!;

        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }

    /// <summary>
    /// Strategy update info
    /// </summary>
    public class BinanceGridInfo
    {
        /// <summary>
        /// The strategy id
        /// </summary>
        [JsonProperty("si")]
        public int StrategyId { get; set; }
        /// <summary>
        /// Strategy type
        /// </summary>
        [JsonProperty("st")]
        public string StrategyType { get; set; } = string.Empty;
        /// <summary>
        /// Stategy status
        /// </summary>
        [JsonProperty("ss")]
        public string StrategyStatus { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Update time
        /// </summary>
        [JsonProperty("ut")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonProperty("r")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Unmatched average price
        /// </summary>
        [JsonProperty("up")]
        public decimal UnmatchedAveragePrice { get; set; }
        /// <summary>
        /// Unmatched quantity
        /// </summary>
        [JsonProperty("uq")]
        public decimal UnmatchedQuantity { get; set; }
        /// <summary>
        /// Unmatched fee
        /// </summary>
        [JsonProperty("uf")]
        public decimal UnmatchedFee { get; set; }
        /// <summary>
        /// Matched profit and loss
        /// </summary>
        [JsonProperty("mp")]
        public decimal MatchedPnl { get; set; }
    }
}
