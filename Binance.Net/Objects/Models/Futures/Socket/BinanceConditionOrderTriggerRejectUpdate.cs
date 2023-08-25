using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Futures.Socket
{

    /// <summary>
    /// 
    /// </summary>
    public class BinanceConditionOrderTriggerRejectUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("T")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Reject info
        /// </summary>
        [JsonProperty("or")]
        public BinanceConditionOrderTriggerReject RejectInfo { get; set; } = null!;
    }

    /// <summary>
    /// Reject info
    /// </summary>
    public class BinanceConditionOrderTriggerReject
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// Reject reason
        /// </summary>
        [JsonProperty("r")]
        public string Reason { get; set; } = string.Empty;
    }
}
