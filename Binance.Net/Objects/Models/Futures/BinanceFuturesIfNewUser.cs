namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// If the new user info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesIfNewUser
    {
        /// <summary>
        /// The broker identifier.
        /// </summary>
        [JsonPropertyName("brokerId")]
        public string BrokerId { get; set; } = string.Empty;
        /// <summary>
        /// Whether the API agent code rebate is active.
        /// </summary>
        [JsonPropertyName("rebateWorking")]
        public bool RebateWorking { get; set; }
        /// <summary>
        /// Whether the account is a new user account.
        /// </summary>
        [JsonPropertyName("ifNewUser")]
        public bool IfNewUser { get; set; }
    }
}
