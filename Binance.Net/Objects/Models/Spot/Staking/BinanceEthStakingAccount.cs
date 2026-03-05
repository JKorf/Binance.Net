namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Eth staking account
    /// </summary>
    [SerializationModel]
    public record BinanceEthStakingAccount
    {
        /// <summary>
        /// Total profit in BETH.
        /// </summary>
        [JsonPropertyName("cumulativeProfitInBETH")]
        public decimal TotalProfitInBeth { get; set; }
        /// <summary>
        /// Last day profit in BETH.
        /// </summary>
        [JsonPropertyName("lastDayProfitInBETH")]
        public decimal LastDayProfitInBeth { get; set; }
    }
}
