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
        /// ["<c>counterParty</c>"] The transfer counterparty.
        /// </summary>
        [JsonPropertyName("counterParty")]
        public string CounterParty { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>email</c>"] The account email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromAccountType</c>"] The source account type.
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public string FromAccountType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAccountType</c>"] The destination account type.
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public string ToAccountType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The transfer status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] The transfer type.
        /// </summary>
        [JsonPropertyName("type")]
        public SubAccountTransferSubAccountType Type { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] The transferred asset.
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>tranId</c>"] The transaction identifier.
        /// </summary>
        [JsonPropertyName("tranId"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>qty</c>"] The transferred quantity.
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] The transfer timestamp.
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}

