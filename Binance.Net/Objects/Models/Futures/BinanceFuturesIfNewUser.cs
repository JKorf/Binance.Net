namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// If the new user info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesIfNewUser
    {
        /// <summary>
        /// Broker Id
        /// </summary>
        [JsonPropertyName("brokerId")]
        public string BrokerId { get; set; } = string.Empty;
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
    }
}
