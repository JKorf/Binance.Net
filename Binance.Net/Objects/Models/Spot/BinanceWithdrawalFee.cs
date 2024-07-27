namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset details
    /// </summary>
    public record BinanceAssetDetails
    {
        /// <summary>
        /// Minimal quantity you can withdraw
        /// </summary>
        [JsonPropertyName("minWithdrawAmount")]
        public decimal MinimalWithdrawQuantity { get; set; }
        /// <summary>
        /// Whether deposits are enabled
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public bool DepositStatus { get; set; }
        /// <summary>
        /// Whether withdrawing is enabled
        /// </summary>
        [JsonPropertyName("withdrawStatus")]
        public bool WithdrawStatus { get; set; }
        /// <summary>
        /// Fee for withdrawing
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Status string for deposit
        /// </summary>
        [JsonPropertyName("depositTip")]
        public string? DepositTip { get; set; }
    }
}
