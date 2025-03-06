using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot Trading socket requests
    /// </summary>
    public interface IBinanceSocketClientSpotApiTrading
    {
        /// <summary>
        /// Cancel all open orders for the symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-open-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrder[]>>> CancelAllOrdersAsync(string symbol, CancellationToken ct = default);
        /// <summary>
        /// Cancel an Oco order by either orderId or clientOrderId
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-oco-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="newClientOrderId">New client order id for the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> CancelOcoOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, string? newClientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Cancel an order by either orderId or clientOrderId
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="newClientOrderId">New client order id for the order</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrder>>> CancelOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, string? newClientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get an oco order by either orderId or clientOrderId
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#account-oco-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Order id, for example `ETHUSDT`</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> GetOcoOrderAsync(long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get Oco order history
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#query-oco-user_data" /></para>
        /// </summary>
        /// <param name="fromOrderId">Filter from order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrderOcoList[]>>> GetOcoOrdersAsync(long? fromOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get open Oco orders
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#current-open-ocos-user_data" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrderOcoList[]>>> GetOpenOcoOrdersAsync(CancellationToken ct = default);
        /// <summary>
        /// Get open orders
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#current-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrder[]>>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default);
        /// <summary>
        /// Get order by either orderId or clientOrderId
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#query-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrder>>> GetOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, CancellationToken ct = default);
        /// <summary>
        /// Get order history
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#account-order-history-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="fromOrderId">Filter from order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrder[]>>> GetOrdersAsync(string symbol, long? fromOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get prevented trades because of self trade prevention
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#account-prevented-matches-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="preventedTradeId">Filter by prevented trade id</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="fromPreventedTradeId">Filter from prevented id</param>
        /// <param name="limit">Max results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinancePreventedTrade[]>>> GetPreventedTradesAsync(string symbol, long? preventedTradeId = null, long? orderId = null, long? fromPreventedTradeId = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Gets user trades for provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#account-trade-history-user_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="fromOrderId">Filter from order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceTrade[]>>> GetUserTradesAsync(string symbol, long? orderId = null, long? fromOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// DEPRECATED, USE PlaceOcoOrderListAsync INSTEAD
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-oco-trade" /></para>
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
        /// <param name="limitIcebergQty">Iceberg quantity of the limit order</param>
        /// <param name="stopIcebergQty">Iceberg quantity of the stop order</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order list info</returns>
        Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> PlaceOcoOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, decimal stopPrice, decimal? stopLimitPrice = null, string? listClientOrderId = null, string? limitClientOrderId = null, string? stopClientOrderId = null, decimal? limitIcebergQuantity = null, decimal? stopIcebergQuantity = null, TimeInForce? stopLimitTimeInForce = null, int? trailingDelta = null, int? limitStrategyId = null, int? limitStrategyType = null, decimal? limitIcebergQty = null, int? stopStrategyId = null, int? stopStrategyType = null, int? stopIcebergQty = null, SelfTradePreventionMode? selfTradePreventionMode = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new OCO order. An OCO has 2 legs called the above leg and below leg. One of the legs must be a LimitMaker order and the other leg must be StopLoss or StopLossLimit order.
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-order-list-oco-trade" /></para>
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
        /// <param name="listClientOrderId">List client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> PlaceOcoOrderListAsync(
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
            string? listClientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Places a new order
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#place-new-order-trade" /></para>
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
        /// <param name="trailingDelta">Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">Strategy id</param>
        /// <param name="strategyType">Strategy type</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed order</returns>
        Task<CallResult<BinanceResponse<BinancePlacedOrder>>> PlaceOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = null, decimal? quoteQuantity = null, string? newClientOrderId = null, decimal? price = null, TimeInForce? timeInForce = null, decimal? stopPrice = null, decimal? icebergQty = null, int? trailingDelta = null, int? strategyId = null, int? strategyType = null, SelfTradePreventionMode? selfTradePreventionMode = null, CancellationToken ct = default);

        /// <summary>
        /// Places a new test order. Test orders are not actually being executed and just test the functionality.
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#test-new-order-trade" /></para>
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
        /// <param name="trailingDelta">Trailing delta value for order in BIPS. A value of 1 means 0.01% trailing delta.</param>
        /// <param name="strategyId">Strategy id</param>
        /// <param name="strategyType">Strategy type</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for the placed test order</returns>
        Task<CallResult<BinanceResponse<BinancePlacedOrder>>> PlaceTestOrderAsync(string symbol, OrderSide side, SpotOrderType type, decimal? quantity = null, decimal? quoteQuantity = null, string? newClientOrderId = null, decimal? price = null, TimeInForce? timeInForce = null, decimal? stopPrice = null, decimal? icebergQty = null, int? trailingDelta = null, int? strategyId = null, int? strategyType = null, SelfTradePreventionMode? selfTradePreventionMode = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an existing order and place a new order on the same symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#cancel-and-replace-order-trade" /></para>
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
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceReplaceOrderResult>>> ReplaceOrderAsync(string symbol, OrderSide side, SpotOrderType type, CancelReplaceMode cancelReplaceMode, long? cancelOrderId = null, string? cancelClientOrderId = null, string? newCancelClientOrderId = null, string? newClientOrderId = null, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, TimeInForce? timeInForce = null, decimal? stopPrice = null, decimal? icebergQty = null, OrderResponseType? orderResponseType = null, int? trailingDelta = null, int? strategyId = null, int? strategyType = null, CancellationToken ct = default);
    }
}
