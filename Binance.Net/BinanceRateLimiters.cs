using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net
{
    /// <summary>
    /// Rate limiters for the Binance API
    /// </summary>
    public static class BinanceRateLimiters
    {
        /// <summary>
        /// Spot API ratelimiter
        /// </summary>
        public static IRateLimitGate? SpotApi { get; } = new RateLimitGate()
                                                                    .AddGuard(new PartialEndpointTotalLimitGuard("/api/", 6000, TimeSpan.FromMinutes(1)))
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/sapi/", 180000, TimeSpan.FromMinutes(1))) // UID limit
                                                                    .AddGuard(new PartialEndpointIndividualLimitGuard("/sapi/", 12000, TimeSpan.FromMinutes(1))) // IP limit
                                                                    .AddGuard(new EndpointLimitGuard("/sapi/", 100, TimeSpan.FromSeconds(10), HttpMethod.Post))
                                                                    .WithLimitBehaviour(RateLimitingBehaviour.Wait)
                                                                    .WithWindowType(RateLimitWindowType.Fixed);
    }
}
