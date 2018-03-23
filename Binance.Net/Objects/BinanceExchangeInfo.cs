using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    public class BinanceExchangeInfo
    {
        public string TimeZone { get; set; }
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
        public BinanceRateLimit[] RateLimits { get; set; }
        public BinanceSymbol[] Symbols { get; set; }
        public object[] ExchangeFilters { get; set; }
    }
}
