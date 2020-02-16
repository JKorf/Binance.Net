using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Upcoming funding rate and time
    /// </summary>
    public class BinanceFundingRate
    {
         /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The upcoming funding rate
        /// </summary>
        public decimal FundingRate { get; set; }
        /// <summary>
        /// The time the next funding rate will be implemented
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// The time now
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
