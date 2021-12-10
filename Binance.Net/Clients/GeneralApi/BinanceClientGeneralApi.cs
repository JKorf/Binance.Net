using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Clients.SpotApi;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.Logging;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc cref="IBinanceClientGeneralApi" />
    public class BinanceClientGeneralApi : RestApiClient, IBinanceClientGeneralApi
    {
        #region fields 
        private readonly BinanceClient _baseClient;
        internal new readonly BinanceClientOptions Options;
        private Log _log;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceClientGeneralApiBrokerage Brokerage { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiFutures Futures { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiLending Lending { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiMining Mining { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiSubAccount SubAccount { get; }
        #endregion

        #region constructor/destructor

        internal BinanceClientGeneralApi(Log log, BinanceClient baseClient, BinanceClientOptions options) : base(options, options.SpotApiOptions)
        {
            Options = options;
            _baseClient = baseClient;
            _log = log;

            Brokerage = new BinanceClientGeneralApiBrokerage(this);
            Futures = new BinanceClientGeneralApiFutures(this);
            Lending = new BinanceClientGeneralApiLending(this);
            Mining = new BinanceClientGeneralApiMining(this);
            SubAccount = new BinanceClientGeneralApiSubAccount(this);
        }

        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result = result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1) where T : class
        {
            return await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight).ConfigureAwait(false);
        }


        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, Options.SpotApiOptions.AutoTimestamp, BinanceClientSpotApi.TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => BinanceClientSpotApi.TimeSyncState.TimeOffset;
    }
}
