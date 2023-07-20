using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc />
    public class BinanceRestClientCoinFuturesApiExchangeData : IBinanceRestClientCoinFuturesApiExchangeData
    {
        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";

        private const string orderBookEndpoint = "depth";
        private const string aggregatedTradesEndpoint = "aggTrades";
        private const string markPriceKlinesEndpoint = "markPriceKlines";

        private const string fundingRateHistoryEndpoint = "fundingRate";

        private const string topLongShortAccountRatioEndpoint = "topLongShortAccountRatio";
        private const string topLongShortPositionRatioEndpoint = "topLongShortPositionRatio";
        private const string globalLongShortAccountRatioEndpoint = "globalLongShortAccountRatio";

        private const string recentTradesEndpoint = "trades";
        private const string historicalTradesEndpoint = "historicalTrades";
        private const string markPriceEndpoint = "premiumIndex";
        private const string continuousContractKlineEndpoint = "continuousKlines";
        private const string indexPriceKlineEndpoint = "indexPriceKlines";
        private const string price24HEndpoint = "ticker/24hr";
        private const string allPricesEndpoint = "ticker/price";
        private const string bookPricesEndpoint = "ticker/bookTicker";
        private const string openInterestEndpoint = "openInterest";
        private const string openInterestHistoryEndpoint = "openInterestHist";
        private const string takerBuySellVolumeRatioEndpoint = "takerBuySellVol";
        private const string basisEndpoint = "basis";
        private const string klinesEndpoint = "klines";

        private const string api = "dapi";
        private const string publicVersion = "1";
        private const string tradingDataapi = "futures/data";

        private readonly ILogger _logger;

        private readonly BinanceRestClientCoinFuturesApi _baseClient;

        internal BinanceRestClientCoinFuturesApiExchangeData(ILogger logger, BinanceRestClientCoinFuturesApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(pingEndpoint, api, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default)
        {
            var url = _baseClient.GetUrl(checkTimeEndpoint, api, "1");
            var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(url, HttpMethod.Get, ct, ignoreRateLimit: true).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Exchange Information

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceFuturesCoinExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, "1"), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient._exchangeInfo = exchangeInfoResult.Data;
            _baseClient._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion

        #region Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20, 50, 100, 500, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 10 : limit <= 50 ? 2 : limit == 100 ? 5 : limit == 500 ? 10 : 20;
            var result = await _baseClient.SendRequestInternal<BinanceFuturesOrderBook>(_baseClient.GetUrl(orderBookEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: requestWeight).ConfigureAwait(false);
            if (result && string.IsNullOrEmpty(result.Data.Symbol))
                result.Data.Symbol = symbol;
            return result.As(result.Data);
        }

        #endregion

        #region Compressed/Aggregate Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceAggregatedTrade>>(_baseClient.GetUrl(aggregatedTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 20).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesFundingRateHistory>>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesFundingRateHistory>>(_baseClient.GetUrl(fundingRateHistoryEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Top Trader Long/Short Ratio (Accounts)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetTopLongShortAccountRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var url = _baseClient.GetUrl(topLongShortAccountRatioEndpoint, tradingDataapi);
            var parameters = new Dictionary<string, object> {
                { url.ToString().Contains("dapi") ? "pair": "symbol", symbolPair },
                { "period", JsonConvert.SerializeObject(period, StaticConverters.StaticPeriodIntervalConverter) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesLongShortRatio>>(url, HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Top Trader Long/Short Ratio (Positions)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetTopLongShortPositionRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var url = _baseClient.GetUrl(topLongShortPositionRatioEndpoint, tradingDataapi);
            var parameters = new Dictionary<string, object> {
                { url.ToString().Contains("dapi") ? "pair": "symbol", symbolPair },
                { "period", JsonConvert.SerializeObject(period, StaticConverters.StaticPeriodIntervalConverter) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesLongShortRatio>>(url, HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Global Long/Short Ratio (Accounts)

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesLongShortRatio>>> GetGlobalLongShortAccountRatioAsync(string symbolPair, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var url = _baseClient.GetUrl(globalLongShortAccountRatioEndpoint, tradingDataapi);
            var parameters = new Dictionary<string, object> {
                { url.ToString().Contains("dapi") ? "pair": "symbol", symbolPair },
                { "period", JsonConvert.SerializeObject(period, StaticConverters.StaticPeriodIntervalConverter) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesLongShortRatio>>(url, HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);

            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", StaticConverters.KlineIntervalConverter(ref interval) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarkIndexKline>>(_baseClient.GetUrl(markPriceKlinesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: requestWeight).ConfigureAwait(false);
        }

        #endregion
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeBase>>(_baseClient.GetUrl(recentTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 5).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeBase>>(_baseClient.GetUrl(historicalTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 20).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        #region Mark Price

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinMarkPrice>>> GetMarkPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinMarkPrice>>(_baseClient.GetUrl(markPriceEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 10).ConfigureAwait(false);

        }
        #endregion

        #region Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", StaticConverters.KlineIntervalConverter(ref interval) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinKline>>(_baseClient.GetUrl(klinesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: requestWeight).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region Continuous contract Kline Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "interval", StaticConverters.KlineIntervalConverter(ref interval) },
                { "contractType", JsonConvert.SerializeObject(contractType, StaticConverters.StaticContractTypeConverter) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinKline>>(_baseClient.GetUrl(continuousContractKlineEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: requestWeight).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region Index price Kline Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "interval", StaticConverters.KlineIntervalConverter(ref interval) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarkIndexKline>>(_baseClient.GetUrl(indexPriceKlineEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region 24h statistics
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetTickersAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            var result = await _baseClient
                .SendRequestInternal<IEnumerable<BinanceFuturesCoin24HPrice>>(_baseClient.GetUrl(price24HEndpoint, api, publicVersion),
                    HttpMethod.Get, ct, parameters, weight: symbol == null ? 40 : 1).ConfigureAwait(false);
            return result.As<IEnumerable<IBinance24HPrice>>(result.Success ? result.Data : null);
        }
        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesBookPrice>>> GetBookPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            return await _baseClient
                .SendRequestInternal<IEnumerable<BinanceFuturesBookPrice>>(
                    _baseClient.GetUrl(bookPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: symbol == null ? 2 : 1)
                .ConfigureAwait(false);
        }

        #endregion

        #region Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesCoinOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };

            return await _baseClient.SendRequestInternal<BinanceFuturesCoinOpenInterest>(_baseClient.GetUrl(openInterestEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinOpenInterestHistory>>> GetOpenInterestHistoryAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "period", JsonConvert.SerializeObject(period, StaticConverters.StaticPeriodIntervalConverter) },
                { "contractType", JsonConvert.SerializeObject(contractType, StaticConverters.StaticContractTypeConverter) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinOpenInterestHistory>>(_baseClient.GetUrl(openInterestHistoryEndpoint, tradingDataapi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "period", JsonConvert.SerializeObject(period, StaticConverters.StaticPeriodIntervalConverter) },
                { "contractType", JsonConvert.SerializeObject(contractType, StaticConverters.StaticContractTypeConverter) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinBuySellVolumeRatio>>(_baseClient.GetUrl(takerBuySellVolumeRatioEndpoint, tradingDataapi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesBasis>>> GetBasisAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "period", JsonConvert.SerializeObject(period, StaticConverters.StaticPeriodIntervalConverter) },
                { "contractType", JsonConvert.SerializeObject(contractType, StaticConverters.StaticContractTypeConverter) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesBasis>>(_baseClient.GetUrl(basisEndpoint, tradingDataapi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Price
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinPrice>>> GetPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinPrice>>(_baseClient.GetUrl(allPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: symbol == null ? 2 : 1).ConfigureAwait(false);
        }
        #endregion


    }
}
