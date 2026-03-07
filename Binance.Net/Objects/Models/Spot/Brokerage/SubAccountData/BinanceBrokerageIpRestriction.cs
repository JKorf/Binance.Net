namespace Binance.Net.Objects.Models.Spot.Brokerage.SubAccountData
{
    /// <summary>
    /// IP Restriction
    /// </summary>
    [SerializationModel]
    public record BinanceBrokerageIpRestrictionBase
    {
        /// <summary>
        /// ["<c>subaccountId</c>"] Sub Account Id
        /// </summary>
        [JsonPropertyName("subaccountId")]
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>apikey</c>"] Api key
        /// </summary>
        [JsonPropertyName("apikey")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>ipList</c>"] IP list
        /// </summary>
        [JsonPropertyName("ipList")]
        public string[] IpList { get; set; } = Array.Empty<string>();

        /// <summary>
        /// ["<c>updateTime</c>"] Timestamp
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
        /// ["<c>ipRestrict</c>"] Ip Restrict
        /// </summary>
        [JsonPropertyName("ipRestrict")]
        public bool IpRestrict { get; set; }
    }
}