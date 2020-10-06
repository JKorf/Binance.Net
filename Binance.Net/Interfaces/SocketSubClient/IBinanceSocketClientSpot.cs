using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.MarketStream;
using Binance.Net.Objects.Spot.UserStream;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Interfaces.SocketSubClient
{
    /// <summary>
    /// Spot subscription interface
    /// </summary>
    public interface IBinanceSocketClientSpot: IBinanceSocketClientBase
    {
        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToTradeUpdates(string symbol,
            Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToTradeUpdates(IEnumerable<string> symbols,
            Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Spot.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">*DEPRICATED; use onAccountPositionMessage instead, this update will be removed in the future* The event handler for whenever an account info update is received</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToUserDataUpdates(
            string listenKey,
            Action<BinanceStreamAccountInfo>? onAccountInfoMessage,
            Action<BinanceStreamOrderUpdate>? onOrderUpdateMessage,
            Action<BinanceStreamOrderList>? onOcoOrderUpdateMessage,
            Action<BinanceStreamPositionsUpdate>? onAccountPositionMessage,
            Action<BinanceStreamBalanceUpdate>? onAccountBalanceUpdate);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Spot.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">*DEPRICATED; use onAccountPositionMessage instead, this update will be removed in the future* The event handler for whenever an account info update is received</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco order status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<BinanceStreamAccountInfo>? onAccountInfoMessage,
            Action<BinanceStreamOrderUpdate>? onOrderUpdateMessage,
            Action<BinanceStreamOrderList>? onOcoOrderUpdateMessage,
            Action<BinanceStreamPositionsUpdate>? onAccountPositionMessage,
            Action<BinanceStreamBalanceUpdate>? onAccountBalanceUpdate);
    }
}