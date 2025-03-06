using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    internal record BinanceSubAccountUniversalTransfersList
    {
        /// <summary>
        /// Transactions
        /// </summary>
        [JsonPropertyName("result")]
        public BinanceSubAccountUniversalTransferTransaction[] Transactions { get; set; } = [];

    }

    /// <summary>
    /// Binance sub account universal transaction
    /// </summary>
    public record BinanceSubAccountUniversalTransferTransaction
    {
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// From email
        /// </summary>
        [JsonPropertyName("fromEmail")]
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// To email
        /// </summary>
        [JsonPropertyName("toEmail")]
        public string ToEmail { get; set; } = string.Empty;

        /// <summary>
        /// From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public TransferAccountType FromAccountType { get; set; }

        /// <summary>
        /// To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public TransferAccountType ToAccountType { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The time the universal transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTimeStamp")]
        public DateTime CreateTime { get; set; }
    }
}
