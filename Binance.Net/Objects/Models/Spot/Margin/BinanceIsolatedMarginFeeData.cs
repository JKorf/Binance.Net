namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Fee data
    /// </summary>
    public class BinanceIsolatedMarginFeeData
    {
        /// <summary>
        /// Vip level
        /// </summary>
        [JsonProperty("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonProperty("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<BinanceIsolatedMarginFeeInfo> FeeInfo { get; set; } = Array.Empty<BinanceIsolatedMarginFeeInfo>();
    }

    /// <summary>
    /// Fee info
    /// </summary>
    public class BinanceIsolatedMarginFeeInfo
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily interest
        /// </summary>
        [JsonProperty("dailyInterest")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// Borrow limit
        /// </summary>
        [JsonProperty("borrowLimit")]
        public decimal BorrowLimit { get; set; }
    }
}
