namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Fee data
    /// </summary>
    [SerializationModel]
    public record BinanceIsolatedMarginFeeData
    {
        /// <summary>
        /// ["<c>vipLevel</c>"] Vip level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public BinanceIsolatedMarginFeeInfo[] FeeInfo { get; set; } = Array.Empty<BinanceIsolatedMarginFeeInfo>();
    }

    /// <summary>
    /// Fee info
    /// </summary>
    public record BinanceIsolatedMarginFeeInfo
    {
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dailyInterest</c>"] Daily interest
        /// </summary>
        [JsonPropertyName("dailyInterest")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// ["<c>borrowLimit</c>"] Borrow limit
        /// </summary>
        [JsonPropertyName("borrowLimit")]
        public decimal BorrowLimit { get; set; }
    }
}

