﻿namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Open Interest History info
    /// </summary>
    public record BinanceFuturesOpenInterestHistory
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Total open interest
        /// </summary>
        public decimal SumOpenInterest { get; set; }

        /// <summary>
        /// Total open interest value
        /// </summary>
        public decimal SumOpenInterestValue { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }
}
