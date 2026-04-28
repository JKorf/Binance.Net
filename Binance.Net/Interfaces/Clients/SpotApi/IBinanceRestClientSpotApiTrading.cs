using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBinanceRestClientSpotApiTrading
    {
        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#test-new-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order/test
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="type">["<c>type</c>"] The order type (limit/market)</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">["<c>price</c>"] The price to use</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] Unique id for order</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Used for stop orders</param>
        /// <param name="icebergQty">["<c>icebergQty</c>"] Used for iceberg orders</param>
        /// <param name="orderResponseType">["<c>newOrderRespType</c>"] Used for the response JSON</param>
        /// <param name="trailingDelta">["<c>trailingDelta</c>"] Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">["<c>strategyId</c>"] Strategy id</param>
        /// <param name="strategyType">["<c>strategyType</c>"] Strategy type</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="computeFeeRates">["<c>computeCommissionRates</c>"] Whether fee rates should be calculated or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Fee information if <paramref name="computeFeeRates"/> is set to true; otherwise an empty result</returns>
        Task<WebCallResult<BinanceTestOrderCommission>> PlaceTestOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? computeFeeRates = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="type">["<c>type</c>"] The order type</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">["<c>price</c>"] The price to use</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] Unique id for order</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Used for stop orders</param>
        /// <param name="icebergQty">["<c>icebergQty</c>"] Used for iceberg orders</param>
        /// <param name="orderResponseType">["<c>newOrderRespType</c>"] Used for the response JSON</param>
        /// <param name="trailingDelta">["<c>trailingDelta</c>"] Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">["<c>strategyId</c>"] Strategy id</param>
        /// <param name="strategyType">["<c>strategyType</c>"] Strategy type</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="pegPriceType">["<c>pegPriceType</c>"] Peg price type</param>
        /// <param name="pegOffsetValue">["<c>pegOffsetValue</c>"] Peg offset value</param>
        /// <param name="pegOffsetType">["<c>pegOffsetType</c>"] Peg offset type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the placed order</returns>
        Task<WebCallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            PegPriceType? pegPriceType = null,
            int? pegOffsetValue = null,
            PegOffsetType? pegOffsetType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#cancel-order-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The order id of the order</param>
        /// <param name="origClientOrderId">["<c>origClientOrderId</c>"] The client order id of the order</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] Unique identifier for this cancel</param>
        /// <param name="cancelRestriction">["<c>cancelRestrictions</c>"] Restrict cancellation based on order state</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the canceled order</returns>
        Task<WebCallResult<BinanceOrderBase>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, CancelRestriction? cancelRestriction = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels all open orders on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#cancel-all-open-orders-on-a-symbol-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the canceled orders</returns>
        Task<WebCallResult<BinanceOrderBase[]>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels an existing order and places a new order on the same symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#cancel-an-existing-order-and-send-a-new-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order/cancelReplace
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="type">["<c>type</c>"] The order type</param>
        /// <param name="cancelReplaceMode">["<c>cancelReplaceMode</c>"] Replacement behavior</param>
        /// <param name="cancelOrderId">["<c>cancelOrderId</c>"] The order id to cancel. Either this or cancelClientOrderId should be provided</param>
        /// <param name="cancelClientOrderId">["<c>cancelOrigClientOrderId</c>"] The client order id to cancel. Either this or cancelOrderId should be provided</param>
        /// <param name="newCancelClientOrderId">["<c>cancelNewClientOrderId</c>"] New client order id for the canceled order</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">["<c>price</c>"] The price to use</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] Unique id for order</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Used for stop orders</param>
        /// <param name="icebergQty">["<c>icebergQty</c>"] Used for iceberg orders</param>
        /// <param name="orderResponseType">["<c>newOrderRespType</c>"] Used for the response JSON</param>
        /// <param name="trailingDelta">["<c>trailingDelta</c>"] Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">["<c>strategyId</c>"] Strategy id</param>
        /// <param name="strategyType">["<c>strategyType</c>"] Strategy type</param>
        /// <param name="cancelRestriction">["<c>cancelRestrictions</c>"] Restrict cancellation based on order state</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Cancel-replace operation result</returns>
        Task<WebCallResult<BinanceReplaceOrderResult>> ReplaceOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            CancelReplaceMode cancelReplaceMode,
            long? cancelOrderId = null,
            string? cancelClientOrderId = null,
            string? newCancelClientOrderId = null,
            string? newClientOrderId = null,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            CancelRestriction? cancelRestriction = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Amends an existing open order (reduces quantity)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-amend-keep-priority-trade" /><br />
        /// Endpoint:<br />
        /// PUT /api/v3/order/amend/keepPriority
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The order id of the order. Either this or origClientOrderId should be provided</param>
        /// <param name="origClientOrderId">["<c>origClientOrderId</c>"] The client order id of the order. Either this or orderId should be provided</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] The new client order id for the order after being amended</param>
        /// <param name="newQuantity">["<c>newQuantity</c>"] The new quantity (must be greater than 0 and less than the order's quantity)</param>
        /// <param name="receiveWindow">The receive window for which this request is active</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The amended order details</returns>
        Task<WebCallResult<BinanceAmendedOrderResult>> AmendOrderAsync(
            string symbol,
            decimal newQuantity,
            long? orderId = null,
            string? origClientOrderId = null,
            string? newClientOrderId = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#query-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The order id of the order</param>
        /// <param name="origClientOrderId">["<c>origClientOrderId</c>"] The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        Task<WebCallResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#current-open-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get open orders for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        Task<WebCallResult<BinanceOrder[]>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all orders for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#all-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/allOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get orders for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">["<c>startTime</c>"] If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">["<c>endTime</c>"] If set, only orders placed before this time will be returned</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<BinanceOrder[]>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Deprecated. Use <see cref="PlaceOcoOrderListAsync"/> instead.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/order/oco
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">["<c>stopLimitTimeInForce</c>"] Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="price">["<c>price</c>"] The price to use</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] The stop price</param>
        /// <param name="stopLimitPrice">["<c>stopLimitPrice</c>"] The price for the stop limit order</param>
        /// <param name="stopClientOrderId">["<c>stopClientOrderId</c>"] Client id for the stop order</param>
        /// <param name="limitClientOrderId">["<c>limitClientOrderId</c>"] Client id for the limit order</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] Client id for the order list</param>
        /// <param name="limitIcebergQuantity">["<c>limitIcebergQty</c>"] Iceberg quantity for the limit order</param>
        /// <param name="stopIcebergQuantity">["<c>stopIcebergQty</c>"] Iceberg quantity for the stop order</param>
        /// <param name="trailingDelta">["<c>trailingDelta</c>"] Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="limitStrategyId">["<c>limitStrategyId</c>"] Strategy id of the limit order</param>
        /// <param name="limitStrategyType">["<c>limitStrategyType</c>"] Strategy type of the limit order</param>
        /// <param name="stopStrategyId">["<c>stopStrategyId</c>"] Strategy id of the stop order</param>
        /// <param name="stopStrategyType">["<c>stopStrategyType</c>"] Strategy type of the stop order</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order list info</returns>
        Task<WebCallResult<BinanceOrderOcoList>> PlaceOcoOrderAsync(string symbol,
            OrderSide side,
            decimal quantity,
            decimal price,
            decimal stopPrice,
            decimal? stopLimitPrice = null,
            string? listClientOrderId = null,
            string? limitClientOrderId = null,
            string? stopClientOrderId = null,
            decimal? limitIcebergQuantity = null,
            decimal? stopIcebergQuantity = null,
            TimeInForce? stopLimitTimeInForce = null,
            int? trailingDelta = null,
            int? limitStrategyId = null,
            int? limitStrategyType = null,
            int? stopStrategyId = null,
            int? stopStrategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new OCO order. An OCO has two legs called the above leg and below leg. One leg must be a LimitMaker order and the other leg must be a StopLoss or StopLossLimit order.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/orderList/oco
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="aboveOrderType">["<c>aboveOrderType</c>"] The above leg order type</param>
        /// <param name="belowOrderType">["<c>belowOrderType</c>"] The below leg order type</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] Client order id for the list</param>
        /// <param name="aboveClientOrderId">["<c>aboveClientOrderId</c>"] Client order id for the above leg</param>
        /// <param name="aboveIcebergQuantity">["<c>aboveIcebergQty</c>"] Iceberg quantity for the above leg</param>
        /// <param name="abovePrice">["<c>abovePrice</c>"] Limit price for the above leg</param>
        /// <param name="aboveStopPrice">["<c>aboveStopPrice</c>"] Stop price for the above leg</param>
        /// <param name="aboveTrailingDelta">["<c>aboveTrailingDelta</c>"] Trailing delta for the above leg</param>
        /// <param name="aboveTimeInForce">["<c>aboveTimeInForce</c>"] Time in force for the above leg</param>
        /// <param name="aboveStrategyId">["<c>aboveStrategyId</c>"] Strategy id for the above leg</param>
        /// <param name="aboveStrategyType">["<c>aboveStrategyType</c>"] Strategy type for the above leg</param>
        /// <param name="belowClientOrderId">["<c>belowClientOrderId</c>"] Client order id for the below leg</param>
        /// <param name="belowIcebergQuantity">["<c>belowIcebergQty</c>"] Iceberg quantity for the below leg</param>
        /// <param name="belowPrice">["<c>belowPrice</c>"] Limit price for the below leg</param>
        /// <param name="belowStopPrice">["<c>belowStopPrice</c>"] Stop price for the below leg</param>
        /// <param name="belowTrailingDelta">["<c>belowTrailingDelta</c>"] Trailing delta for the below leg</param>
        /// <param name="belowTimeInForce">["<c>belowTimeInForce</c>"] Time in force for the below leg</param>
        /// <param name="belowStrategyId">["<c>belowStrategyId</c>"] Strategy id for the below leg</param>
        /// <param name="belowStrategyType">["<c>belowStrategyType</c>"] Strategy type for the below leg</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>OCO order list details</returns>
        Task<WebCallResult<BinanceOrderOcoList>> PlaceOcoOrderListAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            SpotOrderType aboveOrderType,
            SpotOrderType belowOrderType,
            string? listClientOrderId = null,

            string? aboveClientOrderId = null,
            decimal? aboveIcebergQuantity = null,
            decimal? abovePrice = null,
            decimal? aboveStopPrice = null,
            decimal? aboveTrailingDelta = null,
            TimeInForce? aboveTimeInForce = null,
            int? aboveStrategyId = null,
            int? aboveStrategyType = null,

            string? belowClientOrderId = null,
            decimal? belowIcebergQuantity = null,
            decimal? belowPrice = null,
            decimal? belowStopPrice = null,
            decimal? belowTrailingDelta = null,
            TimeInForce? belowTimeInForce = null,
            int? belowStrategyId = null,
            int? belowStrategyType = null,

            SelfTradePreventionMode? selfTradePreventionMode = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending OCO order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/orderList
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderListId">["<c>orderListId</c>"] The id of the order list to cancel</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the canceled order list</returns>
        Task<WebCallResult<BinanceOrderOcoList>> CancelOcoOrderAsync(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific OCO order. Either <paramref name="orderListId"/> or <paramref name="listClientOrderId"/> should be provided.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/orderList
        /// </para>
        /// </summary>
        /// <param name="orderListId">["<c>orderListId</c>"] The list order id of the order</param>
        /// <param name="listClientOrderId">["<c>origClientOrderId</c>"] The client order id of the list order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        Task<WebCallResult<BinanceOrderOcoList>> GetOcoOrderAsync(long? orderListId = null, string? listClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of OCO orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/allOrderList
        /// </para>
        /// </summary>
        /// <param name="fromId">["<c>fromId</c>"] Only return oco orders with id higher than this</param>
        /// <param name="startTime">["<c>startTime</c>"] Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">["<c>endTime</c>"] Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        Task<WebCallResult<BinanceOrderOcoList[]>> GetOcoOrdersAsync(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of open OCO orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/openOrderList
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        Task<WebCallResult<BinanceOrderOcoList[]>> GetOpenOcoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new OTO (One Triggers Other) order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/orderList/oto
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="workingOrderType">["<c>workingOrderType</c>"] Working order type, either Limit or LimitMaker</param>
        /// <param name="workingSide">["<c>workingSide</c>"] Working order side</param>
        /// <param name="workingQuantity">["<c>workingQuantity</c>"] Working order quantity</param>
        /// <param name="workingPrice">["<c>workingPrice</c>"] Working order price</param>
        /// <param name="pendingQuantity">["<c>pendingQuantity</c>"] Pending order quantity</param>
        /// <param name="pendingSide">["<c>pendingSide</c>"] Pending order side</param>
        /// <param name="pendingOrderType">["<c>pendingOrderType</c>"] Pending order type</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] Arbitrary unique ID among open order lists. Automatically generated if not sent.</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="workingClientOrderId">["<c>workingClientOrderId</c>"] Working order client order id</param>
        /// <param name="workingIcebergQuantity">["<c>workingIcebergQty</c>"] Working order iceberg quantity</param>
        /// <param name="workingTimeInForce">["<c>workingTimeInForce</c>"] Working order time in force</param>
        /// <param name="workingStrategyId">["<c>workingStrategyId</c>"] Working order strategy id</param>
        /// <param name="workingStrategyType">["<c>workingStrategyType</c>"] Working order strategy type</param>
        /// <param name="pendingClientOrderId">["<c>pendingClientOrderId</c>"] Pending order client order id</param>
        /// <param name="pendingPrice">["<c>pendingPrice</c>"] Pending order price</param>
        /// <param name="pendingStopPrice">["<c>pendingStopPrice</c>"] Pending order stop price</param>
        /// <param name="pendingTrailingDelta">["<c>pendingTrailingDelta</c>"] Pending order trailing delta</param>
        /// <param name="pendingIcebergQuantity">["<c>pendingIcebergQty</c>"] Pending order iceberg quantity</param>
        /// <param name="pendingTimeInForce">["<c>pendingTimeInForce</c>"] Pending order time in force</param>
        /// <param name="pendingStrategyId">["<c>pendingStrategyId</c>"] Pending order strategy id</param>
        /// <param name="pendingStrategyType">["<c>pendingStrategyType</c>"] Pending order strategy type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>OTO order list details</returns>
        Task<WebCallResult<BinanceOrderOcoList>> PlaceOtoOrderListAsync(
            string symbol,
            OrderSide workingSide,
            SpotOrderType workingOrderType,
            decimal workingQuantity,
            decimal workingPrice,
            decimal pendingQuantity,
            OrderSide pendingSide,
            SpotOrderType pendingOrderType,
            string? listClientOrderId = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            string? workingClientOrderId = null,
            decimal? workingIcebergQuantity = null,
            TimeInForce? workingTimeInForce = null,
            int? workingStrategyId = null,
            int? workingStrategyType = null,
            string? pendingClientOrderId = null,
            decimal? pendingPrice = null,
            decimal? pendingStopPrice = null,
            decimal? pendingTrailingDelta = null,
            decimal? pendingIcebergQuantity = null,
            TimeInForce? pendingTimeInForce = null,
            int? pendingStrategyId = null,
            int? pendingStrategyType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new OTOCO (One Triggers One Cancels The Other) order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/trading-endpoints#order-lists" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/orderList/otoco
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="workingOrderType">["<c>workingOrderType</c>"] Working order type, either Limit or LimitMaker</param>
        /// <param name="workingSide">["<c>workingSide</c>"] Working order side</param>
        /// <param name="workingQuantity">["<c>workingQuantity</c>"] Working order quantity</param>
        /// <param name="workingPrice">["<c>workingPrice</c>"] Working order price</param>
        /// <param name="pendingQuantity">["<c>pendingQuantity</c>"] Pending order quantity</param>
        /// <param name="pendingSide">["<c>pendingSide</c>"] Pending order side</param>
        /// <param name="pendingAboveOrderType">["<c>pendingAboveOrderType</c>"] Pending above order type, LimitMaker, StopLoss or StopLossLimit</param>
        /// <param name="pendingBelowOrderType">["<c>pendingBelowOrderType</c>"] Pending below order type, LimitMaker, StopLoss or StopLossLimit</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] Arbitrary unique ID among open order lists. Automatically generated if not sent.</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="workingClientOrderId">["<c>workingClientOrderId</c>"] Working order client order id</param>
        /// <param name="workingIcebergQuantity">["<c>workingIcebergQty</c>"] Working order iceberg quantity</param>
        /// <param name="workingTimeInForce">["<c>workingTimeInForce</c>"] Working order time in force</param>
        /// <param name="workingStrategyId">["<c>workingStrategyId</c>"] Working order strategy id</param>
        /// <param name="workingStrategyType">["<c>workingStrategyType</c>"] Working order strategy type</param>
        /// <param name="pendingAboveClientOrderId">["<c>pendingAboveClientOrderId</c>"] Pending above order client order id</param>
        /// <param name="pendingAbovePrice">["<c>pendingAbovePrice</c>"] Pending above order price</param>
        /// <param name="pendingAboveStopPrice">["<c>pendingAboveStopPrice</c>"] Pending above order stop price</param>
        /// <param name="pendingAboveTrailingDelta">["<c>pendingAboveTrailingDelta</c>"] Pending above order trailing delta</param>
        /// <param name="pendingAboveIcebergQuantity">["<c>pendingAboveIcebergQty</c>"] Pending above order iceberg quantity</param>
        /// <param name="pendingAboveTimeInForce">["<c>pendingAboveTimeInForce</c>"] Pending above order time in force</param>
        /// <param name="pendingAboveStrategyId">["<c>pendingAboveStrategyId</c>"] Pending above order strategy id</param>
        /// <param name="pendingAboveStrategyType">["<c>pendingAboveStrategyType</c>"] Pending above order strategy type</param>
        /// <param name="pendingBelowClientOrderId">["<c>pendingBelowClientOrderId</c>"] Pending below order client order id</param>
        /// <param name="pendingBelowPrice">["<c>pendingBelowPrice</c>"] Pending below order price</param>
        /// <param name="pendingBelowStopPrice">["<c>pendingBelowStopPrice</c>"] Pending below order stop price</param>
        /// <param name="pendingBelowTrailingDelta">["<c>pendingBelowTrailingDelta</c>"] Pending below order trailing delta</param>
        /// <param name="pendingBelowIcebergQuantity">["<c>pendingBelowIcebergQty</c>"] Pending below order iceberg quantity</param>
        /// <param name="pendingBelowTimeInForce">["<c>pendingBelowTimeInForce</c>"] Pending below order time in force</param>
        /// <param name="pendingBelowStrategyId">["<c>pendingBelowStrategyId</c>"] Pending below order strategy id</param>
        /// <param name="pendingBelowStrategyType">["<c>pendingBelowStrategyType</c>"] Pending below order strategy type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>OTOCO order list details</returns>
        Task<WebCallResult<BinanceOrderOcoList>> PlaceOtocoOrderListAsync(
            string symbol,

            OrderSide workingSide,
            SpotOrderType workingOrderType,
            decimal workingQuantity,
            decimal workingPrice,
            decimal pendingQuantity,
            OrderSide pendingSide,
            SpotOrderType pendingAboveOrderType,
            SpotOrderType pendingBelowOrderType,
            string? listClientOrderId = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            string? workingClientOrderId = null,
            decimal? workingIcebergQuantity = null,
            TimeInForce? workingTimeInForce = null,
            int? workingStrategyId = null,
            int? workingStrategyType = null,
            string? pendingAboveClientOrderId = null,
            decimal? pendingAbovePrice = null,
            decimal? pendingAboveStopPrice = null,
            decimal? pendingAboveTrailingDelta = null,
            decimal? pendingAboveIcebergQuantity = null,
            TimeInForce? pendingAboveTimeInForce = null,
            int? pendingAboveStrategyId = null,
            int? pendingAboveStrategyType = null,
            string? pendingBelowClientOrderId = null,
            decimal? pendingBelowPrice = null,
            decimal? pendingBelowStopPrice = null,
            decimal? pendingBelowTrailingDelta = null,
            decimal? pendingBelowIcebergQuantity = null,
            TimeInForce? pendingBelowTimeInForce = null,
            int? pendingBelowStrategyId = null,
            int? pendingBelowStrategyType = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets user trades for provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints#account-trade-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/myTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to get trades for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Get trades for this order id</param>
        /// <param name="limit">["<c>limit</c>"] The max number of results</param>
        /// <param name="fromId">["<c>fromId</c>"] TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="startTime">["<c>startTime</c>"] Orders newer than this date will be retrieved</param>
        /// <param name="endTime">["<c>endTime</c>"] Orders older than this date will be retrieved</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<BinanceTrade[]>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new margin account order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Margin-Account-New-Order" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="type">["<c>type</c>"] The order type</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">["<c>price</c>"] The price to use</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] Unique id for order</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Used for stop orders</param>
        /// <param name="icebergQuantity">["<c>icebergQty</c>"] Used for iceberg orders</param>
        /// <param name="sideEffectType">["<c>sideEffectType</c>"] Side effect type for this order</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="orderResponseType">["<c>newOrderRespType</c>"] Used for the response JSON</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="autoRepayAtCancel">["<c>autoRepayAtCancel</c>"] Only for MARGIN_BUY or AUTO_BORROW_REPAY orders. True means debt generated by the order is repaid after cancellation. Default is true</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the placed order</returns>
        Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQuantity = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            OrderResponseType? orderResponseType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? autoRepayAtCancel = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels an active margin account order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Margin-Account-Cancel-Order" /><br />
        /// Endpoint:<br />
        /// DELETE /sapi/v1/margin/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] The order id of the order</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="origClientOrderId">["<c>origClientOrderId</c>"] The client order id of the order</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the canceled order</returns>
        Task<WebCallResult<BinanceOrderBase>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all active orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Margin-Account-Cancel-All-Open-Orders" /><br />
        /// Endpoint:<br />
        /// DELETE /sapi/v1/margin/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to cancel orders for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the canceled orders</returns>
        Task<WebCallResult<BinanceOrderBase[]>> CancelAllMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific margin account order. Either orderId or origClientOrderId should be provided.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-Order" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="orderId">["<c>orderId</c>"] The order id of the order</param>
        /// <param name="origClientOrderId">["<c>origClientOrderId</c>"] The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific margin account order</returns>
        Task<WebCallResult<BinanceOrder>> GetMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open margin account orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-Open-Orders" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get open orders for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open margin account orders</returns>
        Task<WebCallResult<BinanceOrder[]>> GetOpenMarginOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all margin account orders for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-All-Orders" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/allOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol to get orders for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="orderId">["<c>orderId</c>"] If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">["<c>startTime</c>"] If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">["<c>endTime</c>"] If set, only orders placed before this time will be returned</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account orders</returns>
        Task<WebCallResult<BinanceOrder[]>> GetMarginOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user margin account trades for provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-Trade-List" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/myTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol to get trades for, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Trades associated with orderId</param>
        /// <param name="startTime">["<c>startTime</c>"] Orders newer than this date will be retrieved</param>
        /// <param name="endTime">["<c>endTime</c>"] Orders older than this date will be retrieved</param>
        /// <param name="limit">["<c>limit</c>"] The max number of results</param>
        /// <param name="fromId">["<c>fromId</c>"] TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account trades</returns>
        Task<WebCallResult<BinanceTrade[]>> GetMarginUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null,
            int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new margin OCO (One Cancels the Other) order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Margin-Account-New-OCO" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/margin/order/oco
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">["<c>stopLimitTimeInForce</c>"] Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">["<c>quantity</c>"] The quantity of the symbol</param>
        /// <param name="price">["<c>price</c>"] The price to use</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] The stop price</param>
        /// <param name="stopLimitPrice">["<c>stopLimitPrice</c>"] The price for the stop limit order</param>
        /// <param name="stopClientOrderId">["<c>stopClientOrderId</c>"] Client id for the stop order</param>
        /// <param name="limitClientOrderId">["<c>limitClientOrderId</c>"] Client id for the limit order</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] Client id for the order list</param>
        /// <param name="limitIcebergQuantity">["<c>limitIcebergQty</c>"] Iceberg quantity for the limit order</param>
        /// <param name="sideEffectType">["<c>sideEffectType</c>"] Side effect type</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="orderResponseType">["<c>newOrderRespType</c>"] Order response type</param>
        /// <param name="stopIcebergQuantity">["<c>stopIcebergQty</c>"] Iceberg quantity for the stop order</param>
        /// <param name="selfTradePreventionMode">["<c>selfTradePreventionMode</c>"] Self trade prevention mode</param>
        /// <param name="autoRepayAtCancel">["<c>autoRepayAtCancel</c>"] Only for MARGIN_BUY or AUTO_BORROW_REPAY orders. True means debt generated by the order is repaid after cancellation. Default is true</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order list info</returns>        
        Task<WebCallResult<BinanceMarginOrderOcoList>> PlaceMarginOCOOrderAsync(string symbol,
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
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? autoRepayAtCancel = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending margin OCO order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Margin-Account-Cancel-OCO" /><br />
        /// Endpoint:<br />
        /// DELETE /sapi/v1/margin/orderList
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="orderListId">["<c>orderListId</c>"] The id of the order list to cancel</param>
        /// <param name="listClientOrderId">["<c>listClientOrderId</c>"] The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">["<c>newClientOrderId</c>"] The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Identifiers for the canceled order list</returns>
        Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific margin OCO order. Either <paramref name="orderListId"/> or <paramref name="origClientOrderId"/> should be provided.
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-OCO" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/orderList
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Mandatory for isolated margin, not supported for cross margin, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="orderListId">["<c>orderListId</c>"] The list order id of the order</param>
        /// <param name="origClientOrderId">["<c>origClientOrderId</c>"] Client order list id. Either this or <paramref name="orderListId"/> must be provided</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of margin OCO orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-All-OCO" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/allOrderList
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Mandatory for isolated margin, not supported for cross margin, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="fromId">["<c>fromId</c>"] Only return oco orders with id higher than this</param>
        /// <param name="startTime">["<c>startTime</c>"] Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">["<c>endTime</c>"] Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        Task<WebCallResult<BinanceMarginOrderOcoList[]>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of open margin OCO orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/margin_trading/trade/Query-Margin-Account-Open-OCO" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/margin/openOrderList
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Mandatory for isolated margin, not supported for cross margin, for example `ETHUSDT`</param>
        /// <param name="isIsolated">["<c>isIsolated</c>"] Whether to use isolated margin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        Task<WebCallResult<BinanceMarginOrderOcoList[]>> GetMarginOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets customer-to-customer trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/c2c/rest-api" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/c2c/orderMatch/listUserOrderHistory
        /// </para>
        /// </summary>
        /// <param name="side">["<c>tradeType</c>"] Trade side</param>
        /// <param name="startTime">["<c>startTimestamp</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTimestamp</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] The page</param>
        /// <param name="pageSize">["<c>rows</c>"] The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Customer-to-customer trade history</returns>
        Task<WebCallResult<BinanceC2CUserTrade[]>> GetC2CTradeHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets pay trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/pay/rest-api" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/pay/transactions
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Pay trade history</returns>
        Task<WebCallResult<BinancePayTrade[]>> GetPayTradeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Requests a convert quote for converting a quote asset (sell) to a base asset (buy)
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/convert/trade" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/convert/getQuote
        /// </para>
        /// </summary>
        /// <param name="quoteAsset">["<c>fromAsset</c>"] Quote asset, for example `ETH`</param>
        /// <param name="baseAsset">["<c>toAsset</c>"] Base asset, for example `ETH`</param>
        /// <param name="quoteQuantity">["<c>fromAmount</c>"] Quote quantity</param>
        /// <param name="baseQuantity">["<c>toAmount</c>"] Base quantity</param>
        /// <param name="walletType">["<c>walletType</c>"] The wallet type for convert</param>
        /// <param name="validTime">["<c>validTime</c>"] The valid time for quote</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Convert quote details</returns>
        Task<WebCallResult<BinanceConvertQuote>> ConvertQuoteRequestAsync(string quoteAsset, string baseAsset, decimal? quoteQuantity = null, decimal? baseQuantity = null, WalletType? walletType = null, ValidTime? validTime = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Accepts a previously requested quote
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/convert/trade/Accept-Quote" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/convert/acceptQuote
        /// </para>
        /// </summary>
        /// <param name="quoteId">["<c>quoteId</c>"] The quote id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Convert accept quote result</returns>
        Task<WebCallResult<BinanceConvertResult>> ConvertAcceptQuoteAsync(string quoteId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets convert order status
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/convert/trade/Order-Status" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/convert/orderStatus
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] The order id of the order</param>
        /// <param name="quoteId">["<c>quoteId</c>"] The quote id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Convert order status</returns>
        Task<WebCallResult<BinanceConvertOrderStatus>> GetConvertOrderStatusAsync(string? orderId = null, string? quoteId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets convert trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/convert/trade/Get-Convert-Trade-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/convert/tradeFlow
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Convert trade history</returns>
        Task<WebCallResult<BinanceListResult<Objects.Models.Spot.Convert.BinanceConvertTrade>>> GetConvertTradeHistoryAsync(DateTime startTime, DateTime endTime, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets prevented matches due to self-trade prevention
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/binance-spot-api-docs/rest-api/account-endpoints#query-prevented-matches-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/myPreventedMatches
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="preventedMatchId">["<c>preventedMatchId</c>"] Filter by prevented match id</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="fromPreventedMatchId">["<c>fromPreventedMatchId</c>"] Filter by min prevented match id</param>
        /// <param name="limit">["<c>size</c>"] Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Prevented trades</returns>
        Task<WebCallResult<BinancePreventedTrade[]>> GetPreventedTradesAsync(string symbol, long? preventedMatchId = null, long? orderId = null, long? fromPreventedMatchId = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new spot time-weighted average price order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/algo/spot-algo/Time-Weighted-Average-Price-New-Order" /><br />
        /// Endpoint:<br />
        /// POST /sapi/v1/algo/spot/newOrderTwap
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="quantity">["<c>quantity</c>"] Order quantity</param>
        /// <param name="duration">["<c>duration</c>"] Duration in seconds. 300 - 86400</param>
        /// <param name="clientOrderId">["<c>clientAlgoId</c>"] Client order id</param>
        /// <param name="limitPrice">["<c>limitPrice</c>"] Limit price of the order. If null will use market price</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algo order result</returns>
        Task<WebCallResult<BinanceAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            int duration,
            string? clientOrderId = null,
            decimal? limitPrice = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a spot algo order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/algo/spot-algo/Cancel-Algo-Order" /><br />
        /// Endpoint:<br />
        /// DELETE /sapi/v1/algo/spot/order
        /// </para>
        /// </summary>
        /// <param name="algoOrderId">["<c>algoId</c>"] Algo order id to cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algo cancel result</returns>
        Task<WebCallResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all open spot algo orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/algo/spot-algo/Query-Current-Algo-Open-Orders" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/algo/spot/openOrders
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open algo orders</returns>
        Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of closed algo orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/algo/spot-algo/Query-Historical-Algo-Orders" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/algo/spot/historicalOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by side</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>pageSize</c>"] Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Closed algo orders</returns>
        Task<WebCallResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets algo sub-order overview
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/algo/spot-algo/Query-Sub-Orders" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/algo/spot/subOrders
        /// </para>
        /// </summary>
        /// <param name="algoId">["<c>algoId</c>"] Algo id</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="limit">["<c>pageSize</c>"] Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Algo sub-order list</returns>
        Task<WebCallResult<BinanceAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}



