namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Test order commission info
    /// </summary>
    public record BinanceTestOrderCommission
    {
        /// <summary>
        /// Standard fee rates on trades from the order
        /// </summary>
        [JsonProperty("standardCommissionForOrder")]
        public BinanceFee StandardFeeForOrder { get; set; } = null!;
        /// <summary>
        /// Tax fee rates on trades from the order
        /// </summary>
        [JsonProperty("taxCommissionForOrder")]
        public BinanceFee TaxFeeForOrder { get; set; } = null!;
        /// <summary>
        /// Discount info
        /// </summary>
        [JsonProperty("discount")]
        public BinanceDiscount Discount { get; set; } = null!;
    }

    /// <summary>
    /// Fee rates
    /// </summary>
    public record BinanceFee
    {
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonProperty("maker")]
        public decimal Maker { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonProperty("taker")]
        public decimal Taker { get; set; }
    }

    /// <summary>
    /// Discount info
    /// </summary>
    public record BinanceDiscount
    {
        /// <summary>
        /// Is discount enabled for the account
        /// </summary>
        [JsonProperty("enabledForAccount")]
        public bool EnabledForAccount { get; set; }
        /// <summary>
        /// Is discount enabled for the symbol
        /// </summary>
        [JsonProperty("enabledForSymbol")]
        public bool EnabledForSymbol { get; set; }
        /// <summary>
        /// The discount asset
        /// </summary>
        [JsonProperty("discountAsset")]
        public string DiscountAsset { get; set; } = string.Empty;
        /// <summary>
        /// Discount rate
        /// </summary>
        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
}
