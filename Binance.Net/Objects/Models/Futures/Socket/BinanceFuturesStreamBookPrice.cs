using Binance.Net.Objects.Models.Spot.Socket;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures book price
    /// </summary>
    public record BinanceFuturesStreamBookPrice : BinanceStreamBookPrice
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TransactionTime { get; set; }
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonProperty("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty("e")] 
        public string Event { get; set; } = string.Empty;
    }
}
