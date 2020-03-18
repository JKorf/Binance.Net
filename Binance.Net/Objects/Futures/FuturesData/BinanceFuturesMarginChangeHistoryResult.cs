using Newtonsoft.Json;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using System;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Result of the margin change history request
    /// </summary>
    public class BinanceFuturesMarginChangeHistoryResult
    {
        /// <summary>
        /// Request amount of margin used
        /// </summary>
        public decimal Amount { get; set; }
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
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Direction of the margin change request
        /// </summary>
        [JsonConverter(typeof(FuturesMarginChangeDirectionTypeConverter))]
        public FuturesMarginChangeDirectionType Type { get; set; }
    }

}
