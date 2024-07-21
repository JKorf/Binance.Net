using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Loan info
    /// </summary>
    public record BinanceLoan
    {
        /// <summary>
        /// Isolated symbol
        /// </summary>
        public string? IsolatedSymbol { get; set; }
        /// <summary>
        /// The asset of the loan
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id of the loan
        /// </summary>
        [JsonPropertyName("txId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Principal repaid 
        /// </summary>
        public decimal Principal { get; set; }
        /// <summary>
        /// Interest repaid 
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Quantity repaid 
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Time of repay completed
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The status of the loan
        /// </summary>
        public MarginStatus Status { get; set; }
    }
}
