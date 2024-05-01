using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Margin
{
    /// <summary>
    /// Transfer history entry
    /// </summary>
    public class BinanceTransferHistory
    {
        /// <summary>
        /// Quanity of the transfer
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Asset of the transfer
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status of the transfer
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("txId")]
        public decimal TransactionId { get; set; }
        /// <summary>
        /// Direction of the transfer
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(TransferDirectionConverter))]
        public TransferDirection Direction { get; set; }
        /// <summary>
        /// Transfer from
        /// </summary>
        [JsonProperty("transFrom")]
        public string TransferFrom { get; set; } = string.Empty;
        /// <summary>
        /// Transfer to
        /// </summary>
        [JsonProperty("transTo")]
        public string TransferTo { get; set; } = string.Empty;
        /// <summary>
        /// Transfer from symbol
        /// </summary>
        [JsonProperty("fromSymbol")]
        public string? FromSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Transfer to symbol
        /// </summary>
        [JsonProperty("toSymbol")]
        public string? ToSymbol { get; set; } = string.Empty;
    }
}
