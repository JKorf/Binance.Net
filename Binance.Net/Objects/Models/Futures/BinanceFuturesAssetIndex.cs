using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Futures asset index
    /// </summary>
    public class BinanceFuturesAssetIndex
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Index
        /// </summary>
        public decimal Index { get; set; }
        /// <summary>
        /// Bid buffer
        /// </summary>
        public decimal BidBuffer { get; set; }
        /// <summary>
        /// Ask buffer
        /// </summary>
        public decimal AskBuffer { get; set; }
        /// <summary>
        /// Bid price
        /// </summary>
        [JsonProperty("bidRate")]
        public decimal BidPrice { get; set; }
        /// <summary>
        /// Ask price
        /// </summary>
        [JsonProperty("askRate")]
        public decimal AskPrice { get; set; }
        /// <summary>
        /// Auto exchange bid buffer
        /// </summary>
        public decimal AutoExchangeBidBuffer { get; set; }
        /// <summary>
        /// Auto exchange ask buffer
        /// </summary>
        public decimal AutoExchangeAskBuffer { get; set; }
        /// <summary>
        /// Auto exchange bid price
        /// </summary>
        [JsonProperty("autoExchangeBidRate")]
        public decimal AutoExchangeBidPrice { get; set; }
        /// <summary>
        /// Auto exchange ask price
        /// </summary>
        [JsonProperty("autoExchangeAskRate")]
        public decimal AutoExchangeAskPrice { get; set; }
    }
}
