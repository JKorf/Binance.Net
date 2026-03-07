namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// If the new user info
    /// </summary>
    [SerializationModel]
    public record BinanceIfNewUser
    {
        /// <summary>
        /// ["<c>apiAgentCode</c>"] The API agent code.
        /// </summary>
        [JsonPropertyName("apiAgentCode")]
        public string ApiAgentCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rebateWorking</c>"] Whether the API agent code rebate is active.
        /// </summary>
        [JsonPropertyName("rebateWorking")]
        public bool RebateWorking { get; set; }
        /// <summary>
        /// ["<c>ifNewUser</c>"] Whether the account is a new user account.
        /// </summary>
        [JsonPropertyName("ifNewUser")]
        public bool IfNewUser { get; set; }
        /// <summary>
        /// ["<c>referrerId</c>"] Referrer Id
        /// </summary>
        [JsonPropertyName("referrerId")]
        public long ReferrerId { get; set; }
    }
}

