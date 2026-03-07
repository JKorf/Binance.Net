namespace Binance.Net.Objects.Models.Spot.NFT
{
    /// <summary>
    /// NFT withdraw
    /// </summary>
    public record BinanceNftWithdraw
    {
        /// <summary>
        /// ["<c>network</c>"] NFT network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txID</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txID")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractAdrress</c>"] NFT contract address
        /// </summary>
        [JsonPropertyName("contractAdrress")] // not a typo, spelled according to binance docs
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tokenId</c>"] NFT token id
        /// </summary>
        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Withdraw fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeAsset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeAsset")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}

