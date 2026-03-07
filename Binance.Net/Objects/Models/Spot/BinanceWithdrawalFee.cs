namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Asset details
    /// </summary>
    [SerializationModel]
    public record BinanceAssetDetails
    {
        /// <summary>
        /// ["<c>minWithdrawAmount</c>"] Minimal quantity you can withdraw
        /// </summary>
        [JsonPropertyName("minWithdrawAmount")]
        public decimal MinimalWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>depositStatus</c>"] Whether deposits are enabled
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public bool DepositStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawStatus</c>"] Whether withdrawing is enabled
        /// </summary>
        [JsonPropertyName("withdrawStatus")]
        public bool WithdrawStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawFee</c>"] Fee for withdrawing
        /// </summary>
        [JsonPropertyName("withdrawFee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// ["<c>depositTip</c>"] Status string for deposit
        /// </summary>
        [JsonPropertyName("depositTip")]
        public string? DepositTip { get; set; }
    }
}

