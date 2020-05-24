using System.Threading.Tasks;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;

namespace Binance.Net
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BinanceFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly BinanceFuturesClient restClient;
        private readonly BinanceFuturesSocketClient socketClient;
        private readonly int? limit;
        private readonly int? updateInterval;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="symbol">The symbol of the order book</param>
        /// <param name="options">The options for the order book</param>
        public BinanceFuturesSymbolOrderBook(string symbol, BinanceFuturesOrderBookOptions? options = null) : base(symbol, options ?? new BinanceFuturesOrderBookOptions())
        {
            symbol.ValidateBinanceSymbol();
            limit = options?.Limit;
            updateInterval = options?.UpdateInterval;
            restClient = new BinanceFuturesClient();
            socketClient = new BinanceFuturesSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            CallResult<UpdateSubscription> subResult;
            if (limit == null)
                subResult = await socketClient.SubscribeToOrderBookUpdatesAsync(Symbol, updateInterval, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await socketClient.SubscribeToPartialOrderBookUpdatesAsync(Symbol, limit.Value, updateInterval, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(null, subResult.Error);

            Status = OrderBookStatus.Syncing;
            if (limit == null)
            {
                var bookResult = await restClient.GetOrderBookAsync(Symbol, limit ?? 1000).ConfigureAwait(false);
                if (!bookResult)
                {
                    await socketClient.UnsubscribeAll().ConfigureAwait(false);
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

        private void HandleUpdate(BinanceFuturesOrderBook data)
        {
            if (limit == null)
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
            if (limit != null)
                return await WaitForSetOrderBook(10000).ConfigureAwait(false);

            var bookResult = await restClient.GetOrderBookAsync(Symbol, limit ?? 5000).ConfigureAwait(false);
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

            restClient?.Dispose();
            socketClient?.Dispose();
        }
    }
}
