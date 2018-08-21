using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Binance.Net.Objects;
using Binance.Net.Converters;
using Binance.Net.Interfaces;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance REST Api
    /// </summary>
    public class BinanceClient : ExchangeClient, IBinanceClient
    {
        #region fields 
        private static BinanceClientOptions defaultOptions = new BinanceClientOptions();
        private static BinanceClientOptions DefaultOptions => new BinanceClientOptions()
        {
            ApiCredentials = new ApiCredentials(defaultOptions.ApiCredentials.Key.GetString(), defaultOptions.ApiCredentials.Secret.GetString()),
            AutoTimestamp = defaultOptions.AutoTimestamp,
            LogVerbosity = defaultOptions.LogVerbosity,
            BaseAddress = defaultOptions.BaseAddress,
            LogWriters = defaultOptions.LogWriters,
            Proxy = defaultOptions.Proxy,
            RateLimiters = defaultOptions.RateLimiters,
            TradeRulesBehaviour = defaultOptions.TradeRulesBehaviour,
            RateLimitingBehaviour = defaultOptions.RateLimitingBehaviour,
            TradeRulesUpdateInterval = defaultOptions.TradeRulesUpdateInterval
        };

        private bool autoTimestamp;
        private TradeRulesBehaviour tradeRulesBehaviour;
        private TimeSpan tradeRulesUpdateInterval;

        private double timeOffset;
        private bool timeSynced;
        private BinanceExchangeInfo exchangeInfo;
        private DateTime? lastExchangeInfoUpdate;
        
        // Addresses
        private string baseApiAddress;
        private const string Api = "api";
        private const string WithdrawalApi = "wapi";

        // Versions
        private const string PublicVersion = "1";
        private const string SignedVersion = "3";
        private const string UserDataStreamVersion = "1";
        private const string WithdrawalVersion = "3";

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

        // User stream
        private const string GetListenKeyEndpoint = "userDataStream";
        private const string KeepListenKeyAliveEndpoint = "userDataStream";
        private const string CloseListenKeyEndpoint = "userDataStream";

        // Withdrawing
        private const string WithdrawEndpoint = "withdraw.html";
        private const string DepositHistoryEndpoint = "depositHistory.html";
        private const string WithdrawHistoryEndpoint = "withdrawHistory.html";
        private const string DepositAddressEndpoint = "depositAddress.html";
        private const string WithdrawalFeeEndpoint = "withdrawFee.html";

        private const string AccountStatusEndpoint = "accountStatus.html";
        private const string SystemStatusEndpoint = "systemStatus.html";
        private const string DustLogEndpoint = "userAssetDribbletLog.html";

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClient(): this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceClient(BinanceClientOptions options): base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
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
        /// Synchronized version of the <see cref="PingAsync"/> method
        /// </summary>
        /// <returns></returns>
        public override CallResult<long> Ping() => PingAsync().Result;

        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if succesful ping, false if no response</returns>
        public override async Task<CallResult<long>> PingAsync()
        {
            var sw = Stopwatch.StartNew();
            var result = await ExecuteRequest<BinancePing>(GetUrl(PingEndpoint, Api, PublicVersion)).ConfigureAwait(false);
            sw.Stop();
            return new CallResult<long>(result.Error == null ? sw.ElapsedMilliseconds: 0, result.Error);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetServerTimeAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<DateTime> GetServerTime(bool resetAutoTimestamp = false) => GetServerTimeAsync(resetAutoTimestamp).Result;

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <returns>Server time</returns>
        public async Task<CallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false)
        {
            var url = GetUrl(CheckTimeEndpoint, Api, PublicVersion);
            if (!autoTimestamp)
            { 
                var result = await ExecuteRequest<BinanceCheckTime>(url).ConfigureAwait(false);
                return new CallResult<DateTime>(result.Data?.ServerTime ?? default(DateTime), result.Error);
            }
            else
            {
                var localTime = DateTime.UtcNow;
                var sw = Stopwatch.StartNew();
                var result = await ExecuteRequest<BinanceCheckTime>(url).ConfigureAwait(false);
                if (!result.Success)
                    return new CallResult<DateTime>(default(DateTime), result.Error);

                if (!timeSynced || resetAutoTimestamp)
                {
                    // Calculate time offset between local and server by taking the elapsed time request time / 2 (round trip)
                    timeOffset = ((result.Data.ServerTime - localTime).TotalMilliseconds) - sw.ElapsedMilliseconds / 2.0;
                    timeSynced = true;
                    log.Write(LogVerbosity.Info, $"Time offset set to {timeOffset}ms");
                }
                return new CallResult<DateTime>(result.Data.ServerTime, result.Error);
            }
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetExchangeInfoAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceExchangeInfo> GetExchangeInfo() => GetExchangeInfoAsync().Result;

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <returns>Exchange info</returns>
        public async Task<CallResult<BinanceExchangeInfo>> GetExchangeInfoAsync()
        {
            var exchangeInfoResult = await ExecuteRequest<BinanceExchangeInfo>(GetUrl(ExchangeInfoEndpoint, Api, PublicVersion)).ConfigureAwait(false);
            if (exchangeInfoResult.Success)
            {
                exchangeInfo = exchangeInfoResult.Data;
                lastExchangeInfoUpdate = DateTime.UtcNow;
                log.Write(LogVerbosity.Info, "Trade rules updated");
            }
            return exchangeInfoResult;
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetOrderBookAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceOrderBook> GetOrderBook(string symbol, int? limit = null) => GetOrderBookAsync(symbol, limit).Result;

        /// <summary>
        /// Gets the order book for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The order book for the symbol</returns>
        public async Task<CallResult<BinanceOrderBook>> GetOrderBookAsync(string symbol, int? limit = null)
        {
            var parameters = new Dictionary<string, object>() { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString());
            return await ExecuteRequest<BinanceOrderBook>(GetUrl(OrderBookEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAggregatedTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceAggregatedTrades[]> GetAggregatedTrades(string symbol, int? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null) => GetAggregatedTradesAsync(symbol, fromId, startTime, endTime, limit).Result;

        /// <summary>
        /// Gets compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
        /// </summary>
        /// <param name="symbol">The symbol to get the trades for</param>
        /// <param name="fromId">ID to get aggregate trades from INCLUSIVE.</param>
        /// <param name="startTime">Time to start getting trades from</param>
        /// <param name="endTime">Time to stop getting trades from</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The aggregated trades list for the symbol</returns>
        public async Task<CallResult<BinanceAggregatedTrades[]>> GetAggregatedTradesAsync(string symbol, int? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>() { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString());
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            parameters.AddOptionalParameter("limit", limit?.ToString());
            
            return await ExecuteRequest<BinanceAggregatedTrades[]>(GetUrl(AggregatedTradesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetRecentTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceRecentTrade[]> GetRecentTrades(string symbol, int? limit = null) => GetRecentTradesAsync(symbol, limit).Result;

        /// <summary>
        /// Gets the recent trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <returns>List of recent trades</returns>
        public async Task<CallResult<BinanceRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null)
        {
            var parameters = new Dictionary<string, object>() { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString());
            return await ExecuteRequest<BinanceRecentTrade[]>(GetUrl(RecentTradesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetHistoricalTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceRecentTrade[]> GetHistoricalTrades(string symbol, int? limit = null, long? fromId = null) => GetHistoricalTradesAsync(symbol, limit, fromId).Result;

        /// <summary>
        /// Gets the historical  trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get recent trades for</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">From which trade id on results should be retrieved</param>
        /// <returns>List of recent trades</returns>
        public async Task<CallResult<BinanceRecentTrade[]>> GetHistoricalTradesAsync(string symbol, int? limit = null, long? fromId = null)
        {
            var parameters = new Dictionary<string, object>() { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString());

            return await ExecuteRequest<BinanceRecentTrade[]>(GetUrl(HistoricalTradesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetKlinesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceKline[]> GetKlines(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null) => GetKlinesAsync(symbol, interval, startTime, endTime, limit).Result;

        /// <summary>
        /// Get candlestick data for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="interval">The candlestick timespan</param>
        /// <param name="startTime">Start time to get candlestick data</param>
        /// <param name="endTime">End time to get candlestick data</param>
        /// <param name="limit">Max number of results</param>
        /// <returns>The candlestick data for the provided symbol</returns>
        public async Task<CallResult<BinanceKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>() {
                { "symbol", symbol },
                { "interval", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)) },
            };
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            parameters.AddOptionalParameter("limit", limit?.ToString());

            return await ExecuteRequest<BinanceKline[]>(GetUrl(KlinesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="Get24HPriceAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<Binance24HPrice> Get24HPrice(string symbol) => Get24HPriceAsync(symbol).Result;

        /// <summary>
        /// Get data regarding the last 24 hours for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <returns>Data over the last 24 hours</returns>
        public async Task<CallResult<Binance24HPrice>> Get24HPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };

            return await ExecuteRequest<Binance24HPrice>(GetUrl(Price24HEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="Get24HPricesListAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<Binance24HPrice[]> Get24HPricesList() => Get24HPricesListAsync().Result;

        /// <summary>
        /// Get data regarding the last 24 hours for all symbols
        /// </summary>
        /// <returns>List of data over the last 24 hours</returns>
        public async Task<CallResult<Binance24HPrice[]>> Get24HPricesListAsync()
        {
            return await ExecuteRequest<Binance24HPrice[]>(GetUrl(Price24HEndpoint, Api, PublicVersion)).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinancePrice> GetPrice(string symbol) => GetPriceAsync(symbol).Result;

        /// <summary>
        /// Gets the price of a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the price for</param>
        /// <returns>Price of symbol</returns>
        public async Task<CallResult<BinancePrice>> GetPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };

            return await ExecuteRequest<BinancePrice>(GetUrl(AllPricesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinancePrice[]> GetAllPrices() => GetAllPricesAsync().Result;

        /// <summary>
        /// Get a list of the prices of all symbols
        /// </summary>
        /// <returns>List of prices</returns>
        public async Task<CallResult<BinancePrice[]>> GetAllPricesAsync()
        {
            return await ExecuteRequest<BinancePrice[]>(GetUrl(AllPricesEndpoint, Api, PublicVersion)).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetBookPriceAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceBookPrice> GetBookPrice(string symbol) => GetBookPriceAsync(symbol).Result;

        /// <summary>
        /// Gets the best price/qantity on the order book for a symbol.
        /// </summary>
        /// <returns>List of book prices</returns>
        public async Task<CallResult<BinanceBookPrice>> GetBookPriceAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };

            return await ExecuteRequest<BinanceBookPrice>(GetUrl(BookPricesEndpoint, Api, PublicVersion), GetMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllBookPricesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceBookPrice[]> GetAllBookPrices() => GetAllBookPricesAsync().Result;

        /// <summary>
        /// Gets the best price/qantity on the order book for all symbols.
        /// </summary>
        /// <returns>List of book prices</returns>
        public async Task<CallResult<BinanceBookPrice[]>> GetAllBookPricesAsync()
        {
            return await ExecuteRequest<BinanceBookPrice[]>(GetUrl(BookPricesEndpoint, Api, PublicVersion)).ConfigureAwait(false);
        }
        #endregion

        #region signed
        /// <summary>
        /// Synchronized version of the <see cref="GetOpenOrdersAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceOrder[]> GetOpenOrders(string symbol = null, int? receiveWindow = null) => GetOpenOrdersAsync(symbol, receiveWindow).Result;

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of open orders</returns>
        public async Task<CallResult<BinanceOrder[]>> GetOpenOrdersAsync(string symbol = null, int? receiveWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString());
            parameters.AddOptionalParameter("symbol", symbol);

            return await ExecuteRequest<BinanceOrder[]>(GetUrl(OpenOrdersEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAllOrdersAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceOrder[]> GetAllOrders(string symbol, long? orderId = null, int? limit = null, int? receiveWindow = null) => GetAllOrdersAsync(symbol, orderId, limit, receiveWindow).Result;

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of orders</returns>
        public async Task<CallResult<BinanceOrder[]>> GetAllOrdersAsync(string symbol, long? orderId = null, int? limit = null, int? receiveWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString());
            parameters.AddOptionalParameter("limit", limit?.ToString());

            return await ExecuteRequest<BinanceOrder[]>(GetUrl(AllOrdersEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="PlaceOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinancePlacedOrder> PlaceOrder(
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
        public async Task<CallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
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
            await CheckAutoTimestamp().ConfigureAwait(false);

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, type).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage);
                return new CallResult<BinancePlacedOrder>(null, new ArgumentError(rulesCheck.ErrorMessage));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;

            var parameters = new Dictionary<string, object>()
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
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewOrderEndpoint, Api, SignedVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Synchronized version of the <see cref="PlaceTestOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinancePlacedOrder> PlaceTestOrder(string symbol,
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
        public async Task<CallResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol,
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
            await CheckAutoTimestamp().ConfigureAwait(false);

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, type).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage);
                return new CallResult<BinancePlacedOrder>(null, new ArgumentError(rulesCheck.ErrorMessage));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;

            var parameters = new Dictionary<string, object>()
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
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinancePlacedOrder>(GetUrl(NewTestOrderEndpoint, Api, SignedVersion), PostMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="QueryOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceOrder> QueryOrder(string symbol, long? orderId = null, string origClientOrderId = null, long? recvWindow = null) => QueryOrderAsync(symbol, orderId, origClientOrderId, recvWindow).Result;

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The specific order</returns>
        public async Task<CallResult<BinanceOrder>> QueryOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, long? recvWindow = null)
        {
            if (orderId == null && origClientOrderId == null)
                return new CallResult<BinanceOrder>(null, new ArgumentError("Either orderId or origClientOrderId should be provided"));

            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());
            
            return await ExecuteRequest<BinanceOrder>(GetUrl(QueryOrderEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="CancelOrderAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceCanceledOrder> CancelOrder(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? recvWindow = null) => CancelOrderAsync(symbol, orderId, origClientOrderId, newClientOrderId, recvWindow).Result;

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<CallResult<BinanceCanceledOrder>> CancelOrderAsync(string symbol, long? orderId = null, string origClientOrderId = null, string newClientOrderId = null, long? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString());
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());
            
            return await ExecuteRequest<BinanceCanceledOrder>(GetUrl(CancelOrderEndpoint, Api, SignedVersion), DeleteMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAccountInfoAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceAccountInfo> GetAccountInfo(long? recvWindow = null) => GetAccountInfoAsync(recvWindow).Result;

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The account information</returns>
        public async Task<CallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            return await ExecuteRequest<BinanceAccountInfo>(GetUrl(AccountInfoEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetMyTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceTrade[]> GetMyTrades(string symbol, int? limit = null, long? fromId = null, long? recvWindow = null) => GetMyTradesAsync(symbol, limit, fromId, recvWindow).Result;

        /// <summary>
        /// Gets all user trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of trades</returns>
        public async Task<CallResult<BinanceTrade[]>> GetMyTradesAsync(string symbol, int? limit = null, long? fromId = null, long? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString(CultureInfo.InvariantCulture));

            return await ExecuteRequest<BinanceTrade[]>(GetUrl(MyTradesEndpoint, Api, SignedVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="WithdrawAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceWithdrawalPlaced> Withdraw(string asset, string address, decimal amount, string addressTag = null, string name = null, int? recvWindow = null) => WithdrawAsync(asset, address,amount, addressTag, name, recvWindow).Result;

        /// <summary>
        /// Withdraw assets from Binance to an address
        /// </summary>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="address">The address to send the funds to</param>
        /// <param name="addressTag">Secondary address identifier for coins like XRP,XMR etc.</param>
        /// <param name="amount">The amount to withdraw</param>
        /// <param name="name">Name for the transaction</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal confirmation</returns>
        public async Task<CallResult<BinanceWithdrawalPlaced>> WithdrawAsync(string asset, string address, decimal amount, string addressTag = null, string name = null, int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "asset", asset },
                { "address", address },
                { "amount", amount.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("name", name);
            parameters.AddOptionalParameter("addressTag", addressTag);
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceWithdrawalPlaced>(GetUrl(WithdrawEndpoint, WithdrawalApi, WithdrawalVersion), PostMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return result;

            if (!result.Data.Success)
                result = new CallResult<BinanceWithdrawalPlaced>(null, ParseErrorResponse(result.Data.Message));
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetDepositHistoryAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceDepositList> GetDepositHistory(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null) => GetDepositHistoryAsync(asset, status, startTime, endTime, recvWindow).Result;

        /// <summary>
        /// Gets the deposit history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of deposits</returns>
        public async Task<CallResult<BinanceDepositList>> GetDepositHistoryAsync(string asset = null, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new DepositStatusConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceDepositList>(GetUrl(DepositHistoryEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return result;

            if (!result.Data.Success)
                result = new CallResult<BinanceDepositList>(null, ParseErrorResponse(result.Data.Message));
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetWithdrawHistoryAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceWithdrawalList> GetWithdrawHistory(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null) => GetWithdrawHistoryAsync(asset, status, startTime, endTime, recvWindow).Result;

        /// <summary>
        /// Gets the withdrawal history
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter start time from</param>
        /// <param name="endTime">Filter end time till</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>List of withdrawals</returns>
        public async Task<CallResult<BinanceWithdrawalList>> GetWithdrawHistoryAsync(string asset = null, WithdrawalStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "timestamp", GetTimestamp() }
            };

            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("status", status != null ? JsonConvert.SerializeObject(status, new WithdrawalStatusConverter(false)): null);
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString(CultureInfo.InvariantCulture) : null);
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceWithdrawalList>(GetUrl(WithdrawHistoryEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return result;
            
            if (!result.Data.Success)
                result = new CallResult<BinanceWithdrawalList>(null, ParseErrorResponse(result.Data.Message));
            return result;
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetDepositAddressAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceDepositAddress> GetDepositAddress(string asset, int? recvWindow = null) => GetDepositAddressAsync(asset, recvWindow).Result;

        /// <summary>
        /// Gets the deposit address for an asset
        /// </summary>
        /// <param name="asset">Asset to get address for</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Deposit address</returns>
        public async Task<CallResult<BinanceDepositAddress>> GetDepositAddressAsync(string asset, int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "asset", asset },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());
                
            return await ExecuteRequest<BinanceDepositAddress>(GetUrl(DepositAddressEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetWithdrawalFeeAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<decimal> GetWithdrawalFee(string asset, int? recvWindow = null) => GetWithdrawalFeeAsync(asset, recvWindow).Result;

        /// <summary>
        /// Gets the withdrawal fee for an asset
        /// </summary>
        /// <param name="asset">Asset to get withdrawal fee for</param>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Withdrawal fee</returns>
        public async Task<CallResult<decimal>> GetWithdrawalFeeAsync(string asset, int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "asset", asset },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceWithdrawalFee>(GetUrl(WithdrawalFeeEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return new CallResult<decimal>(0, result.Error);

            if (!result.Data.Success)
                return new CallResult<decimal>(0, ParseErrorResponse(result.Data.Message));
            return new CallResult<decimal>(result.Data.WithdrawFee, null);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetAccountStatusAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceAccountStatus> GetAccountStatus(int? recvWindow = null) => GetAccountStatusAsync(recvWindow).Result;

        /// <summary>
        /// Gets the status of the account associated with the apikey/secret
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>Account status</returns>
        public async Task<CallResult<BinanceAccountStatus>> GetAccountStatusAsync(int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceAccountStatus>(GetUrl(AccountStatusEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return new CallResult<BinanceAccountStatus>(null, result.Error);

            if (!result.Data.Success)
                return new CallResult<BinanceAccountStatus>(null, ParseErrorResponse(result.Data.Message));
            return new CallResult<BinanceAccountStatus>(result.Data, null);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetSystemStatusAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceSystemStatus> GetSystemStatus() => GetSystemStatusAsync().Result;

        /// <summary>
        /// Gets the status of the Binance platform
        /// </summary>
        /// <returns>The system status</returns>
        public async Task<CallResult<BinanceSystemStatus>> GetSystemStatusAsync()
        {
            return await ExecuteRequest<BinanceSystemStatus>(GetUrl(SystemStatusEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, null, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="GetDustLog"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceDustLog[]> GetDustLog(int? recvWindow = null) => GetDustLogAsync(recvWindow).Result;

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="recvWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <returns>The history of dust conversions</returns>
        public async Task<CallResult<BinanceDustLog[]>> GetDustLogAsync(int? recvWindow = null)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", recvWindow?.ToString());

            var result = await ExecuteRequest<BinanceDustLogListWrapper>(GetUrl(DustLogEndpoint, WithdrawalApi, WithdrawalVersion), GetMethod, parameters, true).ConfigureAwait(false);
            if (!result.Success)
                return new CallResult<BinanceDustLog[]>(null, result.Error);

            if (!result.Data.Success)
                return new CallResult<BinanceDustLog[]>(null, new ServerError("Unknown server error while requesting dust log"));
            return new CallResult<BinanceDustLog[]>(result.Data.Results.Rows, null);
        }

        /// <summary>
        /// Synchronized version of the <see cref="StartUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceListenKey> StartUserStream() => StartUserStreamAsync().Result;

        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in subsequent requests to <see cref="BinanceSocketClient.SubscribeToUserStream"/>. The stream will close after 60 minutes unless a keep alive is send.
        /// </summary>
        /// <returns>Listen key</returns>
        public async Task<CallResult<BinanceListenKey>> StartUserStreamAsync()
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            return await ExecuteRequest<BinanceListenKey>(GetUrl(GetListenKeyEndpoint, Api, UserDataStreamVersion), PostMethod).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="KeepAliveUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<object> KeepAliveUserStream(string listenKey) => KeepAliveUserStreamAsync(listenKey).Result;

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// </summary>
        /// <returns></returns>
        public async Task<CallResult<object>> KeepAliveUserStreamAsync(string listenKey)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);

            var parameters = new Dictionary<string, object>()
            {
                { "listenKey", listenKey },
            };

            return await ExecuteRequest<object>(GetUrl(KeepListenKeyAliveEndpoint, Api, UserDataStreamVersion), PutMethod, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="StopUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<object> StopUserStream(string listenKey) => StopUserStreamAsync(listenKey).Result;

        /// <summary>
        /// Stops the current user stream
        /// </summary>
        /// <returns></returns>
        public async Task<CallResult<object>> StopUserStreamAsync(string listenKey)
        {
            await CheckAutoTimestamp().ConfigureAwait(false);            

            var parameters = new Dictionary<string, object>()
            {
                { "listenKey", listenKey },
            };
            
            return await ExecuteRequest<object>(GetUrl(CloseListenKeyEndpoint, Api, UserDataStreamVersion), DeleteMethod, parameters).ConfigureAwait(false); 
        }
        #endregion      
        #endregion

        #region helpers

        private void Configure(BinanceClientOptions options)
        {
            base.Configure(options);
            if (options.ApiCredentials != null)
                SetAuthenticationProvider(new BinanceAuthenticationProvider(options.ApiCredentials));

            baseApiAddress = options.BaseAddress;
            autoTimestamp = options.AutoTimestamp;
            tradeRulesBehaviour = options.TradeRulesBehaviour;
            tradeRulesUpdateInterval = options.TradeRulesUpdateInterval;
        }

        protected override Error ParseErrorResponse(string error)
        {
            if(error == null)
                return new ServerError("Unknown error, no error message");

            var obj = JObject.Parse(error);
            if(!obj.ContainsKey("msg") && !obj.ContainsKey("code"))
                return new ServerError(error);

            if (obj.ContainsKey("msg") && !obj.ContainsKey("code"))
                return new ServerError((string)obj["msg"]);

            return new ServerError((int)obj["code"], (string)obj["msg"]);
        }

        private Uri GetUrl(string endpoint, string api, string version)
        {
            var result = $"{baseApiAddress}/{api}/v{version}/{endpoint}";
            return new Uri(result);
        }
        
        private long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        private string GetTimestamp()
        {
            var offset = autoTimestamp ? timeOffset : 0;
            return ToUnixTimestamp(DateTime.UtcNow.AddMilliseconds(offset)).ToString();
        }

        private async Task CheckAutoTimestamp()
        {
            if (autoTimestamp && !timeSynced)
                await GetServerTimeAsync().ConfigureAwait(false);
        }

        private async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal quantity, decimal? price, OrderType type)
        {
            decimal outputQuantity = quantity;
            decimal? outputPrice = price;

            if (tradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);

            if (exchangeInfo == null || lastExchangeInfoUpdate == null || (DateTime.UtcNow - lastExchangeInfoUpdate.Value).TotalMinutes > tradeRulesUpdateInterval.TotalMinutes)
                await GetExchangeInfoAsync().ConfigureAwait(false);
            
            if (exchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");

            var symbolData = exchangeInfo.Symbols.SingleOrDefault(s => s.Name.ToLower() == symbol.ToLower());
            if (symbolData == null)
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Symbol {symbol} not found");

            if(!symbolData.OrderTypes.Contains(type))
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: {type} order type not allowed for {symbol}");

            var lotSizeFilter = symbolData.Filters.OfType<BinanceSymbolLotSizeFilter>().SingleOrDefault();
            var minNotionalFilter = symbolData.Filters.OfType<BinanceSymbolMinNotionalFilter>().SingleOrDefault();
            var priceFilter = symbolData.Filters.OfType<BinanceSymbolPriceFilter>().SingleOrDefault();

            if (lotSizeFilter != null)
            {
                outputQuantity = BinanceHelpers.ClampQuantity(lotSizeFilter.MinQuantity, lotSizeFilter.MaxQuantity, lotSizeFilter.StepSize, quantity);
                if (outputQuantity != quantity)
                {
                    if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");

                    log.Write(LogVerbosity.Info, $"Quantity clamped from {quantity} to {outputQuantity}");
                }
            }
            
            if(price != null)
            {
                if (priceFilter != null)
                {
                    outputPrice = BinanceHelpers.ClampPrice(priceFilter.MinPrice, priceFilter.MaxPrice, priceFilter.TickSize, price.Value);
                    if (outputPrice != price)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter failed. Original price: {price}, Closest allowed: {outputPrice}");
                        log.Write(LogVerbosity.Info, $"price clamped from {price} to {outputPrice}");
                    }
                }

                if (minNotionalFilter != null)
                {
                    decimal notional = quantity * price.Value;
                    if (notional < minNotionalFilter.MinNotional)
                        return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: MinNotional filter failed. Order size: {notional}, minimal order size: {minNotionalFilter.MinNotional}");
                }                
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);
        }

        #endregion
    }
}
