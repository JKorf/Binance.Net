using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.Socket;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Clients.Socket
{
    /// <summary>
    /// Client providing access to the Binance Coin futures websocket Api
    /// </summary>
    public class BinanceSocketClientCoinFutures : SocketClient, IBinanceSocketClientCoinFutures
    {
        #region fields
        private const string klineStreamEndpoint = "@kline";
        private const string markPriceStreamEndpoint = "@markPrice";
        private const string indexPriceStreamEndpoint = "@indexPrice";
        private const string continuousKlineStreamEndpoint = "@continuousKline";
        private const string indexKlineStreamEndpoint = "@indexPriceKline";
        private const string markKlineStreamEndpoint = "@markPriceKline";
        private const string symbolMiniTickerStreamEndpoint = "@miniTicker";
        private const string allMiniTickerStreamEndpoint = "!miniTicker@arr";
        private const string symbolTickerStreamEndpoint = "@ticker";
        private const string allTickerStreamEndpoint = "!ticker@arr";

        private const string aggregatedTradesStreamEndpoint = "@aggTrade";
        private const string bookTickerStreamEndpoint = "@bookTicker";
        private const string allBookTickerStreamEndpoint = "!bookTicker";
        private const string liquidationStreamEndpoint = "@forceOrder";
        private const string allLiquidationStreamEndpoint = "!forceOrder@arr";
        private const string partialBookDepthStreamEndpoint = "@depth";
        private const string depthStreamEndpoint = "@depth";

        private const string configUpdateEvent = "ACCOUNT_CONFIG_UPDATE";
        private const string marginUpdateEvent = "MARGIN_CALL";
        private const string accountUpdateEvent = "ACCOUNT_UPDATE";
        private const string orderUpdateEvent = "ORDER_TRADE_UPDATE";
        private const string listenKeyExpiredEvent = "listenKeyExpired";
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientCoinFutures with default options
        /// </summary>
        public BinanceSocketClientCoinFutures() : this(BinanceSocketClientCoinFuturesOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClientCoinFutures using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceSocketClientCoinFutures(BinanceSocketClientCoinFuturesOptions options) : base("Binance", options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            SetDataInterpreter((byte[] data) => string.Empty, null);
            RateLimitPerSocketPerSecond = 4;
        }
        #endregion 

        #region methods

        /// <summary>
        /// Set the default options to be used when creating new BinanceSocketClientCoinFutures instances
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(BinanceSocketClientCoinFuturesOptions options)
        {
            BinanceSocketClientCoinFuturesOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new BinanceAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }

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
                onMessage(data.As<IBinanceStreamKlineData>(result, result.Symbol));
            });
            symbols = symbols.SelectMany(a => intervals.Select(i => a.ToLower(CultureInfo.InvariantCulture) + klineStreamEndpoint + "_" + JsonConvert.SerializeObject(i, new KlineIntervalConverter(false)))).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Index Price Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamIndexPrice>>> onMessage, CancellationToken ct = default) => await SubscribeToIndexPriceUpdatesAsync(new[] { pair }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamIndexPrice>>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var internalHandler = new Action<DataEvent<JToken>>(data => HandlePossibleSingleData(data, onMessage));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) + indexPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await Subscribe(pairs, internalHandler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Stream
        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>> onMessage, CancellationToken ct = default) => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var internalHandler = new Action<DataEvent<JToken>>(data => HandlePossibleSingleData(data, onMessage));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + markPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await Subscribe(symbols, internalHandler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Continuous contract kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToContinuousContractKlineUpdatesAsync(new[] { pair }, contractType, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      "_" +
                                      JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)).ToLower() +
                                      continuousKlineStreamEndpoint +
                                      "_" +
                                      JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(pairs, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Index kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToIndexKlineUpdatesAsync(new[] { pair }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamIndexKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      indexKlineStreamEndpoint +
                                      "_" +
                                      JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(pairs, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark price kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default) => await SubscribeToMarkPriceKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamIndexKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                          markKlineStreamEndpoint +
                                         "_" +
                                         JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default) => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamCoinMiniTick>>>(data => onMessage(data.As<IBinanceMiniTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolMiniTickerStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream
        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamCoinMiniTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceMiniTick>>(data.Data.Data, data.Data.Stream)));
            return await Subscribe(new[] { allMiniTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamCoinTick>>>(data => onMessage(data.As<IBinanceTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolTickerStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamCoinTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceTick>>(data.Data.Data, data.Data.Stream)));
            return await Subscribe(new[] { allTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default) => await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamAggregatedTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + aggregatedTradesStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default) => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + bookTickerStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Book Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await Subscribe(new[] { allBookTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default) => await SubscribeToLiquidationUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data => onMessage(data.As(data.Data.Data.Data, data.Data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + liquidationStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data => onMessage(data.As(data.Data.Data.Data, data.Data.Data.Data.Symbol)));
            return await Subscribe(new[] { allLiquidationStreamEndpoint }, handler, ct).ConfigureAwait(false);
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
                onMessage(data.As<IBinanceFuturesEventOrderBook>(data.Data.Data, data.Data.Data.Symbol));
            });

            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + partialBookDepthStreamEndpoint + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
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
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>>>(data => onMessage(data.As<IBinanceFuturesEventOrderBook>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + depthStreamEndpoint + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await Subscribe(symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region User Data Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onConfigUpdate,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate,
            Action<DataEvent<BinanceStreamEvent>>? onListenKeyExpired,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var handler = new Action<DataEvent<string>>(data =>
            {
                var combinedToken = JToken.Parse(data.Data);
                var token = combinedToken["data"];
                if (token == null)
                    return;

                var evnt = token["e"]?.ToString();
                if (evnt == null)
                    return;

                switch (evnt)
                {
                    case configUpdateEvent:
                        {
                            var result = Deserialize<BinanceFuturesStreamConfigUpdate>(token);
                            if (result)
                                onConfigUpdate?.Invoke(data.As(result.Data, result.Data.LeverageUpdateData?.Symbol));
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from config stream: " + result.Error);

                            break;
                        }
                    case marginUpdateEvent:
                        {
                            var result = Deserialize<BinanceFuturesStreamMarginUpdate>(token);
                            if (result)
                                onMarginUpdate?.Invoke(data.As(result.Data));
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case accountUpdateEvent:
                        {
                            var result = Deserialize<BinanceFuturesStreamAccountUpdate>(token);
                            if (result.Success)
                                onAccountUpdate?.Invoke(data.As(result.Data));
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from account stream: " + result.Error);

                            break;
                        }
                    case orderUpdateEvent:
                        {
                            var result = Deserialize<BinanceFuturesStreamOrderUpdate>(token);
                            if (result)
                                onOrderUpdate?.Invoke(data.As(result.Data, result.Data.UpdateData.Symbol));
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case listenKeyExpiredEvent:
                        {
                            var result = Deserialize<BinanceStreamEvent>(token);
                            if (result)
                                onListenKeyExpired?.Invoke(data.As(result.Data));
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from the expired listen key event: " + result.Error);
                            break;
                        }
                    default:
                        log.Write(LogLevel.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await Subscribe(new[] { listenKey }, handler, ct).ConfigureAwait(false);
        }

        #endregion


        private void HandlePossibleSingleData<T>(DataEvent<JToken> data, Action<DataEvent<IEnumerable<T>>> onMessage)
        {
            var internalData = data.Data["data"];
            if (internalData == null)
                return;

            if (internalData.Type == JTokenType.Array)
            {
                var firstItemTopic = internalData.First()["i"]?.ToString() ?? internalData.First()["s"]?.ToString();
                var deserialized = Deserialize<BinanceCombinedStream<IEnumerable<T>>>(data.Data);
                if (!deserialized)
                    return;

                onMessage(data.As(deserialized.Data.Data, firstItemTopic));
            }
            else
            {
                var symbol = internalData["i"]?.ToString() ?? internalData["s"]?.ToString();
                var deserialized = Deserialize<BinanceCombinedStream<T>>(
                        data.Data);
                if (!deserialized)
                    return;

                onMessage(data.As<IEnumerable<T>>(new[] { deserialized.Data.Data }, symbol));
            }
        }

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            return await SubscribeInternal(ClientOptions.BaseAddress.AppendPath("stream"), topics, onData, ct).ConfigureAwait(false);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternal<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var request = new BinanceSocketRequest
            {
                Method = "SUBSCRIBE",
                Params = topics.ToArray(),
                Id = NextId()
            };

            return SubscribeAsync(url, request, null, false, onData, ct);
        }


        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            if (message.Type != JTokenType.Object)
                return false;

            var id = message["id"];
            if (id == null)
                return false;

            var bRequest = (BinanceSocketRequest)request;
            if ((int)id != bRequest.Id)
                return false;

            var result = message["result"];
            if (result != null && result.Type == JTokenType.Null)
            {
                log.Write(LogLevel.Trace, $"Socket {s.Socket.Id} Subscription completed");
                callResult = new CallResult<object>(null, null);
                return true;
            }

            var error = message["error"];
            if (error == null)
            {
                callResult = new CallResult<object>(null, new ServerError("Unknown error: " + message));
                return true;
            }

            callResult = new CallResult<object>(null, new ServerError(error["code"]!.Value<int>(), error["msg"]!.ToString()));
            return true;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            if (message.Type != JTokenType.Object)
                return false;

            var bRequest = (BinanceSocketRequest)request;
            var stream = message["stream"];
            if (stream == null)
                return false;

            return bRequest.Params.Contains(stream.ToString());
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscription)
        {
            var topics = ((BinanceSocketRequest)subscription.Request!).Params;
            var unsub = new BinanceSocketRequest { Method = "UNSUBSCRIBE", Params = topics, Id = NextId() };
            var result = false;

            if (!connection.Socket.IsOpen)
                return true;

            await connection.SendAndWaitAsync(unsub, ClientOptions.SocketResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var id = data["id"];
                if (id == null)
                    return false;

                if ((int)id != unsub.Id)
                    return false;

                var result = data["result"];
                if (result?.Type == JTokenType.Null)
                {
                    result = true;
                    return true;
                }

                return true;              
            }).ConfigureAwait(false);
            return result;
        }
        #endregion
    }
}
