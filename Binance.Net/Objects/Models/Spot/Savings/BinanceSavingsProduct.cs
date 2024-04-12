namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Savings product
    /// </summary>
    public class BinanceSavingsProduct
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Average annual interest rage
        /// </summary>
        [JsonProperty("avgAnnualInterestRate")]
        public decimal AverageAnnualInterestRate { get; set; }
        /// <summary>
        /// Can purchase
        /// </summary>
        public bool CanPurchase { get; set; }
        /// <summary>
        /// Can redeem
        /// </summary>
        public bool CanRedeem { get; set; }
        /// <summary>
        /// Daily interest per thousand
        /// </summary>
        public decimal DailyInterestPerThousand { get; set; }
        /// <summary>
        /// Is featured
        /// </summary>
        public bool Featured { get; set; }
        /// <summary>
        /// Minimal quantity to purchase
        /// </summary>
        [JsonProperty("minPurchaseAmount")]
        public decimal MinimalPurchaseQuantity { get; set; }
        /// <summary>
        /// Product id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Purchased quantity
        /// </summary>
        [JsonProperty("purchasedAmount")]
        public decimal PurchasedQuantity { get; set; }
        /// <summary>
        /// Status of the product
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Upper limit
        /// </summary>
        [JsonProperty("upLimit")]
        public decimal UpperLimit { get; set; }
        /// <summary>
        /// Upper limit per user
        /// </summary>
        [JsonProperty("upLimitPerUser")]
        public decimal UpperLimitPerUser { get; set; }
    }
}
