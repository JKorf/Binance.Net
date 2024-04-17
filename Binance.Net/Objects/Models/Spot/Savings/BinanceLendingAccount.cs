namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Lending account
    /// </summary>
    public class BinanceLendingAccount
    {
        /// <summary>
        /// Total quantity in btc
        /// </summary>
        [JsonProperty("totalAmountInBTC")]
        public decimal TotalQuantityInBTC { get; set; }
        /// <summary>
        /// Total quantity in usdt
        /// </summary>
        [JsonProperty("totalAmountInUSDT")]
        public decimal TotalQuantityInUSDT { get; set; }
        /// <summary>
        /// Total fixed quantity in btc
        /// </summary>
        [JsonProperty("totalFixedAmountInBTC")]
        public decimal TotalFixedQuantityInBTC { get; set; }
        /// <summary>
        /// Total fixed quantity in usdt
        /// </summary>
        [JsonProperty("totalFixedAmountInUSDT")]
        public decimal TotalFixedQuantityInUSDT { get; set; }
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
        public IEnumerable<BinanceLendingPositionAmount> PositionAmounts { get; set; } = Array.Empty<BinanceLendingPositionAmount>();
    }

    /// <summary>
    /// Lending position amount
    /// </summary>
    public class BinanceLendingPositionAmount
    {
        /// <summary>
        /// Amount of the asset
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Amount in btc
        /// </summary>
        [JsonProperty("amountInBTC")]
        public decimal QuantityInBTC { get; set; }
        /// <summary>
        /// Amount in usdt
        /// </summary>
        [JsonProperty("amountInUSDT")]
        public decimal QuantityInUSDT { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        public string Asset { get; set; } = string.Empty;
    }
}
