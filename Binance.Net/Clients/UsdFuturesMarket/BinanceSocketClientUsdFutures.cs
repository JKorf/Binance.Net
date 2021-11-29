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
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Clients.Socket
{
    /// <summary>
    /// Client providing access to the Binance Usd futures websocket Api
    /// </summary>
    public class BinanceSocketClientUsdFutures : SocketSubClient, IBinanceSocketClientUsdFuturesMarket
    {
        #region fields
        private readonly BinanceSocketClient _baseClient;
        private readonly BinanceSocketClientOptions _options;
        private readonly Log _log;

        private const string klineStreamEndpoint = "@kline";
        private const string markPriceStreamEndpoint = "@markPrice";
        private const string allMarkPriceStreamEndpoint = "!markPrice@arr";
        private const string symbolMiniTickerStreamEndpoint = "@miniTicker";
        private const string allMiniTickerStreamEndpoint = "!miniTicker@arr";
        private const string symbolTickerStreamEndpoint = "@ticker";
        private const string allTickerStreamEndpoint = "!ticker@arr";
        private const string compositeIndexEndpoint = "@compositeIndex";

        private const string aggregatedTradesStreamEndpoint = "@aggTrade";
        private const string bookTickerStreamEndpoint = "@bookTicker";
        private const string allBookTickerStreamEndpoint = "!bookTicker";
        private const string liquidationStreamEndpoint = "@forceOrder";
        private const string allLiquidationStreamEndpoint = "!forceOrder@arr";
        private const string partialBookDepthStreamEndpoint = "@depth";
        private const string depthStreamEndpoint = "@depth";

        private const string bltvInfoEndpoint = "@tokenNav";
        private const string bltvKlineEndpoint = "@nav_kline";

        private const string configUpdateEvent = "ACCOUNT_CONFIG_UPDATE";
        private const string marginUpdateEvent = "MARGIN_CALL";
        private const string accountUpdateEvent = "ACCOUNT_UPDATE";
        private const string orderUpdateEvent = "ORDER_TRADE_UPDATE";
        private const string listenKeyExpiredEvent = "listenKeyExpired";
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientSpot with default options
        /// </summary>
        public BinanceSocketClientUsdFutures(Log log, BinanceSocketClient baseClient, BinanceSocketClientOptions options) :
            base(options.OptionsUsdFutures, options.OptionsUsdFutures.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.OptionsUsdFutures.ApiCredentials))
        {
            _options = options;
            _baseClient = baseClient;
            _log = log;
        }
        #endregion 

        #region methods

        #region Mark Price Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage, CancellationToken ct = default) => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesUsdtStreamMarkPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + markPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Stream for All market

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamMarkPrice>>> onMessage, CancellationToken ct = default)
        {
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceFuturesStreamMarkPrice>>>>(data => onMessage(data.As<IEnumerable<BinanceFuturesStreamMarkPrice>>(data.Data.Data, data.Data.Stream)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { allMarkPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "") }, handler, ct).ConfigureAwait(false);
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
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As<IBinanceStreamKlineData>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.SelectMany(a => intervals.Select(i => a.ToLower(CultureInfo.InvariantCulture) + klineStreamEndpoint + "_" + JsonConvert.SerializeObject(i, new KlineIntervalConverter(false)))).ToArray();
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default) => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamMiniTick>>>(data => onMessage(data.As<IBinanceMiniTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolMiniTickerStreamEndpoint).ToArray();
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream
        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamMiniTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceMiniTick>>(data.Data.Data, data.Data.Stream)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { allMiniTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTick>>>(data => onMessage(data.As<IBinanceTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolTickerStreamEndpoint).ToArray();
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Composite index Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToCompositeIndexUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamCompositeIndex>> onMessage, CancellationToken ct = default)
        {
            var action = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamCompositeIndex>>>(data => {
                onMessage(data.As(data.Data.Data, data.Data.Data.Symbol));
            });
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { symbol + compositeIndexEndpoint }, action, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceTick>>(data.Data.Data, data.Data.Stream)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { allTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
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
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Book Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { allBookTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
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
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Liquidation Order Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>>(data => onMessage(data.As(data.Data.Data.Data, data.Data.Data.Data.Symbol)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { allLiquidationStreamEndpoint }, handler, ct).ConfigureAwait(false);
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
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
                            var result = _baseClient.DeserializeInternal<BinanceFuturesStreamConfigUpdate>(token);
                            if (result)
                                onConfigUpdate?.Invoke(data.As(result.Data, result.Data.LeverageUpdateData?.Symbol));
                            else
                                _log.Write(LogLevel.Warning, "Couldn't deserialize data received from config stream: " + result.Error);

                            break;
                        }
                    case marginUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceFuturesStreamMarginUpdate>(token);
                            if (result)
                                onMarginUpdate?.Invoke(data.As(result.Data));
                            else
                                _log.Write(LogLevel.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case accountUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceFuturesStreamAccountUpdate>(token);
                            if (result.Success)
                                onAccountUpdate?.Invoke(data.As(result.Data));
                            else
                                _log.Write(LogLevel.Warning, "Couldn't deserialize data received from account stream: " + result.Error);

                            break;
                        }
                    case orderUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceFuturesStreamOrderUpdate>(token);
                            if (result)
                                onOrderUpdate?.Invoke(data.As(result.Data, result.Data.UpdateData.Symbol));
                            else
                                _log.Write(LogLevel.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case listenKeyExpiredEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceStreamEvent>(token);
                            if (result)
                                onListenKeyExpired?.Invoke(data.As(result.Data));
                            else
                                _log.Write(LogLevel.Warning, "Couldn't deserialize data received from the expired listen key event: " + result.Error);
                            break;
                        }
                    default:
                        _log.Write(LogLevel.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, new[] { listenKey }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Blvt info update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token,
            Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToBlvtInfoUpdatesAsync(new List<string> { token }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage, CancellationToken ct = default)
        {
            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + bltvInfoEndpoint).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceBlvtInfoUpdate>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.TokenName)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, tokens, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Blvt kline update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token,
            KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            SubscribeToBlvtKlineUpdatesAsync(new List<string> { token }, interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + bltvKlineEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await _baseClient.SubscribeInternal(this, _options.OptionsUsdFutures.BaseAddress, tokens, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
