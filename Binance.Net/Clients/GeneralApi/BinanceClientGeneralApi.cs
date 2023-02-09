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
using Newtonsoft.Json.Linq;

namespace Binance.Net.Clients.GeneralApi
{
    /// <inheritdoc cref="IBinanceClientGeneralApi" />
    public class BinanceClientGeneralApi : RestApiClient, IBinanceClientGeneralApi
    {
        #region fields 
        internal new readonly BinanceClientOptions Options;
        private readonly BinanceClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceClientGeneralApiBrokerage Brokerage { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiFutures Futures { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiSavings Savings { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiCryptoLoans CryptoLoans { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiMining Mining { get; }
        /// <inheritdoc />
        public IBinanceClientGeneralApiSubAccount SubAccount { get; }
        #endregion

        #region constructor/destructor

        internal BinanceClientGeneralApi(Log log, BinanceClient baseClient, BinanceClientOptions options) : base(log, options, options.SpotApiOptions)
        {
            _baseClient = baseClient;
            Options = options;

            Brokerage = new BinanceClientGeneralApiBrokerage(this);
            Futures = new BinanceClientGeneralApiFutures(this);
            Savings = new BinanceClientGeneralApiSavings(this);
            CryptoLoans = new BinanceClientGeneralApiCryptoLoans(this);
            Mining = new BinanceClientGeneralApiMining(this);
            SubAccount = new BinanceClientGeneralApiSubAccount(this);

            requestBodyEmptyContent = "";
            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.MultipleValues;
        }

        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider((BinanceApiCredentials)credentials);

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
            var result = await SendRequestAsync<T>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight, ignoreRatelimit: ignoreRateLimit).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && Options.SpotApiOptions.AutoTimestamp)
            {
                _log.Write(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                BinanceClientSpotApi.TimeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }


        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, Options.SpotApiOptions.AutoTimestamp, Options.SpotApiOptions.TimestampRecalculationInterval, BinanceClientSpotApi.TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => BinanceClientSpotApi.TimeSyncState.TimeOffset;

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
                return new ServerError(error.ToString());

            if (error["msg"] == null && error["code"] == null)
                return new ServerError(error.ToString());

            if (error["msg"] != null && error["code"] == null)
                return new ServerError((string)error["msg"]!);

            return new ServerError((int)error["code"]!, (string)error["msg"]!);
        }
    }
}
