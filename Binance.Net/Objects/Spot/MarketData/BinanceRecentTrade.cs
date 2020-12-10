using Newtonsoft.Json;
using System;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// Recent trade info
    /// </summary>
    public abstract class BinanceRecentTrade : IBinanceRecentTrade
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
        /// <inheritdoc />
        public abstract decimal BaseQuantity { get; set; }
        /// <inheritdoc />
        public abstract decimal QuoteQuantity { get; set; }
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

        decimal ICommonRecentTrade.CommonPrice => Price;
        decimal ICommonRecentTrade.CommonQuantity => BaseQuantity;
        DateTime ICommonRecentTrade.CommonTradeTime => TradeTime;
    }

    /// <summary>
    /// Recent trade with quote quantity
    /// </summary>
    public class BinanceRecentTradeQuote : BinanceRecentTrade
    {
        /// <inheritdoc />
        [JsonProperty("quoteQty")]
        public override decimal QuoteQuantity { get; set; }

        /// <inheritdoc />
        [JsonProperty("qty")]
        public override decimal BaseQuantity { get; set; }
    }

    /// <summary>
    /// Recent trade with base quantity
    /// </summary>
    public class BinanceRecentTradeBase : BinanceRecentTrade
    {
        /// <inheritdoc />
        [JsonProperty("qty")]
        public override decimal QuoteQuantity { get; set; }

        /// <inheritdoc />
        [JsonProperty("baseQty")]
        public override decimal BaseQuantity { get; set; }
    }
}
