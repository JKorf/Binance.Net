namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Binance commissions
    /// </summary>
    [SerializationModel]
    public record BinanceCommissions
    {
        /// <summary>
        /// ["<c>symbol</c>"] The symbol name.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>standardCommission</c>"] Standard commission rates on trades from the order.
        /// </summary>
        [JsonPropertyName("standardCommission")]
        public BinanceCommissionInfo StandardCommissions { get; set; } = null!;
        /// <summary>
        /// ["<c>taxCommission</c>"] Tax commission rates for trades from the order.
        /// </summary>
        [JsonPropertyName("taxCommission")]
        public BinanceCommissionInfo TaxCommissions { get; set; } = null!;
        /// <summary>
        /// ["<c>specialCommission</c>"] Special commission rates for trades from the order.
        /// </summary>
        [JsonPropertyName("specialCommission")]
        public BinanceCommissionInfo SpecialCommissions { get; set; } = null!;
        /// <summary>
        /// ["<c>discount</c>"] Discount commission when paying in BNB
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
        /// ["<c>discount</c>"] Standard commission is reduced by this rate when paying commission in BNB.
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// ["<c>enabledForAccount</c>"] Enabled for account
        /// </summary>
        [JsonPropertyName("enabledForAccount")]
        public bool EnabledForAccount { get; set; }
        /// <summary>
        /// ["<c>enabledForSymbol</c>"] Whether the discount is enabled for this symbol.
        /// </summary>
        [JsonPropertyName("enabledForSymbol")]
        public bool EnabledForSymbol { get; set; }
        /// <summary>
        /// ["<c>discountAsset</c>"] Discount asset
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
        /// ["<c>maker</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal Maker { get; set; }
        /// <summary>
        /// ["<c>taker</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("taker")]
        public decimal Taker { get; set; }
        /// <summary>
        /// ["<c>buyer</c>"] Buyer fee
        /// </summary>
        [JsonPropertyName("buyer")]
        public decimal Buyer { get; set; }
        /// <summary>
        /// ["<c>seller</c>"] Seller fee
        /// </summary>
        [JsonPropertyName("seller")]
        public decimal Sell { get; set; }
    }
}

