using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// The current mark price and previous funding rate for a symbol
    /// </summary>
    public class BinanceMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The current mark price for the symbol
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        public decimal LastFundingRate { get; set; }
        /// <summary>
        /// The time the next funding rate will be implemented
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// The time now
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        
    }
}
