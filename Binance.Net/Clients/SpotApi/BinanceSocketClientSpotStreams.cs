using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceSocketClientSpotStreams : SocketApiClient, IBinanceSocketClientSpotStreams
    {
        #region fields
        private readonly BinanceSocketClient _baseClient;
        private readonly BinanceSocketClientOptions _options;
        private readonly Log _log;

        private const string depthStreamEndpoint = "@depth";
        private const string bookTickerStreamEndpoint = "@bookTicker";
        private const string allBookTickerStreamEndpoint = "!bookTicker";
        private const string klineStreamEndpoint = "@kline";
        private const string tradesStreamEndpoint = "@trade";
        private const string aggregatedTradesStreamEndpoint = "@aggTrade";
        private const string symbolTickerStreamEndpoint = "@ticker";
        private const string allSymbolTickerStreamEndpoint = "!ticker@arr";
        private const string partialBookDepthStreamEndpoint = "@depth";
        private const string symbolMiniTickerStreamEndpoint = "@miniTicker";
        private const string allSymbolMiniTickerStreamEndpoint = "!miniTicker@arr";

        private const string executionUpdateEvent = "executionReport";
        private const string ocoOrderUpdateEvent = "listStatus";
        private const string accountPositionUpdateEvent = "outboundAccountPosition";
        private const string balanceUpdateEvent = "balanceUpdate";
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClientSpot with default options
        /// </summary>
        public BinanceSocketClientSpotStreams(Log log, BinanceSocketClient baseClient, BinanceSocketClientOptions options) :
            base(options, options.SpotStreamsOptions)
        {
            _options = options;
            _baseClient = baseClient;
            _log = log;
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        #region methods

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default) =>
            await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamAggregatedTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + aggregatedTradesStreamEndpoint)
                .ToArray();
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTrade>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + tradesStreamEndpoint).ToArray();
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols,
            KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols,
            IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As<IBinanceStreamKlineData>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.SelectMany(a =>
                intervals.Select(i =>
                    a.ToLower(CultureInfo.InvariantCulture) + klineStreamEndpoint + "_" +
                    JsonConvert.SerializeObject(i, new KlineIntervalConverter(false)))).ToArray();
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol,
            Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default) =>
            await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamMiniTick>>>(data => onMessage(data.As<IBinanceMiniTick>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolMiniTickerStreamEndpoint)
                .ToArray();

            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(
            Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamCoinMiniTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceMiniTick>>(data.Data.Data, data.Data.Stream)));
            return await _baseClient.SubscribeInternal(this, BaseAddress, new[] { allSymbolMiniTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default) =>
            await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + bookTickerStreamEndpoint).ToArray();
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Book Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(
            Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamBookPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await _baseClient.SubscribeInternal(this, BaseAddress, new[] { allBookTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol,
            int levels, int? updateInterval, Action<DataEvent<IBinanceOrderBook>> onMessage, CancellationToken ct = default) =>
            await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage, ct)
                .ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(
            IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceOrderBook>>>(data =>
            {
                data.Data.Data.Symbol = data.Data.Stream.Split('@')[0];
                onMessage(data.As<IBinanceOrderBook>(data.Data.Data, data.Data.Data.Symbol));
            });

            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + partialBookDepthStreamEndpoint + levels +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Diff. Depth Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol,
            int? updateInterval, Action<DataEvent<IBinanceEventOrderBook>> onMessage, CancellationToken ct = default) =>
            await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols,
            int? updateInterval, Action<DataEvent<IBinanceEventOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceEventOrderBook>>>(data => onMessage(data.As<IBinanceEventOrderBook>(data.Data.Data, data.Data.Data.Symbol)));
            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + depthStreamEndpoint +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
            return await _baseClient.SubscribeInternal(this, BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceTick>>(data.Data.Data, data.Data.Stream)));
            return await _baseClient.SubscribeInternal(this, BaseAddress, new[] { allSymbolTickerStreamEndpoint }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region User Data Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceStreamOrderUpdate>>? onOrderUpdateMessage,
            Action<DataEvent<BinanceStreamOrderList>>? onOcoOrderUpdateMessage,
            Action<DataEvent<BinanceStreamPositionsUpdate>>? onAccountPositionMessage,
            Action<DataEvent<BinanceStreamBalanceUpdate>>? onAccountBalanceUpdate,
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
                    case executionUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceStreamOrderUpdate>(token);
                            if (result)
                                onOrderUpdateMessage?.Invoke(data.As(result.Data, result.Data.Id.ToString()));
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case ocoOrderUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceStreamOrderList>(token);
                            if (result)
                                onOcoOrderUpdateMessage?.Invoke(data.As(result.Data, result.Data.Id.ToString()));
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from oco order stream: " + result.Error);
                            break;
                        }
                    case accountPositionUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceStreamPositionsUpdate>(token);
                            if (result)
                                onAccountPositionMessage?.Invoke(data.As(result.Data));
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from account position stream: " + result.Error);
                            break;
                        }
                    case balanceUpdateEvent:
                        {
                            var result = _baseClient.DeserializeInternal<BinanceStreamBalanceUpdate>(token);
                            if (result)
                                onAccountBalanceUpdate?.Invoke(data.As(result.Data, result.Data.Asset));
                            else
                                _log.Write(LogLevel.Warning,
                                    "Couldn't deserialize data received from account position stream: " + result.Error);
                            break;
                        }
                    default:
                        _log.Write(LogLevel.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await _baseClient.SubscribeInternal(this, BaseAddress, new[] { listenKey }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion
    }
}
