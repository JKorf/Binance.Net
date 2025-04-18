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
        /// Transaction id
        /// </summary>
        [JsonPropertyName("transactionId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public AutoInvestOneTimeTransactionStatus Status { get; set; }
    }
}
