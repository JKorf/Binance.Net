using Binance.Net.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

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
