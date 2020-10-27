using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Objects.Spot.MarketData
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public class BinanceExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        public string TimeZone { get; set; } = "";
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// The rate limits used
        /// </summary>
        public IEnumerable<BinanceRateLimit> RateLimits { get; set; } = new List<BinanceRateLimit>();
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<BinanceSymbol> Symbols { get; set; } = new List<BinanceSymbol>();
        /// <summary>
        /// Filters
        /// </summary>
        public IEnumerable<object> ExchangeFilters { get; set; } = new List<object>();
    }
}
