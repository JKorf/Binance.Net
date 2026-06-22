using Binance.Net.Objects.Models.Spot.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Subscriptions;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net.TokenManagement;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceSocketClientSpotApiAccount : IBinanceSocketClientSpotApiAccount
    {
        private readonly BinanceSocketClientSpotApi _client;
        private readonly string? _riskDataBaseAddress;

        private readonly ILogger _logger;

        #region constructor/destructor

        internal BinanceSocketClientSpotApiAccount(ILogger logger, BinanceSocketClientSpotApi client)
        {
            _client = client;
            _riskDataBaseAddress = client.ClientOptions.Environment.RiskDataSocketAddress;
            _logger = logger;
        }

        #endregion

        #region Queries

        #region Get Account Info

        /// <inheritdoc />
        public async Task<QueryResult<BinanceResponse<BinanceAccountInfo>>> GetAccountInfoAsync(bool? omitZeroBalances = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("omitZeroBalances", omitZeroBalances?.ToString().ToLowerInvariant());
            return await _client.QueryAsync<BinanceAccountInfo>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"account.status", parameters, true, true, weight: 20, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Rate Limits

        /// <inheritdoc />
        public async Task<QueryResult<BinanceResponse<BinanceCurrentRateLimit[]>>> GetOrderRateLimitsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbols", symbols);
            return await _client.QueryAsync<BinanceCurrentRateLimit[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"account.rateLimits.orders", parameters, true, true, weight: 40, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region User Data Stream

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onUserDataStreamTerminated = null,
            Action<DataEvent<BinanceStreamBalanceLockUpdate>>? onBalanceLockUpdate = null,
            CancellationToken ct = default)
        {
            var subscription = new BinanceSpotUserDataSubscription(_logger, _client, onOrderUpdateMessage, onOcoOrderUpdateMessage, onAccountPositionMessage, onAccountBalanceUpdate, onUserDataStreamTerminated, onBalanceLockUpdate, false);
            return await _client.SubscribeInternal2Async(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), subscription, ct).ConfigureAwait(false);
        }
        #endregion

        #region Risk User Data Stream

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToUserRiskDataUpdatesAsync(
            Action<DataEvent<BinanceMarginCallUpdate>>? onMarginCallUpdate = null,
            Action<DataEvent<BinanceLiabilityUpdate>>? onLiabilityUpdate = null,
            CancellationToken ct = default)
            => SubscribeToUserRiskDataUpdatesAsync(null, onMarginCallUpdate, onLiabilityUpdate, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserRiskDataUpdatesAsync(
            string? listenKey,
            Action<DataEvent<BinanceMarginCallUpdate>>? onMarginCallUpdate = null,
            Action<DataEvent<BinanceLiabilityUpdate>>? onLiabilityUpdate = null,
            CancellationToken ct = default)
        {
            if (_riskDataBaseAddress == null)
                throw new NotSupportedException("RiskData base address not configured");

            if (listenKey == null && !_client.Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(_client.Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await _client.RiskDataTokenManager.AcquireAsync(new TokenScope(
                    BinanceExchange.Metadata.Id,
                    _client.EnvironmentName,
                    "RiskData",
                    _client.ApiCredentials!.Credential!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(_client.Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var subscription = new BinanceMarginRiskDataSubscription(_logger, _client, listenKey, onMarginCallUpdate, onLiabilityUpdate, false)
            {
                TokenLease = lease
            };
            return await _client.SubscribeInternalAsync(_riskDataBaseAddress, subscription, ct).ConfigureAwait(false);
        }
        #endregion

        #region Margin User Data Stream

        public Task<WebSocketResult<UpdateSubscription>> SubscribeToMarginUserDataUpdatesAsync(
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onUserDataStreamTerminated = null,
            CancellationToken ct = default)
            => SubscribeToMarginUserDataUpdatesAsync(null, onOrderUpdateMessage, onOcoOrderUpdateMessage, onAccountPositionMessage, onAccountBalanceUpdate, onUserDataStreamTerminated, ct);

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarginUserDataUpdatesAsync(
            string? listenToken,
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onUserDataStreamTerminated = null,
            CancellationToken ct = default)
        {
            if (listenToken == null && !_client.Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(_client.Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenToken == null)
            {
                var leaseResult = await _client.MarginTokenManager.AcquireAsync(new TokenScope(
                    BinanceExchange.Metadata.Id,
                    _client.EnvironmentName,
                    "Margin",
                    _client.ApiCredentials!.Credential!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(_client.Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var subscription = new BinanceMarginUserDataSubscription(_logger, _client, listenToken, onOrderUpdateMessage, onOcoOrderUpdateMessage, onAccountPositionMessage, onAccountBalanceUpdate, onUserDataStreamTerminated)
            {
                TokenLease = lease
            };

            return await _client.SubscribeInternal2Async(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), subscription, ct).ConfigureAwait(false);
        }

        public async Task<CallResult> UpdateMarginUserDataTokenAsync(string newListenToken, CancellationToken ct = default)
        {
            var marginSubscriptions = _client.GetMarginUserDataSubscriptions();
            var tasks = new List<Task<WebSocketResult>>();
            foreach (var marginSubscription in marginSubscriptions)
                tasks.Add(marginSubscription.RenewTokenAsync(newListenToken));

            await Task.WhenAll(tasks).ConfigureAwait(false);
            var error = tasks.FirstOrDefault(x => x.Result.Error != null);
            if (error != null)
                return CallResult.Fail(error.Result.Error!);

            return CallResult.Ok();
        }

        #endregion

        #endregion
    }
}
