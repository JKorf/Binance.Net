namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Edit result
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestEditResult
    {
        /// <summary>
        /// The plan identifier.
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
    }


}
