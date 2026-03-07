namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL staking account
    /// </summary>
    [SerializationModel]
    public record BinanceSolStakingAccount
    {
        /// <summary>
        /// ["<c>bnsolAmount</c>"] BNSOL amount.
        /// </summary>
        [JsonPropertyName("bnsolAmount")]
        public decimal BnSolAmount { get; set; }
        /// <summary>
        /// ["<c>holdingInSOL</c>"] Holding in SOL
        /// </summary>
        [JsonPropertyName("holdingInSOL")]
        public decimal HoldingInSOL { get; set; }
        /// <summary>
        /// ["<c>thirtyDaysProfitInSOL</c>"] Thirty days profit in SOL
        /// </summary>
        [JsonPropertyName("thirtyDaysProfitInSOL")]
        public decimal ThirtyDaysProfitInSOL { get; set; }
    }
}

