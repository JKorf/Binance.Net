using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal class BinanceSocketClientCoinFuturesApiAccount : IBinanceSocketClientCoinFuturesApiAccount
    {
        private readonly BinanceSocketClientCoinFuturesApi _client;
        private readonly ILogger _logger;

        internal BinanceSocketClientCoinFuturesApiAccount(ILogger logger, BinanceSocketClientCoinFuturesApi client)
        {
            _client = client;
            _logger = logger;
        }

        #region Queries

        #region Future Account Balance

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceCoinFuturesAccountBalance[]>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceCoinFuturesAccountBalance[]>(_client.ClientOptions.Environment.CoinFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"account.balance", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceFuturesCoinAccountInfo>>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));
            return await _client.QueryAsync<BinanceFuturesCoinAccountInfo>(_client.ClientOptions.Environment.CoinFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"account.status", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
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
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired = null,
            Action<DataEvent<BinanceStrategyUpdate>>? onStrategyUpdate = null,
            Action<DataEvent<BinanceGridUpdate>>? onGridUpdate = null,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new BinanceCoinFuturesUserDataSubscription(_logger, listenKey, onOrderUpdate, onConfigUpdate, onMarginUpdate, onAccountUpdate, onListenKeyExpired, onStrategyUpdate, onGridUpdate);
            return await _client.SubscribeInternalAsync(_client.BaseAddress, subscription, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
