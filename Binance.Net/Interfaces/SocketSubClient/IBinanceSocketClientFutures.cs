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
    /// Futures subscription interface
    /// </summary>
    public interface IBinanceSocketClientFutures: IBinanceSocketClientBase
    {
        
        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(string symbol, int? updateInterval, Action<BinanceFuturesStreamMarkPrice> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<BinanceFuturesStreamMarkPrice> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(IEnumerable<string> symbols, int? updateInterval, Action<BinanceFuturesStreamMarkPrice> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<BinanceFuturesStreamMarkPrice> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a all symbols
        /// </summary>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAllMarkPriceUpdates(int? updateInterval, Action<IEnumerable<BinanceFuturesStreamMarkPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a all symbols
        /// </summary>
        /// /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(int? updateInterval, Action<IEnumerable<BinanceFuturesStreamMarkPrice>> onMessage);
        
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
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onPositionUpdate">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToUserDataUpdates(
            string listenKey,
            Action<decimal>? onCrossWalletUpdate,
            Action<IEnumerable<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<IEnumerable<BinanceFuturesStreamBalance>>? onAccountBalanceUpdate,
            Action<IEnumerable<BinanceFuturesStreamPosition>>? onPositionUpdate,
            Action<BinanceFuturesStreamOrderUpdate>? onOrderUpdate,
            Action<BinanceStreamEvent> onListenKeyExpired);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onCrossWalletUpdate">The event handler for whenever a cross wallet has changed</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onPositionUpdate">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<decimal>? onCrossWalletUpdate,
            Action<IEnumerable<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<IEnumerable<BinanceFuturesStreamBalance>>? onAccountBalanceUpdate,
            Action<IEnumerable<BinanceFuturesStreamPosition>>? onPositionUpdate,
            Action<BinanceFuturesStreamOrderUpdate>? onOrderUpdate,
            Action<BinanceStreamEvent> onListenKeyExpired);
    }
}