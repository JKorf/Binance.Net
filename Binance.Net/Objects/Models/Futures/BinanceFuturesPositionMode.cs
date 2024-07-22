using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// User's position mode
    /// </summary>
    public record BinanceFuturesPositionMode
    {
        /// <summary>
        /// true": Hedge Mode mode; "false": One-way Mode
        /// </summary>
        [JsonPropertyName("dualSidePosition")]
        public bool IsHedgeMode { get; set; }
    }
}
