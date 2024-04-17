namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Redemption quota left
    /// </summary>
    public class BinanceRedemptionQuotaLeft
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily quota
        /// </summary>
        public decimal DailyQuota { get; set; }
        /// <summary>
        /// Left quota
        /// </summary>
        public decimal LeftQuota { get; set; }
        /// <summary>
        /// Minimal redemption quantity
        /// </summary>
        [JsonProperty("minRedemptionAmount")]
        public decimal MinimalRedemptionQuantity { get; set; }
    }
}
