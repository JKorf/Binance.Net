using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    public class BinanceExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        public string TimeZone { get; set; }
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// The rate limits used
        /// </summary>
        public BinanceRateLimit[] RateLimits { get; set; }
        /// <summary>
        /// All symbols supported
        /// </summary>
        public BinanceSymbol[] Symbols { get; set; }

        public object[] ExchangeFilters { get; set; }
    }
}
