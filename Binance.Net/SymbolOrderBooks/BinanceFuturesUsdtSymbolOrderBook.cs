using System.Threading.Tasks;
using Binance.Net.Clients.Rest.UsdFutures;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.Rest.UsdFutures;
using Binance.Net.Interfaces.Clients.Socket;
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
        private readonly IBinanceClientUsdFutures _restClient;
        private readonly IBinanceSocketClientUsdFutures _socketClient;
        private readonly int? _limit;
        private readonly int? _updateInterval;
        private readonly bool _restOwner;
        private readonly bool _socketOwner;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="options">The options for the order book</param>
        public BinanceFuturesUsdtSymbolOrderBook(string symbol, BinanceUsdFuturesOrderBookOptions? options = null) : base("Binance[UsdFutures]", symbol, options ?? new BinanceUsdFuturesOrderBookOptions())
        {
            _limit = options?.Limit;
            _updateInterval = options?.UpdateInterval;
            _restClient = options?.RestClient ?? new BinanceClientUsdFutures();
            _socketClient = options?.SocketClient ?? new BinanceSocketClientUsdFutures();
            _restOwner = options?.RestClient == null;
            _socketOwner = options?.SocketClient == null;

            sequencesAreConsecutive = options?.Limit == null;
            strictLevels = false;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            CallResult<UpdateSubscription> subResult;
            if (_limit == null)
                subResult = await _socketClient.SubscribeToOrderBookUpdatesAsync(Symbol, _updateInterval, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.SubscribeToPartialOrderBookUpdatesAsync(Symbol, _limit.Value, _updateInterval, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(null, subResult.Error);

            Status = OrderBookStatus.Syncing;
            if (_limit == null)
            {
                var bookResult = await _restClient.MarketData.GetOrderBookAsync(Symbol, _limit ?? 1000).ConfigureAwait(false);
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

            var bookResult = await _restClient.MarketData.GetOrderBookAsync(Symbol, _limit ?? 1000).ConfigureAwait(false);
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
        }
    }
}
