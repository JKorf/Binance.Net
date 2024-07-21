namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Wallet balance
    /// </summary>
    public record BinanceWalletBalance
    {
        /// <summary>
        /// Is the wallet activated
        /// </summary>
        [JsonPropertyName("activate")]
        public bool Active { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// Name of the wallet
        /// </summary>
        [JsonPropertyName("walletName")]
        public string WalletName { get; set; } = string.Empty;
    }
}
