namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Test order commission info
    /// </summary>
    [SerializationModel]
    public record BinanceTestOrderCommission
    {
        /// <summary>
        /// ["<c>standardCommissionForOrder</c>"] Standard fee rates on trades from the order
        /// </summary>
        [JsonPropertyName("standardCommissionForOrder")]
        public BinanceFee StandardFeeForOrder { get; set; } = null!;
        /// <summary>
        /// ["<c>taxCommissionForOrder</c>"] Tax fee rates on trades from the order
        /// </summary>
        [JsonPropertyName("taxCommissionForOrder")]
        public BinanceFee TaxFeeForOrder { get; set; } = null!;
        /// <summary>
        /// ["<c>specialCommission</c>"] Special fee rates on trades from the order
        /// </summary>
        [JsonPropertyName("specialCommission")]
        public BinanceFee SpecialFeeForOrder { get; set; } = null!;
        /// <summary>
        /// ["<c>discount</c>"] Discount info
        /// </summary>
        [JsonPropertyName("discount")]
        public BinanceDiscount Discount { get; set; } = null!;
    }

    /// <summary>
    /// Fee rates
    /// </summary>
    public record BinanceFee
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
    }

    /// <summary>
    /// Discount info
    /// </summary>
    public record BinanceDiscount
    {
        /// <summary>
        /// ["<c>enabledForAccount</c>"] Whether discount is enabled for the account.
        /// </summary>
        [JsonPropertyName("enabledForAccount")]
        public bool EnabledForAccount { get; set; }
        /// <summary>
        /// ["<c>enabledForSymbol</c>"] Whether discount is enabled for the symbol.
        /// </summary>
        [JsonPropertyName("enabledForSymbol")]
        public bool EnabledForSymbol { get; set; }
        /// <summary>
        /// ["<c>discountAsset</c>"] The discount asset
        /// </summary>
        [JsonPropertyName("discountAsset")]
        public string DiscountAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>discount</c>"] Discount rate
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
    }
}

