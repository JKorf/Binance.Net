using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.SubClients.Margin
{
    /// <summary>
    /// Margin order endpoints
    /// </summary>
    public class BinanceClientMarginOrders : IBinanceClientMarginOrders
    {
        private const string marginApi = "sapi";
        private const string marginVersion = "1";

        // Margin
        private const string newMarginOrderEndpoint = "margin/order";
        private const string cancelMarginOrderEndpoint = "margin/order";
        private const string myMarginTradesEndpoint = "margin/myTrades";
        private const string allMarginOrdersEndpoint = "margin/allOrders";
        private const string openMarginOrdersEndpoint = "margin/openOrders";
        private const string cancelOpenMarginOrdersEndpoint = "margin/openOrders";
        private const string queryMarginOrderEndpoint = "margin/order";

        // OCO
        private const string newMarginOCOOrderEndpoint = "margin/order/oco";
        private const string cancelMarginOCOOrderEndpoint = "margin/orderList";
        private const string getMarginOCOOrderEndpoint = "margin/orderList";
        private const string allMarginOCOOrderEndpoint = "margin/allOrderList";
        private const string openMarginOCOOrderEndpoint = "margin/openOrderList";

        private readonly BinanceClient _baseClient;
        private readonly Log _log;

        internal BinanceClientMarginOrders(Log log, BinanceClient baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }

        #region Margin Account New Order

        /// <summary>
        /// Margin account new order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="quoteOrderQuantity">The amount of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQuantity">Used for iceberg orders</param>
        /// <param name="sideEffectType">Side effect type for this order</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderResponseType">Used for the response JSON</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQuantity = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            return await _baseClient.PlaceOrderInternal(_baseClient.GetUrlSpot(newMarginOrderEndpoint, marginApi, marginVersion),
                symbol,
                side,
                type,
                quantity,
                quoteOrderQuantity,
                newClientOrderId,
                price,
                timeInForce,
                stopPrice,
                icebergQuantity,
                sideEffectType,
                isIsolated,
                orderResponseType,
                receiveWindow,
                ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account Cancel Order

        /// <summary>
        /// Cancel an active order for margin account
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<BinanceCanceledOrder>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceCanceledOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceCanceledOrder>(_baseClient.GetUrlSpot(cancelMarginOrderEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account Cancel All Open Orders

        /// <summary>
        /// Cancel all active orders for a symbol
        /// </summary>
        /// <param name="symbol">The symbol the to cancel orders for</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<IEnumerable<BinanceCanceledOrder>>> CancelOpenMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceCanceledOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceCanceledOrder>>(_baseClient.GetUrlSpot(cancelOpenMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Order

        /// <summary>
        /// Retrieves data for a specific margin account order. Either orderId or origClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific margin account order</returns>
        public async Task<WebCallResult<BinanceOrder>> GetMarginAccountOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId should be provided");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceOrder>(_baseClient.GetUrlSpot(queryMarginOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Open Order

        /// <summary>
        /// Gets a list of open margin account orders
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open margin account orders</returns>
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginAccountOpenOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol?.ValidateBinanceSymbol();
            if (isIsolated == true && symbol == null)
                throw new ArgumentException("Symbol must be provided for isolated margin");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrlSpot(openMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's All Order

        /// <summary>
        /// Gets all margin account orders for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account orders</returns>
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginAccountOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrlSpot(allMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Account's Trade List

        /// <summary>
        /// Gets all user margin account trades for provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account trades</returns>
        public async Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMarginAccountUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceTrade>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceTrade>>(_baseClient.GetUrlSpot(myMarginTradesEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account New OCO Order

        /// <summary>
        /// Places a new margin OCO(One cancels other) order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The amount of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="stopPrice">The stop price</param>
        /// <param name="stopLimitPrice">The price for the stop limit order</param>
        /// <param name="stopClientOrderId">Client id for the stop order</param>
        /// <param name="limitClientOrderId">Client id for the limit order</param>
        /// <param name="listClientOrderId">Client id for the order list</param>
        /// <param name="limitIcebergQuantity">Iceberg quantity for the limit order</param>
        /// <param name="sideEffectType">Side effect type</param>
        /// <param name="isIsolated">Is isolated</param>
        /// <param name="orderResponseType">Order response type</param>
        /// <param name="stopIcebergQuantity">Iceberg quantity for the stop order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order list info</returns>        
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> PlaceMarginOCOOrderAsync(string symbol,
            OrderSide side,
            decimal price,
            decimal stopPrice,
            decimal quantity,
            decimal? stopLimitPrice = null,
            TimeInForce? stopLimitTimeInForce = null,
            decimal? stopIcebergQuantity = null,
            decimal? limitIcebergQuantity = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            string? listClientOrderId = null,
            string? limitClientOrderId = null,
            string? stopClientOrderId = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginOrderOcoList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, price, stopPrice, null, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _log.Write(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinanceMarginOrderOcoList>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity!.Value;
            price = rulesCheck.Price!.Value;
            stopPrice = rulesCheck.StopPrice!.Value;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("stopLimitPrice", stopLimitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("sideEffectType", sideEffectType == null ? null : JsonConvert.SerializeObject(sideEffectType, new SideEffectTypeConverter(false)));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("limitClientOrderId", limitClientOrderId);
            parameters.AddOptionalParameter("stopClientOrderId", stopClientOrderId);
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopLimitTimeInForce", stopLimitTimeInForce == null ? null : JsonConvert.SerializeObject(stopLimitTimeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrlSpot(newMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <summary>
        /// Cancels a pending margin oco order
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderListId">The id of the order list to cancel</param>
        /// <param name="listClientOrderId">The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginOrderOcoList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrlSpot(cancelMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <summary>
        /// Retrieves data for a specific margin oco order. Either orderListId or listClientOrderId should be provided.
        /// </summary>
        /// <param name="symbol">Mandatory for isolated margin, not supported for cross margin</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderListId">The list order id of the order</param>
        /// <param name="origClientOrderId">Either orderListId or listClientOrderId must be provided</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginOrderOcoList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrlSpot(getMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <summary>
        /// Retrieves a list of margin oco orders matching the parameters
        /// </summary>
        /// <param name="symbol">Mandatory for isolated margin, not supported for cross margin</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="fromId">Only return oco orders with id higher than this</param>
        /// <param name="startTime">Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? JsonConvert.SerializeObject(startTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? JsonConvert.SerializeObject(endTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginOrderOcoList>>(_baseClient.GetUrlSpot(allMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <summary>
        /// Retrieves a list of open margin oco orders
        /// </summary>
        /// <param name="symbol">Mandatory for isolated margin, not supported for cross margin</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginOrderOcoList>>(_baseClient.GetUrlSpot(openMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
