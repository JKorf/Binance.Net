using Newtonsoft.Json;
using CryptoExchange.Net.Converters;
using System;
using Binance.Net.Interfaces;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Mark Price and Funding Rate
    /// </summary>
    public class BinanceFuturesMarkPrice : IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// The current market price
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        public decimal FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime NextFundingTime { get; set; }
    }
}
