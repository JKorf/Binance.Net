using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
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
        [JsonConverter(typeof(TimestampConverter))]
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
    }
}
