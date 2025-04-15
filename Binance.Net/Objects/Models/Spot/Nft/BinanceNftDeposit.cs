namespace Binance.Net.Objects.Models.Spot.NFT
{
    public record BinanceNftDeposit
    {
        /// <summary>
        /// NFT network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txID")]
        public string? TxID { get; set; }
        /// <summary>
        /// NFT contract address
        /// </summary>
        [JsonPropertyName("contractAdrress")] // not a typo, spelled according to binance docs
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// NFT token id
        /// </summary>
        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
