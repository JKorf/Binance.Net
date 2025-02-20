using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.SubAccountData
{
    /// <summary>
    /// Sub account historic transfer
    /// </summary>
    public record BinanceSubAccountTransferSubAccount
    {
        /// <summary>
        /// Counter party of the transfer
        /// </summary>
        [JsonPropertyName("counterParty")]
        public string CounterParty { get; set; } = string.Empty;
        /// <summary>
        /// Email of the account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// From account type
        /// </summary>
        [JsonPropertyName("fromAccountType")]
        public string FromAccountType { get; set; } = string.Empty;
        /// <summary>
        /// To account type
        /// </summary>
        [JsonPropertyName("toAccountType")]
        public string ToAccountType { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonPropertyName("type")]
        public SubAccountTransferSubAccountType Type { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tranId"), JsonConverter(typeof(NumberStringConverter))]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp of the transfer
        /// </summary>
        [JsonPropertyName("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
