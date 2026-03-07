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
        /// ["<c>amount</c>"] The updated margin amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>code</c>"] The request response code.
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>maxNotionalValue</c>"] Maximum margin value
        /// NOTE: string type, because the value can be 'inf' (infinite)
        /// </summary>
        [JsonPropertyName("maxNotionalValue")]
        public string MaxNotionalValue { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Direction of the requested margin change
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesMarginChangeDirectionType Type { get; set; }
    }

}

