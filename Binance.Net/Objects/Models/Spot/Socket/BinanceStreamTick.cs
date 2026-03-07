using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Tick info
    /// </summary>
    [SerializationModel]
    public abstract record BinanceStreamTickBase : BinanceStreamEvent, IBinanceTick
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
        /// ["<c>x</c>"] The close price of the previous day
        /// </summary>
        [JsonPropertyName("x")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>Q</c>"] The most recent trade quantity
        /// </summary>
        [JsonPropertyName("Q")]
        public decimal LastQuantity { get; set; }
        /// <summary>
        /// ["<c>b</c>"] The best bid price in the order book
        /// </summary>
        [JsonPropertyName("b")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>B</c>"] The quantity of the best bid price in the order book
        /// </summary>
        [JsonPropertyName("B")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>a</c>"] The best ask price in the order book
        /// </summary>
        [JsonPropertyName("a")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>A</c>"] The quantity of the best ask price in the order book
        /// </summary>
        [JsonPropertyName("A")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Today's open price.
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Todays high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Todays low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Total traded volume in the base asset
        /// </summary>
        public abstract decimal Volume { get; set; }
        /// <summary>
        /// Total traded volume in the quote asset
        /// </summary>
        public abstract decimal QuoteVolume { get; set; }
        /// <summary>
        /// ["<c>F</c>"] The first trade id of today
        /// </summary>
        [JsonPropertyName("F")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// ["<c>L</c>"] The last trade id of today
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
    }

    /// <summary>
    /// Stream tick
    /// </summary>
    [SerializationModel]
    public record BinanceStreamTick : BinanceStreamTickBase
    {
        /// <summary>
        /// ["<c>v</c>"] Total traded volume in the base asset
        /// </summary>
        [JsonPropertyName("v")]
        public override decimal Volume { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Total traded volume in the quote asset
        /// </summary>
        [JsonPropertyName("q")]
        public override decimal QuoteVolume { get; set; }
    }

    /// <summary>
    /// Stream tick
    /// </summary>
    [SerializationModel]
    public record BinanceStreamCoinTick : BinanceStreamTickBase
    {
        /// <summary>
        /// ["<c>q</c>"] Total traded volume in the base asset
        /// </summary>
        [JsonPropertyName("q")]
        public override decimal Volume { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Total traded volume in the quote asset
        /// </summary>
        [JsonPropertyName("v")]
        public override decimal QuoteVolume { get; set; }
    }
}

