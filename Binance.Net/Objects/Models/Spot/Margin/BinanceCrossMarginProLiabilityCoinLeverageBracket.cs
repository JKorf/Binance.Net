namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Cross margin pro liability coin leverage bracket data
    /// </summary>
    public record BinanceCrossMarginProLiabilityCoinLeverageBracket
    {
        /// <summary>
        /// Asset names
        /// </summary>
        [JsonPropertyName("assetNames")]
        public string[] AssetNames { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Rank
        /// </summary>
        [JsonPropertyName("rank")]
        public int Rank { get; set; }
        /// <summary>
        /// Brackets
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
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public int Leverage { get; set; }
        /// <summary>
        /// Max debt
        /// </summary>
        [JsonPropertyName("maxDebt")]
        public decimal MaxDebt { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintenanceMarginRate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("initialMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// Fast num
        /// </summary>
        [JsonPropertyName("fastNum")]
        public decimal FastNum { get; set; }
    }
}