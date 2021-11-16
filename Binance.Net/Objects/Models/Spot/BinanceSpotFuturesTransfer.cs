using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public class BinanceSpotFuturesTransfer
    {
        /// <summary>
        /// The asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The transaction id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// The quantity transferred
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The transfer direction
        /// </summary>
        [JsonConverter(typeof(FuturesTransferTypeConverter))]
        public FuturesTransferType Type { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// The status of the transfer
        /// </summary>
        [JsonConverter(typeof(FuturesTransferStatusConverter))]
        public FuturesTransferStatus Status { get; set; }
    }
}
