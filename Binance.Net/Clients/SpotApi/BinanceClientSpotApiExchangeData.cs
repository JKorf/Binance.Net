using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.BSwap;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.Staking;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceClientSpotApiExchangeData : IBinanceClientSpotApiExchangeData
    {
        private const string orderBookEndpoint = "depth";
        private const string aggregatedTradesEndpoint = "aggTrades";
        private const string recentTradesEndpoint = "trades";
        private const string historicalTradesEndpoint = "historicalTrades";
        private const string uiKlinesEndpoint = "uiKlines";
        private const string klinesEndpoint = "klines";
        private const string price24HEndpoint = "ticker/24hr";
        private const string rollingWindowPriceEndpoint = "ticker";
        private const string allPricesEndpoint = "ticker/price";
        private const string bookPricesEndpoint = "ticker/bookTicker";
        private const string averagePriceEndpoint = "avgPrice";
        private const string tradeFeeEndpoint = "asset/tradeFee";

        private const string pingEndpoint = "ping";
        private const string checkTimeEndpoint = "time";
        private const string exchangeInfoEndpoint = "exchangeInfo";
        private const string systemStatusEndpoint = "system/status";
        private const string assetDetailsEndpoint = "asset/assetDetail";

        // Margin
        private const string marginAssetEndpoint = "margin/asset";
        private const string marginAssetsEndpoint = "margin/allAssets";
        private const string marginPairEndpoint = "margin/pair";
        private const string marginPairsEndpoint = "margin/allPairs";
        private const string marginPriceIndexEndpoint = "margin/priceIndex";

        private const string isolatedMarginSymbolEndpoint = "margin/isolated/pair";
        private const string isolatedMarginAllSymbolEndpoint = "margin/isolated/allPairs";

        // Blvt
        private const string blvtInfoEndpoint = "blvt/tokenInfo";
        private const string blvtHistoricalKlinesEndpoint = "lvtKlines";

        // Bswap
        private const string bSwapPoolsEndpoint = "bswap/pools";
        private const string bSwapPoolsConfigureEndpoint = "bswap/poolConfigure";

        // Staking
        private const string stakingProductListEndpoint = "staking/productList";

        private const string api = "api";
        private const string publicVersion = "3";

        private const string marginApi = "sapi";
        private const string marginVersion = "1";

        private const string BlvtApi = "sapi";
        private const string blvtVersion = "1";

        private const string bSwapApi = "sapi";
        private const string bSwapVersion = "1";

        private readonly Log _log;

        private readonly BinanceClientSpotApi _baseClient;

        internal BinanceClientSpotApiExchangeData(Log log, BinanceClientSpotApi baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl(pingEndpoint, api, publicVersion), HttpMethod.Get, ct).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(_baseClient.GetUrl(checkTimeEndpoint, api, publicVersion), HttpMethod.Get, ct, ignoreRateLimit: true).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);            
        }

        #endregion

        #region Exchange Information
        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
             => GetExchangeInfoAsync(Array.Empty<string>(), ct);

        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, CancellationToken ct = default)
             => GetExchangeInfoAsync(new string[] { symbol }, ct);

        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(AccountType permissions, CancellationToken ct = default)
             => GetExchangeInfoAsync(new AccountType[] { permissions }, ct);

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            if (symbols.Count() > 1)
                parameters.Add("symbols", JsonConvert.SerializeObject(symbols));
            else if (symbols.Any())
                parameters.Add("symbol", symbols.First());

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array, weight: 10).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient.ExchangeInfo = exchangeInfoResult.Data;
            _baseClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, bool permissions = false, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            if (symbols.Count() > 1)
            {
                parameters.Add(permissions ? "permissions" : "symbols", JsonConvert.SerializeObject(symbols));
            }
            else if (symbols.Any())
            {
                parameters.Add(permissions ? "permissions" : "symbol", symbols.First());
            }

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array, weight: 10).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient.ExchangeInfo = exchangeInfoResult.Data;
            _baseClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(AccountType[] permissions, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            if (permissions.Length > 1)
            {
                List<string> list = new List<string>();
                foreach (var permission in permissions)
                {
                    list.Add(permission.ToString().ToUpper());
                }

                parameters.Add("permissions", JsonConvert.SerializeObject(list));
            }
            else if (permissions.Any())
            {
                parameters.Add("permissions", permissions.First().ToString().ToUpper());
            }

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrl(exchangeInfoEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array, weight: 10).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient.ExchangeInfo = exchangeInfoResult.Data;
            _baseClient.LastExchangeInfoUpdate = DateTime.UtcNow;
            _log.Write(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion

        #region System status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<BinanceSystemStatus>(_baseClient.GetUrl(systemStatusEndpoint, "sapi", "1"), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
        }

        #endregion

        #region asset details
        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<Dictionary<string, BinanceAssetDetails>>(_baseClient.GetUrl(assetDetailsEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Get products

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceProduct>>> GetProductsAsync(CancellationToken ct = default)
        {
            var url = _baseClient.Options.SpotApiOptions.BaseAddress.Replace("api.", "www.").AppendPath("exchange-api/v2/public/asset-service/product/get-products");

            var data = await _baseClient.SendRequestInternal<BinanceExchangeApiWrapper<IEnumerable<BinanceProduct>>>(new Uri(url), HttpMethod.Get, ct).ConfigureAwait(false);
            if (!data)
                return data.As<IEnumerable<BinanceProduct>>(null);

            if (!data.Data.Success)
                return data.AsError<IEnumerable<BinanceProduct>>(new ServerError(data.Data.Code, data.Data.Message + " - " + data.Data.MessageDetail));

            return data.As(data.Data.Data);
        }
        #endregion

        #region Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 5000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var requestWeight = limit == null ? 1 : limit <= 100 ? 1 : limit <= 500 ? 5 : limit <= 1000 ? 10 : 50;
            var result = await _baseClient.SendRequestInternal<BinanceOrderBook>(_baseClient.GetUrl(orderBookEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: requestWeight).ConfigureAwait(false);
            if (result)
                result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region Recent Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(_baseClient.GetUrl(recentTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        #endregion

        #region Old Trade Lookup

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(_baseClient.GetUrl(historicalTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 5).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        #endregion

        #region Compressed/Aggregate Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceAggregatedTrade>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceAggregatedTrade>>(_baseClient.GetUrl(aggregatedTradesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceSpotKline>>(_baseClient.GetUrl(klinesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region UI Kline Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetUiKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceSpotKline>>(_baseClient.GetUrl(uiKlinesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region Current Average Price

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<BinanceAveragePrice>(_baseClient.GetUrl(averagePriceEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region 24hr Ticker Price Change Statistics

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            var result = await _baseClient.SendRequestInternal<Binance24HPrice>(_baseClient.GetUrl(price24HEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 1).ConfigureAwait(false);
            return result.As<IBinanceTick>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            foreach(var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join("," ,symbols.Select(s => $"\"{s}\""))}]" } };
            var symbolCount = symbols.Count();
            var weight = symbolCount <= 20 ? 1 : symbolCount <= 100 ? 20 : 40;
            var result = await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl(price24HEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: weight).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceTick>>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl(price24HEndpoint, api, publicVersion), HttpMethod.Get, ct, weight: 40).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceTick>>(result.Data);
        }

        #endregion

        #region Rolling window price change ticker

        /// <inheritdoc />
        public async Task<WebCallResult<IBinance24HPrice>> GetRollingWindowTickerAsync(string symbol, TimeSpan? windowSize = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("windowSize", windowSize == null ? null : GetWindowSize(windowSize.Value));

            var result = await _baseClient.SendRequestInternal<Binance24HPrice>(_baseClient.GetUrl(rollingWindowPriceEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 2).ConfigureAwait(false);
            return result.As<IBinance24HPrice>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, TimeSpan? windowSize = null, CancellationToken ct = default)
        {
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            parameters.AddOptionalParameter("windowSize", windowSize == null ? null : GetWindowSize(windowSize.Value));
            var symbolCount = symbols.Count();
            var weight = Math.Min(symbolCount * 2, 100);
            var result = await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl(rollingWindowPriceEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: weight).ConfigureAwait(false);
            return result.As<IEnumerable<IBinance24HPrice>>(result.Data);
        }

        private string GetWindowSize(TimeSpan timeSpan)
        {
            if (timeSpan.TotalHours < 1)
                return timeSpan.TotalMinutes + "m";
            else if (timeSpan.TotalHours < 24)
                return timeSpan.TotalHours + "h";
            return timeSpan.TotalDays + "d";
        }
        #endregion

        #region Symbol Price Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await _baseClient.SendRequestInternal<BinancePrice>(_baseClient.GetUrl(allPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            foreach(var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            return await _baseClient.SendRequestInternal<IEnumerable<BinancePrice>>(_baseClient.GetUrl(allPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinancePrice>>(_baseClient.GetUrl(allPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<BinanceBookPrice>(_baseClient.GetUrl(bookPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            foreach(var symbol in symbols)
                symbol.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBookPrice>>(_baseClient.GetUrl(bookPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, parameters, weight: 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBookPrice>>(_baseClient.GetUrl(bookPricesEndpoint, api, publicVersion), HttpMethod.Get, ct, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region GetTradeFee

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol?.ValidateBinanceSymbol();
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceTradeFee>>(_baseClient.GetUrl(tradeFeeEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Query Margin Asset
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginAsset>> GetMarginAssetAsync(string asset, CancellationToken ct = default)
        {
            asset.ValidateNotNull(nameof(asset));

            var parameters = new Dictionary<string, object>
            {
                {"asset", asset}
            };

            return await _baseClient.SendRequestInternal<BinanceMarginAsset>(_baseClient.GetUrl(marginAssetEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, weight: 10).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Pair

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginPair>> GetMarginSymbolAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol}
            };

            return await _baseClient.SendRequestInternal<BinanceMarginPair>(_baseClient.GetUrl(marginPairEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginAsset>>(_baseClient.GetUrl(marginAssetsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginPair>>(_baseClient.GetUrl(marginPairsEndpoint, marginApi, marginVersion), HttpMethod.Get, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin PriceIndex
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol}
            };

            return await _baseClient.SendRequestInternal<BinanceMarginPriceIndex>(_baseClient.GetUrl(marginPriceIndexEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, weight: 10).ConfigureAwait(false);
        }
        #endregion

        #region Query isolated margin symbol
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIsolatedMarginSymbol>> GetIsolatedMarginSymbolAsync(string symbol,
            int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object>
            {
                {"symbol", symbol}
            };

            parameters.AddOptionalParameter("recvWindow",
                receiveWindow?.ToString(CultureInfo.InvariantCulture) ??
                _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient
                .SendRequestInternal<BinanceIsolatedMarginSymbol>(
                    _baseClient.GetUrl(isolatedMarginSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct,
                    parameters, true, weight: 10).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbolsAsync(int? receiveWindow =
            null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow
                                                              ?.ToString(CultureInfo.InvariantCulture) ??
                                                          _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(
                                                              CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceIsolatedMarginSymbol>>(_baseClient.GetUrl(isolatedMarginAllSymbolEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10)
                .ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens

        #region Get Leveraged Token info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtInfo>>> GetLeveragedTokenInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBlvtInfo>>(_baseClient.GetUrl(blvtInfoEndpoint, BlvtApi, blvtVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get historical klines
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtKline>>> GetLeveragedTokensHistoricalKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            // TODO check if URL works
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBlvtKline>>(_baseClient.GetUrl(blvtHistoricalKlinesEndpoint, "fapi", blvtVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Liquidity pools

        #region Get liquid swap pools

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapPool>>> GetLiquidityPoolsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPool>>(_baseClient.GetUrl(bSwapPoolsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Get pool configure

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapPoolConfig>>> GetLiquidityPoolConfigurationAsync(int poolId, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "poolId", poolId }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPoolConfig>>(_baseClient.GetUrl(bSwapPoolsConfigureEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, signed: true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Staking

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceStakingProduct>>> GetStakingProductsAsync(StakingProductType product, string? asset = null, int? page = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "product", EnumConverter.GetString(product) }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceStakingProduct>>(_baseClient.GetUrl(stakingProductListEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
