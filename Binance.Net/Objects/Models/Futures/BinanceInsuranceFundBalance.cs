namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Insurance fund balance info
    /// </summary>
    public record BinanceInsuranceFundBalance
    {
        /// <summary>
        /// The symbols covered by the insurance fund data.
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = [];
        /// <summary>
        /// Insurance fund asset balances.
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceInsuranceFundAsset[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Insurance fund asset balance
    /// </summary>
    public record BinanceInsuranceFundAsset
    {
        /// <summary>
        /// The asset name.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// The last update time.
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
