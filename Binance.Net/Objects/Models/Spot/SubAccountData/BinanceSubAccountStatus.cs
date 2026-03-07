namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub-account Status on Margin/Futures
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountStatus
    {
        /// <summary>
        /// ["<c>email</c>"] The user email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>isSubUserEnabled</c>"] Whether the sub account user is enabled.
        /// </summary>
        [JsonPropertyName("isSubUserEnabled")]
        public bool IsAccountEnabled { get; set; }

        /// <summary>
        /// ["<c>isUserActive</c>"] Whether the sub account user is active.
        /// </summary>
        [JsonPropertyName("isUserActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// ["<c>insertTime</c>"] The time the sub account was created
        /// </summary>
        [JsonPropertyName("insertTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ["<c>isMarginEnabled</c>"] Whether margin is enabled.
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }

        /// <summary>
        /// ["<c>isFutureEnabled</c>"] Whether futures is enabled.
        /// </summary>
        [JsonPropertyName("isFutureEnabled")]
        public bool IsFutureEnabled { get; set; }

        /// <summary>
        /// ["<c>mobile</c>"] The user mobile number.
        /// </summary>
        [JsonPropertyName("mobile")]
        public string? MobileNumber { get; set; }
    }
}

