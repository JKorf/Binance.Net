using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wrapper for kline information for a symbol
    /// </summary>
    public record BinanceStreamIndexKlineData : BinanceStreamEvent
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The data
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
        /// Open time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("t")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime CloseTime { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("s")]
        public string Ignore1 { get; set; } = string.Empty;
        /// <summary>
        /// Kline interval
        /// </summary>
        [JsonPropertyName("i")]
        public KlineInterval Interval { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("f")]
        public string Ignore2 { get; set; } = string.Empty;
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("L")]
        public string Ignore3 { get; set; } = string.Empty;

        /// <summary>
        /// Open price of the kline
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Close price of the kline
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// High price of the kline
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price of the kline
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("v")]
        public string Ignore4 { get; set; } = string.Empty;
        
        /// <summary>
        /// Number of basic data
        /// </summary>
        [JsonPropertyName("n")]
        public int NumberOfBasicData { get; set; }

        /// <summary>
        /// Is the kline closed
        /// </summary>
        [JsonPropertyName("x")]
        public bool Closed { get; set; }

        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("q")]
        public string Ignore5 { get; set; } = string.Empty;
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("V")]
        public string Ignore6 { get; set; } = string.Empty;
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("Q")]
        public string Ignore7 { get; set; } = string.Empty;
        /// <summary>
        /// Ignore
        /// </summary>
        [JsonPropertyName("b")]
        public string Ignore8 { get; set; } = string.Empty;
    }
}
