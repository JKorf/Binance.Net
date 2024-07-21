namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Flexible product position
    /// </summary>
    public record BinanceFlexibleProductPosition
    {
        /// <summary>
        /// Annual interest rate
        /// </summary>
        public decimal AnnualInterestRate { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Average annual interest rate
        /// </summary>
        [JsonPropertyName("avgAnnualInterestRate")]
        public decimal AverageAnnualInterestRate { get; set; }
        /// <summary>
        /// Tier Average annual interest rate
        /// </summary>
        [JsonPropertyName("tierAnnualInterestRate")]
        public Dictionary<string, decimal> TierAnnualInterestRate { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Can redeem
        /// </summary>
        public bool CanRedeem { get; set; }
        /// <summary>
        /// Daily interest rate
        /// </summary>
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Quantity free
        /// </summary>
        [JsonPropertyName("freeAmount")]
        public decimal FreeQuantity { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        [JsonPropertyName("freezeAmount")]
        public decimal FreezeQuantity { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        [JsonPropertyName("lockedAmount")]
        public decimal LockedQuantity { get; set; }

        /// <summary>
        /// The product id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// The product name
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// Redeeming quantity
        /// </summary>
        [JsonPropertyName("redeemingAmount")]
        public decimal RedeemingQuantity { get; set; }
        /// <summary>
        /// Quantity purchased today
        /// </summary>
        [JsonPropertyName("todayPurchasedAmount")]
        public decimal TodayPurchasedQuantity { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Total interest
        /// </summary>
        public decimal TotalInterest { get; set; }
    }
}
