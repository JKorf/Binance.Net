﻿namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// Rate history
    /// </summary>
    [SerializationModel]
    public record BinanceBethRateHistory
    {
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonPropertyName("exchangeRate")]
        public decimal ExchangeRate { get; set; }
        /// <summary>
        /// Annual percentage rate
        /// </summary>
        [JsonPropertyName("annualPercentageRate")]
        public decimal AnnualPercentageRate { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
