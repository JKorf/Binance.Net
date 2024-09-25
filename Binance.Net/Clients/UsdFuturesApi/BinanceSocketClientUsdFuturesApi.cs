using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Socket;
using Binance.Net.Objects.Options;
using Binance.Net.Objects.Sockets;
using Binance.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <summary>
    /// Client providing access to the Binance Usd futures websocket Api
    /// </summary>
    internal partial class BinanceSocketClientUsdFuturesApi : SocketApiClient, IBinanceSocketClientUsdFuturesApi
    {
        #region fields
        private const string _klineStreamEndpoint = "@kline";
        private const string _continuousContractKlineStreamEndpoint = "@continuousKline";
        private const string _markPriceStreamEndpoint = "@markPrice";
        private const string _allMarkPriceStreamEndpoint = "!markPrice@arr";
        private const string _symbolMiniTickerStreamEndpoint = "@miniTicker";    
        private const string _allMiniTickerStreamEndpoint = "!miniTicker@arr";
        private const string _symbolTickerStreamEndpoint = "@ticker";
        private const string _allTickerStreamEndpoint = "!ticker@arr";
        private const string _compositeIndexEndpoint = "@compositeIndex";

        private const string _aggregatedTradesStreamEndpoint = "@aggTrade";
        private const string _tradesStreamEndpoint = "@trade";
        private const string _bookTickerStreamEndpoint = "@bookTicker";
        private const string _allBookTickerStreamEndpoint = "!bookTicker";
        private const string _liquidationStreamEndpoint = "@forceOrder";
        private const string _allLiquidationStreamEndpoint = "!forceOrder@arr";
        private const string _partialBookDepthStreamEndpoint = "@depth";
        private const string _depthStreamEndpoint = "@depth";

        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _streamPath = MessagePath.Get().Property("stream");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientUsdFuturesStreams
        /// </summary>
        internal BinanceSocketClientUsdFuturesApi(ILogger logger, BinanceSocketOptions options) :
            base(logger, options.Environment.UsdFuturesSocketAddress!, options, options.UsdFuturesOptions)
        {
            // When sending more than 4000 bytes the server responds very delayed (somehow connected to the websocket keep alive interval on framework level)
            // See https://dev.binance.vision/t/socket-live-subscribing-server-delay/9645/2
            // To prevent issues we keep below this
            MessageSendSizeLimit = 4000;

            RateLimiter = BinanceExchange.RateLimiter.FuturesSocket;
        }
        #endregion 

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            return baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant() + (deliverTime == null ? string.Empty: "_" + deliverTime.Value.ToString("yyMMdd"));
        }

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();
        public IBinanceSocketClientUsdFuturesApiShared SharedClient => this;

        #region Mark Price Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage, CancellationToken ct = default) => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesUsdtStreamMarkPrice>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _markPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Stream for All market

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesUsdtStreamMarkPrice>>> onMessage, CancellationToken ct = default)
        {
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceFuturesUsdtStreamMarkPrice>>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream)));
            return await SubscribeAsync(BaseAddress, new[] { _allMarkPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "") }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As<IBinanceStreamKlineData>(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.SelectMany(a => intervals.Select(i => a.ToLower(CultureInfo.InvariantCulture) + _klineStreamEndpoint + "_" + EnumConverter.GetString(i))).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Continuous contract kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamContinuousKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToContinuousContractKlineUpdatesAsync(new[] { pair }, contractType, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamContinuousKlineData>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamContinuousKlineData>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      "_" +
                                      EnumConverter.GetString(contractType).ToLower() +
                                      _continuousContractKlineStreamEndpoint +
                                      "_" +
                                      EnumConverter.GetString(interval)).ToArray();
            return await SubscribeAsync(BaseAddress, pairs, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default) => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamMiniTick>>>(data => onMessage(data.As<IBinanceMiniTick>(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _symbolMiniTickerStreamEndpoint).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream
        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamMiniTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceMiniTick>>(data.Data.Data).WithStreamId(data.Data.Stream)));
            return await SubscribeAsync(BaseAddress, new[] { _allMiniTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTick>>>(data => onMessage(data.As<IBinance24HPrice>(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _symbolTickerStreamEndpoint).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Composite index Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCompositeIndexUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamCompositeIndex>> onMessage, CancellationToken ct = default)
        {
            var action = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamCompositeIndex>>>(data =>
            {
                onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol));
        });
            return await SubscribeAsync(BaseAddress, new[] { symbol.ToLower(CultureInfo.InvariantCulture) + _compositeIndexEndpoint }, action, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinance24HPrice>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamTick>>>>(data => onMessage(data.As<IEnumerable<IBinance24HPrice>>(data.Data.Data).WithStreamId(data.Data.Stream)));
            return await SubscribeAsync(BaseAddress, new[] { _allTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default) => await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamAggregatedTrade>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _aggregatedTradesStreamEndpoint).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamTrade>> onMessage, bool filterOutNonTradeUpdates = true, CancellationToken ct = default) =>
            await SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, filterOutNonTradeUpdates, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamTrade>> onMessage, bool filterOutNonTradeUpdates = true, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTrade>>>(data =>
            {
                if (filterOutNonTradeUpdates && data.Data.Data.Type != "MARKET")
                    return;

                onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol));
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _tradesStreamEndpoint).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default) => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _bookTickerStreamEndpoint).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Book Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            return await SubscribeAsync(BaseAddress, new[] { _allBookTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default) => await SubscribeToLiquidationUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data => onMessage(data.As(data.Data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _liquidationStreamEndpoint).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data => onMessage(data.As(data.Data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Data.Symbol)));
            return await SubscribeAsync(BaseAddress, new[] { _allLiquidationStreamEndpoint }, handler, ct).ConfigureAwait(false);
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
                onMessage(data.As<IBinanceFuturesEventOrderBook>(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol));
        });

            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _partialBookDepthStreamEndpoint + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>>>(data => onMessage(data.As<IBinanceFuturesEventOrderBook>(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + _depthStreamEndpoint + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Contract Info Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<BinanceFuturesStreamSymbolUpdate>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamSymbolUpdate>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            return await SubscribeAsync(BaseAddress, new[] { "!contractInfo" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Asset Index Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAssetIndexUpdatesAsync(Action<DataEvent<IEnumerable<BinanceFuturesStreamAssetIndexUpdate>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceFuturesStreamAssetIndexUpdate>>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream)));
            return await SubscribeAsync(BaseAddress, new[] { "!assetIndex@arr" }, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAssetIndexUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamAssetIndexUpdate>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamAssetIndexUpdate>>>(data => onMessage(data.As(data.Data.Data).WithStreamId(data.Data.Stream).WithSymbol(data.Data.Data.Symbol)));
            return await SubscribeAsync(BaseAddress, new[] { symbol.ToLowerInvariant()  + "@assetIndex" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

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
            return await SubscribeAsync(BaseAddress.AppendPath("stream"), subscription, ct).ConfigureAwait(false);
        }

        #endregion

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var subscription = new BinanceSubscription<T>(_logger, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<int?>(_idPath);
            if (id != null)
                return id.ToString();

            return message.GetValue<string>(_streamPath);
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection) => null;
    }
}
