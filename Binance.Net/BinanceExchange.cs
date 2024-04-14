using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;

namespace Binance.Net
{
    /// <summary>
    /// Binance exchange information and helpers
    /// </summary>
    public static class BinanceExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Binance";

        /// <summary>
        /// Rate limiter for the Binance API
        /// </summary>
        public static IAccessLimiter RateLimiter => RateLimiters;

        /// <summary>   
        /// Format 2 assets to form a symbol accepted by the API
        /// </summary>
        /// <param name="baseAsset">The base asset</param>
        /// <param name="quoteAsset">The quote asset</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset) => (baseAsset + quoteAsset).ToUpperInvariant();


        internal static BinanceRateLimiters RateLimiters { get; } = new BinanceRateLimiters();
    }

    /// <summary>
    /// Rate limiters configured for the Binance API
    /// </summary>
    internal class BinanceRateLimiters : IAccessLimiter
    {
        public event Action<RateLimitEvent> RateLimitTriggered
        {
            add
            {
                SpotRestIp.RateLimitTriggered += value;
                SpotRestUid.RateLimitTriggered += value;
                SpotSocket.RateLimitTriggered += value;
                FuturesRest.RateLimitTriggered += value;
                FuturesSocket.RateLimitTriggered += value;
            }
            remove
            {
                SpotRestIp.RateLimitTriggered -= value;
                SpotRestUid.RateLimitTriggered -= value;
                SpotSocket.RateLimitTriggered -= value;
                FuturesRest.RateLimitTriggered -= value;
                FuturesSocket.RateLimitTriggered -= value;
            }
        }


        public IRateLimitGate SpotRestIp { get; } = new RateLimitGate("Spot Rest")
            .AddGuard(new RateLimitGuard((def, host, key) => host, new PathStartFilter("/api/"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // IP limit of 6000 request weight per minute to /api endpoints
            .AddGuard(new RateLimitGuard((def, host, key) => def.Path + def.Method, new PathStartFilter("/sapi/"), 12000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 12000 request weight per minute to /sapi endpoints

        public IRateLimitGate SpotRestUid { get; } = new RateLimitGate("Spot Rest")
            .AddGuard(new RateLimitGuard((def, host, key) => host, new PathStartFilter("/api/"), 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // Uid limit of 6000 request weight per minute to /api endpoints
            .AddGuard(new RateLimitGuard((def, host, key) => key?.GetString() + def.Path + def.Method, new PathStartFilter("/sapi/"), 12000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // Uid limit of 180000 request weight per minute to /sapi endpoints

        public IRateLimitGate SpotSocket { get; } = new RateLimitGate("Spot Socket")
            .AddGuard(new RateLimitGuard((def, host, key) => host, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Connection) }, 300, TimeSpan.FromMinutes(5), RateLimitWindowType.Fixed)) // 300 connections per 5 minutes per host
            .AddGuard(new RateLimitGuard((def, host, key) => def.Path, new IGuardFilter[] { new HostFilter("wss://stream.binance.com"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 5, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 5 requests per second per path (connection)
            .AddGuard(new RateLimitGuard((def, host, key) => def.Path, new IGuardFilter[] { new HostFilter("wss://ws-api.binance.com") }, 6000, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // 6000 request weight per minute per path (connection)

        public IRateLimitGate FuturesRest { get; } = new RateLimitGate("Futures Rest")
            .AddGuard(new RateLimitGuard((def, host, key) => host, new IGuardFilter[] { new HostFilter("https://fapi.binance.com") }, 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)) // IP limit of 2400 request weight per minute to fapi.binance.com host
            .AddGuard(new RateLimitGuard((def, host, key) => host, new IGuardFilter[] { new HostFilter("https://dapi.binance.com") }, 2400, TimeSpan.FromMinutes(1), RateLimitWindowType.Fixed)); // IP limit of 2400 request weight per minute to dapi.binance.com host

        public IRateLimitGate FuturesSocket { get; } = new RateLimitGate("Futures Socket")
            .AddGuard(new RateLimitGuard((def, host, key) => def.Path, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Request) }, 10, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)); // 10 requests per second per path (connection)


        ///// <summary>
        ///// Ratelimiter for Spot endpoints with a IP rate limit
        ///// </summary>
        //public IRateLimitGate SpotRestIp { get; } = new RateLimitGate("Rest Spot IP")
        //                                                            .AddGuard(new PartialEndpointTotalLimitGuard("api/", 6000, TimeSpan.FromMinutes(1)), RateLimitWindowType.Fixed)
        //                                                            .AddGuard(new EndpointPartIndividualLimitGuard("sapi/", 12000, TimeSpan.FromMinutes(1)), RateLimitWindowType.Fixed)
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);

        ///// <summary>
        ///// Ratelimiter for Spot endpoints with a Uid rate limit
        ///// </summary>
        //public IRateLimitGate SpotRestUid { get; } = new RateLimitGate("Rest Spot Uid")
        //                                                            .AddGuard(new EndpointPartIndividualLimitGuard("api/", 6000, TimeSpan.FromMinutes(1)), RateLimitWindowType.Fixed)
        //                                                            .AddGuard(new EndpointPartIndividualLimitGuard("sapi/", 180000, TimeSpan.FromMinutes(1)), RateLimitWindowType.Fixed)
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);

        ///// <summary>
        ///// Ratelimiter for Spot websocket connections
        ///// </summary>
        //public IRateLimitGate SpotApi_Socket { get; } = new RateLimitGate("Socket Spot")
        //                                                            .AddGuard(new HostPerEndpointLimitGuard("wss://stream.binance.com", 5, TimeSpan.FromSeconds(1)), RateLimitWindowType.Fixed) // 5 requests per second
        //                                                            .AddGuard(new ConnectionLimitGuard("wss://stream.binance.com", 300, TimeSpan.FromMinutes(5)), RateLimitWindowType.Fixed) // 300 connection per 5 minutes
        //                                                            .AddGuard(new HostPerEndpointLimitGuard("wss://ws-api.binance.com", 6000, TimeSpan.FromMinutes(1), RateLimitItemType.Request | RateLimitItemType.Connection), RateLimitWindowType.Fixed) // 6000 request weight per minutes, connections count towards this rate
        //                                                            .AddGuard(new ConnectionLimitGuard("wss://ws-api.binance.com", 300, TimeSpan.FromMinutes(5)), RateLimitWindowType.Fixed) // 300 connections per 5 minutes
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);


        ///// <summary>
        ///// Ratelimiter for USD futures endpoints
        ///// </summary>
        //public IRateLimitGate FuturesRest { get; } = new RateLimitGate("Rest Usd Futures")
        //                                                            .AddGuard(new TotalLimitGuard(2400, TimeSpan.FromMinutes(1)), RateLimitWindowType.Fixed)
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);


        ///// <summary>
        ///// Ratelimiter for USD futures websocket connections
        ///// </summary>
        //public IRateLimitGate UsdFuturesApi_Socket { get; } = new RateLimitGate("Socket Usd Futures")
        //                                                            .AddGuard(new HostLimitGuard("wss://fstream.binance.com", 10, TimeSpan.FromSeconds(1)), RateLimitWindowType.Fixed) // 10 requests per second
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);

        ///// <summary>
        ///// Ratelimiter for Coin futures endpoints
        ///// </summary>
        //public IRateLimitGate FuturesRest { get; } = new RateLimitGate("Rest Coin Futures")
        //                                                            .AddGuard(new TotalLimitGuard(2400, TimeSpan.FromMinutes(1)), RateLimitWindowType.Fixed)
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);

        ///// <summary>
        ///// Ratelimiter for Coin futures websocket connections
        ///// </summary>
        //public IRateLimitGate CoinFuturesApi_Socket { get; } = new RateLimitGate("Socket Coin Futures")
        //                                                            .AddGuard(new HostLimitGuard("wss://dstream.binance.com", 10, TimeSpan.FromSeconds(1)), RateLimitWindowType.Fixed) // 10 requests per second
        //                                                            .WithSingleEndpointRateLimitType(RateLimitWindowType.Fixed);
    }
}
