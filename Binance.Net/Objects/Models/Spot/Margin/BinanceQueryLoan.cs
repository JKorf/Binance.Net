using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Loan info
    /// </summary>
    [SerializationModel]
    public record BinanceLoan
    {
        /// <summary>
        /// ["<c>isolatedSymbol</c>"] Isolated symbol
        /// </summary>
        [JsonPropertyName("isolatedSymbol")]
        public string? IsolatedSymbol { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] The asset of the loan
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] The transaction id of the loan
        /// </summary>
        [JsonPropertyName("txId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>principal</c>"] Principal repaid 
        /// </summary>
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }
        /// <summary>
        /// ["<c>interest</c>"] Interest repaid 
        /// </summary>
        [JsonPropertyName("interest")]
        public decimal Interest { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity repaid 
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Time of repay completed
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The status of the loan
        /// </summary>
        [JsonPropertyName("status")]
        public MarginStatus Status { get; set; }
    }
}

