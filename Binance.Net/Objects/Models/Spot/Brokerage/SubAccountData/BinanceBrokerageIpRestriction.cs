namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// IP Restriction
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageIpRestrictionBase
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
        /// IP list
        /// </summary>
        [JsonPropertyName("ipList")]
        public string[] IpList { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// IP Restriction
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageIpRestriction : BinanceBrokerageIpRestrictionBase
    {
        /// <summary>
        /// Ip Restrict
        /// </summary>
        [JsonPropertyName("ipRestrict")]
        public bool IpRestrict { get; set; }
    }
}