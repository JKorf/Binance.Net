using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Flexible product position
    /// </summary>
    public class BinanceFlexibleProductPosition
    {
        /// <summary>
        /// Annual interest rate
        /// </summary>
        public decimal AnnualInterestRate { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Average annual interest rate
        /// </summary>
        [JsonProperty("avgAnnualInterestRate")]
        public decimal AverageAnnualInterestRate { get; set; }
        /// <summary>
        /// Can redeem
        /// </summary>
        public bool CanRedeem { get; set; }
        /// <summary>
        /// Daily interest rate
        /// </summary>
        public decimal DailyInterestRate { get; set; }
        /// <summary>
        /// Amount free
        /// </summary>
        public decimal FreeAmount { get; set; }
        /// <summary>
        /// Amount frozen
        /// </summary>
        public decimal FreezeAmount { get; set; }
        /// <summary>
        /// Amount locked
        /// </summary>
        public decimal LockedAmount { get; set; }

        /// <summary>
        /// The product id
        /// </summary>
        public string ProductId { get; set; } = "";
        /// <summary>
        /// The product name
        /// </summary>
        public string ProductName { get; set; } = "";
        /// <summary>
        /// Redeeming amount
        /// </summary>
        public decimal RedeemingAmount { get; set; }
        /// <summary>
        /// Amount purchased today
        /// </summary>
        public decimal TodayPurchasedAmount { get; set; }
        /// <summary>
        /// Total amount
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// Total interest
        /// </summary>
        public decimal TotalInterest { get; set; }
    }
}
