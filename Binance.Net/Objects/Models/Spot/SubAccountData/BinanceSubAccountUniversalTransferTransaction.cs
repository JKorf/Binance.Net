using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    [SerializationModel]
    internal record BinanceSubAccountUniversalTransfersList
    {
        /// <summary>
        /// ["<c>result</c>"] The returned transfer transactions.
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
        /// ["<c>tranId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// ["<c>fromEmail</c>"] The source account email address.
        /// </summary>
        [JsonPropertyName("fromEmail")]
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>toEmail</c>"] The destination account email address.
        /// </summary>
        [JsonPropertyName("toEmail")]
        public string ToEmail { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>fromAccountType</c>"] From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public TransferAccountType FromAccountType { get; set; }

        /// <summary>
        /// ["<c>toAccountType</c>"] To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public TransferAccountType ToAccountType { get; set; }

        /// <summary>
        /// ["<c>status</c>"] The transfer status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>asset</c>"] The transferred asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// ["<c>createTimeStamp</c>"] The time the universal transaction was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createTimeStamp")]
        public DateTime CreateTime { get; set; }
    }
}

