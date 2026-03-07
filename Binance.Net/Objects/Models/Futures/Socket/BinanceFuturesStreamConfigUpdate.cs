namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Information about leverage of symbol changed
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamConfigUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>ac</c>"] Leverage update data.
        /// </summary>
        [JsonPropertyName("ac")]
        public BinanceFuturesStreamLeverageUpdateData? LeverageUpdateData { get; set; }

        /// <summary>
        /// ["<c>ai</c>"] Position mode update data.
        /// </summary>
        [JsonPropertyName("ai")]
        public BinanceFuturesStreamConfigUpdateData? ConfigUpdateData { get; set; }

        /// <summary>
        /// ["<c>T</c>"] Transaction time
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionTime { get; set; }
        /// <summary>
        /// The listen key the update was for
        /// </summary>
        public string ListenKey { get; set; } = string.Empty;
    }

    /// <summary>
    /// Config update data
    /// </summary>
    public record BinanceFuturesStreamLeverageUpdateData
    {
        /// <summary>
        /// ["<c>s</c>"] The symbol this balance is for
        /// </summary>
        [JsonPropertyName("s")]
        public string? Symbol { get; set; }

        /// <summary>
        /// ["<c>l</c>"] The symbol this leverage is for
        /// </summary>
        [JsonPropertyName("l")]
        public int Leverage { get; set; }
    }

    /// <summary>
    /// Position mode update data
    /// </summary>
    public record BinanceFuturesStreamConfigUpdateData
    {
        /// <summary>
        /// ["<c>j</c>"] Multi-Assets Mode
        /// </summary>
        [JsonPropertyName("j")]
        public bool MultiAssetMode { get; set; }
    }
}

