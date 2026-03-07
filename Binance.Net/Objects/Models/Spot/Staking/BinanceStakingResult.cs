namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    [SerializationModel]
    public record BinanceStakingResult
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}

