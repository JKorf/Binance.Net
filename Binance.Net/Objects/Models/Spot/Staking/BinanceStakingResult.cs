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
        public bool Success { get; set; }
    }

    /// <summary>
    /// Staking result
    /// </summary>
    public record BinanceStakingPositionResult: BinanceStakingResult
    {
        /// <summary>
        /// Id of the position
        /// </summary>
        public string? PositionId { get; set; }
    }
}
