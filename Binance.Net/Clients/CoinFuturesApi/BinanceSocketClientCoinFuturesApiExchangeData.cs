using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal class BinanceSocketClientCoinFuturesApiExchangeData : IBinanceSocketClientCoinFuturesApiExchangeData
    {
        private readonly BinanceSocketClientCoinFuturesApi _client;
        private readonly ILogger _logger;

        private const string _klineStreamEndpoint = "@kline";
        private const string _markPriceStreamEndpoint = "@markPrice";
        private const string _allMarkPriceStreamEndpoint = "!markPrice@arr";
        private const string _indexPriceStreamEndpoint = "@indexPrice";
        private const string _continuousKlineStreamEndpoint = "@continuousKline";
        private const string _indexKlineStreamEndpoint = "@indexPriceKline";
        private const string _markKlineStreamEndpoint = "@markPriceKline";
        private const string _symbolMiniTickerStreamEndpoint = "@miniTicker";
        private const string _allMiniTickerStreamEndpoint = "!miniTicker@arr";
        private const string _symbolTickerStreamEndpoint = "@ticker";
        private const string _allTickerStreamEndpoint = "!ticker@arr";

        private const string _aggregatedTradesStreamEndpoint = "@aggTrade";
        private const string _tradesStreamEndpoint = "@trade";
        private const string _bookTickerStreamEndpoint = "@bookTicker";
        private const string _allBookTickerStreamEndpoint = "!bookTicker";
        private const string _liquidationStreamEndpoint = "@forceOrder";
        private const string _allLiquidationStreamEndpoint = "!forceOrder@arr";
        private const string _partialBookDepthStreamEndpoint = "@depth";
        private const string _depthStreamEndpoint = "@depth";

        internal BinanceSocketClientCoinFuturesApiExchangeData(ILogger logger, BinanceSocketClientCoinFuturesApi client)
        {
            _client = client;
            _logger = logger;
        }

        #region Queries

        #endregion

        #region Streams
        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamCoinKlineData>>>(data =>
            {
                var result = data.Data.Data;
                onMessage(data.As<IBinanceStreamKlineData>(result)
                    .WithStreamId(data.Data.Stream)
                    .WithSymbol(data.Data.Data.Symbol)
                    .WithDataTimestamp(data.Data.Data.EventTime));
            });
            symbols = symbols.SelectMany(a => intervals.Select(i => a.ToLower(CultureInfo.InvariantCulture) + _klineStreamEndpoint + "_" + EnumConverter<KlineInterval>.GetString(i))).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Index Price Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<DataEvent<BinanceFuturesStreamIndexPrice>> onMessage, CancellationToken ct = default) => await SubscribeToIndexPriceUpdatesAsync(new[] { pair }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<DataEvent<BinanceFuturesStreamIndexPrice>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var internalHandler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamIndexPrice>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Pair)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _indexPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, pairs, internalHandler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Stream
        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<BinanceFuturesCoinStreamMarkPrice>> onMessage, CancellationToken ct = default) => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<BinanceFuturesCoinStreamMarkPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var internalHandler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesCoinStreamMarkPrice>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _markPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, internalHandler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Continuous contract kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToContinuousContractKlineUpdatesAsync(new[] { pair }, contractType, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      "_" +
                                      EnumConverter<ContractType>.GetString(contractType).ToLower() +
                                      _continuousKlineStreamEndpoint +
                                      "_" +
                                      EnumConverter<KlineInterval>.GetString(interval)).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, pairs, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Index kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToIndexKlineUpdatesAsync(new[] { pair }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamIndexKlineData>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      _indexKlineStreamEndpoint +
                                      "_" +
                                      EnumConverter<KlineInterval>.GetString(interval)).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, pairs, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark price kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToMarkPriceKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamIndexKlineData>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                          _markKlineStreamEndpoint +
                                         "_" +
                                         EnumConverter<KlineInterval>.GetString(interval)).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default) => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamCoinMiniTick>>>(data =>
                onMessage(data.As<IBinanceMiniTick>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _symbolMiniTickerStreamEndpoint).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream
        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IBinanceMiniTick[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamCoinMiniTick[]>>>(data =>
                onMessage(data.As<IBinanceMiniTick[]>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { _allMiniTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamCoinTick>>>(data =>
                onMessage(data.As<IBinance24HPrice>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _symbolTickerStreamEndpoint).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IBinance24HPrice[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamCoinTick[]>>>(data =>
                onMessage(data.As<IBinance24HPrice[]>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { _allTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default) => await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamAggregatedTrade>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _aggregatedTradesStreamEndpoint).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default) =>
            await SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTrade>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _tradesStreamEndpoint).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Stream for All market

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(Action<DataEvent<BinanceFuturesCoinStreamMarkPrice[]>> onMessage, int? updateInterval = null, CancellationToken ct = default)
        {
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesCoinStreamMarkPrice[]>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { _allMarkPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "") }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default) => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamBookPrice>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _bookTickerStreamEndpoint).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Book Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamBookPrice>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { _allBookTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default) => await SubscribeToLiquidationUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data =>
                onMessage(data.As(data.Data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _liquidationStreamEndpoint).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data =>
                onMessage(data.As(data.Data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { _allLiquidationStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default) => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 250, 500);

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>>>(data =>
            {
                data.Data.Data.Symbol = data.Data.Stream.Split('@')[0];
                onMessage(data.As<IBinanceFuturesEventOrderBook>(data.Data.Data)
                    .WithStreamId(data.Data.Stream)
                    .WithSymbol(data.Data.Data.Symbol)
                    .WithDataTimestamp(data.Data.Data.EventTime));
            });

            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _partialBookDepthStreamEndpoint + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Diff. Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default) => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 250, 500);
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>>>(data =>
                onMessage(data.As<IBinanceFuturesEventOrderBook>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _depthStreamEndpoint + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Contract Info Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<BinanceFuturesStreamSymbolUpdate>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamSymbolUpdate>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { "!contractInfo" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}
