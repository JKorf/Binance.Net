using Binance.Net.Enums;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.AlgoOrders;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD-M futures trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IBinanceRestClientUsdFuturesApiTrading
    {
        /// <summary>
        /// Places a new order
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The quantity of the base symbol</param>
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
        /// <param name="priceProtect">If true when price reaches stopPrice, difference between "MARK_PRICE" and "CONTRACT_PRICE" cannot be larger than "triggerProtect" of the symbol.</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        Task<WebCallResult<BinanceUsdFuturesOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            FuturesOrderType type,
            decimal? quantity,
            decimal? price = null,
            PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            string? newClientOrderId = null,
            decimal? stopPrice = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? closePosition = null,
            OrderResponseType? orderResponseType = null,
            bool? priceProtect = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in one call
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#place-multiple-orders-trade" /></para>
        /// </summary>
        /// <param name="orders">The orders to place</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Returns a list of call results, one for each order. The order the results are in is the order the orders were sent</returns>
        Task<WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>> PlaceMultipleOrdersAsync(
            IEnumerable<BinanceFuturesBatchOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#query-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        Task<WebCallResult<BinanceUsdFuturesOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending order
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#cancel-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceUsdFuturesOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels all open orders
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#cancel-all-open-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceFuturesCancelAllOrders>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders of the specified symbol at the end of the specified countdown. This rest endpoint means to ensure your open orders are canceled in case of an outage. The endpoint should be called repeatedly as heartbeats
        /// so that the existing countdown time can be canceled and replaced by a new one.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#auto-cancel-all-open-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="countDownTime">The time after which all open orders should cancel, or 0 to cancel an existing timer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Countdown result</returns>
        Task<WebCallResult<BinanceFuturesCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels muliple orders
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#cancel-multiple-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderIdList">The list of order ids to cancel</param>
        /// <param name="origClientOrderIdList">The list of client order ids to cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>> CancelMultipleOrdersAsync(string symbol, List<long>? orderIdList = null, List<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an existing order
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#modify-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="price">New price</param>
        /// <param name="orderId">Order id of the order to edit</param>
        /// <param name="origClientOrderId">Client order id of the order to edit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceUsdFuturesOrder>> EditOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Edit multiple existing orders
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#modify-multiple-orders-trade" /></para>
        /// </summary>
        /// <param name="orders">The order info</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>> EditMultipleOrdersAsync(
            IEnumerable<BinanceFuturesBatchEditOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get order edit history
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="clientOrderId">Filter by client order id</param>
        /// <param name="startTime">If set, only orders edited after this time will be returned</param>
        /// <param name="endTime">If set, only orders edited before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesOrderEditHistory>>> GetOrderEditHistoryAsync(string symbol, long? orderId = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific open order. Either orderId or origClientOrderId should be provided.
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#query-current-open-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        Task<WebCallResult<BinanceUsdFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#current-all-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        Task<WebCallResult<IEnumerable<BinanceUsdFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all orders for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#all-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<IEnumerable<BinanceUsdFuturesOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of users forced orders
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#user-39-s-force-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get forced orders for</param>
        /// <param name="closeType">Filter by reason for close</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced orders</returns>
        Task<WebCallResult<IEnumerable<BinanceUsdFuturesOrder>>> GetForcedOrdersAsync(string? symbol = null,
            AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user trades for provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/futures/en/#account-trade-list-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="orderId">Get the trades for a specific order</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesUsdtTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? orderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new Volume Participation order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#volume-participation-vp-new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="urgency">Represent the relative speed of the current execution</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="limitPrice">Limit price of the order. If null will use market price</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoOrderResult>> PlaceVolumeParticipationOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            OrderUrgency urgency,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? limitPrice = null,
            PositionSide? positionSide = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place a new Time Weighted Average Price order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#time-weighted-average-price-twap-new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="duration">Duration in seconds. Less than 5 minutes will be defaulted to 5 minutes, more than 24 hours will be defaulted to 24 hours.</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="reduceOnly">Reduce only</param>
        /// <param name="limitPrice">Limit price of the order. If null will use market price</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            int duration,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? limitPrice = null,
            PositionSide? positionSide = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an algo order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#cancel-algo-order-trade" /></para>
        /// </summary>
        /// <param name="algoOrderId">Algo id to cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of open algo orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-current-algo-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of closed algo orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-historical-algo-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="startTime">Fitler by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get algo sub orders overview
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-sub-orders-user_data" /></para>
        /// </summary>
        /// <param name="algoId">Algo id</param>
        /// <param name="page">Page</param>
        /// <param name="limit">Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

    }
}
