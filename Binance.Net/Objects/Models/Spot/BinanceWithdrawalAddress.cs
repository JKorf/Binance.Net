namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal address info
    /// </summary>
    public record BinanceWithdrawalAddress
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Address tag
        /// </summary>
        [JsonProperty("addressTag")]
        public string? AddressTag { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Network
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Origin
        /// </summary>
        [JsonProperty("origin")]
        public string? Origin { get; set; }
        /// <summary>
        /// Origin type
        /// </summary>
        [JsonProperty("originType")]
        public string OriginType { get; set; } = string.Empty;
        /// <summary>
        /// Is whitelisted
        /// </summary>
        [JsonProperty("whiteStatus")]
        public bool Whitelisted { get; set; }
    }
}
