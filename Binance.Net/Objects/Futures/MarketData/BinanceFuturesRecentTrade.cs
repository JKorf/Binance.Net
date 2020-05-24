using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Recent trade info
    /// </summary>
    public class BinanceFuturesRecentTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonProperty("id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        [JsonProperty("Time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether the buyer is maker
        /// </summary>
        [JsonProperty("isBuyerMaker")]
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Whether the trade was made at the best match
        /// </summary>
        public bool IsBestMatch { get; set; }
    }
}
