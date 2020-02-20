using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Funding rate information for Futures trading
    /// </summary>
    public class BinanceFuturesFundingRateHistory
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The finding rate for the given symbol and time
        /// </summary>
        public decimal FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime FundingTime { get; set; }
    }
}
