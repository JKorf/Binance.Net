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
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Description of the token
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Underlying asset
        /// </summary>
        public string Underlying { get; set; } = string.Empty;
        /// <summary>
        /// Token issued
        /// </summary>
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// Basket
        /// </summary>
        public string Basket { get; set; } = string.Empty;
        /// <summary>
        /// Nav
        /// </summary>
        public decimal Nav { get; set; }
        /// <summary>
        /// Real leverage
        /// </summary>
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Daily management fee
        /// </summary>
        public decimal DailyManagementFee { get; set; }

        /// <summary>
        /// Current baskets
        /// </summary>
        public IEnumerable<BlvtCurrentBasket> CurrentBaskets { get; set; } = Array.Empty<BlvtCurrentBasket>();
        /// <summary>
        /// Redeem fee percentage
        /// </summary>
        [JsonProperty("redeemFeePct")]
        public decimal RedeemFeePercentage { get; set; }
        /// <summary>
        /// Daily redeem limit
        /// </summary>
        public decimal DailyRedeemLimit { get; set; }
        /// <summary>
        /// Purchase fee percentage
        /// </summary>
        [JsonProperty("purchaseFeePct")]
        public decimal PurchaseFeePercentage { get; set; }
        /// <summary>
        /// Daily purchase limit
        /// </summary>
        public decimal DailyPurchaseLimit { get; set; }

        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
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
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Notional value
        /// </summary>
        public decimal NotionalValue { get; set; }
    }
}
