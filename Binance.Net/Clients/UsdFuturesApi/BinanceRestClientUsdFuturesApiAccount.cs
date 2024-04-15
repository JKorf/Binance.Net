using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    public class BinanceRestClientUsdFuturesApiAccount : IBinanceRestClientUsdFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly BinanceRestClientUsdFuturesApi _baseClient;

        internal BinanceRestClientUsdFuturesApiAccount(BinanceRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Change Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceResult>> ModifyPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "dualSidePosition", dualPositionSide.ToString().ToLower() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/positionSide/dual", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Current Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/positionSide/dual", BinanceExchange.RateLimiter.FuturesRest, 30, true);
            return await _baseClient.SendAsync<BinanceFuturesPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Change Initial Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesInitialLeverageChangeResult>> ChangeInitialLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default)
        {
            leverage.ValidateIntBetween(nameof(leverage), 1, 125);
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "leverage", leverage }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/leverage", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesInitialLeverageChangeResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Change Margin Type

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesChangeMarginTypeResult>> ChangeMarginTypeAsync(string symbol, FuturesMarginType marginType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "marginType", JsonConvert.SerializeObject(marginType, new FuturesMarginTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/marginType", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesChangeMarginTypeResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Modify Isolated Position Margin

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesPositionMarginResult>> ModifyPositionMarginAsync(string symbol, decimal quantity, FuturesMarginChangeDirectionType type, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
                { "type", JsonConvert.SerializeObject(type, new FuturesMarginChangeDirectionTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("positionSide", positionSide == null ? null : JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/positionMargin", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesPositionMarginResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Postion Margin Change History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesMarginChangeHistoryResult>>> GetMarginChangeHistoryAsync(string symbol, FuturesMarginChangeDirectionType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("type", type.HasValue ? JsonConvert.SerializeObject(type, new FuturesMarginChangeDirectionTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/positionMargin/history", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesMarginChangeHistoryResult>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Income History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesIncomeHistory>>> GetIncomeHistoryAsync(string? symbol = null, string? incomeType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("incomeType", incomeType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/income", BinanceExchange.RateLimiter.FuturesRest, 30, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesIncomeHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Notional and Leverage Brackets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesSymbolBracket>>> GetBracketsAsync(string? symbolOrPair = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbolOrPair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/leverageBracket", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesSymbolBracket>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Position ADL Quantile Estimations

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesQuantileEstimation>>> GetPositionAdlQuantileEstimationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            if (symbol == null)
            {
                var request1 = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/adlQuantile", BinanceExchange.RateLimiter.FuturesRest, 5, true);
                return await _baseClient.SendAsync<IEnumerable<BinanceFuturesQuantileEstimation>>(request1, parameters, ct).ConfigureAwait(false);
            }

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/adlQuantile", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            var result = await _baseClient.SendAsync<BinanceFuturesQuantileEstimation>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<BinanceFuturesQuantileEstimation>>(null);

            return result.As<IEnumerable<BinanceFuturesQuantileEstimation>>(new[] { result.Data });
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/listenKey", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            var result = await _baseClient.SendAsync<BinanceListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Keepalive User Data Stream

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "fapi/v1/listenKey", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Close User Data Stream

        /// <inheritdoc />
        public async Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/listenKey", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Account Information

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/account", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<BinanceFuturesAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Future Account Balance

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceUsdFuturesAccountBalance>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v2/account", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesAccountBalance>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Multi assets mode

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesMultiAssetMode>> GetMultiAssetsModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/multiAssetsMargin", BinanceExchange.RateLimiter.FuturesRest, 30, true);
            return await _baseClient.SendAsync<BinanceFuturesMultiAssetMode>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceResult>> SetMultiAssetsModeAsync(bool enabled, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "multiAssetsMargin", enabled.ToString() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/multiAssetsMargin", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Position Information

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePositionDetailsUsdt>>> GetPositionInformationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v2/positionRisk", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<BinancePositionDetailsUsdt>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Trading status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/apiTradingStatus", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesTradingStatus>(request, parameters, ct).ConfigureAwait(false);

        }
        #endregion

        #region Future Account User Commission Rate
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesAccountUserCommissionRate>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/commissionRate", BinanceExchange.RateLimiter.FuturesRest, 20, true);
            return await _baseClient.SendAsync<BinanceFuturesAccountUserCommissionRate>(request, parameters, ct).ConfigureAwait(false); 
        }
        #endregion

        #region Get download id for transaction history
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTransactionHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/income/asyn", BinanceExchange.RateLimiter.FuturesRest, 1500, true);
            return await _baseClient.SendAsync<BinanceFuturesDownloadIdInfo>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Download transaction history
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTransactionHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "downloadId", downloadId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/income/asyn/id", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesDownloadLink>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get download id for transaction history
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForOrderHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/order/asyn", BinanceExchange.RateLimiter.FuturesRest, 1500, true);
            return await _baseClient.SendAsync<BinanceFuturesDownloadIdInfo>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Download order history
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForOrderHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "downloadId", downloadId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/order/asyn/id", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesDownloadLink>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get download id for trade history
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesDownloadIdInfo>> GetDownloadIdForTradeHistoryAsync(DateTime startTime, DateTime endTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/trade/asyn", BinanceExchange.RateLimiter.FuturesRest, 1500, true);
            return await _baseClient.SendAsync<BinanceFuturesDownloadIdInfo>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Download trade history
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesDownloadLink>> GetDownloadLinkForTradeHistoryAsync(string downloadId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "downloadId", downloadId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/trade/asyn/id", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesDownloadLink>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Order Rate Limit

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceRateLimit>>> GetOrderRateLimitAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/rateLimit/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceRateLimit>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
