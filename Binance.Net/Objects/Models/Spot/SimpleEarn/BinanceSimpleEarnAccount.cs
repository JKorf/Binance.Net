namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn account info
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnAccount
    {
        /// <summary>
        /// Total quantity in BTC
        /// </summary>
        [JsonPropertyName("totalAmountInBTC")]
        public decimal TotalQuantityInBtc { get; set; }
        /// <summary>
        /// Total quantity in USDT
        /// </summary>
        [JsonPropertyName("totalAmountInUSDT")]
        public decimal TotalQuantityInUsdt { get; set; }
        /// <summary>
        /// Total quantity in BTC in flexible products
        /// </summary>
        [JsonPropertyName("totalFlexibleAmountInBTC")]
        public decimal TotalFlexibleQuantityInBtc { get; set; }
        /// <summary>
        /// Total quantity in USDT in flexible products
        /// </summary>
        [JsonPropertyName("totalFlexibleAmountInUSDT")]
        public decimal TotalFlexibleQuantityInUsdt { get; set; }
        /// <summary>
        /// Total quantity in BTC in locked products
        /// </summary>
        [JsonPropertyName("totalLockedInBTC")]
        public decimal TotalLockedInBtc { get; set; }
        /// <summary>
        /// Total quantity in USDT in locked products
        /// </summary>
        [JsonPropertyName("totalLockedInUSDT")]
        public decimal TotalLockedInUsdt { get; set; }
    }
}
