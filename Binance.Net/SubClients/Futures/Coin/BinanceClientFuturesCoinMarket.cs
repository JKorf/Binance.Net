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
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures.MarketData;
using Binance.Net.Objects.Shared;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// COIN-M futures market endpoints
    /// </summary>
    public class BinanceClientFuturesCoinMarket: BinanceClientFuturesMarket, IBinanceClientFuturesCoinMarket
    {
        private const string recentTradesEndpoint = "trades";
        private const string historicalTradesEndpoint = "historicalTrades";
        private const string markPriceEndpoint = "premiumIndex";
        private const string continuousContractKlineEndpoint = "continuousKlines";
        private const string indexPriceKlineEndpoint = "indexPriceKlines";
        private const string markPriceKlineEndpoint = "markPriceKlines";
        private const string price24HEndpoint = "ticker/24hr";
        private const string allPricesEndpoint = "ticker/price";
        private const string bookPricesEndpoint = "ticker/bookTicker";
        private const string allForcedOrdersEndpoint = "allForceOrders";
        private const string openInterestEndpoint = "openInterest";
        private const string openInterestHistoryEndpoint = "openInterestHist";
        private const string takerBuySellVolumeRatioEndpoint = "takerBuySellVol";
        private const string basisEndpoint = "basis";
        private const string klinesEndpoint = "klines";
        private const string publicVersion = "1";
        private const string tradingDataApi = "futures/data";

        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        internal BinanceClientFuturesCoinMarket(BinanceClient baseClient, BinanceClientFutures futuresClient) : base(baseClient, futuresClient) { }

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public override WebCallResult<IEnumerable<IBinanceRecentTrade>> GetSymbolTrades(string symbol, int? limit = null,
            CancellationToken ct = default)
            => GetSymbolTradesAsync(symbol, limit, ct).Result;

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public override async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetSymbolTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeBase>>(FuturesClient.GetUrl(recentTradesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return new WebCallResult<IEnumerable<IBinanceRecentTrade>>(result.ResponseStatusCode, result.ResponseHeaders,
                result.Data, result.Error);
        }

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public override WebCallResult<IEnumerable<IBinanceRecentTrade>> GetHistoricalSymbolTrades(string symbol,
            int? limit = null, long? fromId = null,
            CancellationToken ct = default) => GetHistoricalSymbolTradesAsync(symbol, limit, fromId, ct).Result;

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public override async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetHistoricalSymbolTradesAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeBase>>(FuturesClient.GetUrl(historicalTradesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return new WebCallResult<IEnumerable<IBinanceRecentTrade>>(result.ResponseStatusCode, result.ResponseHeaders,
                result.Data, result.Error);
        }

        #region Mark Price

        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<IEnumerable<BinanceFuturesCoinMarkPrice>> GetMarkPrices(string? symbol = null, string? pair = null, CancellationToken ct = default) => GetMarkPricesAsync(symbol, pair, ct).Result;

        /// <summary>
        /// Get Mark Price and Funding Rate for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinMarkPrice>>> GetMarkPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinMarkPrice>>(FuturesClient.GetUrl(markPriceEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);

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
        public override WebCallResult<IEnumerable<IBinanceKline>> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetKlinesAsync(symbol, interval, startTime, endTime, limit, ct).Result;

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

            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinKline>>(FuturesClient.GetUrl(klinesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return new WebCallResult<IEnumerable<IBinanceKline>>(result.ResponseStatusCode, result.ResponseHeaders,
                result.Data, result.Error);
        }

        #endregion

        #region Continuous contract Kline Data

        /// <summary>
        /// Get candlestick data for the provided pair
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public WebCallResult<IEnumerable<IBinanceKline>> GetContinuousContractKlines(string pair, ContractType contractType, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetContinuousContractKlinesAsync(pair, contractType, interval, startTime, endTime, limit, ct).Result;

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

            var result = await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinKline>>(FuturesClient.GetUrl(continuousContractKlineEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return new WebCallResult<IEnumerable<IBinanceKline>>(result.ResponseStatusCode, result.ResponseHeaders,
                result.Data, result.Error);
        }

        #endregion

        #region Index price Kline Data

        /// <summary>
        /// Get candlestick data for the provided pair
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public WebCallResult<IEnumerable<BinanceMarkIndexKline>> GetIndexPriceKlines(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetIndexPriceKlinesAsync(pair, interval, startTime, endTime, limit, ct).Result;

        /// <summary>
        /// Get candlestick data for the provided pair
        /// </summary>
        /// <param name="pair">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetIndexPriceKlinesAsync(string pair, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceMarkIndexKline>>(FuturesClient.GetUrl(indexPriceKlineEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Mark price Kline Data

        /// <summary>
        /// Get candlestick data for the provided pair
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public WebCallResult<IEnumerable<BinanceMarkIndexKline>> GetMarkPriceKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetMarkPriceKlinesAsync(symbol, interval, startTime, endTime, limit, ct).Result;

        /// <summary>
        /// Get candlestick data for the provided pair
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarkIndexKline>>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceMarkIndexKline>>(FuturesClient.GetUrl(markPriceKlineEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region 24h statistics
        /// <summary>
        /// Get data regarding the last 24 hours change
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        public WebCallResult<IEnumerable<IBinance24HPrice>> Get24HPrices(string? symbol = null, string? pair = null, CancellationToken ct = default) => Get24HPricesAsync(symbol, pair, ct).Result;

        /// <summary>
        /// Get data regarding the last 24 hours change
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        public async Task<WebCallResult<IEnumerable<IBinance24HPrice>>> Get24HPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            var result = await BaseClient
                .SendRequestInternal<IEnumerable<BinanceFuturesCoin24HPrice>>(FuturesClient.GetUrl(price24HEndpoint, Api, publicVersion),
                    HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return new WebCallResult<IEnumerable<IBinance24HPrice>>(result.ResponseStatusCode, result.ResponseHeaders,
                result.Success ? result.Data : null, result.Error);
        }
        #endregion

        #region Symbol Order Book Ticker

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public WebCallResult<IEnumerable<BinanceFuturesBookPrice>> GetBookPrices(string? symbol = null, string? pair = null, CancellationToken ct = default) => GetBookPricesAsync(symbol, pair, ct).Result;

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesBookPrice>>> GetBookPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            return await BaseClient
                .SendRequestInternal<IEnumerable<BinanceFuturesBookPrice>>(
                    FuturesClient.GetUrl(bookPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters)
                .ConfigureAwait(false);
        }

        #endregion

        #region Get all Liquidation Orders

        /// <summary>
        /// Get all Liquidation Orders
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="startTime">Start time to get  liquidation orders history</param>
        /// <param name="endTime">End time to get liquidation orders history</param>
        /// <param name="limit">Max number of results. Default:100 Max:1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The all liquidation orders</returns>
        public WebCallResult<IEnumerable<BinanceFuturesLiquidation>> GetLiquidationOrders(string? symbol = null, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetLiquidationOrdersAsync(symbol, pair, startTime, endTime, limit, ct).Result;

        /// <summary>
        /// Get all Liquidation Orders
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="startTime">Start time to get  liquidation orders history</param>
        /// <param name="endTime">End time to get liquidation orders history</param>
        /// <param name="limit">Max number of results. Default:100 Max:1000</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The all liquidation orders</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesLiquidation>>> GetLiquidationOrdersAsync(string? symbol = null, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 0, 1000);
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesLiquidation>>(FuturesClient.GetUrl(allForcedOrdersEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Open Interest

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
        public WebCallResult<BinanceFuturesCoinOpenInterest> GetOpenInterest(string symbol, CancellationToken ct = default) => GetOpenInterestAsync(symbol, ct).Result;

        /// <summary>
        /// Get present open interest of a specific symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest info</returns>
        public async Task<WebCallResult<BinanceFuturesCoinOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };

            return await BaseClient.SendRequestInternal<BinanceFuturesCoinOpenInterest>(FuturesClient.GetUrl(openInterestEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion
        
        #region Open Interest History

        /// <summary>
        /// Gets Open Interest History
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="period">The period timespan</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
        public WebCallResult<IEnumerable<BinanceFuturesCoinOpenInterestHistory>> GetOpenInterestHistory(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetOpenInterestHistoryAsync(pair, contractType, period, limit, startTime, endTime, ct).Result;

        /// <summary>
        /// Gets Open Interest History
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get open interest history</param>
        /// <param name="endTime">End time to get open interest history</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open Interest History info</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinOpenInterestHistory>>> GetOpenInterestHistoryAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "period", JsonConvert.SerializeObject(period, new PeriodIntervalConverter(false)) },
                { "contractType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinOpenInterestHistory>>(FuturesClient.GetUrl(openInterestHistoryEndpoint, tradingDataApi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
        public WebCallResult<IEnumerable<BinanceFuturesCoinBuySellVolumeRatio>> GetTakerBuySellVolumeRatio(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetTakerBuySellVolumeRatioAsync(pair, contractType, period, limit, startTime, endTime, ct).Result;

        /// <summary>
        /// Gets Taker Buy/Sell Volume Ratio
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time to get taker buy/sell volume ratio</param>
        /// <param name="endTime">End time to get taker buy/sell volume ratio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Taker Buy/Sell Volume Ratio info</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinBuySellVolumeRatio>>> GetTakerBuySellVolumeRatioAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "period", JsonConvert.SerializeObject(period, new PeriodIntervalConverter(false)) },
                { "contractType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinBuySellVolumeRatio>>(FuturesClient.GetUrl(takerBuySellVolumeRatioEndpoint, tradingDataApi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Taker Buy/Sell Volume Ratio

        /// <summary>
        /// Gets basis
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Basis</returns>
        public WebCallResult<IEnumerable<BinanceFuturesBasis>> GetBasis(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetBasisAsync(pair, contractType, period, limit, startTime, endTime, ct).Result;

        /// <summary>
        /// Gets basis
        /// </summary>
        /// <param name="pair">The pair to get the data for</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="period">The period timespan</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="startTime">Start time</param>
        /// <param name="endTime">End time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Basis</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesBasis>>> GetBasisAsync(string pair, ContractType contractType, PeriodInterval period, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object> {
                { "pair", pair },
                { "period", JsonConvert.SerializeObject(period, new PeriodIntervalConverter(false)) },
                { "contractType", JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)) }
            };

            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesBasis>>(FuturesClient.GetUrl(basisEndpoint, tradingDataApi), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Price
        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="symbol">Retrieve for a symbol</param>
        /// <param name="pair">Retrieve prices for a specific pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        public WebCallResult<IEnumerable<BinanceFuturesCoinPrice>> GetPrices(string? symbol = null, string? pair = null, CancellationToken ct = default) => GetPricesAsync(symbol, pair, ct).Result;

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="symbol">Retrieve for a symbol</param>
        /// <param name="pair">Retrieve prices for a specific pair</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinPrice>>> GetPricesAsync(string? symbol = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesCoinPrice>>(FuturesClient.GetUrl(allPricesEndpoint, Api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }
        #endregion

    }
}
