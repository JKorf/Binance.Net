namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL staking account
    /// </summary>
    [SerializationModel]
    public record BinanceSolStakingAccount
    {
        /// <summary>
        /// BN SOL amount
        /// </summary>
        [JsonPropertyName("bnsolAmount")]
        public decimal BnSolAmount { get; set; }
        /// <summary>
        /// Holding in SOL
        /// </summary>
        [JsonPropertyName("holdingInSOL")]
        public decimal HoldingInSOL { get; set; }
        /// <summary>
        /// Thirty days profit in SOL
        /// </summary>
        [JsonPropertyName("thirtyDaysProfitInSOL")]
        public decimal ThirtyDaysProfitInSOL { get; set; }
    }
}
