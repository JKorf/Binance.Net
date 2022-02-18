using System;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Result of the margin change history request
    /// </summary>
    public class BinanceFuturesMarginChangeHistoryResult
    {
        /// <summary>
        /// Request quantity of margin used
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Base asset used for margin
        /// </summary>
        public string? Asset { get; set; }
        /// <summary>
        /// Symbol margin is placed on
        /// </summary>
        public string? Symbol { get; set; }
        /// <summary>
        /// Time of the margin change request
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Direction of the margin change request
        /// </summary>
        [JsonConverter(typeof(FuturesMarginChangeDirectionTypeConverter))]
        public FuturesMarginChangeDirectionType Type { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
    }

}
