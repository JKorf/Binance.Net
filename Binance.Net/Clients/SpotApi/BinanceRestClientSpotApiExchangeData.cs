using System.Diagnostics;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.ExtensionMethods;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.IsolatedMargin;
using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceRestClientSpotApiExchangeData : IBinanceRestClientSpotApiExchangeData
    {
        private readonly ILogger _logger;

        private readonly BinanceRestClientSpotApi _baseClient;

        internal BinanceRestClientSpotApiExchangeData(ILogger logger, BinanceRestClientSpotApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region Test Connectivity

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PingAsync(CancellationToken ct = default)
        {
            var sw = Stopwatch.StartNew();
            var result = await _baseClient.SendRequestInternal<object>(_baseClient.GetUrl("ping", "api", "3"), HttpMethod.Get, ct, gate: BinanceExchange.RateLimiters.SpotApi_Ip, endpointLimit: new CryptoExchange.Net.RateLimiting.EndpointLimit
            {
                Limit = 2,
                Period = TimeSpan.FromSeconds(5)
            }).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<BinanceCheckTime>(_baseClient.GetUrl("time", "api", "3"), HttpMethod.Get, ct, weight: 1, gate: BinanceExchange.RateLimiters.SpotApi_Ip, endpointLimit: new CryptoExchange.Net.RateLimiting.EndpointLimit
            {
                Limit = 4,
                Period = TimeSpan.FromSeconds(5)
            }).ConfigureAwait(false);
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
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(AccountType permission, CancellationToken ct = default)
             => GetExchangeInfoAsync(new AccountType[] { permission }, ct);

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

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrl("exchangeInfo", "api", "3"), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array, weight: 20, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient._exchangeInfo = exchangeInfoResult.Data;
            _baseClient._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();

            if (symbols.Count() > 1)
            {
                parameters.Add("symbols", JsonConvert.SerializeObject(symbols));
            }
            else if (symbols.Any())
            {
                parameters.Add("symbol", symbols.First());
            }

            var exchangeInfoResult = await _baseClient.SendRequestInternal<BinanceExchangeInfo>(_baseClient.GetUrl("exchangeInfo", "api", "3"), HttpMethod.Get, ct, parameters: parameters, arraySerialization: ArrayParametersSerialization.Array, weight: 20, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient._exchangeInfo = exchangeInfoResult.Data;
            _baseClient._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        #endregion

        #region System status
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<BinanceSystemStatus>(_baseClient.GetUrl("system/status", "sapi", "1"), HttpMethod.Get, ct, null, false, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region asset details
        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<Dictionary<string, BinanceAssetDetails>>(_baseClient.GetUrl("asset/assetDetail", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Get products

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceProduct>>> GetProductsAsync(CancellationToken ct = default)
        {
            var url = ((BinanceEnvironment)_baseClient.ClientOptions.Environment).SpotRestAddress.Replace("api.", "www.").AppendPath("bapi/asset/v2/public/asset-service/product/get-products");
            var data = await _baseClient.SendRequestInternal<BinanceExchangeApiWrapper<IEnumerable<BinanceProduct>>>(new Uri(url), HttpMethod.Get, ct, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
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
            limit?.ValidateIntBetween(nameof(limit), 1, 5000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var requestWeight = limit == null ? 5 : limit <= 100 ? 5 : limit <= 500 ? 25 : limit <= 1000 ? 50 : 250;
            var result = await _baseClient.SendRequestInternal<BinanceOrderBook>(_baseClient.GetUrl("depth", "api", "3"), HttpMethod.Get, ct, parameters, weight: requestWeight, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            if (result)
                result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region Recent Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(_baseClient.GetUrl("trades", "api", "3"), HttpMethod.Get, ct, parameters, weight: 25, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
        }

        #endregion

        #region Old Trade Lookup

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceRecentTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceRecentTradeQuote>>(_baseClient.GetUrl("historicalTrades", "api", "3"), HttpMethod.Get, ct, parameters, weight: 25, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceRecentTrade>>(result.Data);
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

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceAggregatedTrade>>(_baseClient.GetUrl("aggTrades", "api", "3"), HttpMethod.Get, ct, parameters, weight: 2, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceSpotKline>>(_baseClient.GetUrl("klines", "api", "3"), HttpMethod.Get, ct, parameters, weight: 2, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region UI Kline Data

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceKline>>> GetUiKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceSpotKline>>(_baseClient.GetUrl("uiKlines", "api", "3"), HttpMethod.Get, ct, parameters, weight: 2, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceKline>>(result.Data);
        }

        #endregion

        #region Current Average Price

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<BinanceAveragePrice>(_baseClient.GetUrl("avgPrice", "api", "3"), HttpMethod.Get, ct, parameters, weight: 2, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region 24hr Ticker Price Change Statistics

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            var result = await _baseClient.SendRequestInternal<Binance24HPrice>(_baseClient.GetUrl("ticker/24hr", "api", "3"), HttpMethod.Get, ct, parameters, weight: 1, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IBinanceTick>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join("," ,symbols.Select(s => $"\"{s}\""))}]" } };
            var symbolCount = symbols.Count();
            var weight = symbolCount <= 20 ? 2 : symbolCount <= 100 ? 40 : 80;
            var result = await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl("ticker/24hr", "api", "3"), HttpMethod.Get, ct, parameters, weight: weight, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceTick>>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinanceTick>>> GetTickersAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl("ticker/24hr", "api", "3"), HttpMethod.Get, ct, weight: 80, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result.As<IEnumerable<IBinanceTick>>(result.Data);
        }

        #endregion

        #region Rolling window price change ticker

        /// <inheritdoc />
        public async Task<WebCallResult<IBinance24HPrice>> GetRollingWindowTickerAsync(string symbol, TimeSpan? windowSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("windowSize", windowSize == null ? null : GetWindowSize(windowSize.Value));

            var result = await _baseClient.SendRequestInternal<Binance24HPrice>(_baseClient.GetUrl("ticker", "api", "3"), HttpMethod.Get, ct, parameters, weight: 2).ConfigureAwait(false);
            return result.As<IBinance24HPrice>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<IBinance24HPrice>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, TimeSpan? windowSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            parameters.AddOptionalParameter("windowSize", windowSize == null ? null : GetWindowSize(windowSize.Value));
            var symbolCount = symbols.Count();
            var weight = Math.Min(symbolCount * 4, 100);
            var result = await _baseClient.SendRequestInternal<IEnumerable<Binance24HPrice>>(_baseClient.GetUrl("ticker", "api", "3"), HttpMethod.Get, ct, parameters, weight: weight, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
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
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await _baseClient.SendRequestInternal<BinancePrice>(_baseClient.GetUrl("ticker/price", "api", "3"), HttpMethod.Get, ct, parameters, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            return await _baseClient.SendRequestInternal<IEnumerable<BinancePrice>>(_baseClient.GetUrl("ticker/price", "api", "3"), HttpMethod.Get, ct, parameters, weight: 4, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePrice>>> GetPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinancePrice>>(_baseClient.GetUrl("ticker/price", "api", "3"), HttpMethod.Get, ct, weight: 4, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };

            return await _baseClient.SendRequestInternal<BinanceBookPrice>(_baseClient.GetUrl("ticker/bookTicker", "api", "3"), HttpMethod.Get, ct, parameters, weight: 2, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBookPrice>>(_baseClient.GetUrl("ticker/bookTicker", "api", "3"), HttpMethod.Get, ct, parameters, weight: 4, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBookPrice>>> GetBookPricesAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBookPrice>>(_baseClient.GetUrl("ticker/bookTicker", "api", "3"), HttpMethod.Get, ct, weight: 4, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region GetTradeFee

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<IEnumerable<BinanceTradeFee>>(_baseClient.GetUrl("asset/tradeFee", "sapi", "1"), HttpMethod.Get, ct, parameters, true, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get All Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginAsset>>(_baseClient.GetUrl("margin/allAssets", "sapi", "1"), HttpMethod.Get, ct, parameters, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginPair>>(_baseClient.GetUrl("margin/allPairs", "sapi", "1"), HttpMethod.Get, ct, parameters, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
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

            return await _baseClient.SendRequestInternal<BinanceMarginPriceIndex>(_baseClient.GetUrl("margin/priceIndex", "sapi", "1"), HttpMethod.Get, ct, parameters, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }
        #endregion

        #region Query isolated margin symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbolsAsync(string? symbol = null, int? receiveWindow =
            null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow
                                                              ?.ToString(CultureInfo.InvariantCulture) ??
                                                          _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(
                                                              CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceIsolatedMarginSymbol>>(_baseClient.GetUrl("margin/isolated/allPairs", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10, gate: BinanceExchange.RateLimiters.SpotApi_Ip)
                .ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens

        #region Get Leveraged Token info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtInfo>>> GetLeveragedTokenInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBlvtInfo>>(_baseClient.GetUrl("blvt/tokenInfo", "sapi", "1"), HttpMethod.Get, ct, parameters, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
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
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBlvtKline>>(_baseClient.GetUrl("lvtKlines", "fapi", "1"), HttpMethod.Get, ct, parameters, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Get Cross Margin Colleteral Ratio

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceCrossMarginCollateralRatio>>> GetCrossMarginCollateralRatioAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceCrossMarginCollateralRatio>>(_baseClient.GetUrl("margin/crossMarginCollateralRatio", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 100, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Future Hourly Interest Rate

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesInterestRate>>> GetFutureHourlyInterestRateAsync(IEnumerable<string> assets, bool isolated, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "assets", string.Join(",", assets) },
                { "isIsolated", isolated.ToString().ToUpper() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceFuturesInterestRate>>(_baseClient.GetUrl("margin/next-hourly-interest-rate", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 100, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Delist Schedule

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginDelistSchedule>>> GetMarginDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginDelistSchedule>>(_baseClient.GetUrl("margin/delist-schedule", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 100, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Convert

        #region Get Convert List All Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceConvertAssetPair>>> GetConvertListAllPairsAsync(string? quoteAsset = null, string? baseAsset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("fromAsset", quoteAsset);
            parameters.AddOptionalParameter("toAsset", baseAsset);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceConvertAssetPair>>(_baseClient.GetUrl("convert/exchangeInfo", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 20, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Quantity Precision Per Asset

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceConvertQuantityPrecisionAsset>>> GetConvertQuantityPrecisionPerAssetAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceConvertQuantityPrecisionAsset>>(_baseClient.GetUrl("convert/assetInfo", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 100, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Get Delist Schedule

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceDelistSchedule>>> GetDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceDelistSchedule>>(_baseClient.GetUrl("spot/delist-schedule", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 100, gate: BinanceExchange.RateLimiters.SpotApi_Ip).ConfigureAwait(false);
        }

        #endregion
    }
}
