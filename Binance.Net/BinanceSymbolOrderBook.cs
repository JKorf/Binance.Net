using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Objects;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;

namespace Binance.Net
{
    public class BinanceSymbolOrderBook: SymbolOrderBook
    {
        private readonly BinanceClient restClient;
        private readonly BinanceSocketClient socketClient;
        private readonly int? limit;

        public BinanceSymbolOrderBook(string symbol, int? limit = null, LogVerbosity logVerbosity = LogVerbosity.Info, IEnumerable<TextWriter> logWriters = null) : base("Binance", symbol, true, logVerbosity, logWriters)
        {
            this.limit = limit;
            restClient = new BinanceClient();
            socketClient = new BinanceSocketClient();
        }

        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            if (limit.HasValue && limit != 5 && limit != 10 && limit != 20)
                return new CallResult<UpdateSubscription>(null, new ArgumentError("Limit should be one of the following: 5, 10, 20 or null for full order book"));

            CallResult<UpdateSubscription> subResult;
            if (limit == null)
                subResult = await socketClient.SubscribeToDepthStreamAsync(Symbol, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await socketClient.SubscribeToPartialBookDepthStreamAsync(Symbol, limit.Value, HandleUpdate).ConfigureAwait(false);

            if (!subResult.Success)
                return new CallResult<UpdateSubscription>(null, subResult.Error);

            Status = OrderBookStatus.Syncing;
            var bookResult = await restClient.GetOrderBookAsync(Symbol, limit).ConfigureAwait(false);
            if (!bookResult.Success)
            {
                await socketClient.UnsubscribeAll().ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(null, bookResult.Error);
            }

            SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Asks, bookResult.Data.Bids);
            return new CallResult<UpdateSubscription>(subResult.Data, null);
        }

        private void HandleUpdate(BinanceOrderBook data)
        {
            var updates = new List<ProcessEntry>();
            updates.AddRange(data.Asks.Select(a => new ProcessEntry(OrderBookEntryType.Ask, a)));
            updates.AddRange(data.Bids.Select(b => new ProcessEntry(OrderBookEntryType.Bid, b)));
            UpdateOrderBook(data.FirstUpdateId.Value, data.LastUpdateId, updates);
        }

        protected override async Task<CallResult<bool>> DoResync()
        {
            var bookResult = await restClient.GetOrderBookAsync(Symbol, limit).ConfigureAwait(false);
            if (!bookResult.Success)
                return new CallResult<bool>(false, bookResult.Error);

            SetInitialOrderBook(bookResult.Data.LastUpdateId, bookResult.Data.Asks, bookResult.Data.Bids);
            return new CallResult<bool>(true, null);
        }

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
