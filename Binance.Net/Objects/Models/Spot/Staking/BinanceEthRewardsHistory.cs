namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rewards history
    /// </summary>
    [SerializationModel]
    public record BinanceEthRewardsHistory
    {
        /// <summary>
        /// ["<c>asset</c>"] The reward asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] The timestamp.
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>holding</c>"] BETH holding balance
        /// </summary>
        [JsonPropertyName("holding")]
        public decimal Holding { get; set; }
        /// <summary>
        /// ["<c>annualPercentageRate</c>"] Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
    }
}

