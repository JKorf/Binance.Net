using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.UnitTests.TestImplementations
{
    public class BinanceRestApiClient : RestApiClient
    {
        public BinanceRestApiClient(Log log, ClientOptions options, RestApiClientOptions apiOptions) : base(log, options, apiOptions)
        {
        }

        public override TimeSpan? GetTimeOffset() => null;
        public override TimeSyncInfo GetTimeSyncInfo() => null;
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => throw new NotImplementedException();
    }
}
