namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Travel rule withdrawal response
    /// </summary>
    public record BinanceTravelWithdrawalResponse
    {
        /// <summary>
        /// ["<c>trId</c>"] Travel rule id
        /// </summary>
        [JsonPropertyName("trId")]
        public long TravelRuleId { get; set; }
        /// <summary>
        /// ["<c>accpted</c>"] Accepted
        /// </summary>
        [JsonPropertyName("accpted")]
        public bool Accepted { get; set; }
        /// <summary>
        /// ["<c>info</c>"] Info
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; } = string.Empty;
    }
}

