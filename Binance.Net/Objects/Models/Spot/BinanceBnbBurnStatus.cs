namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Bnb burn status
    /// </summary>
    [SerializationModel]
    public record BinanceBnbBurnStatus
    {
        /// <summary>
        /// Whether spot trading BNB burn is enabled.
        /// </summary>
        [JsonPropertyName("spotBNBBurn")]
        public bool SpotBnbBurn { get; set; }
        /// <summary>
        /// Whether margin interest BNB burn is enabled.
        /// </summary>
        [JsonPropertyName("interestBNBBurn")]
        public bool InterestBnbBurn { get; set; }
    }
}
