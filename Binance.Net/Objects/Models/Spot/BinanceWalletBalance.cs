namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Wallet balance
    /// </summary>
    [SerializationModel]
    public record BinanceWalletBalance
    {
        /// <summary>
        /// ["<c>activate</c>"] Is the wallet activated
        /// </summary>
        [JsonPropertyName("activate")]
        public bool Active { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>walletName</c>"] Name of the wallet
        /// </summary>
        [JsonPropertyName("walletName")]
        public string WalletName { get; set; } = string.Empty;
    }
}

