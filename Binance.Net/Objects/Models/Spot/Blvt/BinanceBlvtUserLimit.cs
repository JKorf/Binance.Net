namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Leveraged tokens user limits
    /// </summary>
    public record BinanceBlvtUserLimit
    {
        /// <summary>
        /// Token name
        /// </summary>
        [JsonPropertyName("tokenName")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Daily purchase limit
        /// </summary>
        [JsonPropertyName("userDailyTotalPurchaseLimit")]
        public decimal UserDailyTotalPurchaseLimit { get; set; }
        /// <summary>
        /// Daily redeem limit
        /// </summary>
        [JsonPropertyName("userDailyTotalRedeemLimit")]
        public decimal UserDailyTotalRedeemLimit { get; set; }
    }
}
