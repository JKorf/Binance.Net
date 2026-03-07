namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Staking result
    /// </summary>
    [SerializationModel]
    public record BinanceSolStakingResult
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the request succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>bnsolAmount</c>"] BN SOL amount
        /// </summary>
        [JsonPropertyName("bnsolAmount")]
        public decimal BnSolAmount { get; set; }
        /// <summary>
        /// ["<c>exchangeRate</c>"] Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// ["<c>exchangeRate</c>"] Arrival time for redeeming
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public DateTime? ArrivalTime { get; set; }
    }
}

