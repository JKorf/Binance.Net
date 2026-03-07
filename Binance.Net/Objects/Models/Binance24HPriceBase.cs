using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models
{
    /// <summary>
    /// 24 hour rolling window price data
    /// </summary>
    public abstract record Binance24HPriceBase : IBinance24HPrice
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol the price is for
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>priceChange</c>"] The actual price change in the last 24 hours
        /// </summary>
        [JsonPropertyName("priceChange")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// ["<c>priceChangePercent</c>"] The price change in percentage in the last 24 hours
        /// </summary>
        [JsonPropertyName("priceChangePercent")]
        public decimal PriceChangePercent { get; set; }
        /// <summary>
        /// ["<c>weightedAvgPrice</c>"] The weighted average price in the last 24 hours
        /// </summary>
        [JsonPropertyName("weightedAvgPrice")]
        public decimal WeightedAveragePrice { get; set; }
        /// <summary>
        /// ["<c>lastPrice</c>"] The most recent trade price
        /// </summary>
        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>lastQty</c>"] The most recent trade quantity
        /// </summary>
        [JsonPropertyName("lastQty")]
        public decimal LastQuantity { get; set; }
        /// <summary>
        /// ["<c>openPrice</c>"] The open price 24 hours ago
        /// </summary>
        [JsonPropertyName("openPrice")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>highPrice</c>"] The highest price in the last 24 hours
        /// </summary>
        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>lowPrice</c>"] The lowest price in the last 24 hours
        /// </summary>
        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The base volume traded in the last 24 hours
        /// </summary>
        public abstract decimal Volume { get; set; }
        /// <summary>
        /// The quote asset volume traded in the last 24 hours
        /// </summary>
        public abstract decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>openTime</c>"] Time at which this 24 hours opened
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("openTime")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>closeTime</c>"] Time at which this 24 hours closed
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("closeTime")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>firstId</c>"] The first trade ID in the last 24 hours
        /// </summary>
        [JsonPropertyName("firstId")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>lastId</c>"] The last trade ID in the last 24 hours
        /// </summary>
        [JsonPropertyName("lastId")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>count</c>"] The amount of trades made in the last 24 hours
        /// </summary>
        [JsonPropertyName("count")]
        public long TotalTrades { get; set; }
    }
}

