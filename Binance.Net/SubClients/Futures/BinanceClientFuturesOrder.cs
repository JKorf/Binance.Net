using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Futures
{
    /// <summary>
    /// Futures order endpoints
    /// </summary>
    public abstract class BinanceClientFuturesOrder : IBinanceClientFuturesOrders
    {
        // Orders
        private const string newOrderEndpoint = "order";
        private const string multipleNewOrdersEndpoint = "batchOrders";
        private const string queryOrderEndpoint = "order";
        private const string cancelOrderEndpoint = "order";
        private const string cancelMultipleOrdersEndpoint = "batchOrders";
        private const string cancelAllOrdersEndpoint = "allOpenOrders";
        private const string openOrderEndpoint = "openOrder";
        private const string openOrdersEndpoint = "openOrders";
        private const string allOrdersEndpoint = "allOrders";
        private const string countDownCancelAllEndpoint = "countdownCancelAll";
        private const string forceOrdersEndpoint = "forceOrders";

        /// <summary>
        /// Api path
        /// </summary>
        protected abstract string Api { get; }
        /// <summary>
        /// Signed version
        /// </summary>
        protected const string SignedVersion = "1";

        /// <summary>
        /// Base client
        /// </summary>
        protected readonly BinanceClient BaseClient;
        /// <summary>
        /// Futures client
        /// </summary>
        protected readonly BinanceClientFutures FuturesClient;
        
        private readonly Log _log;

        internal BinanceClientFuturesOrder(Log log, BinanceClient baseClient, BinanceClientFutures futuresClient)
        {
            BaseClient = baseClient;
            FuturesClient = futuresClient;
            _log = log;
        }

        #region New Order

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill/GootTillCrossing)</param>
        /// <param name="positionSide">The position side</param>
        /// <param name="quantity">The amount of the base symbol</param>
        /// <param name="reduceOnly">Specify as true if the order is intended to only reduce the position</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">A unique id among open orders. Automatically generated if not sent.</param>
        /// <param name="stopPrice">Used with STOP/STOP_MARKET or TAKE_PROFIT/TAKE_PROFIT_MARKET orders.</param>
        /// <param name="activationPrice">Used with TRAILING_STOP_MARKET orders, default as the latest price（supporting different workingType)</param>
        /// <param name="callbackRate">Used with TRAILING_STOP_MARKET orders</param>
        /// <param name="closePosition">Close-All，used with STOP_MARKET or TAKE_PROFIT_MARKET.</param>
        /// <param name="workingType">stopPrice triggered by: "MARK_PRICE", "CONTRACT_PRICE"</param>
        /// <param name="orderResponseType">The response type. Default Acknowledge</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        public WebCallResult<BinanceFuturesPlacedOrder> PlaceOrder(
            string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity,
            PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            decimal? price = null,
            string? newClientOrderId = null,
            decimal? stopPrice = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? closePosition = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default) => PlaceOrderAsync(symbol, side, type, quantity, positionSide, timeInForce, reduceOnly, price, newClientOrderId, stopPrice, activationPrice, callbackRate, workingType, closePosition, orderResponseType, receiveWindow, ct).Result;

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the base symbol</param>
        /// <param name="positionSide">The position side</param>
        /// <param name="reduceOnly">Specify as true if the order is intended to only reduce the position</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="activationPrice">Used with TRAILING_STOP_MARKET orders, default as the latest price（supporting different workingType)</param>
        /// <param name="callbackRate">Used with TRAILING_STOP_MARKET orders</param>
        /// <param name="workingType">stopPrice triggered by: "MARK_PRICE", "CONTRACT_PRICE"</param>
        /// <param name="closePosition">Close-All，used with STOP_MARKET or TAKE_PROFIT_MARKET.</param>
        /// <param name="orderResponseType">The response type. Default Acknowledge</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        public async Task<WebCallResult<BinanceFuturesPlacedOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity,
            PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            decimal? price = null,
            string? newClientOrderId = null,
            decimal? stopPrice = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? closePosition = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (closePosition == true && positionSide != null)
            {
                if (positionSide == PositionSide.Short && side == OrderSide.Sell)
                    throw new ArgumentException("Can't close short position with order side sell");
                if (positionSide == PositionSide.Long && side == OrderSide.Buy)
                    throw new ArgumentException("Can't close long position with order side buy");
            }

            if(orderResponseType == OrderResponseType.Full)
                throw new ArgumentException("OrderResponseType.Full is not supported in Futures");


            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesPlacedOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await FuturesClient.CheckTradeRules(symbol, quantity, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinanceFuturesPlacedOrder>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("positionSide", positionSide == null ? null : JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("activationPrice", activationPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("callbackRate", callbackRate?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("workingType", workingType == null ? null : JsonConvert.SerializeObject(workingType, new WorkingTypeConverter(false)));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString().ToLower());
            parameters.AddOptionalParameter("closePosition", closePosition?.ToString().ToLower());
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesPlacedOrder>(FuturesClient.GetUrl(newOrderEndpoint, Api, SignedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    

        #endregion

        #region Multiple New Orders
        /// <summary>
        /// Place multiple orders in one call
        /// </summary>
        /// <param name="orders">The orders to place</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Returns a list of call results, one for each order. The order the results are in is the order the orders were sent</returns>
        public WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>> PlaceMultipleOrders(
            BinanceFuturesBatchOrder[] orders,
            int? receiveWindow = null,
            CancellationToken ct = default) => PlaceMultipleOrdersAsync(orders, receiveWindow, ct).Result;

        /// <summary>
        /// Place multiple orders in one call
        /// </summary>
        /// <param name="orders">The orders to place</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Returns a list of call results, one for each order. The order the results are in is the order the orders were sent</returns>
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>>> PlaceMultipleOrdersAsync(
            BinanceFuturesBatchOrder[] orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (orders.Length <= 0 || orders.Length > 5)
                throw new ArgumentException("Order list should be at least 1 and max 5 orders");

            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (BaseClient.TradeRulesBehaviour != TradeRulesBehaviour.None)
            {
                foreach (var order in orders)
                {
                    var rulesCheck = await FuturesClient.CheckTradeRules(order.Symbol, order.Quantity, order.Price, order.StopPrice, order.Type, ct).ConfigureAwait(false);
                    if (!rulesCheck.Passed)
                    {
                        _log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage!);
                        return new WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>>(null, null, null,
                            new ArgumentError(rulesCheck.ErrorMessage!));
                    }

                    order.Quantity = rulesCheck.Quantity;
                    order.Price = rulesCheck.Price;
                    order.StopPrice = rulesCheck.StopPrice;
                }
            }

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };

            var parameterOrders = new Dictionary<string, object>[orders.Length];
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new Dictionary<string, object>()
                {
                    { "symbol", order.Symbol },
                    { "side", JsonConvert.SerializeObject(order.Side, new OrderSideConverter(false)) },
                    { "type", JsonConvert.SerializeObject(order.Type, new OrderTypeConverter(false)) },
                    { "newOrderRespType", "RESULT" }
                };

                orderParameters.AddOptionalParameter("quantity", order.Quantity?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", order.NewClientOrderId);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("timeInForce", order.TimeInForce == null ? null : JsonConvert.SerializeObject(order.TimeInForce, new TimeInForceConverter(false)));
                orderParameters.AddOptionalParameter("positionSide", order.PositionSide == null ? null : JsonConvert.SerializeObject(order.PositionSide, new PositionSideConverter(false)));
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("workingType", order.WorkingType == null ? null : JsonConvert.SerializeObject(order.WorkingType, new WorkingTypeConverter(false)));
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                parameterOrders[i] = orderParameters;
                i++;
            }

            parameters.Add("batchOrders", JsonConvert.SerializeObject(parameterOrders));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var response = await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesMultipleOrderPlaceResult>>(FuturesClient.GetUrl(multipleNewOrdersEndpoint, Api, SignedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            if (!response.Success)
                return WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>>.CreateErrorResult(response.ResponseStatusCode, response.ResponseHeaders, response.Error!);

            var result = new List<CallResult<BinanceFuturesPlacedOrder>>();
            foreach (var item in response.Data)
            {
                if (item.Code != 0)
                    result.Add(new CallResult<BinanceFuturesPlacedOrder>(null, new ServerError(item.Code, item.Message)));
                else
                    result.Add(new CallResult<BinanceFuturesPlacedOrder>(item, null));
            }

            return new WebCallResult<IEnumerable<CallResult<BinanceFuturesPlacedOrder>>>(response.ResponseStatusCode, response.ResponseHeaders, result, null);
        }

        #endregion

        #region Query Order

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        public WebCallResult<BinanceFuturesOrder> GetOrder(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default) => GetOrderAsync(symbol, orderId, origClientOrderId, receiveWindow, ct).Result;

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        public async Task<WebCallResult<BinanceFuturesOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesOrder>(FuturesClient.GetUrl(queryOrderEndpoint, Api, SignedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }


        #endregion

        #region Cancel Order

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">The new client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public WebCallResult<BinanceFuturesCancelOrder> CancelOrder(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default) => CancelOrderAsync(symbol, orderId, origClientOrderId, newClientOrderId, receiveWindow, ct).Result;

        /// <summary>
        /// Cancels a pending order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<BinanceFuturesCancelOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesCancelOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesCancelOrder>(FuturesClient.GetUrl(cancelOrderEndpoint, Api, SignedVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Open Orders

        /// <summary>
        /// Cancels all open orders
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public WebCallResult<BinanceFuturesCancelAllOrders> CancelAllOrders(string symbol, long? receiveWindow = null, CancellationToken ct = default) => CancelAllOrdersAsync(symbol, receiveWindow, ct).Result;

        /// <summary>
        /// Cancels all open orders
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<BinanceFuturesCancelAllOrders>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesCancelAllOrders>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesCancelAllOrders>(FuturesClient.GetUrl(cancelAllOrdersEndpoint, Api, SignedVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Auto-Cancel All Open Orders

        /// <summary>
        /// Cancel all open orders of the specified symbol at the end of the specified countdown. This rest endpoint means to ensure your open orders are canceled in case of an outage. The endpoint should be called repeatedly as heartbeats
        /// so that the existing countdown time can be canceled and replaced by a new one.
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="countDownTime">The time after which all open orders should cancel, or 0 to cancel an existing timer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Countdown result</returns>
        public WebCallResult<BinanceFuturesCountDownResult> CancelAllOrdersAfterTimeout(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default) => CancelAllOrdersAfterTimeoutAsync(symbol, countDownTime, receiveWindow, ct).Result;

        /// <summary>
        /// Cancel all open orders of the specified symbol at the end of the specified countdown. This rest endpoint means to ensure your open orders are canceled in case of an outage. The endpoint should be called repeatedly as heartbeats
        /// so that the existing countdown time can be canceled and replaced by a new one.
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="countDownTime">The time after which all open orders should cancel, or 0 to cancel an existing timer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Countdown result</returns>
        public async Task<WebCallResult<BinanceFuturesCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesCountDownResult>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() },
                { "countdownTime", (int)countDownTime.TotalMilliseconds }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesCountDownResult>(FuturesClient.GetUrl(countDownCancelAllEndpoint, Api, SignedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Multiple Orders
        /// <summary>
        /// Cancels multiple orders
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderIdList">The list of order ids to cancel</param>
        /// <param name="origClientOrderIdList">The list of client order ids to cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public WebCallResult<IEnumerable<CallResult<BinanceFuturesCancelOrder>>> CancelMultipleOrders(string symbol, List<long>? orderIdList = null, List<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default) => CancelMultipleOrdersAsync(symbol, orderIdList, origClientOrderIdList, receiveWindow, ct).Result;

        /// <summary>
        /// Cancels multiple orders
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderIdList">The list of order ids to cancel</param>
        /// <param name="origClientOrderIdList">The list of client order ids to cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceFuturesCancelOrder>>>> CancelMultipleOrdersAsync(string symbol, List<long>? orderIdList = null, List<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<CallResult<BinanceFuturesCancelOrder>>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (orderIdList == null && origClientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or origClientOrderIdList must be sent");

            if (orderIdList?.Count > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (origClientOrderIdList?.Count > 10)
                throw new ArgumentException("origClientOrderIdList cannot contain more than 10 items");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (origClientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", origClientOrderIdList.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var response = await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesMultipleOrderCancelResult>>(FuturesClient.GetUrl(cancelMultipleOrdersEndpoint, Api, SignedVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);

            if (!response.Success)
                return WebCallResult<IEnumerable<CallResult<BinanceFuturesCancelOrder>>>.CreateErrorResult(response.ResponseStatusCode, response.ResponseHeaders, response.Error!);

            var result = new List<CallResult<BinanceFuturesCancelOrder>>();
            foreach (var item in response.Data)
            {
                if (item.Code != 0)
                    result.Add(new CallResult<BinanceFuturesCancelOrder>(null, new ServerError(item.Code, item.Message)));
                else
                    result.Add(new CallResult<BinanceFuturesCancelOrder>(item, null));
            }

            return new WebCallResult<IEnumerable<CallResult<BinanceFuturesCancelOrder>>>(response.ResponseStatusCode, response.ResponseHeaders, result, null);
        }

        #endregion

        #region Query Current Open Order

        /// <summary>
        /// Retrieves data for a specific open order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        public WebCallResult<BinanceFuturesOrder> GetOpenOrder(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default) => GetOpenOrderAsync(symbol, orderId, origClientOrderId, receiveWindow, ct).Result;

        /// <summary>
        /// Retrieves data for a specific open order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        public async Task<WebCallResult<BinanceFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceFuturesOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<BinanceFuturesOrder>(FuturesClient.GetUrl(openOrderEndpoint, Api, SignedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Current All Open Orders

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        public WebCallResult<IEnumerable<BinanceFuturesOrder>> GetOpenOrders(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default) => GetOpenOrdersAsync(symbol, receiveWindow, ct).Result;

        /// <summary>
        /// Gets a list of open orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesOrder>>(FuturesClient.GetUrl(openOrdersEndpoint, Api, SignedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region All Orders

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public WebCallResult<IEnumerable<BinanceFuturesOrder>> GetAllOrders(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default) => GetAllOrdersAsync(symbol, orderId, startTime, endTime, limit, receiveWindow, ct).Result;

        /// <summary>
        /// Gets all orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetAllOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesOrder>>(FuturesClient.GetUrl(allOrdersEndpoint, Api, SignedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region User's Force Orders

        /// <summary>
        /// Gets a list of users forced orders
        /// </summary>
        /// <param name="symbol">The symbol to get forced orders for</param>
        /// <param name="closeType">Filter by reason for close</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced orders</returns>
        public WebCallResult<IEnumerable<BinanceFuturesOrder>> GetForcedOrders(string? symbol = null, AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default) => GetForcedOrdersAsync(symbol, closeType, startTime, endTime, receiveWindow, ct).Result;

        /// <summary>
        /// Gets a list of users forced orders
        /// </summary>
        /// <param name="symbol">The symbol to get forced orders for</param>
        /// <param name="closeType">Filter by reason for close</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced orders</returns>
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceFuturesOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("autoCloseType", closeType.HasValue ? JsonConvert.SerializeObject(closeType, new AutoCloseTypeConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            
            return await BaseClient.SendRequestInternal<IEnumerable<BinanceFuturesOrder>>(FuturesClient.GetUrl(forceOrdersEndpoint, Api, SignedVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
