﻿using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace Binance.Net.Objects.Futures.MarketData
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public class BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// The timezone the server uses
        /// </summary>
        public string TimeZone { get; set; } = string.Empty;
        /// <summary>
        /// The current server time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
        /// <summary>
        /// The rate limits used
        /// </summary>
        public IEnumerable<BinanceRateLimit> RateLimits { get; set; } = Array.Empty<BinanceRateLimit>();
        /// <summary>
        /// Filters
        /// </summary>
        public IEnumerable<object> ExchangeFilters { get; set; } = Array.Empty<object>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    public class BinanceFuturesUsdtExchangeInfo: BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<BinanceFuturesUsdtSymbol> Symbols { get; set; } = Array.Empty<BinanceFuturesUsdtSymbol>();

        /// <summary>
        /// All assets
        /// </summary>
        public IEnumerable<BinanceFuturesUsdtAsset> Assets { get; set; } = Array.Empty<BinanceFuturesUsdtAsset>();
    }

    /// <summary>
    /// Exchange info
    /// </summary>
    public class BinanceFuturesCoinExchangeInfo : BinanceFuturesExchangeInfo
    {
        /// <summary>
        /// All symbols supported
        /// </summary>
        public IEnumerable<BinanceFuturesCoinSymbol> Symbols { get; set; } = Array.Empty<BinanceFuturesCoinSymbol>();
    }
}
