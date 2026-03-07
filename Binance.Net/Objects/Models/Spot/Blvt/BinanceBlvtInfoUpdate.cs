namespace Binance.Net.Objects.Models.Spot.Blvt
{
    /// <summary>
    /// Blvt info update
    /// </summary>
    [SerializationModel]
    public record BinanceBlvtInfoUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>s</c>"] Token name
        /// </summary>
        [JsonPropertyName("s")]
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>m</c>"] Token issued
        /// </summary>
        [JsonPropertyName("m")]
        public decimal TokenIssued { get; set; }
        /// <summary>
        /// ["<c>n</c>"] Nav
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Nav { get; set; }

        /// <summary>
        /// ["<c>b</c>"] Baskets
        /// </summary>
        [JsonPropertyName("b")]
        public BlvtBasket[] Baskets { get; set; } = Array.Empty<BlvtBasket>();
        /// <summary>
        /// ["<c>l</c>"] Real leverage.
        /// </summary>
        [JsonPropertyName("l")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Target leverage.
        /// </summary>
        [JsonPropertyName("t")]
        public decimal TargetLeverage { get; set; }
        /// <summary>
        /// ["<c>f</c>"] Funding ratio
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
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>n</c>"] Position
        /// </summary>
        [JsonPropertyName("n")]
        public decimal Position { get; set; }
    }
}

