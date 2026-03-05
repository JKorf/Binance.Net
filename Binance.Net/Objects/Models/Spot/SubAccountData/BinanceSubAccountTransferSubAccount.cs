using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account historic transfer
    /// </summary>
    [SerializationModel]
    public record BinanceSubAccountTransferSubAccount
    {
        /// <summary>
        /// The transfer counterparty.
        /// </summary>
        [JsonPropertyName("counterParty")]
        public string CounterParty { get; set; } = string.Empty;
        /// <summary>
        /// The account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// The source account type.
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public string FromAccountType { get; set; } = string.Empty;
        /// <summary>
        /// The destination account type.
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public string ToAccountType { get; set; } = string.Empty;
        /// <summary>
        /// The transfer status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// The transfer type.
        /// </summary>
        [JsonPropertyName("type")]
        public SubAccountTransferSubAccountType Type { get; set; }
        /// <summary>
        /// The transferred asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The transferred quantity.
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The transfer timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
