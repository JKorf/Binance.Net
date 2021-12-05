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

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc cref="IBinanceClientGeneralApi" />
    public class BinanceClientGeneralApi : RestApiClient, IBinanceClientGeneralApi
    {
        #region fields 
        private readonly BinanceClient _baseClient;
        internal readonly BinanceClientOptions Options;
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

        internal BinanceClientGeneralApi(BinanceClient baseClient, BinanceClientOptions options) : base(options, options.SpotApiOptions)
        {
            Options = options;
            _baseClient = baseClient;

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

        internal string GetTimestamp()
        {
            var offset = Options.AutoTimestamp ? BinanceClientSpotApi.CalculatedTimeOffset : 0;
            offset += Options.SpotApiOptions.TimestampOffset.TotalMilliseconds;
            return DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow.AddMilliseconds(offset))!.Value.ToString(CultureInfo.InvariantCulture);
        }

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result = result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<WebCallResult<DateTime>> CheckAutoTimestamp(CancellationToken ct)
        {
            if (Options.AutoTimestamp && (!BinanceClientSpotApi.TimeSynced || DateTime.UtcNow - BinanceClientSpotApi.LastTimeSync > Options.AutoTimestampRecalculationInterval))
                return await _baseClient.SpotApi.ExchangeData.GetServerTimeAsync(BinanceClientSpotApi.TimeSynced, ct).ConfigureAwait(false);

            return new WebCallResult<DateTime>(null, null, default, null);
        }

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1) where T : class
        {
            if (signed)
            {
                var timestampResult = await ((BinanceClientSpotApi)_baseClient.SpotApi).CheckAutoTimestamp(cancellationToken).ConfigureAwait(false);
                if (!timestampResult)
                    return new WebCallResult<T>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

                if (parameters == null)
                    parameters = new Dictionary<string, object>();

                parameters.Add("timestamp", GetTimestamp());
            }

            return await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight).ConfigureAwait(false);
        }

    }
}
