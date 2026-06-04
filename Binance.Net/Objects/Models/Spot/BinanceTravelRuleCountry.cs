namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule country list response
    /// </summary>
    public record BinanceTravelRuleCountryList
    {
        /// <summary>
        /// ["<c>countries</c>"] List of active countries
        /// </summary>
        [JsonPropertyName("countries")]
        public BinanceTravelRuleCountry[] Countries { get; set; } = [];
        /// <summary>
        /// ["<c>lastUpdated</c>"] Last update time in epoch milliseconds
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// Travel rule country
    /// </summary>
    public record BinanceTravelRuleCountry
    {
        /// <summary>
        /// ["<c>countryCode</c>"] ISO 2-digit country code, lower case
        /// </summary>
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>countryName</c>"] Country display name
        /// </summary>
        [JsonPropertyName("countryName")]
        public string CountryName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>blockType</c>"] Status: supported, limited, or blocked
        /// </summary>
        [JsonPropertyName("blockType")]
        public string BlockType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositAllowed</c>"] Whether deposits are permitted from this country
        /// </summary>
        [JsonPropertyName("depositAllowed")]
        public bool DepositAllowed { get; set; }
        /// <summary>
        /// ["<c>withdrawalAllowed</c>"] Whether withdrawals are permitted to this country
        /// </summary>
        [JsonPropertyName("withdrawalAllowed")]
        public bool WithdrawalAllowed { get; set; }
        /// <summary>
        /// ["<c>hasRegionRestrictions</c>"] Whether the country has region-level restrictions requiring a specific city value in questionnaires
        /// </summary>
        [JsonPropertyName("hasRegionRestrictions")]
        public bool HasRegionRestrictions { get; set; }
    }
}
