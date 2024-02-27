using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.Objects.Models.Spot.SimpleEarn
{
    /// <summary>
    /// Flexible product position info
    /// </summary>
    public class BinanceSimpleEarnFlexiblePosition
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Latest annual percentage rate
        /// </summary>
        [JsonProperty("latestAnnualPercentageRate")]
        public decimal LatestAnnualPercentageRate { get; set; }
        /// <summary>
        /// Tier annual percentage rate
        /// </summary>
        [JsonProperty("tierAnnualPercentageRate")]
        public Dictionary<string, decimal> TierAnnualPercentageRate { get; set; } = new Dictionary<string, decimal>();
        /// <summary>
        /// Can redeem product
        /// </summary>
        [JsonProperty("canRedeem")]
        public bool CanRedeem { get; set; }
        /// <summary>
        /// product id
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Total quantity of position
        /// </summary>
        [JsonProperty("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Air drop percentage rate of yesterday
        /// </summary>
        [JsonProperty("yesterdayAirdropPercentageRate")]
        public decimal YesterdayAirdropPercentageRate { get; set; }
        /// <summary>
        /// Air drop asset
        /// </summary>
        [JsonProperty("airDropAsset")]
        public string AirDropAsset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral quantity
        /// </summary>
        [JsonProperty("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Realtime rewards of yesterday
        /// </summary>
        [JsonProperty("yesterdayRealTimeRewards")]
        public decimal YesterdayRealTimeRewards { get; set; }
        /// <summary>
        /// Cumulative bonus rewards
        /// </summary>
        [JsonProperty("cumulativeBonusRewards")]
        public decimal CumulativeBonusRewards { get; set; }
        /// <summary>
        /// Cumulative realtime rewards
        /// </summary>
        [JsonProperty("cumulativeRealTimeRewards")]
        public decimal CumulativeRealTimeRewards { get; set; }
        /// <summary>
        /// Cumulative total rewards
        /// </summary>
        [JsonProperty("cumulativeTotalRewards")]
        public decimal CumulativeTotalRewards { get; set; }
        /// <summary>
        /// Is auto subscribe enabled
        /// </summary>
        [JsonProperty("autoSubscribe")]
        public bool AutoSubscribe { get; set; }
    }
}
