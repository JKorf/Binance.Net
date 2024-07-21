namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Buy Sell Volume Ratio Info
    /// </summary>
    public record BinanceFuturesBuySellVolumeRatio
    {
        /// <summary>
        /// buy/sell ratio
        /// </summary>
        [JsonPropertyName("buySellRatio")]
        public decimal BuySellRatio { get; set; }

        /// <summary>
        /// buy volume
        /// </summary>
        [JsonPropertyName("buyVol")]
        public decimal BuyVolume { get; set; }

        /// <summary>
        /// sell volume
        /// </summary>
        [JsonPropertyName("sellVol")]
        public decimal SellVolume { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}