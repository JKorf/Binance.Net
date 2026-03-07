namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// If the new user info
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesIfNewUser
    {
        /// <summary>
        /// ["<c>brokerId</c>"] The broker identifier.
        /// </summary>
        [JsonPropertyName("brokerId")]
        public string BrokerId { get; set; } = string.Empty;
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
    }
}

