using System;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BinanceFuturesUsdtSymbolOrderBook : SymbolOrderBook
    {
        private readonly IBinanceClient _restClient;
        private readonly IBinanceSocketClient _socketClient;
        private readonly int? _limit;
        private readonly int? _updateInterval;
        private readonly bool _restOwner;
        private readonly bool _socketOwner;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="options">The options for the order book</param>
        public BinanceFuturesUsdtSymbolOrderBook(string symbol, BinanceOrderBookOptions? options = null) : base(symbol, options ?? new BinanceOrderBookOptions())
        {
            _limit = options?.Limit;
            _updateInterval = options?.UpdateInterval;
            _restClient = options?.RestClient ?? new BinanceClient();
            _socketClient = options?.SocketClient ?? new BinanceSocketClient();
            _restOwner = options?.RestClient == null;
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            CallResult<UpdateSubscription> subResult;
            if (_limit == null)
                subResult = await _socketClient.FuturesUsdt.SubscribeToOrderBookUpdatesAsync(Symbol, _updateInterval, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.FuturesUsdt.SubscribeToPartialOrderBookUpdatesAsync(Symbol, _limit.Value, _updateInterval, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(null, subResult.Error);

            Status = OrderBookStatus.Syncing;
            if (_limit == null)
            {
                var bookResult = await _restClient.FuturesUsdt.Market.GetOrderBookAsync(Symbol, _limit ?? 1000).ConfigureAwait(false);
                if (!bookResult)
                {
                    await _socketClient.UnsubscribeAllAsync().ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, bookResult.Error);
                }

                SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                var setResult = await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
                return setResult ? subResult : new CallResult<UpdateSubscription>(null, setResult.Error);
            }

            return new CallResult<UpdateSubscription>(subResult.Data, null);
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
        protected override async Task<CallResult<bool>> DoResyncAsync()
        {
            if (_limit != null)
                return await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);

            var bookResult = await _restClient.FuturesUsdt.Market.GetOrderBookAsync(Symbol, _limit ?? 1000).ConfigureAwait(false);
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

            if (_restOwner)
                _restClient?.Dispose();
            if (_socketOwner)
                _socketClient?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
