namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Rolling window tick info
    /// </summary>
    [SerializationModel]
    public record BinanceStreamRollingWindowTick : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol this data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>p</c>"] The price change of this symbol
        /// </summary>
        [JsonPropertyName("p")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>P</c>"] The price change percentage of this symbol
        /// </summary>
        [JsonPropertyName("P")]
        public decimal PriceChangePercent { get; set; }
        /// <summary>
        /// ["<c>w</c>"] The weighted average
        /// </summary>
        [JsonPropertyName("w")]
        public decimal WeightedAveragePrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] The current close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Tick open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Tick high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Tick low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>F</c>"] The first trade id of the tick
        /// </summary>
        [JsonPropertyName("F")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The last trade id of the tick
        /// </summary>
        [JsonPropertyName("L")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>n</c>"] The total number of trades.
        /// </summary>
        [JsonPropertyName("n")]
        public long TotalTrades { get; set; }
        /// <summary>
        /// ["<c>O</c>"] The open time of these stats
        /// </summary>
        [JsonPropertyName("O"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>C</c>"] The close time of these stats
        /// </summary>
        [JsonPropertyName("C"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Quote volume
        /// </summary>
        [JsonPropertyName("q")]
        public decimal QuoteVolume { get; set; }
    }
}

