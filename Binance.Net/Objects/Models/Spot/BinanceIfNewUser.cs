namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// If the new user info
    /// </summary>
    [SerializationModel]
    public record BinanceIfNewUser
    {
        /// <summary>
        /// Api Agent Code
        /// </summary>
        [JsonPropertyName("apiAgentCode")]
        public string ApiAgentCode { get; set; } = string.Empty;
        /// <summary>
        /// If the apiAgentCode is working
        /// </summary>
        [JsonPropertyName("rebateWorking")]
        public bool RebateWorking { get; set; }
        /// <summary>
        /// If new user
        /// </summary>
        [JsonPropertyName("ifNewUser")]
        public bool IfNewUser { get; set; }
        /// <summary>
        /// Referrer Id
        /// </summary>
        [JsonPropertyName("referrerId")]
        public long ReferrerId { get; set; }
    }
}
