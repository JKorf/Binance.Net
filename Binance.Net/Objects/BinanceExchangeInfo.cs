using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects
{
    public class BinanceExchangeInfo
    {
        public string TimeZone { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
        public List<BinanceRateLimit> RateLimits { get; set; }
        public List<BinanceSymbol> Symbols { get; set; }
        public List<object> ExchangeFilters { get; set; }
    }
}
