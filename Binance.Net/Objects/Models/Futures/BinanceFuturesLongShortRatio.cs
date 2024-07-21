namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Long Short Ratio Info
    /// </summary>
    public record BinanceFuturesLongShortRatio
    {
        /// <summary>
        /// The symbol or pair the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string SymbolPair { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; } = string.Empty;

        /// <summary>
        /// long/short ratio
        /// </summary>
        [JsonPropertyName("longShortRatio")]
        public decimal LongShortRatio { get; set; }

        /// <summary>
        /// longs percentage (in decimal form)
        /// </summary>
        [JsonPropertyName("longAccount")]
        public decimal LongAccount { get; set; }
        [JsonPropertyName("longPosition")]
        private decimal LongPosition
        {
            set => LongAccount = value;
        }

        /// <summary>
        /// shorts percentage (in decimal form)
        /// </summary>
        [JsonPropertyName("shortAccount")]
        public decimal ShortAccount { get; set; }
        [JsonPropertyName("shortPosition")]
        private decimal ShortPosition
        {
            set => ShortAccount = value;
        }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}