namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    public record BinanceStakingResult 
    {
        /// <summary>
        /// Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
