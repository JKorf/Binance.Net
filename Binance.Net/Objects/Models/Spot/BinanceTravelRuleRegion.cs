namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule region list response
    /// </summary>
    public record BinanceTravelRuleRegionList
    {
        /// <summary>
        /// ["<c>countryCode</c>"] ISO 2-digit country code, lower case
        /// </summary>
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>regions</c>"] List of active regions for the country
        /// </summary>
        [JsonPropertyName("regions")]
        public BinanceTravelRuleRegion[] Regions { get; set; } = [];
        /// <summary>
        /// ["<c>lastUpdated</c>"] Last update time in epoch milliseconds
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// Travel rule region. The RegionName value is used in the city/bnfCorpCity/corpCity fields of travel rule questionnaires.
    /// </summary>
    public record BinanceTravelRuleRegion
    {
        /// <summary>
        /// ["<c>regionName</c>"] Region/city name. Use this exact value in questionnaire city fields.
        /// </summary>
        [JsonPropertyName("regionName")]
        public string RegionName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>blockType</c>"] Status: supported, limited, or blocked
        /// </summary>
        [JsonPropertyName("blockType")]
        public string BlockType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositAllowed</c>"] Whether deposits are permitted from this region
        /// </summary>
        [JsonPropertyName("depositAllowed")]
        public bool DepositAllowed { get; set; }
        /// <summary>
        /// ["<c>withdrawalAllowed</c>"] Whether withdrawals are permitted to this region
        /// </summary>
        [JsonPropertyName("withdrawalAllowed")]
        public bool WithdrawalAllowed { get; set; }
    }
}
