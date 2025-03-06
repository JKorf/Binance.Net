namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub-account Status on Margin/Futures
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountStatus
    {
        /// <summary>
        /// User email
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Sub account user enabled
        /// </summary>
        [JsonPropertyName("isSubUserEnabled")]
        public bool IsAccountEnabled { get; set; }

        /// <summary>
        /// Sub account user active
        /// </summary>
        [JsonPropertyName("isUserActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The time the sub account was created
        /// </summary>
        [JsonPropertyName("insertTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Is Margin enabled
        /// </summary>
        [JsonPropertyName("isMarginEnabled")]
        public bool IsMarginEnabled { get; set; }

        /// <summary>
        /// Is Futures enabled
        /// </summary>
        [JsonPropertyName("isFutureEnabled")]
        public bool IsFutureEnabled { get; set; }

        /// <summary>
        /// User mobile number
        /// </summary>
        [JsonPropertyName("mobile")]
        public string? MobileNumber { get; set; }
    }
}
