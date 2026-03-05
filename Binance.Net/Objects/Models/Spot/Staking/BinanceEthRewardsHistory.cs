namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rewards history
    /// </summary>
    [SerializationModel]
    public record BinanceEthRewardsHistory
    {
        /// <summary>
        /// The reward asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// BETH holding balance
        /// </summary>
        [JsonPropertyName("holding")]
        public decimal Holding { get; set; }
        /// <summary>
        /// Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
    }
}
