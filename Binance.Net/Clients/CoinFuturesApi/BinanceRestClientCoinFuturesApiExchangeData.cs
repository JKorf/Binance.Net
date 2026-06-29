using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Diagnostics;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc />
    internal class BinanceRestClientCoinFuturesApiExchangeData : IBinanceRestClientCoinFuturesApiExchangeData
    {
        private readonly ILogger _logger;
        private static readonly RequestDefinitionCache _definitions = new();

        private readonly BinanceRestClientCoinFuturesApi _baseClient;

        internal BinanceRestClientCoinFuturesApiExchangeData(ILogger logger, BinanceRestClientCoinFuturesApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<HttpResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/ping", BinanceExchange.RateLimiter.FuturesRest, 1);
            var result = await _baseClient.SendAsync<object>(request, null, ct).ConfigureAwait(false);
            sw.Stop();
            return result.Success ? HttpResult.Ok(result, sw.ElapsedMilliseconds) : HttpResult.Fail<long>(result);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/time", BinanceExchange.RateLimiter.FuturesRest, 1);
            var result = await _baseClient.SendAsync<BinanceCheckTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.ServerTime);
        }

        #endregion

        #region Exchange Information

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/exchangeInfo", BinanceExchange.RateLimiter.FuturesRest, 1);
            var exchangeInfoResult = await _baseClient.SendAsync<BinanceFuturesCoinExchangeInfo>(request, null, ct).ConfigureAwait(false);
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/depth", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesOrderBook>(request, parameters, ct, requestWeight).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/aggTrades", BinanceExchange.RateLimiter.FuturesRest, 20);
            return await _baseClient.SendAsync<BinanceAggregatedTrade[]>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/fundingRate", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinanceFuturesFundingRateHistory[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Info

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesFundingInfo[]>> GetFundingInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/fundingInfo", BinanceExchange.RateLimiter.FuturesRest, 0);
            return await _baseClient.SendAsync<BinanceFuturesFundingInfo[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Top Trader Long/Short Ratio (Accounts)

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesLongShortRatio[]>> GetTopLongShortAccountRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", symbolPair },
            };
            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "futures/data/topLongShortAccountRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
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
                { "pair", symbolPair }
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "futures/data/topLongShortPositionRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
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
                { "pair", symbolPair }
            };

            parameters.Add("period", period);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "futures/data/globalLongShortAccountRatio", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/markPriceKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            return await _baseClient.SendAsync<BinanceMarkIndexKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Recent Trades

        /// <inheritdoc />
        public async Task<HttpResult<IBinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/trades", BinanceExchange.RateLimiter.FuturesRest, 5);
            var result = await _baseClient.SendAsync<BinanceRecentTradeBase[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceRecentTrade[]>(result);

            return HttpResult.Ok<IBinanceRecentTrade[]>(result, result.Data);
        }

        #endregion

        #region Trade History

        /// <inheritdoc />
        public async Task<HttpResult<IBinanceRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/historicalTrades", BinanceExchange.RateLimiter.FuturesRest, 20);
            var result = await _baseClient.SendAsync<BinanceRecentTradeBase[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinanceRecentTrade[]>(result);

            return HttpResult.Ok<IBinanceRecentTrade[]>(result, result.Data);
        }

        #endregion

        #region Mark Price

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinMarkPrice[]>> GetMarkPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/premiumIndex", BinanceExchange.RateLimiter.FuturesRest, 10);
            return await _baseClient.SendAsync<BinanceFuturesCoinMarkPrice[]>(request, parameters, ct).ConfigureAwait(false);

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
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/klines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesCoinKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/premiumIndexKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            return await _baseClient.SendAsync<BinanceMarkIndexKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Continuous contract Kline Data

        /// <inheritdoc />
        public async Task<HttpResult<IBinanceKline[]>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair }
            };
            parameters.Add("interval", interval);
            parameters.Add("contractType", contractType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/continuousKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesCoinKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);

            if (!result.Success)
                return HttpResult.Fail<IBinanceKline[]>(result);

            return HttpResult.Ok<IBinanceKline[]>(result, result.Data);
        }

        #endregion

        #region Index price Kline Data

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarkIndexKline[]>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair }
            };
            parameters.Add("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/indexPriceKlines", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            return await _baseClient.SendAsync<BinanceMarkIndexKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region 24h statistics
        /// <inheritdoc />
        public async Task<HttpResult<IBinance24HPrice[]>> GetTickersAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            var requestWeight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/ticker/24hr", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            var result = await _baseClient.SendAsync<BinanceFuturesCoin24HPrice[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<IBinance24HPrice[]>(result);

            return HttpResult.Ok<IBinance24HPrice[]>(result, result.Data);
        }
        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesBookPrice[]>> GetBookPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            var requestWeight = symbol == null ? 5 : 2;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/ticker/bookTicker", BinanceExchange.RateLimiter.FuturesRest, requestWeight);
            return await _baseClient.SendAsync<BinanceFuturesBookPrice[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/openInterest", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinanceFuturesCoinOpenInterest>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinOpenInterestHistory[]>> GetOpenInterestHistoryAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair }
            };

            parameters.Add("period", period);
            parameters.Add("contractType", contractType);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "futures/data/openInterestHist", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesCoinOpenInterestHistory[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinBuySellVolumeRatio[]>> GetTakerBuySellVolumeRatioAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair }
            };

            parameters.Add("period", period);
            parameters.Add("contractType", contractType);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "futures/data/takerBuySellVol", BinanceExchange.RateLimiter.EndpointLimit, 1, false,
                limitGuard: new SingleLimitGuard(1000, TimeSpan.FromMinutes(5), RateLimitWindowType.Sliding));
            return await _baseClient.SendAsync<BinanceFuturesCoinBuySellVolumeRatio[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Basis

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesBasis[]>> GetBasisAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings) {
                { "pair", pair }
            };

            parameters.Add("period", period);
            parameters.Add("contractType", contractType);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "futures/data/basis", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendAsync<BinanceFuturesBasis[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Price
        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinPrice[]>> GetPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            var weight = symbol == null ? 2 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/ticker/price", BinanceExchange.RateLimiter.FuturesRest, weight);
            return await _baseClient.SendAsync<BinanceFuturesCoinPrice[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }
        #endregion

        #region Get Index Price Constituents

        /// <inheritdoc />
        public async Task<HttpResult<BinanceConstituents>> GetIndexPriceConstituentsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/dapi/v1/constituents", BinanceExchange.RateLimiter.FuturesRest, 2, false);
            var result = await _baseClient.SendAsync<BinanceConstituents>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
