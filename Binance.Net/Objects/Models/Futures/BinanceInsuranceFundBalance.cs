namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Insurance fund balance info
    /// </summary>
    public record BinanceInsuranceFundBalance
    {
        /// <summary>
        /// ["<c>symbols</c>"] The symbols covered by the insurance fund data.
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = [];
        /// <summary>
        /// ["<c>assets</c>"] Insurance fund asset balances.
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
        /// ["<c>asset</c>"] The asset name.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginBalance</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] The last update time.
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}

