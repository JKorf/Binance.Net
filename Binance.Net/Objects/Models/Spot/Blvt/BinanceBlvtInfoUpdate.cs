namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Blvt info update
    /// </summary>
    [SerializationModel]
    public record BinanceBlvtInfoUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Token name
        /// </summary>
        [JsonPropertyName("s")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonPropertyName("m")]
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// Nav
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Nav { get; set; }

        /// <summary>
        /// Baskets
        /// </summary>
        [JsonPropertyName("b")]
        public BlvtBasket[] Baskets { get; set; } = Array.Empty<BlvtBasket>();
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonPropertyName("l")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Token issued
        /// </summary>
        [JsonPropertyName("t")]
        public decimal TargetLeverage { get; set; }
        /// <summary>
        /// Funding ratio
        /// </summary>
        [JsonPropertyName("f")]
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
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Position { get; set; }
    }
}
