using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public record BinanceTransfer
    {
        /// <summary>
        /// The asset which was transfered
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity transfered
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transfer type
        /// </summary>
        public UniversalTransferType Type { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("tranId")]
        public long Id { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
