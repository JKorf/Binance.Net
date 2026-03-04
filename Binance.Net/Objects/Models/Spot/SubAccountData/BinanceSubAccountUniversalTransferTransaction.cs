using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    [SerializationModel]
    internal record BinanceSubAccountUniversalTransfersList
    {
        /// <summary>
        /// The returned transfer transactions.
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
        /// The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// The source account email address.
        /// </summary>
        [JsonPropertyName("fromEmail")]
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// The destination account email address.
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
        /// The transfer status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The transferred asset.
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
