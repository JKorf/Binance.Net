using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string Asset { get; set; } = "";
        /// <summary>
        /// Amount transfered
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Transfer type
        /// </summary>
        [JsonConverter(typeof(UniversalTransferTypeConverter))]
        public UniversalTransferType Type { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = "";
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("tranId")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
