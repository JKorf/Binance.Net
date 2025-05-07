using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Objects.Sockets;

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
        public async Task<CallResult<BinanceResponse<BinanceUsdFuturesAccountBalance[]>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceUsdFuturesAccountBalance[]>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"v2/account.balance", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceFuturesAccountInfoV3>>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));
            return await _client.QueryAsync<BinanceFuturesAccountInfoV3>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"v2/account.status", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region User Data Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onConfigUpdate = null,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate = null,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<BinanceFuturesStreamTradeUpdate>>? onTradeUpdate = null,
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
            Action<DataEvent<BinanceStrategyUpdate>>? onStrategyUpdate = null,
            Action<DataEvent<BinanceGridUpdate>>? onGridUpdate = null,
            Action<DataEvent<BinanceConditionOrderTriggerRejectUpdate>>? onConditionalOrderTriggerRejectUpdate = null,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new BinanceUsdFuturesUserDataSubscription(_logger, new List<string> { listenKey }, onOrderUpdate, onTradeUpdate, onConfigUpdate, onMarginUpdate, onAccountUpdate, onListenKeyExpired, onStrategyUpdate, onGridUpdate, onConditionalOrderTriggerRejectUpdate);
            return await _client.SubscribeInternalAsync(_client.BaseAddress, subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
