using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// Recent trade info
    /// </summary>
    public class BinanceRecentTrade
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

    /// <summary>
    /// Recent trade with quote quantity
    /// </summary>
    public class BinanceRecentTradeQuote : BinanceRecentTrade
    {
        /// <summary>
        /// The quote quantity of the trade
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Recent trade with base quantity
    /// </summary>
    public class BinanceRecentTradeBase : BinanceRecentTrade
    {
        /// <summary>
        /// The base quantity of the trade
        /// </summary>
        [JsonProperty("baseQty")]
        public decimal BaseQuantity { get; set; }
    }
}
