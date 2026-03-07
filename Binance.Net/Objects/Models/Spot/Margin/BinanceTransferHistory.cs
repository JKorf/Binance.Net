using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Transfer history entry
    /// </summary>
    [SerializationModel]
    public record BinanceTransferHistory
    {
        /// <summary>
        /// ["<c>amount</c>"] Quantity of the transfer
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>asset</c>"] Asset of the transfer
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp of the transaction
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>txId</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("txId")]
        public decimal TransactionId { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Direction of the transfer
        /// </summary>
        [JsonPropertyName("type")]
        public TransferDirection Direction { get; set; }
        /// <summary>
        /// ["<c>transFrom</c>"] Transfer from
        /// </summary>
        [JsonPropertyName("transFrom")]
        public string TransferFrom { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transTo</c>"] Transfer to
        /// </summary>
        [JsonPropertyName("transTo")]
        public string TransferTo { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromSymbol</c>"] Transfer from symbol
        /// </summary>
        [JsonPropertyName("fromSymbol")]
        public string? FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toSymbol</c>"] Transfer to symbol
        /// </summary>
        [JsonPropertyName("toSymbol")]
        public string? ToSymbol { get; set; } = string.Empty;
    }
}

