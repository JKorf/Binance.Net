using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Margin;
using Binance.Net.Objects.Models.Spot.Socket;
using Binance.Net.Objects.Sockets.Subscriptions;
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
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/websocket-api/account-requests" /></para>
        /// </summary>
        /// <param name="omitZeroBalances">When true only return non-zero balances in the account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceAccountInfo>>> GetAccountInfoAsync(bool? omitZeroBalances = null, CancellationToken ct = default);

        /// <summary>
        /// Get order rate limit status
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/websocket-api/account-requests#unfilled-order-count-user_data" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbols, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<CallResult<BinanceResponse<BinanceCurrentRateLimit[]>>> GetOrderRateLimitsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the account update stream
        /// <para><a href="https://developers.binance.com/docs/binance-spot-api-docs/user-data-stream#web-socket-payloads" /></para>
        /// </summary>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco order status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onUserDataStreamTerminated">The event handler for when the User Data Stream is stopped</param>
        /// <param name="onBalanceLockUpdate">The event handler for when the part of your spot wallet balance is locked/unlocked by an external system, for example when used as margin collateral.</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
                                                                             Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
                                                                             Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
                                                                             Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
                                                                             Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
                                                                             Action<DataEvent<BinanceStreamEvent>>? onUserDataStreamTerminated = null,
                                                                             Action<DataEvent<BinanceStreamBalanceLockUpdate>>? onBalanceLockUpdate = null,
                                                                             CancellationToken ct = default);


        /// <summary>
        /// Subscribes to the cross margin account update stream using a listenToken
        /// <para><a href="https://developers.binance.com/docs/margin_trading/trade-data-stream" /></para>
        /// </summary>
        /// <param name="listenToken">The listenToken obtained from StartMarginUserListenTokenAsync</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco order status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onUserDataStreamTerminated">The event handler for when the User Data Stream is stopped</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarginUserDataUpdatesAsync(
            string listenToken,
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onUserDataStreamTerminated = null,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the risk data account update stream. Prior to using this, the <see cref="IBinanceRestClientSpotApiAccount.StartRiskDataUserStreamAsync(CancellationToken)">StartRiskDataUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IBinanceRestClientSpotApiAccount.StartRiskDataUserStreamAsync(CancellationToken)">StartRiskDataUserStreamAsync</see> method</param>
        /// <param name="onMarginCallUpdate">Event handler for margin call status updates</param>
        /// <param name="onLiabilityUpdate">Event handler for liability updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserRiskDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceMarginCallUpdate>>? onMarginCallUpdate = null,
            Action<DataEvent<BinanceLiabilityUpdate>>? onLiabilityUpdate = null,
            CancellationToken ct = default);

        /// <summary>
        /// Seamlessly renew the margin user data stream listen token on the existing
        /// connection without disconnecting. Call every ~12 hours before expiry.
        /// </summary>
        Task<CallResult> RenewMarginUserDataTokenAsync(
            string newListenToken,
            CancellationToken ct = default);
    }
}