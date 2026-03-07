using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    [SerializationModel]
    public record BinanceStreamIndexKlineData : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol the data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>k</c>"] The data
        /// </summary>
        [JsonPropertyName("k")]
        public BinanceFuturesStreamIndexKline Data { get; set; } = default!;
    }

    /// <summary>
    /// Index kline
    /// </summary>
    public record BinanceFuturesStreamIndexKline
    {
        /// <summary>
        /// ["<c>t</c>"] Open time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>T</c>"] Close time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// ["<c>s</c>"] Ignore
        /// </summary>
        [JsonPropertyName("s")]
        public string Ignore1 { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] Kline interval
        /// </summary>
        [JsonPropertyName("i")]
        public KlineInterval Interval { get; set; }

        /// <summary>
        /// ["<c>f</c>"] Ignore
        /// </summary>
        [JsonPropertyName("f")]
        public string Ignore2 { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>L</c>"] Ignore
        /// </summary>
        [JsonPropertyName("L")]
        public string Ignore3 { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>o</c>"] Open price of the kline
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price of the kline
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price of the kline
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price of the kline
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// ["<c>v</c>"] Ignore
        /// </summary>
        [JsonPropertyName("v")]
        public string Ignore4 { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>n</c>"] Number of data points in the candle.
        /// </summary>
        [JsonPropertyName("n")]
        public int NumberOfBasicData { get; set; }

        /// <summary>
        /// ["<c>x</c>"] Is the kline closed
        /// </summary>
        [JsonPropertyName("x")]
        public bool Closed { get; set; }

        /// <summary>
        /// ["<c>q</c>"] Ignore
        /// </summary>
        [JsonPropertyName("q")]
        public string Ignore5 { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>V</c>"] Ignore
        /// </summary>
        [JsonPropertyName("V")]
        public string Ignore6 { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>Q</c>"] Ignore
        /// </summary>
        [JsonPropertyName("Q")]
        public string Ignore7 { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>b</c>"] Ignore
        /// </summary>
        [JsonPropertyName("b")]
        public string Ignore8 { get; set; } = string.Empty;
    }
}

