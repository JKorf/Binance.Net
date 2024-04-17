namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Long Short Ratio Info
    /// </summary>
    public class BinanceFuturesLongShortRatio
    {
        /// <summary>
        /// The symbol or pair the information is about
        /// </summary>
        [JsonProperty("symbol")]
        public string SymbolPair { get; set; } = string.Empty;

        /// <summary>
        /// Pair
        /// </summary>
        public string? Pair { get; set; } = string.Empty;

        /// <summary>
        /// long/short ratio
        /// </summary>
        public decimal LongShortRatio { get; set; }

        /// <summary>
        /// longs percentage (in decimal form)
        /// </summary>
        [JsonProperty("longAccount")]
        public decimal LongAccount { get; set; }
        [JsonProperty("longPosition")]
        private decimal LongPosition
        {
            set => LongAccount = value;
        }

        /// <summary>
        /// shorts percentage (in decimal form)
        /// </summary>
        [JsonProperty("shortAccount")]
        public decimal ShortAccount { get; set; }
        [JsonProperty("shortPosition")]
        private decimal ShortPosition
        {
            set => ShortAccount = value;
        }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}