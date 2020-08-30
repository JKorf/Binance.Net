using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.MarketStream;
using Binance.Net.Objects.Futures.UserStream;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Interfaces.SocketSubClient
{
    /// <summary>
    /// Futures socket client streams
    /// </summary>
    public interface IBinanceSocketClientFutures: IBinanceSocketClientBase
    { 
        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToLiquidationUpdates(string symbol, Action<BinanceFuturesStreamLiquidation> onMessage);

        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<BinanceFuturesStreamLiquidation> onMessage);

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToLiquidationUpdates(IEnumerable<string> symbols, Action<BinanceFuturesStreamLiquidation> onMessage);

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<BinanceFuturesStreamLiquidation> onMessage);

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAllLiquidationUpdates(Action<BinanceFuturesStreamLiquidation> onMessage);

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<BinanceFuturesStreamLiquidation> onMessage);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onCrossWalletUpdate">The event handler for whenever a cross wallet has changed</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountUpdate">The event handler for whenever an account update is received</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToUserDataUpdates(
            string listenKey,
            Action<decimal>? onCrossWalletUpdate,
            Action<IEnumerable<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<BinanceFuturesAccountUpdate>? onAccountUpdate,
            Action<BinanceFuturesStreamOrderUpdate>? onOrderUpdate,
            Action<BinanceStreamEvent> onListenKeyExpired);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onCrossWalletUpdate">The event handler for whenever a cross wallet has changed</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountUpdate">The event handler for whenever an account update is received</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<decimal>? onCrossWalletUpdate,
            Action<IEnumerable<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<BinanceFuturesAccountUpdate>? onAccountUpdate,
            Action<BinanceFuturesStreamOrderUpdate>? onOrderUpdate,
            Action<BinanceStreamEvent> onListenKeyExpired);
    }
}