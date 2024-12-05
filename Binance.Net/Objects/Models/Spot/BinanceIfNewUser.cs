namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// If the new user info
    /// </summary>
    public record BinanceIfNewUser
    {
        /// <summary>
        /// Api Agen tCode
        /// </summary>
        [JsonPropertyName("apiAgentCode")]
        public string ApiAgentCode { get; set; } = string.Empty;
        /// <summary>
        /// If the apiAgentCode is working
        /// </summary>
        [JsonPropertyName("rebateWorking")]
        public bool RebateWorking { get; set; } = false;
        /// <summary>
        /// If new user
        /// </summary>
        [JsonPropertyName("ifNewUser")]
        public bool ifNewUser { get; set; } = false;
        /// <summary>
        /// Referrer Id
        /// </summary>
        [JsonPropertyName("referrerId")]
        public long ReferrerId { get; set; } = 0;
    }
}
