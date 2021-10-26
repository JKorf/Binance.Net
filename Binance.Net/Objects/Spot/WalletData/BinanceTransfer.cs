using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Spot.WalletData
{
    /// <summary>
    /// Transfer info
    /// </summary>
    public class BinanceTransfer
    {
        /// <summary>
        /// The asset which was transfered
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity transfered
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonConverter(typeof(UniversalTransferTypeConverter))]
        public UniversalTransferType Type { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("tranId")]
        public long Id { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
