﻿using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Binance Spot Account socket requests and subscriptions
    /// </summary>
    public interface IBinanceSocketClientSpotApiAccount
    {
        /// <summary>
        /// Gets account information, including balances
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#account-information-user_data" /></para>
        /// </summary>
        /// <param name="omitZeroBalances">When true only return non-zero balances in the account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceAccountInfo>>> GetAccountInfoAsync(bool? omitZeroBalances = null, CancellationToken ct = default);

        /// <summary>
        /// Get order rate limit status
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#account-order-rate-limits-user_data" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<IEnumerable<BinanceCurrentRateLimit>>>> GetOrderRateLimitsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Sends a keep alive for the current user stream listen key to keep the stream from closing. Stream auto closes after 60 minutes if no keep alive is send. 30 minute interval for keep alive is recommended.
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#ping-user-data-stream-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<object>>> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);
        /// <summary>
        /// Starts a user stream by requesting a listen key. This listen key can be used in a subsequent request to SubscribeToUserDataUpdates. The stream will close after 60 minutes unless a keep alive is send.
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#start-user-data-stream-user_stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<string>>> StartUserStreamAsync(CancellationToken ct = default);
        /// <summary>
        /// Stops the current user stream
        /// <para><a href="https://binance-docs.github.io/apidocs/websocket_api/en/#stop-user-data-stream-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<object>>> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Spot.UserStreams.StartUserStream method should be called.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#user-data-streams" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco order status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onListenKeyExpired">The event handler for when the listen key has expired. No events will be send anymore after this</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(string listenKey,
                                                                             Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
                                                                             Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
                                                                             Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
                                                                             Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
                                                                             Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
                                                                             CancellationToken ct = default);
    }
}