using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Crypto loan income info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanIncome
    {
        /// <summary>
        /// ["<c>asset</c>"] The income asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Income type
        /// </summary>
        [JsonPropertyName("type")]
        public LoanIncomeType Type { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] The income timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>tranId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public string TransactionId { get; set; } = string.Empty;
    }
}

