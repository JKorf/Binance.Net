using System;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    public class Binance24HPrice : Binance24HPriceBase, IBinanceTick, ICommonTicker
    {
        /// <summary>
        /// The close price 24 hours ago
        /// </summary>
        [JsonProperty("prevClosePrice")]
        public decimal PrevDayClosePrice { get; set; }
        /// <summary>
        /// The best bid price in the order book
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// The size of the best bid price in the order book
        /// </summary>
        [JsonProperty("bidQty")]
        public decimal BidQuantity { get; set; }
        /// <summary>
        /// The best ask price in the order book
        /// </summary>
        public decimal AskPrice { get; set; }
        /// <summary>
        /// The size of the best ask price in the order book
        /// </summary>
        [JsonProperty("AskQty")]
        public decimal AskQuantity { get; set; }
        
        /// <inheritdoc />
        [JsonProperty("volume")]
        public override decimal BaseVolume { get; set; }
        /// <inheritdoc />
        [JsonProperty("quoteVolume")]
        public override decimal QuoteVolume { get; set; }

        string ICommonTicker.CommonSymbol => Symbol;
        decimal ICommonTicker.CommonHigh => HighPrice;
        decimal ICommonTicker.CommonLow => LowPrice;
        decimal ICommonTicker.CommonVolume => BaseVolume;
    }
}
