using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Blvt kline
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<BinanceBlvtKline>))]
    [SerializationModel]
    public record BinanceBlvtKline
    {
        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        [ArrayProperty(2)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        [ArrayProperty(3)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        [ArrayProperty(4)]
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// Real leverage
        /// </summary>
        [ArrayProperty(5)]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// The time this candlestick closed
        /// </summary>
        [ArrayProperty(6), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }

        [ArrayProperty(7)] internal string Ignore { get; set; } = string.Empty;

        /// <summary>
        /// Number of updates
        /// </summary>
        [ArrayProperty(8)]
        public int NavUpdates { get; set; }
    }
}
