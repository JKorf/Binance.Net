namespace Binance.Net.Objects.Models.Futures.Socket
{

    /// <summary>
    /// 
    /// </summary>
    public record BinanceConditionOrderTriggerRejectUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Reject info
        /// </summary>
        [JsonPropertyName("or")]
        public BinanceConditionOrderTriggerReject RejectInfo { get; set; } = null!;
    }

    /// <summary>
    /// Reject info
    /// </summary>
    public record BinanceConditionOrderTriggerReject
    {
        /// <summary>
        /// The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// Reject reason
        /// </summary>
        [JsonPropertyName("r")]
        public string Reason { get; set; } = string.Empty;
    }
}
