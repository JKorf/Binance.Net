namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    public record BinanceSolStakingResult
    {
        /// <summary>
        /// Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// BN SOL amount
        /// </summary>
        [JsonPropertyName("bnsolAmount")]
        public decimal BnSolAmount { get; set; }
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// Arrival time for redeeming
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public DateTime? ArrivalTime { get; set; }
    }
}
