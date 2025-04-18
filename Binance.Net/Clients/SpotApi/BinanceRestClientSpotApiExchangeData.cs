using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Binance.Net.Converters;
using Binance.Net.Enums;
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
    internal class BinanceRestClientSpotApiExchangeData : IBinanceRestClientSpotApiExchangeData
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ping", BinanceExchange.RateLimiter.SpotRestIp);
            var result = await _baseClient.SendAsync<object>(request, null, ct).ConfigureAwait(false);
            sw.Stop();
            return result ? result.As(sw.ElapsedMilliseconds) : result.As<long>(default!);
        }

        #endregion

        #region Check Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/time", BinanceExchange.RateLimiter.SpotRestIp);
            var result = await _baseClient.SendAsync<BinanceCheckTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Exchange Information

        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default)
             => GetExchangeInfoAsync(Array.Empty<string>(), returnPermissionSets, symbolStatus, ct);

        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, bool? returnPermissionSets = null, CancellationToken ct = default)
             => GetExchangeInfoAsync(new string[] { symbol }, returnPermissionSets, null, ct);

        /// <inheritdoc />
        public Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(PermissionType permission, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default)
             => GetExchangeInfoAsync(new PermissionType[] { permission }, returnPermissionSets, symbolStatus, ct);

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(PermissionType[] permissions, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("showPermissionSets", returnPermissionSets?.ToString().ToLowerInvariant());
            parameters.AddOptionalEnum("symbolStatus", symbolStatus);

            if (permissions.Length > 1)
            {
                List<string> list = new List<string>();
                foreach (var permission in permissions)
                {
                    list.Add($"\"{permission.ToString().ToUpper()}\"");
                }

                parameters.Add("permissions", list.ToArray());
            }
            else if (permissions.Any())
            {
                parameters.Add("permissions", permissions.First().ToString().ToUpper());
            }

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/exchangeInfo", BinanceExchange.RateLimiter.SpotRestIp, 20, false, arraySerialization: ArrayParametersSerialization.JsonArray);
            var exchangeInfoResult = await _baseClient.SendAsync<BinanceExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
            if (!exchangeInfoResult)
                return exchangeInfoResult;

            _baseClient._exchangeInfo = exchangeInfoResult.Data;
            _baseClient._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return exchangeInfoResult;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, bool? returnPermissionSets = null, SymbolStatus? symbolStatus = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("showPermissionSets", returnPermissionSets?.ToString().ToLowerInvariant());
            parameters.AddOptionalEnum("symbolStatus", symbolStatus);

            if (symbols.Count() > 1)
            {
                parameters.Add("symbols", JsonSerializer.Serialize(symbols.ToArray(), (JsonTypeInfo<string[]>)BinanceExchange._serializerContext.GetTypeInfo(typeof(string[]))!));
            }
            else if (symbols.Any())
            {
                parameters.Add("symbol", symbols.First());
            }

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/exchangeInfo", BinanceExchange.RateLimiter.SpotRestIp, 20, false, arraySerialization: ArrayParametersSerialization.Array);
            var exchangeInfoResult = await _baseClient.SendAsync<BinanceExchangeInfo>(request, parameters, ct).ConfigureAwait(false);
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/system/status", BinanceExchange.RateLimiter.SpotRestIp);
            return await _baseClient.SendAsync<BinanceSystemStatus>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Asset Details
        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/asset/assetDetail", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<Dictionary<string, BinanceAssetDetails>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get products

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceProduct[]>> GetProductsAsync(CancellationToken ct = default)
        {
            var url = _baseClient.ClientOptions.Environment.SpotRestAddress.Replace("api.", "www.");
            var request = _definitions.GetOrCreate(HttpMethod.Get, "bapi/asset/v2/public/asset-service/product/get-products");
            var data = await _baseClient.SendToAddressAsync<BinanceExchangeApiWrapper<BinanceProduct[]>>(url, request, null, ct).ConfigureAwait(false);
            if (!data)
                return data.As<BinanceProduct[]>(null);

            if (!data.Data.Success)
                return data.AsError<BinanceProduct[]>(new ServerError(data.Data.Code, data.Data.Message + " - " + data.Data.MessageDetail));

            return data.As(data.Data.Data);
        }
        #endregion

        #region Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 5000);
            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            var requestWeight = limit == null ? 5 : limit <= 100 ? 5 : limit <= 500 ? 25 : limit <= 1000 ? 50 : 250;

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/depth", BinanceExchange.RateLimiter.SpotRestIp, requestWeight);
            var result = await _baseClient.SendAsync<BinanceOrderBook>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (result)
                result.Data.Symbol = symbol;
            return result;
        }

        #endregion

        #region Recent Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/trades", BinanceExchange.RateLimiter.SpotRestIp, 25);
            var result = await _baseClient.SendAsync<BinanceRecentTradeQuote[]>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IBinanceRecentTrade[]>(result.Data);
        }

        #endregion

        #region Old Trade Lookup

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/historicalTrades", BinanceExchange.RateLimiter.SpotRestIp, 25);
            var result = await _baseClient.SendAsync<BinanceRecentTradeQuote[]>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IBinanceRecentTrade[]>(result.Data);
        }

        #endregion

        #region Compressed/Aggregate Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAggregatedTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/aggTrades", BinanceExchange.RateLimiter.SpotRestIp, 4);
            return await _baseClient.SendAsync<BinanceAggregatedTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Kline/Candlestick Data

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new ParameterCollection {
                { "symbol", symbol },
            };
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/klines", BinanceExchange.RateLimiter.SpotRestIp, 2);
            var result = await _baseClient.SendAsync<BinanceSpotKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IBinanceKline[]>(result.Data);
        }

        #endregion

        #region UI Kline Data

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceKline[]>> GetUiKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new ParameterCollection {
                { "symbol", symbol },
            };
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/uiKlines", BinanceExchange.RateLimiter.SpotRestIp, 2);
            var result = await _baseClient.SendAsync<BinanceSpotKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IBinanceKline[]>(result.Data);
        }

        #endregion

        #region Current Average Price

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbol", symbol } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/avgPrice", BinanceExchange.RateLimiter.SpotRestIp, 2);
            return await _baseClient.SendAsync<BinanceAveragePrice>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region 24hr Ticker Price Change Statistics

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceTick>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbol", symbol } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/24hr", BinanceExchange.RateLimiter.SpotRestIp, 1);
            var result = await _baseClient.SendAsync<Binance24HPrice>(request, parameters, ct, 1).ConfigureAwait(false);
            return result.As<IBinanceTick>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceTick[]>> GetTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            var symbolCount = symbols.Count();
            var weight = symbolCount <= 20 ? 2 : symbolCount <= 100 ? 40 : 80;

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/24hr", BinanceExchange.RateLimiter.SpotRestIp, weight);
            var result = await _baseClient.SendAsync<Binance24HPrice[]>(request, parameters, ct, weight).ConfigureAwait(false);
            return result.As<IBinanceTick[]>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IBinanceTick[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/24hr", BinanceExchange.RateLimiter.SpotRestIp, 80);
            var result = await _baseClient.SendAsync<Binance24HPrice[]>(request, null, ct, 80).ConfigureAwait(false);
            return result.As<IBinanceTick[]>(result.Data);
        }

        #endregion

        #region Trading Day Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTradingDayTicker>> GetTradingDayTickerAsync(string symbol, string? timeZone = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("timeZone", timeZone);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/tradingDay", BinanceExchange.RateLimiter.SpotRestIp, 4);
            return await _baseClient.SendAsync<BinanceTradingDayTicker>(request, parameters, ct, 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceTradingDayTicker[]>> GetTradingDayTickersAsync(IEnumerable<string> symbols, string? timeZone = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            parameters.AddOptional("timeZone", timeZone);
            var symbolCount = symbols.Count();
            var weight = Math.Min(symbolCount * 4, 50);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/tradingDay", BinanceExchange.RateLimiter.SpotRestIp, weight);
            return await _baseClient.SendAsync<BinanceTradingDayTicker[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }
        #endregion

        #region Rolling window price change ticker

        /// <inheritdoc />
        public async Task<WebCallResult<IBinance24HPrice>> GetRollingWindowTickerAsync(string symbol, TimeSpan? windowSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("windowSize", windowSize == null ? null : GetWindowSize(windowSize.Value));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker", BinanceExchange.RateLimiter.SpotRestIp, 2);
            var result = await _baseClient.SendAsync<Binance24HPrice>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IBinance24HPrice>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IBinance24HPrice[]>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, TimeSpan? windowSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            parameters.AddOptionalParameter("windowSize", windowSize == null ? null : GetWindowSize(windowSize.Value));
            var symbolCount = symbols.Count();
            var weight = Math.Min(symbolCount * 4, 100);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker", BinanceExchange.RateLimiter.SpotRestIp, weight);
            var result = await _baseClient.SendAsync<Binance24HPrice[]>(request, parameters, ct, weight).ConfigureAwait(false);
            return result.As<IBinance24HPrice[]>(result.Data);
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
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/price", BinanceExchange.RateLimiter.SpotRestIp, 1);
            return await _baseClient.SendAsync<BinancePrice>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePrice[]>> GetPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/price", BinanceExchange.RateLimiter.SpotRestIp, 4);
            return await _baseClient.SendAsync<BinancePrice[]>(request, parameters, ct, 4).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePrice[]>> GetPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/price", BinanceExchange.RateLimiter.SpotRestIp, 4);
            return await _baseClient.SendAsync<BinancePrice[]>(request, null, ct, 4).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Order Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbol", symbol } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/bookTicker", BinanceExchange.RateLimiter.SpotRestIp, 2);
            return await _baseClient.SendAsync<BinanceBookPrice>(request, parameters, ct, 2).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBookPrice[]>> GetBookPricesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection { { "symbols", $"[{string.Join(",", symbols.Select(s => $"\"{s}\""))}]" } };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/bookTicker", BinanceExchange.RateLimiter.SpotRestIp, 4);
            return await _baseClient.SendAsync<BinanceBookPrice[]>(request, parameters, ct, 4).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBookPrice[]>> GetBookPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/ticker/bookTicker", BinanceExchange.RateLimiter.SpotRestIp, 4);
            return await _baseClient.SendAsync<BinanceBookPrice[]>(request, null, ct, 4).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Assets

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginAsset[]>> GetMarginAssetsAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("asset", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/allAssets", BinanceExchange.RateLimiter.SpotRestIp, 1);
            return await _baseClient.SendAsync<BinanceMarginAsset[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get All Margin Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginPair[]>> GetMarginSymbolsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/allPairs", BinanceExchange.RateLimiter.SpotRestIp, 1);
            return await _baseClient.SendAsync<BinanceMarginPair[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin PriceIndex
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = new ParameterCollection
            {
                {"symbol", symbol}
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/priceIndex", BinanceExchange.RateLimiter.SpotRestIp, 10);
            return await _baseClient.SendAsync<BinanceMarginPriceIndex>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Query isolated margin symbol

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceIsolatedMarginSymbol[]>> GetIsolatedMarginSymbolsAsync(string? symbol = null, int? receiveWindow =
            null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow
                                                              ?.ToString(CultureInfo.InvariantCulture) ??
                                                          _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(
                                                              CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/isolated/allPairs", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceIsolatedMarginSymbol[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens

        #region Get Leveraged Token info

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBlvtInfo[]>> GetLeveragedTokenInfoAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/blvt/tokenInfo", BinanceExchange.RateLimiter.SpotRestIp, 1);
            return await _baseClient.SendAsync<BinanceBlvtInfo[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get historical klines
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBlvtKline[]>> GetLeveragedTokensHistoricalKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
            };
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var url = _baseClient.ClientOptions.Environment.UsdFuturesRestAddress;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/lvtKlines", BinanceExchange.RateLimiter.FuturesRest, 1);
            return await _baseClient.SendToAddressAsync<BinanceBlvtKline[]>(url!, request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Get Cross Margin Colleteral Ratio

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceCrossMarginCollateralRatio[]>> GetCrossMarginCollateralRatioAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/crossMarginCollateralRatio", BinanceExchange.RateLimiter.SpotRestIp, 100, false);
            return await _baseClient.SendAsync<BinanceCrossMarginCollateralRatio[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Future Hourly Interest Rate

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesInterestRate[]>> GetFutureHourlyInterestRateAsync(IEnumerable<string> assets, bool isolated, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "assets", string.Join(",", assets) },
                { "isIsolated", isolated.ToString().ToUpper() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/next-hourly-interest-rate", BinanceExchange.RateLimiter.SpotRestIp, 100, true);
            return await _baseClient.SendAsync<BinanceFuturesInterestRate[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Margin Delist Schedule

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginDelistSchedule[]>> GetMarginDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/delist-schedule", BinanceExchange.RateLimiter.SpotRestIp, 100);
            return await _baseClient.SendAsync<BinanceMarginDelistSchedule[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert

        #region Get Convert List All Pairs

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertAssetPair[]>> GetConvertListAllPairsAsync(string? quoteAsset = null, string? baseAsset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("fromAsset", quoteAsset);
            parameters.AddOptionalParameter("toAsset", baseAsset);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/convert/exchangeInfo", BinanceExchange.RateLimiter.SpotRestIp, 20);
            return await _baseClient.SendAsync<BinanceConvertAssetPair[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Quantity Precision Per Asset

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertQuantityPrecisionAsset[]>> GetConvertQuantityPrecisionPerAssetAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/convert/assetInfo", BinanceExchange.RateLimiter.SpotRestIp, 100);
            return await _baseClient.SendAsync<BinanceConvertQuantityPrecisionAsset[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Get Delist Schedule

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceDelistSchedule[]>> GetDelistScheduleAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/spot/delist-schedule", BinanceExchange.RateLimiter.SpotRestIp, 100);
            return await _baseClient.SendAsync<BinanceDelistSchedule[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
