using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.TokenManagement;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal class BinanceSocketClientUsdFuturesApiAccount : IBinanceSocketClientUsdFuturesApiAccount
    {
        private readonly BinanceSocketClientUsdFuturesApi _client;
        private readonly ILogger _logger;

        internal BinanceSocketClientUsdFuturesApiAccount(ILogger logger, BinanceSocketClientUsdFuturesApi client)
        {
            _client = client;
            _logger = logger;
        }

        #region Queries

        #region Future Account Balance

        /// <inheritdoc />
        public async Task<QueryResult<BinanceResponse<BinanceUsdFuturesAccountBalance[]>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceUsdFuturesAccountBalance[]>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"v2/account.balance", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<QueryResult<BinanceResponse<BinanceFuturesAccountInfoV3>>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));
            return await _client.QueryAsync<BinanceFuturesAccountInfoV3>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"v2/account.status", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<QueryResult<BinanceResponse<BinanceFuturesAccountInfo>>> GetAccountInfoV1Async(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));
            return await _client.QueryAsync<BinanceFuturesAccountInfo>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"account.status", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region User Data Streams

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onConfigUpdate = null,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate = null,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<BinanceFuturesStreamTradeUpdate>>? onTradeUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
            Action<DataEvent<BinanceStrategyUpdate>>? onStrategyUpdate = null,
            Action<DataEvent<BinanceGridUpdate>>? onGridUpdate = null,
            Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? onConditionalOrderTriggerRejectUpdate = null,
            Action<DataEvent<BinanceAlgoOrderUpdate>>? onAlgoOrderUpdate = null,
            CancellationToken ct = default)
            => await SubscribeToUserDataUpdatesAsync(
                null,
                onConfigUpdate,
                onMarginUpdate,
                onAccountUpdate, 
                onOrderUpdate,
                onTradeUpdate,
                onListenKeyExpired,
                onStrategyUpdate,
                onGridUpdate,
                onConditionalOrderTriggerRejectUpdate,
                onAlgoOrderUpdate,
                ct).ConfigureAwait(false);



        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string? listenKey = null,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onConfigUpdate = null,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate = null,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<BinanceFuturesStreamTradeUpdate>>? onTradeUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
            Action<DataEvent<BinanceStrategyUpdate>>? onStrategyUpdate = null,
            Action<DataEvent<BinanceGridUpdate>>? onGridUpdate = null,
            Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? onConditionalOrderTriggerRejectUpdate = null,
            Action<DataEvent<BinanceAlgoOrderUpdate>>? onAlgoOrderUpdate = null,
            CancellationToken ct = default)
        {
            if (listenKey == null && !_client.Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(_client.Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {   
                var leaseResult = await _client.TokenManager.AcquireAsync(new TokenScope(
                    BinanceExchange.Metadata.Id,
                    _client.EnvironmentName,
                    "UsdFutures",
                    _client.ApiCredentials!.Credential!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(_client.Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var subscription = new BinanceUsdFuturesUserDataSubscription(
                _logger,
                _client,
                listenKey,
                onOrderUpdate,
                onTradeUpdate,
                onConfigUpdate,
                onMarginUpdate,
                onAccountUpdate,
                onListenKeyExpired,
                onStrategyUpdate,
                onGridUpdate,
                onConditionalOrderTriggerRejectUpdate,
                onAlgoOrderUpdate)
            {
                TokenLease = lease
            };
            var result = await _client.SubscribeInternalAsync(_client.BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        #endregion

        #endregion
    }
}
