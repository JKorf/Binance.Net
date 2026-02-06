namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule withdrawal response
    /// </summary>
    public record BinanceTravelWithdrawalResponse
    {
        /// <summary>
        /// Travel rule id
        /// </summary>
        [JsonPropertyName("trId")]
        public long TravelRuleId { get; set; }
        /// <summary>
        /// Accepted
        /// </summary>
        [JsonPropertyName("accpted")]
        public bool Accepted { get; set; }
        /// <summary>
        /// Info
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
    }
}
