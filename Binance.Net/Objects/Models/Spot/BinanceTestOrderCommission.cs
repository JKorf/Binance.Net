namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Test order commission info
    /// </summary>
    [SerializationModel]
    public record BinanceTestOrderCommission
    {
        /// <summary>
        /// Standard fee rates on trades from the order
        /// </summary>
        [JsonPropertyName("standardCommissionForOrder")]
        public BinanceFee StandardFeeForOrder { get; set; } = null!;
        /// <summary>
        /// Tax fee rates on trades from the order
        /// </summary>
        [JsonPropertyName("taxCommissionForOrder")]
        public BinanceFee TaxFeeForOrder { get; set; } = null!;
        /// <summary>
        /// Special fee rates on trades from the order
        /// </summary>
        [JsonPropertyName("specialCommission")]
        public BinanceFee SpecialFeeForOrder { get; set; } = null!;
        /// <summary>
        /// Discount info
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
        /// Maker fee
        /// </summary>
        [JsonPropertyName("maker")]
        public decimal Maker { get; set; }
        /// <summary>
        /// Taker fee
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
        /// Is discount enabled for the account
        /// </summary>
        [JsonPropertyName("enabledForAccount")]
        public bool EnabledForAccount { get; set; }
        /// <summary>
        /// Is discount enabled for the symbol
        /// </summary>
        [JsonPropertyName("enabledForSymbol")]
        public bool EnabledForSymbol { get; set; }
        /// <summary>
        /// The discount asset
        /// </summary>
        [JsonPropertyName("discountAsset")]
        public string DiscountAsset { get; set; } = string.Empty;
        /// <summary>
        /// Discount rate
        /// </summary>
        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }
    }
}
