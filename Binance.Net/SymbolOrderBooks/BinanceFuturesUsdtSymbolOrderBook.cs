﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BinanceFuturesUsdtSymbolOrderBook : SymbolOrderBook
    {
        private readonly IBinanceRestClient _restClient;
        private readonly IBinanceSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;
        private readonly int? _limit;
        private readonly int? _updateInterval;
        private readonly bool _clientOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BinanceFuturesUsdtSymbolOrderBook(string symbol, Action<BinanceOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        [ActivatorUtilitiesConstructor]
        public BinanceFuturesUsdtSymbolOrderBook(
            string symbol,
            Action<BinanceOrderBookOptions>? optionsDelegate,
            ILogger<BinanceFuturesUsdtSymbolOrderBook>? logger,
            IBinanceRestClient? restClient,
            IBinanceSocketClient? socketClient) : base(logger, "Binance", symbol)
        {
            var options = BinanceOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;
            _updateInterval = options?.UpdateInterval;
            _limit = options?.Limit;

            Levels = options?.Limit;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new BinanceSocketClient();
            _restClient = restClient ?? new BinanceRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            CallResult<UpdateSubscription> subResult;
            if (_limit == null)
                subResult = await _socketClient.UsdFuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, _updateInterval, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.UsdFuturesApi.SubscribeToPartialOrderBookUpdatesAsync(Symbol, _limit.Value, _updateInterval, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            if (_limit == null)
            {
                var bookResult = await _restClient.UsdFuturesApi.ExchangeData.GetOrderBookAsync(Symbol, _limit ?? 1000).ConfigureAwait(false);
                if (!bookResult)
                {
                    await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(bookResult.Error!);
                }

                SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
            }

            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<IBinanceFuturesEventOrderBook> data)
        {
            if (_limit == null)
            {
                UpdateOrderBook(data.Data.FirstUpdateId ?? 0, data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks);
            }
            else
            {
                SetInitialOrderBook(data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks);
            }
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (_limit != null)
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);

            var bookResult = await _restClient.UsdFuturesApi.ExchangeData.GetOrderBookAsync(Symbol, _limit ?? 1000).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
