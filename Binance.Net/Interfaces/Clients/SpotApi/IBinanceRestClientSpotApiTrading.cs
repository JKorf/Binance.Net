using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.ConvertTransfer;
using Binance.Net.Objects.Models.Spot.Margin;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IBinanceRestClientSpotApiTrading
    {
        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#test-new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type (limit/market)</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel)</param>
        /// <param name="quantity">The quantity of the symbol</param>
        /// <param name="quoteQuantity">The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">User for iceberg orders</param>
        /// <param name="orderResponseType">Used for the response JSON</param>
        /// <param name="trailingDelta">Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">Strategy id</param>
        /// <param name="strategyType">Strategy type</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="computeFeeRates">Whether fee rates should be calculated or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Fee info if computeCommissionRates was set to true, else empty</returns>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The quantity of the symbol</param>
        /// <param name="quoteQuantity">The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">Used for the response JSON</param>
        /// <param name="trailingDelta">Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">Strategy id</param>
        /// <param name="strategyType">Strategy type</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
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
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#cancel-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="cancelRestriction">Restrict cancellation based on order state</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceOrderBase>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, CancelRestriction? cancelRestriction = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels all open orders on a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#cancel-all-open-orders-on-a-symbol-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an existing order and place a new order on the same symbol
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="cancelReplaceMode">Replacement behavior</param>
        /// <param name="cancelOrderId">The order id to cancel. Either this or cancelClientOrderId should be provided</param>
        /// <param name="cancelClientOrderId">The client order id to cancel. Either this or cancelOrderId should be provided</param>
        /// <param name="newCancelClientOrderId">New client order id for the canceled order</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The quantity of the symbol</param>
        /// <param name="quoteQuantity">The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQty">Used for iceberg orders</param>
        /// <param name="orderResponseType">Used for the response JSON</param>
        /// <param name="trailingDelta">Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">Strategy id</param>
        /// <param name="strategyType">Strategy type</param>
        /// <param name="cancelRestriction">Restrict cancellation based on order state</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        Task<WebCallResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#current-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all orders for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#all-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for, for example `ETHUSDT`</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED, USE PlaceOcoOrderListAsync INSTEAD
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#new-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The quantity of the symbol</param>
        /// <param name="price">The price to use</param>
        /// <param name="stopPrice">The stop price</param>
        /// <param name="stopLimitPrice">The price for the stop limit order</param>
        /// <param name="stopClientOrderId">Client id for the stop order</param>
        /// <param name="limitClientOrderId">Client id for the limit order</param>
        /// <param name="listClientOrderId">Client id for the order list</param>
        /// <param name="limitIcebergQuantity">Iceberg quantity for the limit order</param>
        /// <param name="stopIcebergQuantity">Iceberg quantity for the stop order</param>
        /// <param name="trailingDelta">Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="limitStrategyId">Strategy id of the limit order</param>
        /// <param name="limitStrategyType">Strategy type of the limit order</param>
        /// <param name="stopStrategyId">Strategy id of the stop order</param>
        /// <param name="stopStrategyType">Strategy type of the stop order</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
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
        /// Place a new OCO order. An OCO has 2 legs called the above leg and below leg. One of the legs must be a LimitMaker order and the other leg must be StopLoss or StopLossLimit order.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#new-order-list-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="quantity">The quantity of the symbol</param>
        /// <param name="aboveOrderType">The above leg order type</param>
        /// <param name="belowOrderType">The below leg order type</param>
        /// <param name="aboveClientOrderId">Client order id for the above leg</param>
        /// <param name="aboveIcebergQuantity">Ice berg quantity for the above leg</param>
        /// <param name="abovePrice">Limit price for the above leg</param>
        /// <param name="aboveStopPrice">Stop price for the above leg</param>
        /// <param name="aboveTrailingDelta">Trailing delta for the above leg</param>
        /// <param name="aboveTimeInForce">Time in force for the above leg</param>
        /// <param name="aboveStrategyId">Strategy id for the above leg</param>
        /// <param name="aboveStrategyType">Strategy type for the above leg</param>
        /// <param name="belowClientOrderId">Client order id for the below leg</param>
        /// <param name="belowIcebergQuantity">Ice berg quantity for the below leg</param>
        /// <param name="belowPrice">Limit price for the below leg</param>
        /// <param name="belowStopPrice">Stop price for the below leg</param>
        /// <param name="belowTrailingDelta">Trailing delta for the below leg</param>
        /// <param name="belowTimeInForce">Time in force for the below leg</param>
        /// <param name="belowStrategyId">Strategy id for the below leg</param>
        /// <param name="belowStrategyType">Strategy type for the below leg</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceOrderOcoList>> PlaceOcoOrderListAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            SpotOrderType aboveOrderType,
            SpotOrderType belowOrderType,

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
        /// Cancels a pending oco order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#cancel-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderListId">The id of the order list to cancel</param>
        /// <param name="listClientOrderId">The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceOrderOcoList>> CancelOcoOrderAsync(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific oco order. Either orderListId or listClientOrderId should be provided.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-oco-user_data" /></para>
        /// </summary>
        /// <param name="orderListId">The list order id of the order</param>
        /// <param name="listClientOrderId">The client order id of the list order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        Task<WebCallResult<BinanceOrderOcoList>> GetOcoOrderAsync(long? orderListId = null, string? listClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of oco orders matching the parameters
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-all-oco-user_data" /></para>
        /// </summary>
        /// <param name="fromId">Only return oco orders with id higher than this</param>
        /// <param name="startTime">Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOcoOrdersAsync(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of open oco orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-open-oco-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOpenOcoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new OTOCO (One Triggers Other) order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#new-order-list-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="workingOrderType">Working order type, either Limit or LimitMaker</param>
        /// <param name="workingSide">Working order side</param>
        /// <param name="workingQuantity">Working order quantity</param>
        /// <param name="workingPrice">Working order price</param>
        /// <param name="pendingQuantity">Pending order quantity</param>
        /// <param name="pendingSide">Pending order side</param>
        /// <param name="pendingOrderType">Pending order type</param>
        /// <param name="listClientOrderId">Arbitrary unique ID among open order lists. Automatically generated if not sent.</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="workingClientOrderId">Working order client order id</param>
        /// <param name="workingIcebergQuantity">Working order iceberg quantity</param>
        /// <param name="workingTimeInForce">Working order time in force</param>
        /// <param name="workingStrategyId">Working order strategy id</param>
        /// <param name="workingStrategyType">Working order strategy type</param>
        /// <param name="pendingClientOrderId">Pending order client order id</param>
        /// <param name="pendingPrice">Pending order price</param>
        /// <param name="pendingStopPrice">Pending order stop price</param>
        /// <param name="pendingTrailingDelta">Pending order trailing delta</param>
        /// <param name="pendingIcebergQuantity">Pending order iceberg quantity</param>
        /// <param name="pendingTimeInForce">Pending order time in force</param>
        /// <param name="pendingStrategyId">Pending order strategy id</param>
        /// <param name="pendingStrategyType">Pending order strategy type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
        /// Place a new OTOCO (One Triggers One Cancels The Other) order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#new-order-list-otoco-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="workingOrderType">Working order type, either Limit or LimitMaker</param>
        /// <param name="workingSide">Working order side</param>
        /// <param name="workingQuantity">Working order quantity</param>
        /// <param name="workingPrice">Working order price</param>
        /// <param name="pendingQuantity">Pending order quantity</param>
        /// <param name="pendingSide">Pending order side</param>
        /// <param name="pendingAboveOrderType">Pending above order type, LimitMaker, StopLoss or StopLossLimit</param>
        /// <param name="pendingBelowOrderType">Pending below order type, LimitMaker, StopLoss or StopLossLimit</param>
        /// <param name="listClientOrderId">Arbitrary unique ID among open order lists. Automatically generated if not sent.</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="workingClientOrderId">Working order client order id</param>
        /// <param name="workingIcebergQuantity">Working order iceberg quantity</param>
        /// <param name="workingTimeInForce">Working order time in force</param>
        /// <param name="workingStrategyId">Working order strategy id</param>
        /// <param name="workingStrategyType">Working order strategy type</param>
        /// <param name="pendingAboveClientOrderId">Pending above order client order id</param>
        /// <param name="pendingAbovePrice">Pending above order price</param>
        /// <param name="pendingAboveStopPrice">Pending above order stop price</param>
        /// <param name="pendingAboveTrailingDelta">Pending above order trailing delta</param>
        /// <param name="pendingAboveIcebergQuantity">Pending above order iceberg quantity</param>
        /// <param name="pendingAboveTimeInForce">Pending above order time in force</param>
        /// <param name="pendingAboveStrategyId">Pending above order strategy id</param>
        /// <param name="pendingAboveStrategyType">Pending above order strategy type</param>
        /// <param name="pendingBelowClientOrderId">Pending below order client order id</param>
        /// <param name="pendingBelowPrice">Pending below order price</param>
        /// <param name="pendingBelowStopPrice">Pending below order stop price</param>
        /// <param name="pendingBelowTrailingDelta">Pending below order trailing delta</param>
        /// <param name="pendingBelowIcebergQuantity">Pending below order iceberg quantity</param>
        /// <param name="pendingBelowTimeInForce">Pending below order time in force</param>
        /// <param name="pendingBelowStrategyId">Pending below order strategy id</param>
        /// <param name="pendingBelowStrategyType">Pending below order strategy type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#account-trade-list-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get trades for, for example `ETHUSDT`</param>
        /// <param name="orderId">Get trades for this order id</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trades</returns>
        Task<WebCallResult<IEnumerable<BinanceTrade>>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Margin account new order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#margin-account-new-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="type">The order type</param>
        /// <param name="timeInForce">Lifetime of the order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The quantity of the symbol</param>
        /// <param name="quoteQuantity">The quantity of the quote symbol. Only valid for market orders</param>
        /// <param name="price">The price to use</param>
        /// <param name="newClientOrderId">Unique id for order</param>
        /// <param name="stopPrice">Used for stop orders</param>
        /// <param name="icebergQuantity">Used for iceberg orders</param>
        /// <param name="sideEffectType">Side effect type for this order</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderResponseType">Used for the response JSON</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="autoRepayAtCancel">Only when MARGIN_BUY or AUTO_BORROW_REPAY order takes effect, true means that the debt generated by the order needs to be repay after the order is cancelled. The default is true</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
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
        /// Cancel an active order for margin account
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="newClientOrderId">Unique identifier for this cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceOrderBase>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all active orders for a symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-all-open-orders-on-a-symbol-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the to cancel orders for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific margin account order. Either orderId or origClientOrderId should be provided.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific margin account order</returns>
        Task<WebCallResult<BinanceOrder>> GetMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open margin account orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get open orders for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of open margin account orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenMarginOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all margin account orders for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-all-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderId">If set, only orders with an order id higher than the provided will be returned</param>
        /// <param name="startTime">If set, only orders placed after this time will be returned</param>
        /// <param name="endTime">If set, only orders placed before this time will be returned</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account orders</returns>
        Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets all user margin account trades for provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-trade-list-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get trades for, for example `ETHUSDT`</param>
        /// <param name="orderId">Trades associated with orderId</param>
        /// <param name="startTime">Orders newer than this date will be retrieved</param>
        /// <param name="endTime">Orders older than this date will be retrieved</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="fromId">TradeId to fetch from. Default gets most recent trades</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin account trades</returns>
        Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMarginUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null,
            int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new margin OCO(One cancels other) order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#margin-account-new-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="side">The order side (buy/sell)</param>
        /// <param name="stopLimitTimeInForce">Lifetime of the stop order (GoodTillCancel/ImmediateOrCancel/FillOrKill)</param>
        /// <param name="quantity">The quantity of the symbol</param>
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
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="autoRepayAtCancel">Only when MARGIN_BUY or AUTO_BORROW_REPAY order takes effect, true means that the debt generated by the order needs to be repay after the order is cancelled. The default is true</param>
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
        /// Cancels a pending margin oco order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#margin-account-cancel-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderListId">The id of the order list to cancel</param>
        /// <param name="listClientOrderId">The client order id of the order list to cancel</param>
        /// <param name="newClientOrderId">The new client order list id for the order list</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific margin oco order. Either orderListId or listClientOrderId should be provided.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-oco-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Mandatory for isolated margin, not supported for cross margin, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="orderListId">The list order id of the order</param>
        /// <param name="origClientOrderId">Either orderListId or listClientOrderId must be provided</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order list</returns>
        Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of margin oco orders matching the parameters
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-all-oco-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Mandatory for isolated margin, not supported for cross margin, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="fromId">Only return oco orders with id higher than this</param>
        /// <param name="startTime">Only return oco orders placed later than this. Only valid if fromId isn't provided</param>
        /// <param name="endTime">Only return oco orders placed before this. Only valid if fromId isn't provided</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order lists matching the parameters</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a list of open margin oco orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-margin-account-39-s-open-oco-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Mandatory for isolated margin, not supported for cross margin, for example `ETHUSDT`</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Open order lists</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to a token
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#subscribe-blvt-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token to subscribe to</param>
        /// <param name="cost">Cost of the subscription</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBlvtSubscribeResult>> SubscribeLeveragedTokenAsync(string tokenName, decimal cost, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get subscription records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-subscription-record-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Filter by token</param>
        /// <param name="id">Filter by id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBlvtSubscription>>> GetLeveragedTokensSubscriptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem a token
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-blvt-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Name of the token to redeem</param>
        /// <param name="quantity">Quantity to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBlvtRedeemResult>> RedeemLeveragedTokenAsync(string tokenName, decimal quantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-redemption-record-user_data" /></para>
        /// </summary>
        /// <param name="tokenName">Filter by token</param>
        /// <param name="id">Filter by id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBlvtRedemption>>> GetLeveragedTokensRedemptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Customer to Customer trade history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-c2c-trade-history-user_data" /></para>
        /// </summary>
        /// <param name="side">Trade side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceC2CUserTrade>>> GetC2CTradeHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get pay trade history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-pay-trade-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinancePayTrade>>> GetPayTradeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Request a quote for convert asset (selling asset) for base asset (buying asset)
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#send-quote-request-user_data" /></para>
        /// </summary>
        /// <param name="quoteAsset">Quote asset, for example `ETH`</param>
        /// <param name="baseAsset">Base asset, for example `ETH`</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="baseQuantity">Quote quantity</param>
        /// <param name="walletType">The wallet type for convert</param>
        /// <param name="validTime">The valid time for quote</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceConvertQuote>> ConvertQuoteRequestAsync(string quoteAsset, string baseAsset, decimal? quoteQuantity = null, decimal? baseQuantity = null, WalletType? walletType = null, ValidTime? validTime = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Accept the previously requested quote
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#accept-quote-trade" /></para>
        /// </summary>
        /// <param name="quoteId">The quote id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceConvertResult>> ConvertAcceptQuoteAsync(string quoteId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get convert order status
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#order-status-user_data" /></para>
        /// </summary>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="quoteId">The quote id of the order</param>
        /// <param name="receiveWindow"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<BinanceConvertOrderStatus>> GetConvertOrderStatusAsync(string? orderId = null, string? quoteId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get convert trade history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-convert-trade-history-user_data" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceListResult<Objects.Models.Spot.Convert.BinanceConvertTrade>>> GetConvertTradeHistoryAsync(DateTime startTime, DateTime endTime, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get prevented matches because of self trade prevention
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-prevented-matches-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="preventedMatchId">Filter by prevented match id</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="fromPreventedMatchId">Filter by min prevented match id</param>
        /// <param name="limit">Max results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinancePreventedTrade>>> GetPreventedTradesAsync(string symbol, long? preventedMatchId = null, long? orderId = null, long? fromPreventedMatchId = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new spot time weighted average price order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#time-weighted-average-price-twap-new-order-trade-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="duration">Duration in seconds. 300 - 86400</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="limitPrice">Limit price of the order. If null will use market price</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
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
        /// Cancel a spot algo order
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#cancel-algo-order-trade-2" /></para>
        /// </summary>
        /// <param name="algoOrderId">Algo order id to cancel</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get all open spot algo orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-current-algo-open-orders-user_data-2" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of closed algo orders
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-historical-algo-orders-user_data-2" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#query-historical-algo-orders-user_data-2" /></para>
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
