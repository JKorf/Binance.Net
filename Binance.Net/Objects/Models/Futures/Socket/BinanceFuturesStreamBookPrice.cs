using Binance.Net.Objects.Models.Spot.Socket;

namespace Binance.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Futures book price
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesStreamBookPrice : BinanceStreamBookPrice
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? TransactionTime { get; set; }
        /// <summary>
        /// The time the event happened
        /// </summary>
        [JsonPropertyName("E"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonPropertyName("e")]
        public string Event { get; set; } = string.Empty;
    }
}
