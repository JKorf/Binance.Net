using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Repay info
    /// </summary>
    public record BinanceRepay
    {
        /// <summary>
        /// Isolated symbol
        /// </summary>
        [JsonPropertyName("isolatedSymbol")]
        public string? IsolatedSymbol { get; set; }
        /// <summary>
        /// The asset of the repay
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id of the repay
        /// </summary>`
        [JsonPropertyName("txId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Total quantity repaid
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Interest repaid
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// Principal repaid
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// Time of repay completed
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The status of the repay
        /// </summary>
        [JsonPropertyName("status")]
        public MarginStatus Status { get; set; }
    }
}
