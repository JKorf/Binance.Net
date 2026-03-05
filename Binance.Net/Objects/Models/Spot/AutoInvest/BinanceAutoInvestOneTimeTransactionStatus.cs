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
        /// The transaction identifier.
        /// </summary>
        [JsonPropertyName("transactionId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// The transaction status.
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestOneTimeTransactionStatus Status { get; set; }
    }
}
