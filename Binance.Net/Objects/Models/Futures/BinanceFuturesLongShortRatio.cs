namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Long Short Ratio Info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesLongShortRatio
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol or pair the information is about
        /// </summary>
        [JsonPropertyName("symbol")]
        public string SymbolPair { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string? Pair { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>longShortRatio</c>"] long/short ratio
        /// </summary>
        [JsonPropertyName("longShortRatio")]
        public decimal LongShortRatio { get; set; }

        /// <summary>
        /// ["<c>longAccount</c>"] longs percentage (in decimal form)
        /// </summary>
        [JsonPropertyName("longAccount")]
        public decimal LongAccount { get; set; }
        [JsonInclude, JsonPropertyName("longPosition")]
        internal decimal LongPosition
        {
            get => LongAccount;
            set => LongAccount = value;
        }

        /// <summary>
        /// ["<c>shortAccount</c>"] shorts percentage (in decimal form)
        /// </summary>
        [JsonPropertyName("shortAccount")]
        public decimal ShortAccount { get; set; }
        [JsonInclude, JsonPropertyName("shortPosition")]
        internal decimal ShortPosition
        {
            get => ShortAccount;
            set => ShortAccount = value;
        }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}