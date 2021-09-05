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

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public override async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(FuturesClient.GetUrl(recentTradesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
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

        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Mark data</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesMarkPrice>>> GetMarkPricesAsync(string? symbol = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            if (symbol == null)
            {
                return await BaseClient
                    .SendRequestInternal<IEnumerable<BinanceFuturesMarkPrice>>(
                        FuturesClient.GetUrl(markPriceEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters)
                    .ConfigureAwait(false);
            }
            else
            {
                var result = await BaseClient
                    .SendRequestInternal<BinanceFuturesMarkPrice>(
                        FuturesClient.GetUrl(markPriceEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters)
                    .ConfigureAwait(false);
                return result.As<IEnumerable<BinanceFuturesMarkPrice>>(result.Success ? new[] { result.Data }: null);
            }
        }
        #endregion

        #region 24h statistics
        /// <summary>
        /// Get data regarding the last 24 hours change
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        public async Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetTickersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            if (symbol != null)
            {
                var result = await BaseClient
                    .SendRequestInternal<Binance24HPrice>(FuturesClient.GetUrl(price24HEndpoint, Api, publicVersion),
                        HttpMethod.Get, ct, parameters).ConfigureAwait(false);
                return result.As<IEnumerable<IBinance24HPrice>>(result.Success ? new[] { result.Data } : null);
            }
            else
            {
                var result = await BaseClient
                    .SendRequestInternal<IEnumerable<Binance24HPrice>>(FuturesClient.GetUrl(price24HEndpoint, Api, publicVersion),
                        HttpMethod.Get, ct, parameters).ConfigureAwait(false);
                return result.As<IEnumerable<IBinance24HPrice>>(result.Data);
            }
        }
        #endregion

        #region Kline/Candlestick Data

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
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

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);

            if (symbol == null)
            {
                return await BaseClient
                    .SendRequestInternal<IEnumerable<BinanceBookPrice>>(
                        FuturesClient.GetUrl(bookPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters)
                    .ConfigureAwait(false);
            }
            else
            {
                var result = await BaseClient.SendRequestInternal<BinanceBookPrice>(FuturesClient.GetUrl(bookPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
                return result.As<IEnumerable<BinanceBookPrice>>(result.Success ? new[] { result.Data } : null);
            }
        }

        #endregion

        #region Open Interest

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
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

        /// <summary>
        /// Gets Open Interest History
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
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

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
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

        /// <summary>
        /// Gets composite index info
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCompositeIndexInfo>>> GetCompositeIndexInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCompositeIndexInfo>>(FuturesClient.GetUrl(compositeIndexApi, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get price

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        public async Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await BaseClient.SendRequestInternal<BinancePrice>(FuturesClient.GetUrl(allPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default)
        {
            return await BaseClient.SendRequestInternal<IEnumerable<BinancePrice>>(FuturesClient.GetUrl(allPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }
        #endregion

        #region Continuous contract Kline Data

        /// <summary>
        /// Get candlestick data for the provided pair
        /// </summary>
        /// <param name="pair">The symbol to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
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
