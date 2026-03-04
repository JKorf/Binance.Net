using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result of the requested margin amount change
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesPositionMarginResult
    {
        /// <summary>
        /// The updated margin amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// The request response code.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Maximum margin value
        /// NOTE: string type, because the value can be 'inf' (infinite)
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public string MaxNotionalValue { get; set; } = string.Empty;
        /// <summary>
        /// Direction of the requested margin change
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesMarginChangeDirectionType Type { get; set; }
    }

}
