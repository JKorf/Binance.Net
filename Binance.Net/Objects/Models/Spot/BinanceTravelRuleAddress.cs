namespace Binance.Net.Objects.Models.Spot
{
    internal record BinanceTravelRuleAddressWrapper
    {
        [JsonPropertyName("addressVerifyItemList")]
        public BinanceTravelRuleAddress[] Addresses { get; set; } = [];
    }

    /// <summary>
    /// Travel rule address
    /// </summary>
    public record BinanceTravelRuleAddress
    {
        /// <summary>
        /// ["<c>status</c>"] The address verification status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>token</c>"] The asset token.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>walletAddress</c>"] Wallet address
        /// </summary>
        [JsonPropertyName("walletAddress")]
        public string WalletAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>addressQuestionnaire</c>"] Address questionnaire answers
        /// </summary>
        [JsonPropertyName("addressQuestionnaire")]
        public Dictionary<string, object> AddressQuestionnaire { get; set; } = new();
    }
}

