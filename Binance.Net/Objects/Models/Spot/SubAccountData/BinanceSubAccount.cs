namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    [SerializationModel]
    internal record BinanceSubAccountWrapper
    {
        [JsonPropertyName("subAccounts")]
        public BinanceSubAccount[]? SubAccounts { get; set; }
    }

    /// <summary>
    /// Sub account details
    /// </summary>
    public record BinanceSubAccount
    {
        /// <summary>
        /// The email associated with the sub account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Is account frozen
        /// </summary>
        [JsonPropertyName("isFreeze")]
        public bool IsFreeze { get; set; } = false;
        /// <summary>
        /// The time the sub account was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
