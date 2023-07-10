using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Rolling window tick info
    /// </summary>
    public class BinanceRollingWindowTick
    {
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The current close price. This is the latest price for this symbol.
        /// </summary>
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Tick open price
        /// </summary>
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Tick high price
        /// </summary>
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Tick low price
        /// </summary>
        public decimal LowPrice { get; set; }
        /// <summary>
        /// The first trade id of the tick
        /// </summary>
        [JsonProperty("firstId")]
        public long FirstTradeId { get; set; }
        /// <summary>
        /// The last trade id of the tick
        /// </summary>
        [JsonProperty("lastId")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// The total trades of id
        /// </summary>
        [JsonProperty("count")]
        public long TotalTrades { get; set; }
        /// <summary>
        /// The open time of these stats
        /// </summary>
        [JsonProperty("openTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// The close time of these stats
        /// </summary>
        [JsonProperty("closeTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// Quote volume
        /// </summary>
        public decimal QuoteVolume { get; set; }
    }
}
