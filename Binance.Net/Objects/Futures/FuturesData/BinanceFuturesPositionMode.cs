using Binance.Net.Converters;
using Binance.Net.Enums;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// User's position mode
    /// </summary>
    public class BinanceFuturesPositionMode
    {
        /// <summary>
        /// true": Hedge Mode mode; "false": One-way Mode
        /// </summary>
        [JsonProperty("dualSidePosition"), JsonConverter(typeof(PositionModeConverter))]
        public PositionMode PositionMode { get; set; }
    }
}
