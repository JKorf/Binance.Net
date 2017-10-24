using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocketSharp;
using Binance.Net.Objects;
using Binance.Net.Converters;
using Binance.Net.Implementations;
using Binance.Net.Interfaces;

namespace Binance.Net
{
    public static class BinanceClient
    {
        #region fields
        private static string key;
        private static HMACSHA256 encryptor;

        private static List<BinanceStream> sockets = new List<BinanceStream>();
        private static int lastStreamId;
        private static object streamIdLock = new object();
        private static string listenKey;
        private static Action<BinanceStreamAccountInfo> accountInfoCallback;
        private static Action<BinanceStreamOrderUpdate> orderUpdateCallback;

        private static double timeOffset;
        private static bool timeSynced;

        // Addresses
        private const string BaseApiAddress = "https://www.binance.com";
        private const string BaseWebsocketAddress = "wss://stream.binance.com:9443/ws/";
        private const string Api = "api";
        private const string WithdrawalApi = "wapi";

        // Versions
        private const string PublicVersion = "1";
        private const string SignedVersion = "3";
        private const string UserDataStreamVersion = "1";
        private const string WithdrawalVersion = "1";

        // Methods
        private const string GetMethod = "GET";
        private const string PostMethod = "POST";
        private const string DeleteMethod = "DELETE";
        private const string PutMethod = "PUT";

        // Public
        private const string PingEndpoint = "ping";
        private const string CheckTimeEndpoint = "time";
        private const string OrderBookEndpoint = "depth";
        private const string AggregatedTradesEndpoint = "aggTrades";
        private const string KlinesEndpoint = "klines";
        private const string Price24HEndpoint = "ticker/24hr";
        private const string AllPricesEndpoint = "ticker/allPrices";
        private const string BookPricesEndpoint = "ticker/allBookTickers";

        // Signed
        private const string OpenOrdersEndpoint = "openOrders";
        private const string AllOrdersEndpoint = "allOrders";
        private const string NewOrderEndpoint = "order";
        private const string NewTestOrderEndpoint = "order/test";
        private const string QueryOrderEndpoint = "order";
        private const string CancelOrderEndpoint = "order";
        private const string AccountInfoEndpoint = "account";
        private const string MyTradesEndpoint = "myTrades";

        // User stream
        private const string GetListenKeyEndpoint = "userDataStream";
        private const string KeepListenKeyAliveEndpoint = "userDataStream";
        private const string CloseListenKeyEndpoint = "userDataStream";
        private const string DepthStreamEndpoint = "@depth";
        private const string KlineStreamEndpoint = "@kline";
        private const string TradesStreamEndpoint = "@aggTrade";

        // Withdrawing
        private const string WithdrawEndpoint = "withdraw.html";
        private const string DepositHistoryEndpoint = "getDepositHistory.html";
        private const string WithdrawHistoryEndpoint = "getWithdrawHistory.html";
        #endregion

        #region properties
        public static bool AutoTimestamp { get; set; } = false;
        public static IRequestFactory RequestFactory { get; set; } = new RequestFactory();
        #endregion

        #region methods
        /// <summary>
        /// Sets the API credentials to use. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret associated with the key</param>
        public static void SetAPICredentials(string apiKey, string apiSecret)
        {
            SetAPIKey(apiKey);
            SetAPISecret(apiSecret);
        }

        /// <summary>
        /// Sets the API Key. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        public static void SetAPIKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("Api key empty");

            key = apiKey;
        }

        /// <summary>
        /// Sets the API Secret. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiSecret">The api secret</param>
        public static void SetAPISecret(string apiSecret)
        {
            if (string.IsNullOrEmpty(apiSecret))
                throw new ArgumentException("Api secret empty");

            encryptor = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
        }

        #region public
        /// <summary>
        /// Synchronized version of the <see cref="PingAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<bool> Ping() => PingAsync().Result;

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if succesful ping, false if no response</returns>
        public static async Task<ApiResult<bool>> PingAsync()
        {
            var result = await ExecuteRequest<BinancePing>(GetUrl(PingEndpoint, Api, PublicVersion));
            return new ApiResult<bool>() { Success = result.Success, Data = result.Data != null };
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetServerTimeAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<DateTime> GetServerTime() => GetServerTimeAsync().Result;

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <returns>Server time</returns>
        public static async Task<ApiResult<DateTime>> GetServerTimeAsync()
        {
            var url = GetUrl(CheckTimeEndpoint, Api, PublicVersion);
            if (!AutoTimestamp)
            { 
                var result = await ExecuteRequest<BinanceCheckTime>(url);
                return new ApiResult<DateTime>() { Success = result.Success, Data = result.Data?.ServerTime ?? default(DateTime) };
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var sw = Stopwatch.StartNew();
                var result = await ExecuteRequest<BinanceCheckTime>(url);
                if (!result.Success)
                    return new ApiResult<DateTime>() { Success = false, Error = result.Error };

                // Calculate time offset between local and server by taking the elapsed time request time / 2 (round trip)
                timeOffset = ((result.Data.ServerTime - localTime).TotalMilliseconds) - sw.ElapsedMilliseconds / 2;
                timeSynced = true;
                Log.Write(LogLevel.Debug, $"Time offset set to {timeOffset}");
                return new ApiResult<DateTime>() { Success = result.Success, Data = result.Data.ServerTime };
            }
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetOrderBookAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceOrderBook> GetOrderBook(string symbol, int? limit = null) => GetOrderBookAsync(symbol, limit).Result;

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The order book for the symbol</returns>
        public static async Task<ApiResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null)
        {
            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>() { { "symbol", symbol } };

            AddOptionalParameter(parameters, "limit", limit?.ToString());

            return await ExecuteRequest<BinanceOrderBook>(GetUrl(OrderBookEndpoint, Api, PublicVersion, parameters));
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAggregatedTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceAggregatedTrades[]> GetAggregatedTrades(string symbol, int? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null) => GetAggregatedTradesAsync(symbol, fromId, startTime, endTime, limit).Result;

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        public static async Task<ApiResult<BinanceAggregatedTrades[]>> GetAggregatedTradesAsync(string symbol, int? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>() { { "symbol", symbol } };

            AddOptionalParameter(parameters, "fromId", fromId?.ToString());
            AddOptionalParameter(parameters, "startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "limit", limit?.ToString());
            
            return await ExecuteRequest<BinanceAggregatedTrades[]>(GetUrl(AggregatedTradesEndpoint, Api, PublicVersion, parameters));
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetKlinesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceKline[]> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null) => GetKlinesAsync(symbol, interval, startTime, endTime, limit).Result;

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public static async Task<ApiResult<BinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>() {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter()) },
            };

            AddOptionalParameter(parameters, "startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "limit", limit?.ToString());

            return await ExecuteRequest<BinanceKline[]>(GetUrl(KlinesEndpoint, Api, PublicVersion, parameters));
        }

        /// <summary>
        /// Synchronized version of the <see cref="Get24HPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<Binance24hPrice> Get24HPrices(string symbol) => Get24HPricesAsync(symbol).Result;

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <returns>Data over the last 24 hours</returns>
        public static async Task<ApiResult<Binance24hPrice>> Get24HPricesAsync(string symbol)
        {
            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>() { { "symbol", symbol } };
            return await ExecuteRequest<Binance24hPrice>(GetUrl(Price24HEndpoint, Api, PublicVersion, parameters));
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinancePrice[]> GetAllPrices() => GetAllPricesAsync().Result;

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <returns>List of prices</returns>
        public static async Task<ApiResult<BinancePrice[]>> GetAllPricesAsync()
        {
            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            return await ExecuteRequest<BinancePrice[]>(GetUrl(AllPricesEndpoint, Api, PublicVersion));
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllBookPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceBookPrice[]> GetAllBookPrices() => GetAllBookPricesAsync().Result;

        /// <summary>
        /// Gets the best price/qantity on the order book for all symbols.
        /// </summary>
        /// <returns>List of book prices</returns>
        public static async Task<ApiResult<BinanceBookPrice[]>> GetAllBookPricesAsync()
        {
            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            return await ExecuteRequest<BinanceBookPrice[]>(GetUrl(BookPricesEndpoint, Api, PublicVersion));
        }
        #endregion

        #region signed
        /// <summary>
        /// Synchronized version of the <see cref="GetOpenOrdersAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceOrder[]> GetOpenOrders(string symbol, int? receiveWindow = null) => GetOpenOrdersAsync(symbol, receiveWindow).Result;

        /// <summary>
        /// Gets a list of open orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of open orders</returns>
        public static async Task<ApiResult<BinanceOrder[]>> GetOpenOrdersAsync(string symbol, int? receiveWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceOrder[]>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "recvWindow", receiveWindow?.ToString());

            return await ExecuteRequest<BinanceOrder[]>(GetUrl(OpenOrdersEndpoint, Api, SignedVersion, parameters), true);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllOrdersAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceOrder[]> GetAllOrders(string symbol, int? orderId = null, int? limit = null, int? receiveWindow = null) => GetAllOrdersAsync(symbol, orderId, limit, receiveWindow).Result;

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of orders</returns>
        public static async Task<ApiResult<BinanceOrder[]>> GetAllOrdersAsync(string symbol, int? orderId = null, int? limit = null, int? receiveWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceOrder[]>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "orderId", orderId?.ToString());
            AddOptionalParameter(parameters, "recvWindow", receiveWindow?.ToString());
            AddOptionalParameter(parameters, "limit", limit?.ToString());

            return await ExecuteRequest<BinanceOrder[]>(GetUrl(AllOrdersEndpoint, Api, SignedVersion, parameters), true);
        }

        /// <summary>
        /// Synchronized version of the <see cref="PlaceOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinancePlacedOrder> PlaceOrder(string symbol, OrderSide side, OrderType type, TimeInForce timeInForce, double quantity, double price, string newClientOrderId = null, double? stopPrice = null, double? icebergQty = null) => PlaceOrderAsync(symbol, side, type, timeInForce, quantity, price, newClientOrderId, stopPrice, icebergQty).Result;

        /// <summary>
        /// Places a new order
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
        /// <returns>Id's for the placed order</returns>
        public static async Task<ApiResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, TimeInForce timeInForce, double quantity, double price, string newClientOrderId = null, double? stopPrice = null, double? icebergQty = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinancePlacedOrder>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter()) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter()) },
                { "timeInForce", JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter()) },
                { "quantity", quantity.ToString() },
                { "price", price.ToString() },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "newClientOrderId", newClientOrderId ?? null);
            AddOptionalParameter(parameters, "stopPrice", stopPrice?.ToString());
            AddOptionalParameter(parameters, "icebergQty", icebergQty?.ToString());

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewOrderEndpoint, Api, SignedVersion, parameters), true, PostMethod);
        }

        /// <summary>
        /// Synchronized version of the <see cref="PlaceTestOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinancePlacedOrder> PlaceTestOrder(string symbol, OrderSide side, OrderType type, TimeInForce timeInForce, double quantity, double price, string newClientOrderId = null, double? stopPrice = null, double? icebergQty = null) => PlaceTestOrderAsync(symbol, side, type, timeInForce, quantity, price, newClientOrderId, stopPrice, icebergQty).Result;
        
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
        /// <returns>Id's for the placed test order</returns>
        public static async Task<ApiResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol, OrderSide side, OrderType type, TimeInForce timeInForce, double quantity, double price, string newClientOrderId = null, double? stopPrice = null, double? icebergQty = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinancePlacedOrder>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter()) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter()) },
                { "timeInForce", JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter()) },
                { "quantity", quantity.ToString() },
                { "price", price.ToString() },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "newClientOrderId", newClientOrderId ?? null);
            AddOptionalParameter(parameters, "stopPrice", stopPrice?.ToString());
            AddOptionalParameter(parameters, "icebergQty", icebergQty?.ToString());

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewTestOrderEndpoint, Api, SignedVersion, parameters), true, PostMethod);
        }

        /// <summary>
        /// Synchronized version of the <see cref="QueryOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceOrder> QueryOrder(string symbol, long? orderId = null, string origClientOrderId = null, long? recvWindow = null) => QueryOrderAsync(symbol, orderId, origClientOrderId, recvWindow).Result;

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The specific order</returns>
        public static async Task<ApiResult<BinanceOrder>> QueryOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceOrder>("No api credentials provided, can't request private endpoints");

            if (orderId == null && origClientOrderId == null)
                return ThrowErrorMessage<BinanceOrder>("Either orderId or origClientOrderId is required");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "orderId", orderId?.ToString());
            AddOptionalParameter(parameters, "origClientOrderId", origClientOrderId ?? null);
            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());
            
            return await ExecuteRequest<BinanceOrder>(GetUrl(QueryOrderEndpoint, Api, SignedVersion, parameters), true, GetMethod);
        }

        /// <summary>
        /// Synchronized version of the <see cref="CancelOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinancePlacedOrder> CancelOrder(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? recvWindow = null) => CancelOrderAsync(symbol, orderId, origClientOrderId, newClientOrderId, recvWindow).Result;

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        public static async Task<ApiResult<BinancePlacedOrder>> CancelOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinancePlacedOrder>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync(); 

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "orderId", orderId?.ToString());
            AddOptionalParameter(parameters, "origClientOrderId", origClientOrderId ?? null);
            AddOptionalParameter(parameters, "newClientOrderId", newClientOrderId ?? null);
            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());
            
            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(CancelOrderEndpoint, Api, SignedVersion, parameters), true, DeleteMethod);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAccountInfoAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceAccountInfo> GetAccountInfo(long? recvWindow = null) => GetAccountInfoAsync(recvWindow).Result;

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The account information</returns>
        public static async Task<ApiResult<BinanceAccountInfo>> GetAccountInfoAsync(long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceAccountInfo>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());

            return await ExecuteRequest<BinanceAccountInfo>(GetUrl(AccountInfoEndpoint, Api, SignedVersion, parameters), true, GetMethod);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetMyTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceTrade[]> GetMyTrades(string symbol, int? limit = null, long? fromId = null, long? recvWindow = null) => GetMyTradesAsync(symbol, limit, fromId, recvWindow).Result;

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of trades</returns>
        public static async Task<ApiResult<BinanceTrade[]>> GetMyTradesAsync(string symbol, int? limit = null, long? fromId = null, long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceTrade[]>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "limit", limit?.ToString());
            AddOptionalParameter(parameters, "fromId", fromId?.ToString());
            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());

            return await ExecuteRequest<BinanceTrade[]>(GetUrl(MyTradesEndpoint, Api, SignedVersion, parameters), true, GetMethod);
        }

        /// <summary>
        /// Synchronized version of the <see cref="WithdrawAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceWithdrawalPlaced> Withdraw(string asset, string address, double amount, string name = null, long? recvWindow = null) => WithdrawAsync(asset, address, amount, name, recvWindow).Result;

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="name">Name for the transaction</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal confirmation</returns>
        public static async Task<ApiResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, double amount, string name = null, long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceWithdrawalPlaced>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "asset", asset },
                { "address", address },
                { "amount", amount.ToString() },
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "name", name);
            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());    

            var result = await ExecuteRequest<BinanceWithdrawalPlaced>(GetUrl(WithdrawEndpoint, WithdrawalApi, WithdrawalVersion, parameters), true, PostMethod);
            if (!result.Success || result.Data == null)
                return result;

            result.Success = result.Data.Success;
            if (!result.Success)
                result.Error = new BinanceError() { Message = result.Data.Message };
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetDepositHistoryAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceDepositList> GetDepositHistory(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, long? recvWindow = null) => GetDepositHistoryAsync(asset, status, startTime, endTime, recvWindow).Result;

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of deposits</returns>
        public static async Task<ApiResult<BinanceDepositList>> GetDepositHistoryAsync(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceDepositList>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "asset", asset);
            AddOptionalParameter(parameters, "status", status != null ? JsonConvert.SerializeObject(status, new DepositStatusConverter()): null);
            AddOptionalParameter(parameters, "startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceDepositList>(GetUrl(DepositHistoryEndpoint, WithdrawalApi, WithdrawalVersion, parameters), true, PostMethod);
            if (!result.Success || result.Data == null)
                return result;

            result.Success = result.Data.Success;
            if (!result.Success)
                result.Error = new BinanceError() { Message = result.Data.Message };
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetWithdrawHistoryAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceWithdrawalList> GetWithdrawHistory(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, long? recvWindow = null) => GetWithdrawHistoryAsync(asset, status, startTime, endTime, recvWindow).Result;

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of withdrawals</returns>
        public static async Task<ApiResult<BinanceWithdrawalList>> GetWithdrawHistoryAsync(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, long? recvWindow = null)
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceWithdrawalList>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "timestamp", GetTimestamp() }
            };

            AddOptionalParameter(parameters, "asset", asset);
            AddOptionalParameter(parameters, "status", status != null ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter()): null);
            AddOptionalParameter(parameters, "startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            AddOptionalParameter(parameters, "recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceWithdrawalList>(GetUrl(WithdrawHistoryEndpoint, WithdrawalApi, WithdrawalVersion, parameters), true, PostMethod);
            if (!result.Success || result.Data == null)
                return result;

            result.Success = result.Data.Success;
            if (!result.Success)
                result.Error = new BinanceError() { Message = result.Data.Message };
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="StartUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<BinanceListenKey> StartUserStream() => StartUserStreamAsync().Result;

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key will be used in subsequent requests to <see cref="StartAccountUpdateStream"/> and <see cref="StartOrderUpdateStream"/>
        /// </summary>
        /// <returns>Listen key</returns>
        public static async Task<ApiResult<BinanceListenKey>> StartUserStreamAsync()
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<BinanceListenKey>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var result = await ExecuteRequest<BinanceListenKey>(GetUrl(GetListenKeyEndpoint, Api, UserDataStreamVersion), false, PostMethod);
            listenKey = result.Data.ListenKey;
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="KeepAliveUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<object> KeepAliveUserStream() => KeepAliveUserStreamAsync().Result;

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to prevent timeouts
        /// </summary>
        /// <returns></returns>
        public static async Task<ApiResult<object>> KeepAliveUserStreamAsync()
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<object>("No api credentials provided, can't request private endpoints");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "listenKey", listenKey },
            };

            return await ExecuteRequest<object>(GetUrl(KeepListenKeyAliveEndpoint, Api, UserDataStreamVersion, parameters), false, PutMethod); ;
        }

        /// <summary>
        /// Synchronized version of the <see cref="StopUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public static ApiResult<object> StopUserStream() => StopUserStreamAsync().Result;

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <returns></returns>
        public static async Task<ApiResult<object>> StopUserStreamAsync()
        {
            if (key == null || encryptor == null)
                return ThrowErrorMessage<object>("No api credentials provided, can't request private endpoints");

            if (listenKey == null)
                return ThrowErrorMessage<object>("No user stream open, can't close");

            if (AutoTimestamp && !timeSynced)
                await GetServerTimeAsync();

            var parameters = new Dictionary<string, string>()
            {
                { "listenKey", listenKey },
            };

            var result = await ExecuteRequest<object>(GetUrl(CloseListenKeyEndpoint, Api, UserDataStreamVersion, parameters), false, DeleteMethod);
            listenKey = null;
            return result;
        }
        #endregion

        #region websocket
        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>Returns a <see cref="BinanceStreamConnection"/> object which contains a success flag and a stream id. This stream id can be used to close this 
        /// specific stream using the <see cref="StopStream(int)"/> method</returns>
        public static BinanceStreamConnection SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKline> onMessage)
        {
            var socket = CreateSocket(BaseWebsocketAddress + symbol + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter()));
            if (socket == null)
                return new BinanceStreamConnection() { Succes = false };

            socket.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamKline>(s.Data));

            Log.Write(LogLevel.Debug, $"Started kline stream for {symbol}: {interval}");
            return new BinanceStreamConnection() { StreamId = socket.StreamId, Succes = true };            
        }

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>Returns a <see cref="BinanceStreamConnection"/> object which contains a success flag and a stream id. This stream id can be used to close this 
        /// specific stream using the <see cref="StopStream(int)"/> method</returns>
        public static BinanceStreamConnection SubscribeToDepthStream(string symbol, Action<BinanceStreamDepth> onMessage)
        {
            var socket = CreateSocket(BaseWebsocketAddress + symbol + DepthStreamEndpoint);
            if (socket == null)
                return new BinanceStreamConnection() { Succes = false };

            socket.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamDepth>(s.Data));

            Log.Write(LogLevel.Debug, $"Started depth stream for {symbol}");
            return new BinanceStreamConnection() { StreamId = socket.StreamId, Succes = true };
        }

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>Returns a <see cref="BinanceStreamConnection"/> object which contains a success flag and a stream id. This stream id can be used to close this 
        /// specific stream using the <see cref="StopStream(int)"/> method</returns>
        public static BinanceStreamConnection SubscribeToTradesStream(string symbol, Action<BinanceStreamTrade> onMessage)
        {
            var socket = CreateSocket(BaseWebsocketAddress + symbol + TradesStreamEndpoint);
            if (socket == null)
                return new BinanceStreamConnection() { Succes = false };

            socket.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTrade>(s.Data));

            Log.Write(LogLevel.Debug, $"Started trade stream for {symbol}");
            return new BinanceStreamConnection() { StreamId = socket.StreamId, Succes = true };
        }

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="onMessage">The event handler for the data received</param>
        /// <returns>bool indicating success</returns>
        public static ApiResult<bool> SubscribeToAccountUpdateStream(Action<BinanceStreamAccountInfo> onMessage)
        {
            if (listenKey == null)
                return ThrowErrorMessage<bool>("Cannot start stream without listen key. Call the StartUserStream function and try again");

            accountInfoCallback = onMessage;

            return new ApiResult<bool>() { Success = CreateUserStream() };
        }

        /// <summary>
        /// Subscribes to the order update stream. Prior to using this, the <see cref="StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="onMessage">The event handler for the data received</param>
        /// <returns>bool indicating success</returns>
        public static ApiResult<bool> SubscribeToOrderUpdateStream(Action<BinanceStreamOrderUpdate> onMessage)
        {
            if (listenKey == null)
                return ThrowErrorMessage<bool>("Cannot start stream without listen key. Call the StartUserStream function and try again");

            orderUpdateCallback = onMessage;
            return new ApiResult<bool>() { Success = CreateUserStream() };
        }

        /// <summary>
        /// Unsubscribes from the account update stream
        /// </summary>
        /// <returns></returns>
        public static void UnsubscribeFromAccountUpdateStream()
        {
            accountInfoCallback = null;

            // Close the socket if we're not listening for anything
            if (orderUpdateCallback == null)
            {
                lock(sockets)
                    sockets.SingleOrDefault(s => s.UserStream)?.Socket.Close();
            }
        }
        
        /// <summary>
        /// Unsubscribes from the order update stream
        /// </summary>
        /// <returns></returns>
        public static void UnsubscribeFromOrderUpdateStream()
        {
            orderUpdateCallback = null;
            
            // Close the socket if we're not listening for anything
            if (accountInfoCallback == null)
            {
                lock(sockets)
                    sockets.SingleOrDefault(s => s.UserStream)?.Socket.Close();
            }
        }

        /// <summary>
        /// Unsubscribes from a stream with the provided stream id
        /// </summary>
        /// <param name="streamId">SteamId of the stream</param>
        public static void UnsubscribeFromStream(int streamId)
        {
            lock(sockets)
                sockets.SingleOrDefault(s => s.StreamId == streamId)?.Socket.Close();
        }
        
        private static void OnUserMessage(string data)
        {
            if (data.Contains("outboundAccountInfo"))
                accountInfoCallback?.Invoke(JsonConvert.DeserializeObject<BinanceStreamAccountInfo>(data));
            else if (data.Contains("executionReport"))
                orderUpdateCallback?.Invoke(JsonConvert.DeserializeObject<BinanceStreamOrderUpdate>(data));
        }

        private static bool CreateUserStream()
        {
            lock (sockets)
                if (sockets.Any(s => s.UserStream))
                    return true; 

            var socket = CreateSocket(BaseWebsocketAddress + listenKey);
            if (socket == null)
                return false;

            socket.UserStream = true;
            socket.Socket.OnMessage += (o, s) => OnUserMessage(s.Data);
            Log.Write(LogLevel.Debug, $"User stream started");
            return true;
        }


        private static BinanceStream CreateSocket(string url)
        {
            try
            {
                var socket = new WebSocket(url);
                socket.OnClose += Socket_OnClose;
                socket.OnError += Socket_OnError;
                socket.OnOpen += Socket_OnOpen;
                socket.Connect();
                var socketObject = new BinanceStream() { Socket = socket, StreamId = NextStreamId() };
                lock (sockets)
                    sockets.Add(socketObject);
                return socketObject;
            }
            catch (Exception e)
            {
                Log.Write(LogLevel.Error, $"Couldn't open socket stream: {e.Message}");
                return null;
            }
        }

        private static void Socket_OnOpen(object sender, EventArgs e)
        {
            Log.Write(LogLevel.Debug, $"Socket opened to {((WebSocket)sender).Url}");
        }

        private static void Socket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            Log.Write(LogLevel.Error, $"Socket error {e.Message}");
        }

        private static void Socket_OnClose(object sender, CloseEventArgs e)
        {
            Log.Write(LogLevel.Debug, $"Socket closed");
            lock(sockets)
                sockets.RemoveAll(s => s.Socket == (WebSocket)sender);
        }
        #endregion
        #endregion

        #region helpers
        private static ApiResult<T> ThrowErrorMessage<T>(string message)
        {
            Log.Write(LogLevel.Warning, $"Call failed: {message}");
            var result = (ApiResult<T>)Activator.CreateInstance(typeof(ApiResult<T>));
            result.Error = new BinanceError()
            {
                Message = message
            };
            return result;
        }

        private static void AddOptionalParameter(Dictionary<string, string> dictionary, string key, string value)
        {
            if (value != null)
                dictionary.Add(key, value);
        }
        
        private static Uri GetUrl(string endpoint, string api, string version, Dictionary<string, string> parameters = null)
        {
            var result = $"{BaseApiAddress}/{api}/v{version}/{endpoint}";
            if (parameters != null)
                result += $"?{string.Join("&", parameters.Select(s => $"{s.Key}={s.Value}"))}";
            return new Uri(result);
        }

        private static async Task<ApiResult<T>> ExecuteRequest<T>(Uri uri, bool signed = false, string method = GetMethod)
        {
            var apiResult = (ApiResult<T>)Activator.CreateInstance(typeof(ApiResult<T>));
            try
            {
                var uriString = uri.ToString();
                if (signed)
                    uriString += $"&signature={ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(uri.Query.Replace("?", ""))))}";

                var request = RequestFactory.Create(uriString);
                request.Headers.Add("X-MBX-APIKEY", key);
                request.Method = method;

                Log.Write(LogLevel.Debug, $"Sending {method} request to {uriString}");
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var data = await reader.ReadToEndAsync();
                    var result =  JsonConvert.DeserializeObject<T>(data);
                    apiResult.Success = true;
                    apiResult.Data = result;
                    return apiResult;
                }            
            }
            catch(WebException we)
            {
                var response = (HttpWebResponse)we.Response;
                if (((int)response.StatusCode) >= 400)
                {
                    try
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            var error = JsonConvert.DeserializeObject<BinanceError>(await reader.ReadToEndAsync());
                            apiResult.Success = false;
                            apiResult.Error = error;
                            Log.Write(LogLevel.Warning, $"Request to {uri} returned an error: {apiResult.Error.Code} - {apiResult.Error.Message}");
                            return apiResult;
                        }
                    }
                    catch(Exception)
                    {
                        Log.Write(LogLevel.Warning, $"Request to {uri} failed: " + we.Message);
                        return ExceptionToApiResult(apiResult, we);
                    }
                }

                Log.Write(LogLevel.Warning, $"Request to {uri} failed: " + we.Message);
                return ExceptionToApiResult(apiResult, we);
            }
            catch (Exception e)
            {
                Log.Write(LogLevel.Warning, $"Request to {uri} failed: " + e.Message);
                return ExceptionToApiResult(apiResult, e);
            }
        }

        private static ApiResult<T> ExceptionToApiResult<T>(ApiResult<T> apiResult, Exception e)
        {
            apiResult.Success = false;
            apiResult.Error = new BinanceError() { Code = 0, Message = e.Message };
            return apiResult;
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }

        private static long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        private static string GetTimestamp()
        {
            var offset = AutoTimestamp ? timeOffset : 0;
            return ToUnixTimestamp(DateTime.UtcNow.AddMilliseconds(offset)).ToString();
        }

        private static int NextStreamId()
        {
            lock(streamIdLock)
            {
                lastStreamId++;
                return lastStreamId;
            }
        }

        /// <summary>
        /// Sets the verbosity of the log messages
        /// </summary>
        /// <param name="level">Verbosity level</param>
        public static void SetVerbosity(LogLevel verbosity)
        {
            Log.Level = verbosity;
        }
        #endregion
    }
}
