namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rate history
    /// </summary>
    [SerializationModel]
    public record BinanceBethRateHistory
    {
        /// <summary>
        /// ["<c>exchangeRate</c>"] Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// ["<c>annualPercentageRate</c>"] Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}

