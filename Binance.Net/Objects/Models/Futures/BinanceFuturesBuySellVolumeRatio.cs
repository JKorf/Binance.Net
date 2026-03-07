namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Buy Sell Volume Ratio Info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesBuySellVolumeRatio
    {
        /// <summary>
        /// ["<c>buySellRatio</c>"] buy/sell ratio
        /// </summary>
        [JsonPropertyName("buySellRatio")]
        public decimal BuySellRatio { get; set; }

        /// <summary>
        /// ["<c>buyVol</c>"] buy volume
        /// </summary>
        [JsonPropertyName("buyVol")]
        public decimal BuyVolume { get; set; }

        /// <summary>
        /// ["<c>sellVol</c>"] sell volume
        /// </summary>
        [JsonPropertyName("sellVol")]
        public decimal SellVolume { get; set; }

        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}