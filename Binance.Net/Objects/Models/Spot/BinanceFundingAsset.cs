namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Funding wallet asset
    /// </summary>
    [SerializationModel]
    public record BinanceFundingAsset
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity available
        /// </summary>
        [JsonPropertyName("free")]
        public decimal Available { get; set; }
        /// <summary>
        /// Quantity locked
        /// </summary>
        [JsonPropertyName("locked")]
        public decimal Locked { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// Quantity withdrawing
        /// </summary>
        [JsonPropertyName("withdrawing")]
        public decimal Withdrawing { get; set; }
        /// <summary>
        /// Value in btc
        /// </summary>
        [JsonPropertyName("btcValuation")]
        public decimal BtcValuation { get; set; }
    }
}
