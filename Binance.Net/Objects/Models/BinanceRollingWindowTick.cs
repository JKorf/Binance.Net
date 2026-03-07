namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Rolling window tick info
    /// </summary>
    [SerializationModel]
    public record BinanceRollingWindowTick
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol this data is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>lastPrice</c>"] The current close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>openPrice</c>"] Tick open price
        /// </summary>
        [JsonPropertyName("openPrice")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>highPrice</c>"] Tick high price
        /// </summary>
        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>lowPrice</c>"] Tick low price
        /// </summary>
        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>firstId</c>"] The first trade id of the tick
        /// </summary>
        [JsonPropertyName("firstId")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>lastId</c>"] The last trade id of the tick
        /// </summary>
        [JsonPropertyName("lastId")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>count</c>"] The total trades of id
        /// </summary>
        [JsonPropertyName("count")]
        public long TotalTrades { get; set; }
        /// <summary>
        /// ["<c>openTime</c>"] The open time of these stats
        /// </summary>
        [JsonPropertyName("openTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>closeTime</c>"] The close time of these stats
        /// </summary>
        [JsonPropertyName("closeTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>quoteVolume</c>"] Quote volume
        /// </summary>
        [JsonPropertyName("quoteVolume")]
        public decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>priceChange</c>"] Price change
        /// </summary>
        [JsonPropertyName("priceChange")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>priceChangePercent</c>"] Price change percentage
        /// </summary>
        [JsonPropertyName("priceChangePercent")]
        public decimal PriceChangePercentage { get; set; }
        /// <summary>
        /// ["<c>weightedAvgPrice</c>"] Weighted average price
        /// </summary>
        [JsonPropertyName("weightedAvgPrice")]
        public decimal WeightedAveragePrice { get; set; }
    }
}

