namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Rolling window tick info
    /// </summary>
    [SerializationModel]
    public record BinanceStreamRollingWindowTick : BinanceStreamEvent
    {
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The price change of this symbol
        /// </summary>
        [JsonPropertyName("p")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// The price change percentage of this symbol
        /// </summary>
        [JsonPropertyName("P")]
        public decimal PriceChangePercent { get; set; }
        /// <summary>
        /// The weighted average
        /// </summary>
        [JsonPropertyName("w")]
        public decimal WeightedAveragePrice { get; set; }
        /// <summary>
        /// The current close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Tick open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Tick high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Tick low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The first trade id of the tick
        /// </summary>
        [JsonPropertyName("F")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade id of the tick
        /// </summary>
        [JsonPropertyName("L")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// The total trades of id
        /// </summary>
        [JsonPropertyName("n")]
        public long TotalTrades { get; set; }
        /// <summary>
        /// The open time of these stats
        /// </summary>
        [JsonPropertyName("O"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The close time of these stats
        /// </summary>
        [JsonPropertyName("C"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Quote volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
    }
}
