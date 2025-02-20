using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Transfer history entry
    /// </summary>
    public record BinanceTransferHistory
    {
        /// <summary>
        /// Quantity of the transfer
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset of the transfer
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public decimal TransactionId { get; set; }
        /// <summary>
        /// Direction of the transfer
        /// </summary>
        [JsonPropertyName("type")]
        public TransferDirection Direction { get; set; }
        /// <summary>
        /// Transfer from
        /// </summary>
        [JsonPropertyName("transFrom")]
        public string TransferFrom { get; set; } = string.Empty;
        /// <summary>
        /// Transfer to
        /// </summary>
        [JsonPropertyName("transTo")]
        public string TransferTo { get; set; } = string.Empty;
        /// <summary>
        /// Transfer from symbol
        /// </summary>
        [JsonPropertyName("fromSymbol")]
        public string? FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Transfer to symbol
        /// </summary>
        [JsonPropertyName("toSymbol")]
        public string? ToSymbol { get; set; } = string.Empty;
    }
}
