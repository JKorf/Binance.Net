namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    public class BinanceStakingResult 
    {
        /// <summary>
        /// Successful
        /// </summary>
        public bool Success { get; set; }
    }

    /// <summary>
    /// Staking result
    /// </summary>
    public class BinanceStakingPositionResult: BinanceStakingResult
    {
        /// <summary>
        /// Id of the position
        /// </summary>
        public string? PositionId { get; set; }
    }
}
