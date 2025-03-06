namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    [SerializationModel]
    public record BinanceStakingResult
    {
        /// <summary>
        /// Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
