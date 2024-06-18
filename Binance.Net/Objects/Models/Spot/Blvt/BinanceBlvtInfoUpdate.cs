namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Blvt info update
    /// </summary>
    public record BinanceBlvtInfoUpdate: BinanceStreamEvent
    {
        /// <summary>
        /// Token name
        /// </summary>
        [JsonProperty("s")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonProperty("m")]
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// Nav
        /// </summary>
        [JsonProperty("n")]
        public decimal Nav { get; set; }

        /// <summary>
        /// Baskets
        /// </summary>
        [JsonProperty("b")]
        public IEnumerable<BlvtBasket> Baskets { get; set; } = Array.Empty<BlvtBasket>();
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonProperty("l")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonProperty("t")]
        public decimal TargetLeverage { get; set; }
        /// <summary>
        /// Funding ratio
        /// </summary>
        [JsonProperty("f")]
        public decimal FundingRatio { get; set; }
    }

    /// <summary>
    /// Basket
    /// </summary>
    public record BlvtBasket
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position
        /// </summary>
        [JsonProperty("n")]
        public decimal Position { get; set; }
    }
}
