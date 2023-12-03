using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using Binance.Net.Interfaces.Clients.GeneralApi;
using Binance.Net.Clients.SpotApi;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Binance.Net.Objects.Options;
using System.Linq;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc cref="IBinanceRestClientGeneralApi" />
    public class BinanceRestClientGeneralApi : RestApiClient, IBinanceRestClientGeneralApi
    {
        #region fields 
        /// <inheritdoc />
        public new BinanceRestApiOptions ApiOptions => (BinanceRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new BinanceRestOptions ClientOptions => (BinanceRestOptions)base.ClientOptions;

        private readonly BinanceRestClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiBrokerage Brokerage { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiFutures Futures { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiSavings Savings { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiLoans CryptoLoans { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiMining Mining { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IBinanceRestClientGeneralApiStaking Staking { get; }
        #endregion

        #region constructor/destructor

        internal BinanceRestClientGeneralApi(ILogger logger, HttpClient? httpClient, BinanceRestClient baseClient, BinanceRestOptions options) 
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            Brokerage = new BinanceRestClientGeneralApiBrokerage(this);
            Futures = new BinanceRestClientGeneralApiFutures(this);
            Savings = new BinanceRestClientGeneralApiSavings(this);
            CryptoLoans = new BinanceRestClientGeneralApiLoans(this);
            Mining = new BinanceRestClientGeneralApiMining(this);
            SubAccount = new BinanceRestClientGeneralApiSubAccount(this);
            Staking = new BinanceRestClientGeneralApiStaking(this);

            requestBodyEmptyContent = "";
            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
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
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            var result = await SendRequestAsync<T>(uri, method, cancellationToken, parameters, signed, null, postPosition, arraySerialization, weight, ignoreRatelimit: ignoreRateLimit).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                BinanceRestClientSpotApi._timeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }


        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), BinanceRestClientSpotApi._timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => BinanceRestClientSpotApi._timeSyncState.TimeOffset;

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, string data)
        {
            var errorData = ValidateJson(data);
            if (!errorData)
                return new ServerError(data);

            if (!errorData.Data.HasValues)
                return new ServerError(errorData.Data.ToString());

            if (errorData.Data["msg"] == null && errorData.Data["code"] == null)
                return new ServerError(errorData.Data.ToString());

            if (errorData.Data["msg"] != null && errorData.Data["code"] == null)
                return new ServerError((string)errorData.Data["msg"]!);

            return new ServerError((int)errorData.Data["code"]!, (string)errorData.Data["msg"]!);
        }

        /// <inheritdoc />
        protected override Error ParseRateLimitResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, string data)
        {
            var error = GetRateLimitError(data);
            var retryAfterHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("Retry-After", StringComparison.InvariantCultureIgnoreCase));
            if (retryAfterHeader.Value?.Any() != true)
                return error;

            var value = retryAfterHeader.Value.First();
            if (!int.TryParse(value, out var seconds))
                return error;

            error.RetryAfter = DateTime.UtcNow.AddSeconds(seconds);
            return error;
        }

        private BinanceRateLimitError GetRateLimitError(string data)
        {
            var errorData = ValidateJson(data);
            if (!errorData)
                return new BinanceRateLimitError(data);

            if (!errorData.Data.HasValues)
                return new BinanceRateLimitError(errorData.Data.ToString());

            if (errorData.Data["msg"] == null && errorData.Data["code"] == null)
                return new BinanceRateLimitError(errorData.Data.ToString());

            if (errorData.Data["msg"] != null && errorData.Data["code"] == null)
                return new BinanceRateLimitError((string)errorData.Data["msg"]!);

            return new BinanceRateLimitError((int)errorData.Data["code"]!, (string)errorData.Data["msg"]!, null);
        }
    }
}
