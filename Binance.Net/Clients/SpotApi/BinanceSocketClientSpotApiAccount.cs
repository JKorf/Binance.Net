using Binance.Net.Objects.Models.Spot.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Subscriptions;
using Binance.Net.Objects.Models;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceSocketClientSpotApiAccount : IBinanceSocketClientSpotApiAccount
    {
        private readonly BinanceSocketClientSpotApi _client;

        private readonly ILogger _logger;

        #region constructor/destructor

        internal BinanceSocketClientSpotApiAccount(ILogger logger, BinanceSocketClientSpotApi client)
        {
            _client = client;
            _logger = logger;
        }

        #endregion

        #region Queries

        #region Get Account Info

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceAccountInfo>>> GetAccountInfoAsync(bool? omitZeroBalances = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("omitZeroBalances", omitZeroBalances?.ToString().ToLowerInvariant());
            return await _client.QueryAsync<BinanceAccountInfo>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"account.status", parameters, true, true, weight: 20, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order Rate Limits

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceCurrentRateLimit[]>>> GetOrderRateLimitsAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            return await _client.QueryAsync<BinanceCurrentRateLimit[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"account.rateLimits.orders", parameters, true, true, weight: 40, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Start User Stream

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<string>>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var result = await _client.QueryAsync<BinanceListenKey>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"userDataStream.start", new Dictionary<string, object>(), true, weight: 2, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<BinanceResponse<string>>(result.Error!);

            return result.As(new BinanceResponse<string>
            {
                Ratelimits = result.Data!.Ratelimits!,
                Result = result.Data!.Result?.ListenKey!
            });
        }

        #endregion

        #region Keep Alive User Stream

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<object>>> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("listenKey", listenKey);
            return await _client.QueryAsync<object>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"userDataStream.ping", parameters, true, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Stop User Stream

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<object>>> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("listenKey", listenKey);
            return await _client.QueryAsync<object>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"userDataStream.stop", parameters, true, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region User Data Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage = null,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage = null,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
            Action<DataEvent<BinanceStreamEvent>>? onUserDataStreamTerminated = null,
            Action<DataEvent<BinanceStreamBalanceLockUpdate>>? onBalanceLockUpdate = null,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var subscription = new BinanceSpotUserDataSubscription(_logger, new List<string> { listenKey }, onOrderUpdateMessage, onOcoOrderUpdateMessage, onAccountPositionMessage, onAccountBalanceUpdate, onListenKeyExpired, onUserDataStreamTerminated, onBalanceLockUpdate, false);
            return await _client.SubscribeInternalAsync(_client.BaseAddress, subscription, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
