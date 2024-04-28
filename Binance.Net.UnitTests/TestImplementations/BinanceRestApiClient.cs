using Binance.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;

namespace Binance.Net.UnitTests.TestImplementations
{
    public class BinanceRestApiClient : RestApiClient
    {
        public BinanceRestApiClient(ILogger logger, BinanceRestOptions options, BinanceRestApiOptions apiOptions) : base(logger, null, "https://test.com", options, apiOptions)
        {
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToUpperInvariant()}{quoteAsset.ToUpperInvariant()}";
        public override TimeSpan? GetTimeOffset() => null;
        public override TimeSyncInfo GetTimeSyncInfo() => null;
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => throw new NotImplementedException();
    }
}
