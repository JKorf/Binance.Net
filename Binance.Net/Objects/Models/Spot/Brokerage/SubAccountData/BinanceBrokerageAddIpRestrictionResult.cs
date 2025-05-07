namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// Add IP Restriction Result
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageAddIpRestrictionResult
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Api key
        /// </summary>
        [JsonPropertyName("apikey")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// IP
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }
}