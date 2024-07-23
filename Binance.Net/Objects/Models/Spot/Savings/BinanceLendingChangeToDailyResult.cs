﻿namespace Binance.Net.Objects.Models.Spot.Lending
{
    /// <summary>
    /// Purchase result
    /// </summary>
    public record BinanceLendingChangeToDailyResult
    {
        /// <summary>
        /// The id of the purchase
        /// </summary>
        public int DailyPurchaseId { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
