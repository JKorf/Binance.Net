using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures trading websocket API
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/websocket-api" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
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
        /// <param name="priceMatch">Only avaliable for Limit/Stop/TakeProfit order</param>
        /// <param name="selfTradePreventionMode">Self trade prevention mode</param>
        /// <param name="goodTillDate">Order cancel time for timeInForce GoodTillDate</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> PlaceOrderAsync(string symbol,
            Enums.OrderSide side,
            FuturesOrderType type,
            decimal? quantity,
            decimal? price = null,
            Enums.PositionSide? positionSide = null,
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
            PriceMatch? priceMatch = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            DateTime? goodTillDate = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Edit an existing order
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/websocket-api/Modify-Order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="price">New price</param>
        /// <param name="priceMatch">Only avaliable for Limit/Stop/TakeProfit order</param>
        /// <param name="orderId">Order id of the order to edit</param>
        /// <param name="origClientOrderId">Client order id of the order to edit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> EditOrderAsync(string symbol, OrderSide side, decimal quantity, decimal? price = null, PriceMatch? priceMatch = null, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels a pending order
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/websocket-api/Cancel-Order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Id's for canceled order</returns>
        Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieves data for a specific order. Either orderId or origClientOrderId should be provided.
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/websocket-api/Query-Order" /></para>
        /// </summary>
        /// <param name="symbol">The symbol the order is for, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id of the order</param>
        /// <param name="origClientOrderId">The client order id of the order</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The specific order</returns>
        Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position information
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/trade/websocket-api/Position-Info-V2" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<BinanceResponse<IEnumerable<BinancePositionV3>>>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

    }
}
