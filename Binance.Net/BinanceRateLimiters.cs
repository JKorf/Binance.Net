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
        /// Spot API ratelimiter
        /// </summary>
        public static IRateLimitGate? SpotApi_Ip { get; } = new RateLimitGate()
                                                                    .AddGuard(new PartialEndpointTotalLimitGuard("/api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/sapi/", 12000, TimeSpan.FromMinutes(1)))
                                                                    .WithLimitBehaviour(RateLimitingBehaviour.Wait)
                                                                    .WithWindowType(RateLimitWindowType.Fixed);

        /// <summary>
        /// Spot API ratelimiter
        /// </summary>
        public static IRateLimitGate? SpotApi_Uid { get; } = new RateLimitGate()
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/sapi/", 180000, TimeSpan.FromMinutes(1)))
                                                                    .WithLimitBehaviour(RateLimitingBehaviour.Wait)
                                                                    .WithWindowType(RateLimitWindowType.Fixed);
    }
}
