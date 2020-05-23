using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Redemption record
    /// </summary>
    public class BinanceRedemptionRecord
    {
        /// <summary>
        /// Amount purchased
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Redeem type
        /// </summary>
        [JsonConverter(typeof(RedeemTypeConverter))]
        public RedeemType Type { get; set; }
        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; } = "";
        /// <summary>
        /// Principal
        /// </summary>
        public decimal Principal { get; set; }

        /// <summary>
        /// Purchase status
        /// </summary>
        public string Status { get; set; } = "";
    }
}
