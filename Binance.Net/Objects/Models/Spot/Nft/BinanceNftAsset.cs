namespace Binance.Net.Objects.Models.Spot.NFT
{
    /// <summary>
    /// NFT asset
    /// </summary>
    public record BinanceNftAsset
    {
        /// <summary>
        /// ["<c>network</c>"] NFT network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractAddress</c>"] NFT contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tokenId</c>"] NFT token id
        /// </summary>
        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; } = string.Empty;
    }
}

