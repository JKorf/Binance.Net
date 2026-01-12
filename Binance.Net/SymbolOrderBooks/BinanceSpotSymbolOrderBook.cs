using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BinanceSpotSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IBinanceRestClient _restClient;
        private readonly IBinanceSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;
        private readonly int? _updateInterval;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BinanceSpotSymbolOrderBook(string symbol, Action<BinanceOrderBookOptions>? optionsDelegate = null)
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
        public BinanceSpotSymbolOrderBook(
            string symbol,
            Action<BinanceOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IBinanceRestClient? restClient,
            IBinanceSocketClient? socketClient) : base(logger, "Binance", "Spot", symbol)
        {
            var options = BinanceOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;
            _skipSequenceCheckFirstUpdateAfterSnapshotSet = true;
            _updateInterval = options?.UpdateInterval;

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
            if (Levels == null)
                subResult = await _socketClient.SpotApi.ExchangeData.SubscribeToOrderBookUpdatesAsync(Symbol, _updateInterval, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.SpotApi.ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, _updateInterval, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            if (Levels == null)
            {
                // Small delay to make sure the snapshot is from after our first stream update
                await Task.Delay(200).ConfigureAwait(false);
                var bookResult = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(Symbol, Levels ?? 5000).ConfigureAwait(false);
                if (!bookResult)
                {
                    _logger.Log(LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book");
                    await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(bookResult.Error!);
                }

                SetSnapshot(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
            }

            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        private void HandleUpdate(DataEvent<IBinanceEventOrderBook> data)
        {
            if (data.Data.FirstUpdateId != null)
                UpdateOrderBook(data.Data.FirstUpdateId.Value, data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
            else
                UpdateOrderBook(data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }


        private void HandleUpdate(DataEvent<IBinanceOrderBook> data)
        {
            if (Levels == null)
                UpdateOrderBook(data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
            else
                SetSnapshot(data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);

            // Small delay to make sure the snapshot is from after our first stream update
            await Task.Delay(200).ConfigureAwait(false);
            var bookResult = await _restClient.SpotApi.ExchangeData.GetOrderBookAsync(Symbol, Levels ?? 5000).ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetSnapshot(bookResult.Data.LastUpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
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
