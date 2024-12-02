using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net
{
    /// <summary>
    /// Binance exchange information and configuration
    /// </summary>
    public static class BinanceExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Binance";

        /// <summary>
        /// Exchange name
        /// </summary>
        public static string DisplayName => "Binance";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/Binance.Net/master/Binance.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.binance.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://binance-docs.github.io/apidocs/spot/en/#change-log"
            };

        internal const string ClientOrderIdSpot = "x-VICEW9VV";
        internal const string ClientOrderIdFutures = "x-d63tKbx3";
        internal const string ClientOrderIdPrefixSpot = ClientOrderIdSpot + LibraryHelpers.ClientOrderIdSeperator;
        internal const string ClientOrderIdPrefixFutures = ClientOrderIdFutures + LibraryHelpers.ClientOrderIdSeperator;

        /// <summary>
        /// Format a base and quote asset to a Binance recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            if (tradingMode == TradingMode.Spot)
                return baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant();

            if (tradingMode.IsLinear())
                return baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant() + (deliverTime == null ? string.Empty : "_" + deliverTime.Value.ToString("yyMMdd"));

            return baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant() + (deliverTime == null ? "_PERP" : "_" + deliverTime.Value.ToString("yyMMdd"));
        }

        /// <summary>
        /// Rate limiter configuration for the Binance API
        /// </summary>
        public static BinanceRateLimiters RateLimiter { get; } = new BinanceRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the Binance API
    /// </summary>
    public class BinanceRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal BinanceRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            EndpointLimit = new RateLimitGate("Endpoint Limit");
            SpotRestIp = new RateLimitGate("Spot Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api/"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // IP limit of 6000 request weight per minute to /api endpoints
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new PathStartFilter("sapi/"), 12000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 12000 request weight per endpoint per minute to /sapi endpoints
            SpotRestUid = new RateLimitGate("Spot Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new PathStartFilter("api/"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // Uid limit of 6000 request weight per minute to /api endpoints
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new PathStartFilter("sapi/"), 180000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // Uid limit of 180000 request weight per minute to /sapi endpoints
            SpotSocket = new RateLimitGate("Spot Socket")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Connection) }, 300, TimeSpan.FromMinutes(5), RateLimitWindowType.Fixed)) // 300 connections per 5 minutes per host
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new HostFilter("wss://stream.binance.com"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 4, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding)) // 5 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new HostFilter("wss://ws-api.binance.com") }, 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed, connectionWeight: 2)); // 6000 request weight per minute in total
            FuturesRest = new RateLimitGate("Futures Rest")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new HostFilter("https://fapi.binance.com") }, 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // IP limit of 2400 request weight per minute to fapi.binance.com host
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new HostFilter("https://dapi.binance.com") }, 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 2400 request weight per minute to dapi.binance.com host
            FuturesSocket = new RateLimitGate("Futures Socket")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Request), new HostFilter("wss://dstream.binance.com") }, 10, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 10 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Request), new HostFilter("wss://fstream.binance.com") }, 10, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 10 requests per second per path (connection)
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new HostFilter("wss://ws-fapi.binance.com") }, 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed, connectionWeight: 5));

            EndpointLimit.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotRestIp.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotRestUid.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SpotSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            FuturesRest.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            FuturesSocket.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }

        internal IRateLimitGate EndpointLimit { get; private set; }

        internal IRateLimitGate SpotRestIp { get; private set; } 

        internal IRateLimitGate SpotRestUid { get; private set; } 

        internal IRateLimitGate SpotSocket { get; private set; } 

        internal IRateLimitGate FuturesRest { get; private set; } 

        internal IRateLimitGate FuturesSocket { get; private set; } 

    }
}
