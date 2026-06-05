using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Diagnostics;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BinanceRestClientUsdFuturesApiExchangeData : IBinanceRestClientUsdFuturesApiExchangeData
    {
        private readonly ILogger _logger;
        private static readonly RequestDefinitionCache _definitions = new();

        private readonly BinanceRestClientUsdFuturesApi _baseClient;

        internal BinanceRestClientUsdFuturesApiExchangeData(ILogger logger, BinanceRestClientUsdFuturesApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<HttpResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ping", BinanceExchange.RateLimiter.FuturesRest, 1);
            var result = await _baseClient.SendAsync<object>(request, null, ct).ConfigureAwait(false);
            sw.Stop();
            return result.Success ? HttpResult.Ok(result, sw.ElapsedMilliseconds) : HttpResult.Fail<long>(result);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/time", BinanceExchange.RateLimiter.FuturesRest, 1);
            var result = await _baseClient.SendAsync<BinanceCheckTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.ServerTime);
        }

        #endregion

        #region Exchange Information

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesUsdtExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/exchangeInfo", BinanceExchange.RateLimiter.FuturesRest, 1);
            var exchangeInfoResult = await _baseClient.SendAsync<BinanceFuturesUsdtExchangeInfo>(request, null, ct).ConfigureAwait(false);
            if (!exchangeInfoResult.Success)
                return exchangeInfoResult;

            _baseClient._exchangeInfo = exchangeInfoResult.Data;
            _baseClient._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion

        #region Order Book

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20, 50, 100, 500, 1000);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 10 : limit <= 50 ? 2 : limit == 100 ? 5 : limit == 500 ? 10 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/depth", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesOrderBook>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (result.Success && string.IsNullOrEmpty(result.Data.Symbol))
                result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region RPI Order Book

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrderBook>> GetRpiOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", 1000);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/rpiDepth", BinanceExchange.RateLimiter.FuturesRest, 20);
            var result = await _baseClient.SendAsync<BinanceFuturesOrderBook>(request, parameters, ct).ConfigureAwait(false);
            if (result.Success && string.IsNullOrEmpty(result.Data.Symbol))
                result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region Compressed/Aggregate Trades List

        /// <inheritdoc />
        public async Task<HttpResult<BinanceAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/aggTrades", BinanceExchange.RateLimiter.FuturesRest, 20);
            return await _baseClient.SendAsync<BinanceAggregatedTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Info

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesFundingInfo[]>> GetFundingInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/fundingInfo", BinanceExchange.RateLimiter.FuturesRest, 0);
            return await _baseClient.SendAsync<BinanceFuturesFundingInfo[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesFundingRateHistory[]>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/fundingRate", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(500, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesFundingRateHistory[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Top Trader Long/Short Ratio (Accounts)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesLongShortRatio[]>> GetTopLongShortAccountRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbolPair },
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "futures/data/topLongShortAccountRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesLongShortRatio[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Top Trader Long/Short Ratio (Positions)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesLongShortRatio[]>> GetTopLongShortPositionRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbolPair },
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "futures/data/topLongShortPositionRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesLongShortRatio[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Global Long/Short Ratio (Accounts)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesLongShortRatio[]>> GetGlobalLongShortAccountRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbolPair },
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "futures/data/globalLongShortAccountRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesLongShortRatio[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarkIndexKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbol },
            };

            parameters.Add("interval", interval);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/markPriceKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            return await _baseClient.SendAsync<BinanceMarkIndexKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Get Recent Trades
        /// <inheritdoc />
        public async Task<HttpResult<IBinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/trades", BinanceExchange.RateLimiter.FuturesRest, 5);
            var result = await _baseClient.SendAsync<BinanceRecentTradeQuote[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceRecentTrade[]>(result);

            return HttpResult.Ok<IBinanceRecentTrade[]>(result, result.Data);
        }
        #endregion

        #region Get Trade History
        /// <inheritdoc />
        public async Task<HttpResult<IBinanceRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/historicalTrades", BinanceExchange.RateLimiter.FuturesRest, 20);
            var result = await _baseClient.SendAsync<BinanceRecentTradeQuote[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceRecentTrade[]>(result);

            return HttpResult.Ok<IBinanceRecentTrade[]>(result, result.Data);
        }
        #endregion

        #region Mark Price

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesMarkPrice>> GetMarkPriceAsync(string symbol,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/premiumIndex", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinanceFuturesMarkPrice>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesMarkPrice[]>> GetMarkPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/premiumIndex", BinanceExchange.RateLimiter.FuturesRest, 10);
            return await _baseClient.SendAsync<BinanceFuturesMarkPrice[]>(request, null, ct).ConfigureAwait(false);
        }
        #endregion

        #region 24h statistics
        /// <inheritdoc />
        public async Task<HttpResult<IBinance24HPrice>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/24hr", BinanceExchange.RateLimiter.FuturesRest, 1);
            var result = await _baseClient.SendAsync<Binance24HPrice>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinance24HPrice>(result);

            return HttpResult.Ok<IBinance24HPrice>(result, result.Data);
        }

        /// <inheritdoc />
        public async Task<HttpResult<IBinance24HPrice[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/24hr", BinanceExchange.RateLimiter.FuturesRest, 40);
            var result = await _baseClient.SendAsync<Binance24HPrice[]>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinance24HPrice[]>(result);

            return HttpResult.Ok<IBinance24HPrice[]>(result, result.Data);
        }
        #endregion

        #region Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<HttpResult<IBinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbol },
            };
            parameters.Add("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/klines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesUsdtKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceKline[]>(result);

            return HttpResult.Ok<IBinanceKline[]>(result, result.Data);
        }

        #endregion

        #region Kline/Premium Index

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarkIndexKline[]>> GetPremiumIndexKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbol },
            };
            parameters.Add("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/premiumIndexKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            return await _baseClient.SendAsync<BinanceMarkIndexKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<HttpResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/bookTicker", BinanceExchange.RateLimiter.FuturesRest, 2);
            return await _baseClient.SendAsync<BinanceBookPrice>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceBookPrice[]>> GetBookPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/bookTicker", BinanceExchange.RateLimiter.FuturesRest, 5);
            return await _baseClient.SendAsync<BinanceBookPrice[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/openInterest", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinanceFuturesOpenInterest>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOpenInterestHistory[]>> GetOpenInterestHistoryAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbol },
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "futures/data/openInterestHist", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesOpenInterestHistory[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesBuySellVolumeRatio[]>> GetTakerBuySellVolumeRatioAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "symbol", symbol },
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "futures/data/takerlongshortRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesBuySellVolumeRatio[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Composite index symbol information

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCompositeIndexInfo[]>> GetCompositeIndexInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);

            var weight = symbol == null ? 10 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/indexInfo", BinanceExchange.RateLimiter.FuturesRest, weight);
            return await _baseClient.SendAsync<BinanceFuturesCompositeIndexInfo[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Get price

        /// <inheritdoc />
        public async Task<HttpResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v2/ticker/price", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinancePrice>(request, parameters, ct, 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinancePrice[]>> GetPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v2/ticker/price", BinanceExchange.RateLimiter.FuturesRest, 2);
            return await _baseClient.SendAsync<BinancePrice[]>(request, null, ct, 2).ConfigureAwait(false);
        }
        #endregion

        #region Continuous contract Kline Data

        /// <inheritdoc />
        public async Task<HttpResult<IBinanceKline[]>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair },
            };
            parameters.Add("interval", interval);
            parameters.Add("contractType", contractType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/continuousKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesUsdtKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceKline[]>(result);

            return HttpResult.Ok<IBinanceKline[]>(result, result.Data);
        }

        #endregion

        #region Index Price Kline Data

        /// <inheritdoc />
        public async Task<HttpResult<IBinanceKline[]>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair },
            };
            parameters.Add("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/indexPriceKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesUsdtKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceKline[]>(result);

            return HttpResult.Ok<IBinanceKline[]>(result, result.Data);
        }

        #endregion

        #region Asset index

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesAssetIndex[]>> GetAssetIndexesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/assetIndex", BinanceExchange.RateLimiter.FuturesRest, 10);
            return await _baseClient.SendAsync<BinanceFuturesAssetIndex[]>(request, null, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesAssetIndex>> GetAssetIndexAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/assetIndex", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinanceFuturesAssetIndex>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Basis

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesBasis[]>> GetBasisAsync(string symbol, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "pair", symbol }
            };
            parameters.Add("contractType", contractType);
            parameters.Add("period", period);
            parameters.Add("limit", limit ?? 30);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "futures/data/basis", BinanceExchange.RateLimiter.FuturesRest, 0);
            return await _baseClient.SendAsync<BinanceFuturesBasis[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Symbols

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesConvertSymbol[]>> GetConvertSymbolsAsync(string? fromAsset = null, string? toAsset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("fromAsset", fromAsset);
            parameters.Add("toAsset", toAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/convert/exchangeInfo", BinanceExchange.RateLimiter.FuturesRest, 20, false);
            var result = await _baseClient.SendAsync<BinanceFuturesConvertSymbol[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Price Constituents

        /// <inheritdoc />
        public async Task<HttpResult<BinanceConstituents>> GetIndexPriceConstituentsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/constituents", BinanceExchange.RateLimiter.FuturesRest, 2, false);
            var result = await _baseClient.SendAsync<BinanceConstituents>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Insurance Fund Balances

        /// <inheritdoc />
        public async Task<HttpResult<BinanceInsuranceFundBalance>> GetInsuranceFundBalancesAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/insuranceBalance", BinanceExchange.RateLimiter.FuturesRest, 1, false);
            var result = await _baseClient.SendAsync<BinanceInsuranceFundBalance>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Insurance Fund Balances

        /// <inheritdoc />
        public async Task<HttpResult<BinanceInsuranceFundBalance[]>> GetInsuranceFundBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/insuranceBalance", BinanceExchange.RateLimiter.FuturesRest, 1, false);
            var result = await _baseClient.SendAsync<BinanceInsuranceFundBalance[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol ADL ratings

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSymbolAdlRate>> GetSymbolAdlRiskRatingAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/symbolAdlRisk", BinanceExchange.RateLimiter.FuturesRest, 1, false);
            var result = await _baseClient.SendAsync<BinanceSymbolAdlRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BinanceSymbolAdlRate[]>> GetSymbolAdlRiskRatingsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/symbolAdlRisk", BinanceExchange.RateLimiter.FuturesRest, 1, false);
            var result = await _baseClient.SendAsync<BinanceSymbolAdlRate[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Get Trading Schedule

        /// <inheritdoc />
        public async Task<HttpResult<BinanceTradingSchedule>> GetTradingScheduleAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/tradingSchedule", BinanceExchange.RateLimiter.FuturesRest, 5, false);
            var result = await _baseClient.SendAsync<BinanceTradingSchedule>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
