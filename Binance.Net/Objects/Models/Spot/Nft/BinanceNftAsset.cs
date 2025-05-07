namespace Binance.Net.Objects.Models.Spot.NFT
{
    /// <summary>
    /// NFT asset
    /// </summary>
    public record BinanceNftAsset
    {
        /// <summary>
        /// NFT network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// NFT contract address
        /// </summary>
        [JsonPropertyName("contractAddress")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// NFT token id
        /// </summary>
        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; } = string.Empty;
    }
}
