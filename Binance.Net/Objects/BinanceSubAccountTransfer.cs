using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceSubAccountTransferWrapper
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
        public bool Success { get; set; }
        public BinanceSubAccountTransfer[] Transfers { get; set; }
    }

    public class BinanceSubAccountTransfer
    {
        /// <summary>
        /// From which email the transfer originated
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// To which email the transfer was to
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// The asset of the transfer
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The quantity of the transfer
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The timestamp of the transfer
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
