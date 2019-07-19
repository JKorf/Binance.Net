using Binance.Net.Converters;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance REST Api
    /// </summary>
    public class BinanceClient : RestClient, IBinanceClient
    {
        #region fields 
        private static BinanceClientOptions defaultOptions = new BinanceClientOptions();
        private static BinanceClientOptions DefaultOptions => defaultOptions.Copy();

        private bool autoTimestamp;
        private TimeSpan autoTimestampRecalculationInterval;
        private TimeSpan timestampOffset;
        private TradeRulesBehaviour tradeRulesBehaviour;
        private TimeSpan tradeRulesUpdateInterval;
        private TimeSpan defaultReceiveWindow;

        private double calculatedTimeOffset;
        private bool timeSynced;
        private DateTime lastTimeSync;

        private BinanceExchangeInfo exchangeInfo;
        private DateTime? lastExchangeInfoUpdate;


        // Addresses
        private const string Api = "api";
        private const string MarginApi = "sapi";
        private const string WithdrawalApi = "wapi";

        // Versions
        private const string PublicVersion = "1";
        private const string SignedVersion = "3";
        private const string UserDataStreamVersion = "1";
        private const string WithdrawalVersion = "3";
        private const string MarginVersion = "1";

        // Methods
        private const string GetMethod = "GET";
        private const string PostMethod = "POST";
        private const string DeleteMethod = "DELETE";
        private const string PutMethod = "PUT";

        // Public
        private const string PingEndpoint = "ping";
        private const string CheckTimeEndpoint = "time";
        private const string ExchangeInfoEndpoint = "exchangeInfo";
        private const string OrderBookEndpoint = "depth";
        private const string AggregatedTradesEndpoint = "aggTrades";
        private const string RecentTradesEndpoint = "trades";
        private const string HistoricalTradesEndpoint = "historicalTrades";
        private const string KlinesEndpoint = "klines";
        private const string Price24HEndpoint = "ticker/24hr";
        private const string AllPricesEndpoint = "ticker/price";
        private const string BookPricesEndpoint = "ticker/bookTicker";

        // Signed
        private const string OpenOrdersEndpoint = "openOrders";
        private const string AllOrdersEndpoint = "allOrders";
        private const string NewOrderEndpoint = "order";
        private const string NewTestOrderEndpoint = "order/test";
        private const string QueryOrderEndpoint = "order";
        private const string CancelOrderEndpoint = "order";
        private const string AccountInfoEndpoint = "account";
        private const string MyTradesEndpoint = "myTrades";

        // Margin
        private const string MarginTransferEndpoint = "margin/transfer";
        private const string MarginBorrowEndpoint = "margin/loan";
        private const string MarginRepayEndpoint = "margin/transfer";
        private const string NewMarginOrderEndpoint = "margin/order";
        private const string CancelMarginOrderEndpoint = "margin/order";

        // User stream
        private const string GetListenKeyEndpoint = "userDataStream";
        private const string KeepListenKeyAliveEndpoint = "userDataStream";
        private const string CloseListenKeyEndpoint = "userDataStream";

        // Withdrawing
        private const string WithdrawEndpoint = "withdraw.html";
        private const string DepositHistoryEndpoint = "depositHistory.html";
        private const string WithdrawHistoryEndpoint = "withdrawHistory.html";
        private const string DepositAddressEndpoint = "depositAddress.html";

        private const string TradeFeeEndpoint = "tradeFee.html";
        private const string AssetDetailsEndpoint = "assetDetail.html";
        private const string AccountStatusEndpoint = "accountStatus.html";
        private const string SystemStatusEndpoint = "systemStatus.html";
        private const string DustLogEndpoint = "userAssetDribbletLog.html";
        private const string TradingStatusEndpoint = "apiTradingStatus.html";

        // Sub accounts
        private const string SubAccountListEndpoint = "sub-account/list.html";
        private const string SubAccountTransferHistoryEndpoint = "sub-account/transfer/history.html";
        private const string TransferSubAccountEndpoint = "sub-account/transfer.html";


        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceClient(BinanceClientOptions options) : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            Configure(options);
        }
        #endregion

        #region methods
        #region public
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(BinanceClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new BinanceAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        public override CallResult<long> Ping() => PingAsync().Result;

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        public override async Task<CallResult<long>> PingAsync()
        {
            var sw = Stopwatch.StartNew();
            var result = await ExecuteRequest<BinancePing>(GetUrl(PingEndpoint, Api, PublicVersion)).ConfigureAwait(false);
            sw.Stop();
            return new CallResult<long>(result.Error == null ? sw.ElapsedMilliseconds : 0, result.Error);
        }

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <returns>Server time</returns>
        public WebCallResult<DateTime> GetServerTime(bool resetAutoTimestamp = false) => GetServerTimeAsync(resetAutoTimestamp).Result;

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <returns>Server time</returns>
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false)
        {
            var url = GetUrl(CheckTimeEndpoint, Api, PublicVersion);
            if (!autoTimestamp)
            {
                var result = await ExecuteRequest<BinanceCheckTime>(url).ConfigureAwait(false);
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ServerTime ?? default(DateTime), result.Error);
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var result = await ExecuteRequest<BinanceCheckTime>(url).ConfigureAwait(false);
                if (!result.Success)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default(DateTime), result.Error);

                if (timeSynced && !resetAutoTimestamp)
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);

                if (TotalRequestsMade == 1)
                {
                    // If this was the first request make another one to calculate the offset since the first one can be slower
                    localTime = DateTime.UtcNow;
                    result = await ExecuteRequest<BinanceCheckTime>(url).ConfigureAwait(false);
                    if (!result.Success)
                        return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, default(DateTime), result.Error);
                }

                // Calculate time offset between local and server
                var offset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                if (offset >= 0 && offset < 500)
                {
                    // Small offset, probably mainly due to ping. Don't adjust time
                    calculatedTimeOffset = 0;
                    timeSynced = true;
                    lastTimeSync = DateTime.UtcNow;
                    log.Write(LogVerbosity.Info, $"Time offset between 0 and 500ms ({offset}ms), no adjustment needed");
                    return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
                }

                calculatedTimeOffset = (result.Data.ServerTime - localTime).TotalMilliseconds;
                timeSynced = true;
                lastTimeSync = DateTime.UtcNow;
                log.Write(LogVerbosity.Info, $"Time offset set to {calculatedTimeOffset}ms");
                return new WebCallResult<DateTime>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.ServerTime, result.Error);
            }
        }

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <returns>Exchange info</returns>
        public WebCallResult<BinanceExchangeInfo> GetExchangeInfo() => GetExchangeInfoAsync().Result;

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <returns>Exchange info</returns>
        public async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync()
        {
            var exchangeInfoResult = await ExecuteRequest<BinanceExchangeInfo>(GetUrl(ExchangeInfoEndpoint, Api, PublicVersion)).ConfigureAwait(false);
            if (!exchangeInfoResult.Success)
                return exchangeInfoResult;

            exchangeInfo = exchangeInfoResult.Data;
            lastExchangeInfoUpdate = DateTime.UtcNow;
            log.Write(LogVerbosity.Info, "Trade rules updated");
            return exchangeInfoResult;
        }

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The order book for the symbol</returns>
        public WebCallResult<BinanceOrderBook> GetOrderBook(string symbol, int? limit = null) => GetOrderBookAsync(symbol, limit).Result;

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The order book for the symbol</returns>
        public async Task<WebCallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString());
            return await ExecuteRequest<BinanceOrderBook>(GetUrl(OrderBookEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        public WebCallResult<BinanceAggregatedTrades[]> GetAggregatedTrades(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null) => GetAggregatedTradesAsync(symbol, fromId, startTime, endTime, limit).Result;

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        public async Task<WebCallResult<BinanceAggregatedTrades[]>> GetAggregatedTradesAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString());
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            parameters.AddOptionalParameter("limit", limit?.ToString());

            return await ExecuteRequest<BinanceAggregatedTrades[]>(GetUrl(AggregatedTradesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <returns>List of recent trades</returns>
        public WebCallResult<BinanceRecentTrade[]> GetRecentTrades(string symbol, int? limit = null) => GetRecentTradesAsync(symbol, limit).Result;

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <returns>List of recent trades</returns>
        public async Task<WebCallResult<BinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString());
            return await ExecuteRequest<BinanceRecentTrade[]>(GetUrl(RecentTradesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <returns>List of recent trades</returns>
        public WebCallResult<BinanceRecentTrade[]> GetHistoricalTrades(string symbol, int? limit = null, long? fromId = null) => GetHistoricalTradesAsync(symbol, limit, fromId).Result;

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <returns>List of recent trades</returns>
        public async Task<WebCallResult<BinanceRecentTrade[]>> GetHistoricalTradesAsync(string symbol, int? limit = null, long? fromId = null)
        {
            var parameters = new Dictionary<string, object> { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString());

            return await ExecuteRequest<BinanceRecentTrade[]>(GetUrl(HistoricalTradesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public WebCallResult<BinanceKline[]> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null) => GetKlinesAsync(symbol, interval, startTime, endTime, limit).Result;

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public async Task<WebCallResult<BinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) }
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            parameters.AddOptionalParameter("limit", limit?.ToString());

            return await ExecuteRequest<BinanceKline[]>(GetUrl(KlinesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <returns>Data over the last 24 hours</returns>
        public WebCallResult<Binance24HPrice> Get24HPrice(string symbol) => Get24HPriceAsync(symbol).Result;

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <returns>Data over the last 24 hours</returns>
        public async Task<WebCallResult<Binance24HPrice>> Get24HPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await ExecuteRequest<Binance24HPrice>(GetUrl(Price24HEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <returns>List of data over the last 24 hours</returns>
        public WebCallResult<Binance24HPrice[]> Get24HPricesList() => Get24HPricesListAsync().Result;

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <returns>List of data over the last 24 hours</returns>
        public async Task<WebCallResult<Binance24HPrice[]>> Get24HPricesListAsync()
        {
            return await ExecuteRequest<Binance24HPrice[]>(GetUrl(Price24HEndpoint, Api, PublicVersion)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <returns>Price of symbol</returns>
        public WebCallResult<BinancePrice> GetPrice(string symbol) => GetPriceAsync(symbol).Result;

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <returns>Price of symbol</returns>
        public async Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await ExecuteRequest<BinancePrice>(GetUrl(AllPricesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <returns>List of prices</returns>
        public WebCallResult<BinancePrice[]> GetAllPrices() => GetAllPricesAsync().Result;

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <returns>List of prices</returns>
        public async Task<WebCallResult<BinancePrice[]>> GetAllPricesAsync()
        {
            return await ExecuteRequest<BinancePrice[]>(GetUrl(AllPricesEndpoint, Api, PublicVersion)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <returns>List of book prices</returns>
        public WebCallResult<BinanceBookPrice> GetBookPrice(string symbol) => GetBookPriceAsync(symbol).Result;

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// </summary>
        /// <returns>List of book prices</returns>
        public async Task<WebCallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

            return await ExecuteRequest<BinanceBookPrice>(GetUrl(BookPricesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// </summary>
        /// <returns>List of book prices</returns>
        public WebCallResult<BinanceBookPrice[]> GetAllBookPrices() => GetAllBookPricesAsync().Result;

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// </summary>
        /// <returns>List of book prices</returns>
        public async Task<WebCallResult<BinanceBookPrice[]>> GetAllBookPricesAsync()
        {
            return await ExecuteRequest<BinanceBookPrice[]>(GetUrl(BookPricesEndpoint, Api, PublicVersion)).ConfigureAwait(false);
        }
        #endregion

        #region signed
        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of open orders</returns>
        public WebCallResult<BinanceOrder[]> GetOpenOrders(string symbol = null, int? receiveWindow = null) => GetOpenOrdersAsync(symbol, receiveWindow).Result;

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of open orders</returns>
        public async Task<WebCallResult<BinanceOrder[]>> GetOpenOrdersAsync(string symbol = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceOrder[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            return await ExecuteRequest<BinanceOrder[]>(GetUrl(OpenOrdersEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of orders</returns>
        public WebCallResult<BinanceOrder[]> GetAllOrders(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null) => GetAllOrdersAsync(symbol, orderId, startTime, endTime, limit, receiveWindow).Result;

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<BinanceOrder[]>> GetAllOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceOrder[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString());

            return await ExecuteRequest<BinanceOrder[]>(GetUrl(AllOrdersEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed order</returns>
        public WebCallResult<BinancePlacedOrder> PlaceOrder(
            string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null) => PlaceOrderAsync(symbol, side, type, quantity, newClientOrderId, price, timeInForce, stopPrice, icebergQty, orderResponseType, receiveWindow).Result;

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed order</returns>
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinancePlacedOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, type).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage);
                return new WebCallResult<BinancePlacedOrder>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewOrderEndpoint, Api, SignedVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type (limit/market)</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">User for iceberg orders</param>
        /// <param name="orderResponseType">What kind of response should be returned</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed test order</returns>
        public WebCallResult<BinancePlacedOrder> PlaceTestOrder(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null) => PlaceTestOrderAsync(symbol, side, type, quantity, newClientOrderId, price, timeInForce, stopPrice, icebergQty, orderResponseType, receiveWindow).Result;

        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type (limit/market)</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">User for iceberg orders</param>
        /// <param name="orderResponseType">What kind of response should be returned</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed test order</returns>
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinancePlacedOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, type).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage);
                return new WebCallResult<BinancePlacedOrder>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewTestOrderEndpoint, Api, SignedVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The specific order</returns>
        public WebCallResult<BinanceOrder> QueryOrder(string symbol, long? orderId = null, string origClientOrderId = null, long? receiveWindow = null) => QueryOrderAsync(symbol, orderId, origClientOrderId, receiveWindow).Result;

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The specific order</returns>
        public async Task<WebCallResult<BinanceOrder>> QueryOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, long? receiveWindow = null)
        {
            if (orderId == null && origClientOrderId == null)
                return new WebCallResult<BinanceOrder>(null, null, null, new ArgumentError("Either orderId or origClientOrderId should be provided"));

            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceOrder>(GetUrl(QueryOrderEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        public WebCallResult<BinanceCanceledOrder> CancelOrder(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? receiveWindow = null) => CancelOrderAsync(symbol, orderId, origClientOrderId, newClientOrderId, receiveWindow).Result;

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<BinanceCanceledOrder>> CancelOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceCanceledOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceCanceledOrder>(GetUrl(CancelOrderEndpoint, Api, SignedVersion), DeleteMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The account information</returns>
        public WebCallResult<BinanceAccountInfo> GetAccountInfo(long? receiveWindow = null) => GetAccountInfoAsync(receiveWindow).Result;

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The account information</returns>
        public async Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceAccountInfo>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceAccountInfo>(GetUrl(AccountInfoEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of trades</returns>
        public WebCallResult<BinanceTrade[]> GetMyTrades(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null) => GetMyTradesAsync(symbol, startTime, endTime, limit, fromId, receiveWindow).Result;

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of trades</returns>
        public async Task<WebCallResult<BinanceTrade[]>> GetMyTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceTrade[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceTrade[]>(GetUrl(MyTradesEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="name">Name for the transaction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal confirmation</returns>
        public WebCallResult<BinanceWithdrawalPlaced> Withdraw(string asset, string address, decimal amount, string addressTag = null, string name = null, int? receiveWindow = null) => WithdrawAsync(asset, address, amount, addressTag, name, receiveWindow).Result;

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="name">Name for the transaction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal confirmation</returns>
        public async Task<WebCallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal amount, string addressTag = null, string name = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceWithdrawalPlaced>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "address", address },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("name", name);
            parameters.AddOptionalParameter("addressTag", addressTag);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceWithdrawalPlaced>(GetUrl(WithdrawEndpoint, WithdrawalApi, WithdrawalVersion), PostMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
            {
                return result;
            }

            if (!result.Data.Success)
            {
                return new WebCallResult<BinanceWithdrawalPlaced>(result.ResponseStatusCode, result.ResponseHeaders, null, ParseErrorResponse(result.Data.Message));
            }

            return result;
        }

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of deposits</returns>
        public WebCallResult<BinanceDepositList> GetDepositHistory(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null) => GetDepositHistoryAsync(asset, status, startTime, endTime, receiveWindow).Result;

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of deposits</returns>
        public async Task<WebCallResult<BinanceDepositList>> GetDepositHistoryAsync(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceDepositList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceDepositList>(GetUrl(DepositHistoryEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
            {
                return result;
            }

            if (!result.Data.Success)
            {
                return new WebCallResult<BinanceDepositList>(result.ResponseStatusCode, result.ResponseHeaders, null, ParseErrorResponse(result.Data.Message));
            }

            return result;
        }

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of withdrawals</returns>
        public WebCallResult<BinanceWithdrawalList> GetWithdrawHistory(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null) => GetWithdrawHistoryAsync(asset, status, startTime, endTime, receiveWindow).Result;

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of withdrawals</returns>
        public async Task<WebCallResult<BinanceWithdrawalList>> GetWithdrawHistoryAsync(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceWithdrawalList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };

            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceWithdrawalList>(GetUrl(WithdrawHistoryEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
            {
                return result;
            }

            if (!result.Data.Success)
            {
                return new WebCallResult<BinanceWithdrawalList>(result.ResponseStatusCode, result.ResponseHeaders, null, ParseErrorResponse(result.Data.Message));
            }

            return result;
        }

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Deposit address</returns>
        public WebCallResult<BinanceDepositAddress> GetDepositAddress(string asset, int? receiveWindow = null) => GetDepositAddressAsync(asset, receiveWindow).Result;

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Deposit address</returns>
        public async Task<WebCallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceDepositAddress>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceDepositAddress>(GetUrl(DepositAddressEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the withdrawal fee for an symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Trade fees</returns>
        public WebCallResult<BinanceTradeFee[]> GetTradeFee(string symbol = null, int? receiveWindow = null) => GetTradeFeeAsync(symbol, receiveWindow).Result;

        /// <summary>
        /// Gets the trade fee for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Trade fees</returns>
        public async Task<WebCallResult<BinanceTradeFee[]>> GetTradeFeeAsync(string symbol = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceTradeFee[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceTradeFeeWrapper>(GetUrl(TradeFeeEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<BinanceTradeFee[]>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<BinanceTradeFee[]>(result.ResponseStatusCode, result.ResponseHeaders, null, ParseErrorResponse(result.Data.Message)) : new WebCallResult<BinanceTradeFee[]>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Asset detail</returns>
        public WebCallResult<Dictionary<string, BinanceAssetDetails>> GetAssetDetails(int? receiveWindow = null) => GetAssetDetailsAsync(receiveWindow).Result;

        /// <summary>
        /// Gets the withdraw/deposit details for an asset
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Asset detail</returns>
        public async Task<WebCallResult<Dictionary<string, BinanceAssetDetails>>> GetAssetDetailsAsync(int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<Dictionary<string, BinanceAssetDetails>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceAssetDetailsWrapper>(GetUrl(AssetDetailsEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<Dictionary<string, BinanceAssetDetails>>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<Dictionary<string, BinanceAssetDetails>>(result.ResponseStatusCode, result.ResponseHeaders, null, ParseErrorResponse(JToken.Parse(result.Data.Message))) : new WebCallResult<Dictionary<string, BinanceAssetDetails>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Data, null);
        }

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Account status</returns>
        public WebCallResult<BinanceAccountStatus> GetAccountStatus(int? receiveWindow = null) => GetAccountStatusAsync(receiveWindow).Result;

        /// <summary>
        /// Gets the status of the account associated with the api key/secret
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Account status</returns>
        public async Task<WebCallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceAccountStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceAccountStatus>(GetUrl(AccountStatusEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<BinanceAccountStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<BinanceAccountStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, ParseErrorResponse(result.Data.Message)) : new WebCallResult<BinanceAccountStatus>(result.ResponseStatusCode, result.ResponseHeaders, result.Data, null);
        }

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <returns>The system status</returns>
        public WebCallResult<BinanceSystemStatus> GetSystemStatus() => GetSystemStatusAsync().Result;

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <returns>The system status</returns>
        public async Task<WebCallResult<BinanceSystemStatus>> GetSystemStatusAsync()
        {
            return await ExecuteRequest<BinanceSystemStatus>(GetUrl(SystemStatusEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, null, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The history of dust conversions</returns>
        public WebCallResult<BinanceDustLog[]> GetDustLog(int? receiveWindow = null) => GetDustLogAsync(receiveWindow).Result;

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The history of dust conversions</returns>
        public async Task<WebCallResult<BinanceDustLog[]>> GetDustLogAsync(int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceDustLog[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceDustLogListWrapper>(GetUrl(DustLogEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<BinanceDustLog[]>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<BinanceDustLog[]>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError("Unknown server error while requesting dust log")) : new WebCallResult<BinanceDustLog[]>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Results.Rows, null);
        }

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="accountStatus">Filter the list by account status</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of sub accounts</returns>
        public WebCallResult<BinanceSubAccount[]> GetSubAccounts(string email = null, SubAccountStatus? accountStatus = null, int? page = null, int? limit = null, int? receiveWindow = null) => GetSubAccountsAsync(email, accountStatus, page, limit, receiveWindow).Result;

        /// <summary>
        /// Gets a list of sub accounts associated with this master account
        /// </summary>
        /// <param name="email">Filter the list by email</param>
        /// <param name="accountStatus">Filter the list by account status</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of sub accounts</returns>
        public async Task<WebCallResult<BinanceSubAccount[]>> GetSubAccountsAsync(string email = null, SubAccountStatus? accountStatus = null, int? page = null, int? limit = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceSubAccount[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("status", accountStatus != null ? JsonConvert.SerializeObject(accountStatus, new WithdrawalStatusConverter(false)) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceSubAccountWrapper>(GetUrl(SubAccountListEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<BinanceSubAccount[]>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<BinanceSubAccount[]>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message)) : new WebCallResult<BinanceSubAccount[]>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.SubAccounts, null);
        }

        /// <summary>
        /// Gets the history of sub account transfers
        /// </summary>
        /// <param name="email">Filter the history by email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of transfers</returns>
        public WebCallResult<BinanceSubAccountTransfer[]> GetSubAccountTransferHistory(string email = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null) => GetSubAccountTransferHistoryAsync(email, startTime, endTime, page, limit, receiveWindow).Result;

        /// <summary>
        /// Gets the history of sub account transfers
        /// </summary>
        /// <param name="email">Filter the history by email</param>
        /// <param name="startTime">Filter the history by startTime</param>
        /// <param name="endTime">Filter the history by endTime</param>
        /// <param name="page">The page of the results</param>
        /// <param name="limit">The max amount of results to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of transfers</returns>
        public async Task<WebCallResult<BinanceSubAccountTransfer[]>> GetSubAccountTransferHistoryAsync(string email = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceSubAccountTransfer[]>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("email", email);
            parameters.AddOptionalParameter("startTime", startTime != null ? JsonConvert.SerializeObject(startTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? JsonConvert.SerializeObject(endTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("page", page?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceSubAccountTransferWrapper>(GetUrl(SubAccountTransferHistoryEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<BinanceSubAccountTransfer[]>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<BinanceSubAccountTransfer[]>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message)) : new WebCallResult<BinanceSubAccountTransfer[]>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Transfers, null);
        }

        /// <summary>
        /// Transfers an asset from one sub account to another
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The result of the transfer</returns>
        public WebCallResult<BinanceSubAccountTransferResult> TransferSubAccount(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null) => TransferSubAccountAsync(fromEmail, toEmail, asset, amount, receiveWindow).Result;

        /// <summary>
        /// Transfers an asset from one sub account to another
        /// </summary>
        /// <param name="fromEmail">From which account to transfer</param>
        /// <param name="toEmail">To which account to transfer</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">The quantity to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The result of the transfer</returns>
        public async Task<WebCallResult<BinanceSubAccountTransferResult>> TransferSubAccountAsync(string fromEmail, string toEmail, string asset, decimal amount, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceSubAccountTransferResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "fromEmail", fromEmail },
                { "toEmail", toEmail },
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceSubAccountTransferResult>(GetUrl(TransferSubAccountEndpoint, WithdrawalApi, WithdrawalVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The trading status of the account</returns>
        public WebCallResult<BinanceTradingStatus> GetTradingStatus(int? receiveWindow = null) => GetTradingStatusAsync(receiveWindow).Result;

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The trading status of the account</returns>
        public async Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceTradingStatus>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", GetTimestamp() },
            };

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await ExecuteRequest<BinanceTradingStatusWrapper>(GetUrl(TradingStatusEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
            {
                return new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, result.Error);
            }

            return !result.Data.Success ? new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, null, new ServerError(result.Data.Message)) : new WebCallResult<BinanceTradingStatus>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Status, null);
        }


        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to <see cref="BinanceSocketClient.SubscribeToUserStream"/>. The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <returns>Listen key</returns>
        public WebCallResult<string> StartUserStream() => StartUserStreamAsync().Result;

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to <see cref="BinanceSocketClient.SubscribeToUserStream"/>. The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <returns>Listen key</returns>
        public async Task<WebCallResult<string>> StartUserStreamAsync()
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<string>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var result = await ExecuteRequest<BinanceListenKey>(GetUrl(GetListenKeyEndpoint, Api, UserDataStreamVersion), PostMethod).ConfigureAwait(false);
            return new WebCallResult<string>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.ListenKey, result.Error);
        }

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <returns></returns>
        public WebCallResult<object> KeepAliveUserStream(string listenKey) => KeepAliveUserStreamAsync(listenKey).Result;

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<object>> KeepAliveUserStreamAsync(string listenKey)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            return await ExecuteRequest<object>(GetUrl(KeepListenKeyAliveEndpoint, Api, UserDataStreamVersion), PutMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <returns></returns>
        public WebCallResult<object> StopUserStream(string listenKey) => StopUserStreamAsync(listenKey).Result;

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <returns></returns>
        public async Task<WebCallResult<object>> StopUserStreamAsync(string listenKey)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<object>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "listenKey", listenKey }
            };

            return await ExecuteRequest<object>(GetUrl(CloseListenKeyEndpoint, Api, UserDataStreamVersion), DeleteMethod, parameters).ConfigureAwait(false);
        }
        #endregion

        #region margin
        /// <summary>
        /// Execute transfer between spot account and margin account.
        /// </summary>
        /// <param name="asset">The asset being transferred, e.g., BTC</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="type">TransferDirection (MainToMargin/MarginToMain)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Transaction Id</returns>
        public WebCallResult<BinanceMarginTransaction> Transfer(string asset, decimal amount, TransferDirectionType type, int? receiveWindow = null) => TransferAsync(asset, amount, type, receiveWindow).Result;

        /// <summary>
        /// Execute transfer between spot account and margin account.
        /// </summary>
        /// <param name="asset">The asset being transferred, e.g., BTC</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="type">TransferDirection (MainToMargin/MarginToMain)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Transaction Id</returns>
        public async Task<WebCallResult<BinanceMarginTransaction>> TransferAsync(string asset, decimal amount, TransferDirectionType type, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceMarginTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "type", JsonConvert.SerializeObject(type, new TransferDirectionTypeConverter(false)) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceMarginTransaction>(GetUrl(MarginTransferEndpoint, MarginApi, MarginVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// </summary>
        /// <param name="asset">The asset being borrow, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Transaction Id</returns>
        public WebCallResult<BinanceMarginTransaction> Borrow(string asset, decimal amount, int? receiveWindow = null) => BorrowAsync(asset, amount, receiveWindow).Result;

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// </summary>
        /// <param name="asset">The asset being borrow, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Transaction Id</returns>
        public async Task<WebCallResult<BinanceMarginTransaction>> BorrowAsync(string asset, decimal amount, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceMarginTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceMarginTransaction>(GetUrl(MarginBorrowEndpoint, MarginApi, MarginVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Repay loan for margin account.
        /// </summary>
        /// <param name="asset">The asset being repay, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Transaction Id</returns>
        public WebCallResult<BinanceMarginTransaction> Repay(string asset, decimal amount, int? receiveWindow = null) => RepayAsync(asset, amount, receiveWindow).Result;

        /// <summary>
        /// Repay loan for margin account.
        /// </summary>
        /// <param name="asset">The asset being repay, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Transaction Id</returns>
        public async Task<WebCallResult<BinanceMarginTransaction>> RepayAsync(string asset, decimal amount, int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceMarginTransaction>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "asset", asset },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceMarginTransaction>(GetUrl(MarginRepayEndpoint, MarginApi, MarginVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Margin account new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed order</returns>
        public WebCallResult<BinancePlacedOrder> PlaceMarginOrder(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null) => PlaceMarginOrderAsync(symbol, side, type, quantity, newClientOrderId, price, timeInForce, stopPrice, icebergQty, orderResponseType, receiveWindow).Result;

        /// <summary>
        /// Margin account new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">The type of response to receive</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for the placed order</returns>
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal quantity,
            string newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinancePlacedOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, type).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage);
                return new WebCallResult<BinancePlacedOrder>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewMarginOrderEndpoint, MarginApi, MarginVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an active order for margin account
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        public WebCallResult<BinanceCanceledOrder> CancelMarginOrder(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? receiveWindow = null) => CancelOrderAsync(symbol, orderId, origClientOrderId, newClientOrderId, receiveWindow).Result;

        /// <summary>
        /// Cancel an active order for margin account
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<BinanceCanceledOrder>> CancelMarginOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? receiveWindow = null)
        {
            var timestampResult = await CheckAutoTimestamp().ConfigureAwait(false);
            if (!timestampResult.Success)
                return new WebCallResult<BinanceCanceledOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? defaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceCanceledOrder>(GetUrl(CancelMarginOrderEndpoint, MarginApi, SignedVersion), DeleteMethod, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region helpers

        private void Configure(BinanceClientOptions options)
        {
            autoTimestamp = options.AutoTimestamp;
            tradeRulesBehaviour = options.TradeRulesBehaviour;
            tradeRulesUpdateInterval = options.TradeRulesUpdateInterval;
            autoTimestampRecalculationInterval = options.AutoTimestampRecalculationInterval;
            timestampOffset = options.TimestampOffset;
            defaultReceiveWindow = options.ReceiveWindow;

            postParametersPosition = PostParameters.InUri;
        }

        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
            {
                return new ServerError(error.ToString());
            }

            if (error["msg"] == null && error["code"] == null)
            {
                return new ServerError(error.ToString());
            }

            if (error["msg"] != null && error["code"] == null)
            {
                return new ServerError((string)error["msg"]);
            }

            return new ServerError((int)error["code"], (string)error["msg"]);
        }

        private Uri GetUrl(string endpoint, string api, string version)
        {
            var result = $"{BaseAddress}/{api}/v{version}/{endpoint}";
            return new Uri(result);
        }

        private static long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        private string GetTimestamp()
        {
            var offset = autoTimestamp ? calculatedTimeOffset : 0;
            offset += timestampOffset.TotalMilliseconds;
            return ToUnixTimestamp(DateTime.UtcNow.AddMilliseconds(offset)).ToString();
        }

        private async Task<WebCallResult<DateTime>> CheckAutoTimestamp()
        {
            if (autoTimestamp && (!timeSynced || DateTime.UtcNow - lastTimeSync > autoTimestampRecalculationInterval))
            {
                return await GetServerTimeAsync(timeSynced).ConfigureAwait(false);
            }

            return new WebCallResult<DateTime>(null, null, default(DateTime), null);
        }

        private async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal quantity, decimal? price, OrderType type)
        {
            var outputQuantity = quantity;
            var outputPrice = price;

            if (tradeRulesBehaviour == TradeRulesBehaviour.None)
            {
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);
            }

            if (exchangeInfo == null || lastExchangeInfoUpdate == null || (DateTime.UtcNow - lastExchangeInfoUpdate.Value).TotalMinutes > tradeRulesUpdateInterval.TotalMinutes)
            {
                await GetExchangeInfoAsync().ConfigureAwait(false);
            }

            if (exchangeInfo == null)
            {
                return BinanceTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");
            }

            var symbolData = exchangeInfo.Symbols.SingleOrDefault(s => string.Equals(s.Name, symbol, StringComparison.CurrentCultureIgnoreCase));
            if (symbolData == null)
            {
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Symbol {symbol} not found");
            }

            if (!symbolData.OrderTypes.Contains(type))
            {
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: {type} order type not allowed for {symbol}");
            }

            if (symbolData.LotSizeFilter != null || (symbolData.MarketLotSizeFilter != null && type == OrderType.Market))
            {
                var minQty = symbolData.LotSizeFilter?.MinQuantity;
                var maxQty = symbolData.LotSizeFilter?.MaxQuantity;
                var stepSize = symbolData.LotSizeFilter?.StepSize;
                if (type == OrderType.Market && symbolData.MarketLotSizeFilter != null)
                {
                    minQty = symbolData.MarketLotSizeFilter.MinQuantity;
                    if (symbolData.MarketLotSizeFilter.MaxQuantity != 0)
                    {
                        maxQty = symbolData.MarketLotSizeFilter.MaxQuantity;
                    }

                    if (symbolData.MarketLotSizeFilter.StepSize != 0)
                    {
                        stepSize = symbolData.MarketLotSizeFilter.StepSize;
                    }
                }

                if (minQty.HasValue)
                {
                    outputQuantity = BinanceHelpers.ClampQuantity(minQty.Value, maxQty.Value, stepSize.Value, quantity);
                    if (outputQuantity != quantity)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");
                        }

                        log.Write(LogVerbosity.Info, $"Quantity clamped from {quantity} to {outputQuantity}");
                    }
                }
            }

            if (price == null)
            {
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, null);
            }

            if (symbolData.PriceFilter != null)
            {
                if (symbolData.PriceFilter.MaxPrice != 0 && symbolData.PriceFilter.MinPrice != 0)
                {
                    outputPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice, symbolData.PriceFilter.MaxPrice, price.Value);
                    if (outputPrice != price)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter max/min failed. Original price: {price}, Closest allowed: {outputPrice}");
                        }

                        log.Write(LogVerbosity.Info, $"price clamped from {price} to {outputPrice}");
                    }
                }

                if (symbolData.PriceFilter.TickSize != 0)
                {
                    var beforePrice = outputPrice;
                    outputPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, price.Value);
                    if (outputPrice != beforePrice)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter tick failed. Original price: {price}, Closest allowed: {outputPrice}");
                        }

                        log.Write(LogVerbosity.Info, $"price rounded from {beforePrice} to {outputPrice}");
                    }
                }
            }

            if (symbolData.MinNotionalFilter == null)
            {
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);
            }

            var notional = quantity * price.Value;
            if (notional < symbolData.MinNotionalFilter.MinNotional)
            {
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: MinNotional filter failed. Order size: {notional}, minimal order size: {symbolData.MinNotionalFilter.MinNotional}");
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);
        }

        #endregion
    }
}