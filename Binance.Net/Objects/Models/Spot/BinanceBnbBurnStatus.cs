namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Bnb burn status
    /// </summary>
    [SerializationModel]
    public record BinanceBnbBurnStatus
    {
        /// <summary>
        /// ["<c>spotBNBBurn</c>"] Whether spot trading BNB burn is enabled.
        /// </summary>
        [JsonPropertyName("spotBNBBurn")]
        public bool SpotBnbBurn { get; set; }
        /// <summary>
        /// ["<c>interestBNBBurn</c>"] Whether margin interest BNB burn is enabled.
        /// </summary>
        [JsonPropertyName("interestBNBBurn")]
        public bool InterestBnbBurn { get; set; }
    }
}

