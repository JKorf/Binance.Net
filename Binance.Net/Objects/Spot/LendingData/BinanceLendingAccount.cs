using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.LendingData
{
    /// <summary>
    /// Lending account
    /// </summary>
    public class BinanceLendingAccount
    {
        /// <summary>
        /// Total amount in btc
        /// </summary>
        public decimal TotalAmountInBTC { get; set; }
        /// <summary>
        /// Total amount in usdt
        /// </summary>
        public decimal TotalAmountInUSDT { get; set; }
        /// <summary>
        /// Total fixed amount in btc
        /// </summary>
        public decimal TotalFixedAmountInBTC { get; set; }
        /// <summary>
        /// Total fixed amount in usdt
        /// </summary>
        public decimal TotalFixedAmountInUSDT { get; set; }
        /// <summary>
        /// Total flexible in btc
        /// </summary>
        public decimal TotalFlexibleInBTC { get; set; }
        /// <summary>
        /// Total flexible in usdt
        /// </summary>
        public decimal TotalFlexibleInUSDT { get; set; }

        /// <summary>
        /// Position amounts
        /// </summary>
        [JsonProperty("positionAmountVos")]
        public IEnumerable<BinanceLendingPositionAmount> PositionAmounts { get; set; } = new List<BinanceLendingPositionAmount>();
    }

    /// <summary>
    /// Lending position amount
    /// </summary>
    public class BinanceLendingPositionAmount
    {
        /// <summary>
        /// Amount of the asset
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Amount in btc
        /// </summary>
        public decimal AmountInBTC { get; set; }
        /// <summary>
        /// Amount in usdt
        /// </summary>
        public decimal AmountInUSDT { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = "";
    }
}
