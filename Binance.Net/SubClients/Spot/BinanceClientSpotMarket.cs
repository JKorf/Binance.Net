using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Spot
{
    /// <summary>
    /// Spot market endpoints
    /// </summary>
    public class BinanceClientSpotMarket : IBinanceClientSpotMarket
    {
        private const string orderBookEndpoint = "depth";
        private const string aggregatedTradesEndpoint = "aggTrades";
        private const string recentTradesEndpoint = "trades";
        private const string historicalTradesEndpoint = "historicalTrades";
        private const string klinesEndpoint = "klines";
        private const string price24HEndpoint = "ticker/24hr";
        private const string allPricesEndpoint = "ticker/price";
        private const string bookPricesEndpoint = "ticker/bookTicker";
        private const string averagePriceEndpoint = "avgPrice";
        private const string tradeFeeEndpoint = "tradeFee.html";

        private const string api = "api";
        private const string publicVersion = "3";

        private readonly BinanceClient _baseClient;

        internal BinanceClientSpotMarket(BinanceClient baseClient)
        {
            _baseClient = baseClient;
        }

        #region Order Book

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        public WebCallResult<BinanceOrderBook> GetOrderBook(string symbol, int? limit = null, CancellationToken ct = default) => GetOrderBookAsync(symbol, limit, ct).Result;

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The order book for the symbol</returns>
        public async Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20, 50, 100, 500, 1000, 5000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await _baseClient.SendRequestInternal<BinanceOrderBook>(_baseClient.GetUrl(false, orderBookEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            if (result)
                result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region Recent Trades List

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public WebCallResult<IEnumerable<BinanceRecentTrade>> GetSymbolTrades(string symbol, int? limit = null, CancellationToken ct = default) => GetSymbolTradesAsync(symbol, limit, ct).Result;

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public async Task<WebCallResult<IEnumerable<BinanceRecentTrade>>> GetSymbolTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTrade>>(_baseClient.GetUrl(false, recentTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Old Trade Lookup

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public WebCallResult<IEnumerable<BinanceRecentTrade>> GetHistoricalSymbolTrades(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default) => GetHistoricalSymbolTradesAsync(symbol, limit, fromId, ct).Result;

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of recent trades</returns>
        public async Task<WebCallResult<IEnumerable<BinanceRecentTrade>>> GetHistoricalSymbolTradesAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTrade>>(_baseClient.GetUrl(false, historicalTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Compressed/Aggregate Trades List

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        public WebCallResult<IEnumerable<BinanceAggregatedTrade>> GetAggregatedTrades(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetAggregatedTradesAsync(symbol, fromId, startTime, endTime, limit, ct).Result;

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        public async Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradesAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceAggregatedTrade>>(_baseClient.GetUrl(false, aggregatedTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
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
        public WebCallResult<IEnumerable<BinanceKline>> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default) => GetKlinesAsync(symbol, interval, startTime, endTime, limit, ct).Result;

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
        public async Task<WebCallResult<IEnumerable<BinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? BinanceClient.ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? BinanceClient.ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceKline>>(_baseClient.GetUrl(false, klinesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Current Average Price

        /// <summary>
        /// Gets current average price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public WebCallResult<BinanceAveragePrice> GetCurrentAvgPrice(string symbol, CancellationToken ct = default) => GetCurrentAvgPriceAsync(symbol, ct).Result;

        /// <summary>
        /// Gets current average price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<BinanceAveragePrice>(_baseClient.GetUrl(false, averagePriceEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region 24hr Ticker Price Change Statistics

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        public WebCallResult<Binance24HPrice> Get24HPrice(string symbol, CancellationToken ct = default) => Get24HPriceAsync(symbol, ct).Result;

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Data over the last 24 hours</returns>
        public async Task<WebCallResult<Binance24HPrice>> Get24HPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<Binance24HPrice>(_baseClient.GetUrl(false, price24HEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        public WebCallResult<IEnumerable<Binance24HPrice>> Get24HPrices(CancellationToken ct = default) => Get24HPricesAsync(ct).Result;

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of data over the last 24 hours</returns>
        public async Task<WebCallResult<IEnumerable<Binance24HPrice>>> Get24HPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl(false, price24HEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Price Ticker

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        public WebCallResult<BinancePrice> GetPrice(string symbol, CancellationToken ct = default) => GetPriceAsync(symbol, ct).Result;

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        public async Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await _baseClient.SendRequestInternal<BinancePrice>(_baseClient.GetUrl(false, allPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        public WebCallResult<IEnumerable<BinancePrice>> GetAllPrices(CancellationToken ct = default) => GetAllPricesAsync(ct).Result;

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of prices</returns>
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetAllPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinancePrice>>(_baseClient.GetUrl(false, allPricesEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Order Book Ticker

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public WebCallResult<BinanceBookPrice> GetBookPrice(string symbol, CancellationToken ct = default) => GetBookPriceAsync(symbol, ct).Result;

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <param name="symbol">Symbol to get book price for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public async Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<BinanceBookPrice>(_baseClient.GetUrl(false, bookPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public WebCallResult<IEnumerable<BinanceBookPrice>> GetAllBookPrices(CancellationToken ct = default) => GetAllBookPricesAsync(ct).Result;

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetAllBookPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBookPrice>>(_baseClient.GetUrl(false, bookPricesEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region GetTradeFee
        /// <summary>
        /// Gets the withdrawal fee for an symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        public WebCallResult<IEnumerable<BinanceTradeFee>> GetTradeFee(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default) => GetTradeFeeAsync(symbol, receiveWindow, ct).Result;

        /// <summary>
        /// Gets the trade fee for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        public async Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol?.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceTradeFee>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceTradeFeeWrapper>(_baseClient.GetUrl(false, tradeFeeEndpoint, "wapi", "3"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            if (!result)
                return new WebCallResult<IEnumerable<BinanceTradeFee>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);

            return !result.Data.Success ? new WebCallResult<IEnumerable<BinanceTradeFee>>(result.ResponseStatusCode, result.ResponseHeaders, null, _baseClient.ParseErrorResponseInternal(result.Data.Message)) : new WebCallResult<IEnumerable<BinanceTradeFee>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        #endregion
    }
}
