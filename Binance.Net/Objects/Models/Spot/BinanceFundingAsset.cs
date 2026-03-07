namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Funding wallet asset
    /// </summary>
    [SerializationModel]
    public record BinanceFundingAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>free</c>"] Quantity available
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>locked</c>"] Quantity locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>freeze</c>"] Quantity frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// ["<c>withdrawing</c>"] Quantity withdrawing
        /// </summary>
        [JsonPropertyName("withdrawing")]
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// ["<c>btcValuation</c>"] Value in BTC.
        /// </summary>
        [JsonPropertyName("btcValuation")]
        public decimal BtcValuation { get; set; }
    }
}

