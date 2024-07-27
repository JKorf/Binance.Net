namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Leveraged token info
    /// </summary>
    public record BinanceBlvtInfo
    {
        /// <summary>
        /// Name of the token
        /// </summary>
        [JsonPropertyName("tokenName")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Description of the token
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Underlying asset
        /// </summary>
        [JsonPropertyName("underlying")]
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonPropertyName("tokenIssued")]
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// Basket
        /// </summary>
        [JsonPropertyName("basket")]
        public string Basket { get; set; } = string.Empty;
        /// <summary>
        /// Nav
        /// </summary>
        [JsonPropertyName("nav")]
        public decimal Nav { get; set; }
        /// <summary>
        /// Real leverage
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Daily management fee
        /// </summary>
        [JsonPropertyName("dailyManagementFee")]
        public decimal DailyManagementFee { get; set; }

        /// <summary>
        /// Current baskets
        /// </summary>
        [JsonPropertyName("currentBaskets")]
        public IEnumerable<BlvtCurrentBasket> CurrentBaskets { get; set; } = Array.Empty<BlvtCurrentBasket>();
        /// <summary>
        /// Redeem fee percentage
        /// </summary>
        [JsonPropertyName("redeemFeePct")]
        public decimal RedeemFeePercentage { get; set; }
        /// <summary>
        /// Daily redeem limit
        /// </summary>
        [JsonPropertyName("dailyRedeemLimit")]
        public decimal DailyRedeemLimit { get; set; }
        /// <summary>
        /// Purchase fee percentage
        /// </summary>
        [JsonPropertyName("purchaseFeePct")]
        public decimal PurchaseFeePercentage { get; set; }
        /// <summary>
        /// Daily purchase limit
        /// </summary>
        [JsonPropertyName("dailyPurchaseLimit")]
        public decimal DailyPurchaseLimit { get; set; }

        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Basket
    /// </summary>
    public record BlvtCurrentBasket
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Notional value
        /// </summary>
        [JsonPropertyName("notionalValue")]
        public decimal NotionalValue { get; set; }
    }
}
