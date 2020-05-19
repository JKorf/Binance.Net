using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Savings product
    /// </summary>
    public class BinanceSavingsProduct
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = "";
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
        /// Minimal amount to purchase
        /// </summary>
        [JsonProperty("minPurchaseAmount")]
        public decimal MinimalPurchaseAmount { get; set; }
        /// <summary>
        /// Product id
        /// </summary>
        public string ProductId { get; set; } = "";
        /// <summary>
        /// Purchased amount
        /// </summary>
        public decimal PurchasedAmount { get; set; }
        /// <summary>
        /// Status of the product
        /// </summary>
        public string Status { get; set; } = "";
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
