namespace Binance.Net.Objects.Models.Futures.Socket
{

    /// <summary>
    /// Condition order reject update
    /// </summary>
    [SerializationModel]
    public record BinanceConditionOrderTriggerRejectUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// ["<c>T</c>"] The event timestamp.
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("T")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>or</c>"] Reject info
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
        /// ["<c>s</c>"] The symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] Order id
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>r</c>"] Reject reason
        /// </summary>
        [JsonPropertyName("r")]
        public string Reason { get; set; } = string.Empty;
    }
}

