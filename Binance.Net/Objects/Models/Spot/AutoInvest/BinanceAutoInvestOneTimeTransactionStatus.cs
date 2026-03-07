using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Transaction status
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestOneTimeTransactionStatus
    {
        /// <summary>
        /// ["<c>transactionId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("transactionId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// ["<c>status</c>"] The transaction status.
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestOneTimeTransactionStatus Status { get; set; }
    }
}

