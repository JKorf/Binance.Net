using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.IsolatedMarginData
{
    /// <summary>
    /// Isolated margin transfer
    /// </summary>
    public class BinanceIsolatedMarginTransfer
    {
        /// <summary>
        /// Amount of the transfer
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Transfer asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// Status of the transfer
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp of the transfer
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonProperty("txId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// From
        /// </summary>
        [JsonProperty("transFrom"), JsonConverter(typeof(IsolatedMarginTransferDirectionConverter))]
        public IsolatedMarginTransferDirection From { get; set; }
        /// <summary>
        /// To
        /// </summary>
        [JsonProperty("transTo"), JsonConverter(typeof(IsolatedMarginTransferDirectionConverter))]
        public IsolatedMarginTransferDirection To { get; set; }
    }
}
