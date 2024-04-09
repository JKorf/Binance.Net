using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Guards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net
{
    /// <summary>
    /// Rate limiters for the Binance API
    /// </summary>
    public static class BinanceRateLimiting
    {
        /// <summary>
        /// Ratelimiter for Spot endpoints with a IP rate limit
        /// </summary>
        public static IRateLimitGate? SpotApi_Ip { get; } = new RateLimitGate()
                                                                    .AddGuard(new PartialEndpointTotalLimitGuard("/api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/sapi/", 12000, TimeSpan.FromMinutes(1)))
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for Spot endpoints with a Uid rate limit
        /// </summary>
        public static IRateLimitGate? SpotApi_Uid { get; } = new RateLimitGate()
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/sapi/", 180000, TimeSpan.FromMinutes(1)))
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Ratelimiter for Spot websocket connections
        /// </summary>
        public static IRateLimitGate? SpotApi_Socket { get; } = new RateLimitGate()
                                                                    .AddGuard(new HostLimitGuard("stream.binance.com", 5, TimeSpan.FromSeconds(1))) // 5 requests per second
                                                                    .AddGuard(new HostLimitGuard("stream.binance.com", 300, TimeSpan.FromMinutes(5), RateLimitItemType.Connection)) // 300 connection per 5 minutes
                                                                    .AddGuard(new HostLimitGuard("ws-api.binance.com", 6000, TimeSpan.FromMinutes(1), RateLimitItemType.Request | RateLimitItemType.Connection), 1) // 6000 request weight per minutes, connections count as 1 weight
                                                                    .AddGuard(new HostLimitGuard("ws-api.binance.com", 300, TimeSpan.FromMinutes(5), RateLimitItemType.Connection)) // 300 connections per 5 minutes
                                                                    .WithWindowType(RateLimitWindowType.Fixed);
    }
}
