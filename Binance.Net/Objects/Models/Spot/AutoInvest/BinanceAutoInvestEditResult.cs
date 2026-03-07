namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Edit result
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestEditResult
    {
        /// <summary>
        /// ["<c>planId</c>"] The plan identifier.
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// ["<c>nextExecutionDateTime</c>"] Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
    }


}

