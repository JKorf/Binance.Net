using Newtonsoft.Json;
using Binance.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Result of the requested margin amount change
    /// </summary>
    public class BinanceFuturesPositionMarginResult
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
        /// Maximum margin value
        /// </summary>
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// Direction of the requested margin change
        /// </summary>
        [JsonConverter(typeof(FuturesMarginChangeDirectionTypeConverter))]
        public FuturesMarginChangeDirectionType Type { get; set; }
    }

}
