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
        [JsonProperty("avgAnnualInterestRate")]
        public decimal AverageAnnualInterestRate { get; set; }
        /// <summary>
        /// Tier Average annual interest rate
        /// </summary>
        [JsonProperty("tierAnnualInterestRate")]
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
        [JsonProperty("freeAmount")]
        public decimal FreeQuantity { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        [JsonProperty("freezeAmount")]
        public decimal FreezeQuantity { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        [JsonProperty("lockedAmount")]
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
        [JsonProperty("redeemingAmount")]
        public decimal RedeemingQuantity { get; set; }
        /// <summary>
        /// Quantity purchased today
        /// </summary>
        [JsonProperty("todayPurchasedAmount")]
        public decimal TodayPurchasedQuantity { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonProperty("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Total interest
        /// </summary>
        public decimal TotalInterest { get; set; }
    }
}
