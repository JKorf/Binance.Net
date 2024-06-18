using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result of the requested margin amount change
    /// </summary>
    public record BinanceFuturesPositionMarginResult
    {
        /// <summary>
        /// New margin amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Request response code
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Maximum margin value
        /// NOTE: string type, because the value van be 'inf' (infinite)
        /// </summary>
        public string MaxNotionalValue { get; set; } = string.Empty;
        /// <summary>
        /// Direction of the requested margin change
        /// </summary>
        [JsonConverter(typeof(FuturesMarginChangeDirectionTypeConverter))]
        public FuturesMarginChangeDirectionType Type { get; set; }
    }

}
