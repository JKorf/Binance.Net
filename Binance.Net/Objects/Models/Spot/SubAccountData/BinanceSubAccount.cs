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
        /// ["<c>email</c>"] The email associated with the sub account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isFreeze</c>"] Is account frozen
        /// </summary>
        [JsonPropertyName("isFreeze")]
        public bool IsFreeze { get; set; } = false;
        /// <summary>
        /// ["<c>createTime</c>"] The time the sub account was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
    }
}

