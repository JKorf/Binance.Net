using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures account websocket API
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApiAccount
    {
        /// <summary>
        /// Gets account balances
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/websocket-api" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<CallResult<BinanceResponse<IEnumerable<BinanceUsdFuturesAccountBalance>>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account information, including position and balances
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/account/websocket-api/Account-Information-V2" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<BinanceResponse<BinanceFuturesAccountInfoV3>>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="IBinanceRestClientUsdFuturesApiAccount.StartUserStreamAsync(CancellationToken)">restClient.UsdFuturesApi.Account.StartUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/user-data-streams" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IBinanceRestClientUsdFuturesApiAccount.StartUserStreamAsync(CancellationToken)">restClient.UsdFuturesApi.Account.StartUserStreamAsync</see> method</param>
        /// <param name="onLeverageUpdate">The event handler for leverage changed update</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountUpdate">The event handler for whenever an account update is received</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onTradeUpdate">The event handler for whenever an trade status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <param name="onStrategyUpdate">The event handler for whenever a strategy update is received</param>
        /// <param name="onGridUpdate">The event handler for whenever a grid update is received</param>
        /// <param name="onConditionalOrderTriggerRejectUpdate">The event handler for whenever a trigger order failed to place an order</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onLeverageUpdate = null,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate = null,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<BinanceFuturesStreamTradeUpdate>>? onTradeUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
            Action<DataEvent<BinanceStrategyUpdate>>? onStrategyUpdate = null,
            Action<DataEvent<BinanceGridUpdate>>? onGridUpdate = null,
            Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? onConditionalOrderTriggerRejectUpdate = null,
            CancellationToken ct = default);
    }
}
