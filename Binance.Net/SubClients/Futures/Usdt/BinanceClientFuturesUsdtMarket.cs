using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Futures.Usdt
{
    /// <summary>
    /// USDT-M futures market endpoints
    /// </summary>
    public class BinanceClientFuturesUsdtMarket : BinanceClientFuturesMarket, IBinanceClientFuturesUsdtMarket
    {
        private const string recentTradesEndpoint = "trades";
        private const string historicalTradesEndpoint = "historicalTrades";
        private const string markPriceEndpoint = "premiumIndex";
        private const string price24HEndpoint = "ticker/24hr";
        private const string allPricesEndpoint = "ticker/price";
        private const string bookPricesEndpoint = "ticker/bookTicker";
        private const string openInterestEndpoint = "openInterest";
        private const string openInterestHistoryEndpoint = "openInterestHist";
        private const string takerBuySellVolumeRatioEndpoint = "takerlongshortRatio";
        private const string compositeIndexApi = "indexInfo";
        private const string klinesEndpoint = "klines";
        private const string continuousContractKlineEndpoint = "continuousKlines";

        private const string publicVersion = "1";
        private const string tradingDataApi = "futures/data";

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "fapi";

        internal BinanceClientFuturesUsdtMarket(BinanceClient baseClient, BinanceClientFutures futuresClient) : base(baseClient, futuresClient) { }

        /// <inheritdoc />
        public override async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(FuturesClient.GetUrl(recentTradesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        /// <inheritdoc />
        public override async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(FuturesClient.GetUrl(historicalTradesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        #region Mark Price

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesMarkPrice>> GetMarkPriceAsync(string symbol,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            return await BaseClient
                .SendRequestInternal<BinanceFuturesMarkPrice>(
                    FuturesClient.GetUrl(markPriceEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters)
                .ConfigureAwait(false);            
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesMarkPrice>>> GetMarkPricesAsync(CancellationToken ct = default)
        {
            return await BaseClient
                .SendRequestInternal<IEnumerable<BinanceFuturesMarkPrice>>(
                    FuturesClient.GetUrl(markPriceEndpoint, Api, publicVersion), HttpMethod.Get, ct)
                .ConfigureAwait(false);
        }
        #endregion

        #region 24h statistics
        /// <inheritdoc />
        public async Task<WebCallResult<IBinance24HPrice>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            var result = await BaseClient
                .SendRequestInternal<Binance24HPrice>(FuturesClient.GetUrl(price24HEndpoint, Api, publicVersion),
                    HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IBinance24HPrice>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetTickersAsync(CancellationToken ct = default)
        {
            var result = await BaseClient
                .SendRequestInternal<IEnumerable<Binance24HPrice>>(FuturesClient.GetUrl(price24HEndpoint, Api, publicVersion),
                    HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As<IEnumerable<IBinance24HPrice>>(result.Data);
        }
        #endregion

        #region Kline/Candlestick Data

        /// <inheritdoc />
        public override async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesUsdtKline>>(FuturesClient.GetUrl(klinesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            return await BaseClient.SendRequestInternal<BinanceBookPrice>(FuturesClient.GetUrl(bookPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);           
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default)
        {
            return await BaseClient
                .SendRequestInternal<IEnumerable<BinanceBookPrice>>(
                    FuturesClient.GetUrl(bookPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct)
                .ConfigureAwait(false);
        }

        #endregion

        #region Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };

            return await BaseClient.SendRequestInternal<BinanceFuturesOpenInterest>(FuturesClient.GetUrl(openInterestEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOpenInterestHistory>>> GetOpenInterestHistoryAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new PeriodIntervalConverter(false)) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesOpenInterestHistory>>(FuturesClient.GetUrl(openInterestHistoryEndpoint, tradingDataApi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string symbol, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new PeriodIntervalConverter(false)) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesBuySellVolumeRatio>>(FuturesClient.GetUrl(takerBuySellVolumeRatioEndpoint, tradingDataApi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Composite index symbol information

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCompositeIndexInfo>>> GetCompositeIndexInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCompositeIndexInfo>>(FuturesClient.GetUrl(compositeIndexApi, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get price

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await BaseClient.SendRequestInternal<BinancePrice>(FuturesClient.GetUrl(allPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default)
        {
            return await BaseClient.SendRequestInternal<IEnumerable<BinancePrice>>(FuturesClient.GetUrl(allPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }
        #endregion

        #region Continuous contract Kline Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetContinuousContractKlinesAsync(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) },
                { "contractType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesUsdtKline>>(FuturesClient.GetUrl(continuousContractKlineEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion
    }
}
