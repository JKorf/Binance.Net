﻿using System.Threading.Tasks;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BinanceSymbolOrderBook : SymbolOrderBook
    {
        private readonly BinanceClient _restClient;
        private readonly BinanceSocketClient _socketClient;
        private readonly int? _updateInterval;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="options">The options for the order book</param>
        public BinanceSymbolOrderBook(string symbol, BinanceOrderBookOptions? options = null) : base(symbol, options ?? new BinanceOrderBookOptions())
        {
            symbol.ValidateBinanceSymbol();
            Levels = options?.Limit;
            _updateInterval = options?.UpdateInterval;
            _restClient = new BinanceClient();
            _socketClient = new BinanceSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
                subResult = await _socketClient.Spot.SubscribeToOrderBookUpdatesAsync(Symbol, _updateInterval, data => HandleUpdate((BinanceOrderBook)data)).ConfigureAwait(false);
            else
                subResult = await _socketClient.Spot.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, _updateInterval, data => HandleUpdate((BinanceOrderBook)data)).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(null, subResult.Error);

            Status = OrderBookStatus.Syncing;
            if (Levels == null)
            {
                var bookResult = await _restClient.Spot.Market.GetOrderBookAsync(Symbol, Levels ?? 5000).ConfigureAwait(false);
                if (!bookResult)
                {
                    await _socketClient.UnsubscribeAll().ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, bookResult.Error);
                }

                SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                var setResult = await WaitForSetOrderBook(10000).ConfigureAwait(false);
                return setResult ? subResult : new CallResult<UpdateSubscription>(null, setResult.Error);
            }

            return new CallResult<UpdateSubscription>(subResult.Data, null);
        }

        private void HandleUpdate(BinanceOrderBook data)
        {
            if (Levels == null)
            {
                UpdateOrderBook(data.FirstUpdateId ?? 0, data.LastUpdateId, data.Bids, data.Asks);
            }
            else
            {
                SetInitialOrderBook(data.LastUpdateId, data.Bids, data.Asks);
            }
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResync()
        {
            if (Levels != null)
                return await WaitForSetOrderBook(10000).ConfigureAwait(false);

            var bookResult = await _restClient.Spot.Market.GetOrderBookAsync(Symbol, Levels ?? 5000).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(false, bookResult.Error);

            SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true, null);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            _restClient?.Dispose();
            _socketClient?.Dispose();
        }
    }
}
