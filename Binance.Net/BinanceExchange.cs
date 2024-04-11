using CryptoExchange.Net.RateLimiting;
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
                SpotApi_Ip.RateLimitTriggered += value;
                SpotApi_Uid.RateLimitTriggered += value;
                SpotApi_Socket.RateLimitTriggered += value;
                UsdFuturesApi_Rest.RateLimitTriggered += value;
                UsdFuturesApi_Socket.RateLimitTriggered += value;
            }
            remove
            {
                SpotApi_Ip.RateLimitTriggered -= value;
                SpotApi_Uid.RateLimitTriggered -= value;
                SpotApi_Socket.RateLimitTriggered -= value;
                UsdFuturesApi_Rest.RateLimitTriggered -= value;
                UsdFuturesApi_Socket.RateLimitTriggered -= value;
            }
        }


        /// <summary>
        /// Ratelimiter for Spot endpoints with a IP rate limit
        /// </summary>
        public IRateLimitGate SpotApi_Ip { get; } = new RateLimitGate("Rest Spot IP")
                                                                    .AddGuard(new PartialEndpointTotalLimitGuard("api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("sapi/", 12000, TimeSpan.FromMinutes(1)))
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for Spot endpoints with a Uid rate limit
        /// </summary>
        public IRateLimitGate SpotApi_Uid { get; } = new RateLimitGate("Rest Spot Uid")
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("sapi/", 180000, TimeSpan.FromMinutes(1)))
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for Spot websocket connections
        /// </summary>
        public IRateLimitGate SpotApi_Socket { get; } = new RateLimitGate("Socket Spot")
                                                                    .AddGuard(new HostLimitGuard("stream.binance.com", 5, TimeSpan.FromSeconds(1))) // 5 requests per second
                                                                    .AddGuard(new ConnectionLimitGuard("stream.binance.com", 300, TimeSpan.FromMinutes(5))) // 300 connection per 5 minutes
                                                                    .AddGuard(new HostLimitGuard("ws-api.binance.com", 6000, TimeSpan.FromMinutes(1), RateLimitItemType.Request | RateLimitItemType.Connection)) // 6000 request weight per minutes, connections count towards this rate
                                                                    .AddGuard(new ConnectionLimitGuard("ws-api.binance.com", 300, TimeSpan.FromMinutes(5))) // 300 connections per 5 minutes
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for USD futures endpoints
        /// </summary>
        public IRateLimitGate UsdFuturesApi_Rest { get; } = new RateLimitGate("Rest Usd Futures")
                                                                    .AddGuard(new TotalLimitGuard(2400, TimeSpan.FromMinutes(1)))
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for USD futures websocket connections
        /// </summary>
        public IRateLimitGate UsdFuturesApi_Socket { get; } = new RateLimitGate("Socket Usd Futures")
                                                                    .AddGuard(new HostLimitGuard("fstream.binance.com", 10, TimeSpan.FromSeconds(1))) // 10 requests per second
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for Coin futures endpoints
        /// </summary>
        public IRateLimitGate CoinFuturesApi_Rest { get; } = new RateLimitGate("Rest Coin Futures")
                                                                    .AddGuard(new TotalLimitGuard(2400, TimeSpan.FromMinutes(1)))
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for Coin futures websocket connections
        /// </summary>
        public IRateLimitGate CoinFuturesApi_Socket { get; } = new RateLimitGate("Socket Coin Futures")
                                                                    .AddGuard(new HostLimitGuard("dstream.binance.com", 10, TimeSpan.FromSeconds(1))) // 10 requests per second
                                                                    .WithWindowType(RateLimitWindowType.Fixed);
    }
}
