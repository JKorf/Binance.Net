namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Simple earn account info
    /// </summary>
    [SerializationModel]
    public record BinanceSimpleEarnAccount
    {
        /// <summary>
        /// ["<c>totalAmountInBTC</c>"] Total quantity in BTC
        /// </summary>
        [JsonPropertyName("totalAmountInBTC")]
        public decimal TotalQuantityInBtc { get; set; }
        /// <summary>
        /// ["<c>totalAmountInUSDT</c>"] Total quantity in USDT
        /// </summary>
        [JsonPropertyName("totalAmountInUSDT")]
        public decimal TotalQuantityInUsdt { get; set; }
        /// <summary>
        /// ["<c>totalFlexibleAmountInBTC</c>"] Total quantity in BTC in flexible products
        /// </summary>
        [JsonPropertyName("totalFlexibleAmountInBTC")]
        public decimal TotalFlexibleQuantityInBtc { get; set; }
        /// <summary>
        /// ["<c>totalFlexibleAmountInUSDT</c>"] Total quantity in USDT in flexible products
        /// </summary>
        [JsonPropertyName("totalFlexibleAmountInUSDT")]
        public decimal TotalFlexibleQuantityInUsdt { get; set; }
        /// <summary>
        /// ["<c>totalLockedInBTC</c>"] Total quantity in BTC in locked products
        /// </summary>
        [JsonPropertyName("totalLockedInBTC")]
        public decimal TotalLockedInBtc { get; set; }
        /// <summary>
        /// ["<c>totalLockedInUSDT</c>"] Total quantity in USDT in locked products
        /// </summary>
        [JsonPropertyName("totalLockedInUSDT")]
        public decimal TotalLockedInUsdt { get; set; }
    }
}

