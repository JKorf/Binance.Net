namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Binance commissions
    /// </summary>
    [SerializationModel]
    public record BinanceCommissions
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Standard commission rates on trades from the order.
        /// </summary>
        [JsonPropertyName("standardCommission")]
        public BinanceCommissionInfo StandardCommissions { get; set; } = null!;
        /// <summary>
        /// Tax commission rates for trades from the order.
        /// </summary>
        [JsonPropertyName("taxCommission")]
        public BinanceCommissionInfo TaxCommissions { get; set; } = null!;
        /// <summary>
        /// Discount commission when paying in BNB
        /// </summary>
        [JsonPropertyName("discount")]
        public BinanceDiscountInfo Discount { get; set; } = null!;

    }

    /// <summary>
    /// Commission info
    /// </summary>
    public record BinanceDiscountInfo
    {
        /// <summary>
        /// Standard commission is reduced by this rate when paying commission in BNB.
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// Enabled for account
        /// </summary>
        [JsonPropertyName("enabledForAccount")]
        public bool EnabledForAccount { get; set; }
        /// <summary>
        /// Enabled for symbol
        /// </summary>
        [JsonPropertyName("enabledForSymbol")]
        public bool EnabledForSymbol { get; set; }
        /// <summary>
        /// Discount asset
        /// </summary>
        [JsonPropertyName("discountAsset")]
        public string DiscountAsset { get; set; } = string.Empty;
    }

    /// <summary>
    /// Commission info
    /// </summary>
    public record BinanceCommissionInfo
    {
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal Maker { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("taker")]
        public decimal Taker { get; set; }
        /// <summary>
        /// Buyer fee
        /// </summary>
        [JsonPropertyName("buyer")]
        public decimal Buyer { get; set; }
        /// <summary>
        /// Seller fee
        /// </summary>
        [JsonPropertyName("seller")]
        public decimal Sell { get; set; }
    }
}
