namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Cross margin pro liability coin leverage bracket data
    /// </summary>
    public record BinanceCrossMarginProLiabilityCoinLeverageBracket
    {
        /// <summary>
        /// ["<c>assetNames</c>"] Asset names
        /// </summary>
        [JsonPropertyName("assetNames")]
        public string[] AssetNames { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>rank</c>"] Rank
        /// </summary>
        [JsonPropertyName("rank")]
        public int Rank { get; set; }
        /// <summary>
        /// ["<c>brackets</c>"] Brackets
        /// </summary>
        [JsonPropertyName("brackets")]
        public BinanceCrossMarginProBracket[] Brackets { get; set; } = Array.Empty<BinanceCrossMarginProBracket>();
    }

    /// <summary>
    /// Cross margin pro bracket data
    /// </summary>
    public record BinanceCrossMarginProBracket
    {
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// ["<c>maxDebt</c>"] Max debt
        /// </summary>
        [JsonPropertyName("maxDebt")]
        public decimal MaxDebt { get; set; }
        /// <summary>
        /// ["<c>maintenanceMarginRate</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenanceMarginRate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>initialMarginRate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("initialMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>fastNum</c>"] Fast num
        /// </summary>
        [JsonPropertyName("fastNum")]
        public decimal FastNum { get; set; }
    }
}