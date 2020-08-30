﻿using CryptoExchange.Net.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SocketSubClient;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.MarketStream;
using Binance.Net.Objects.Futures.UserStream;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.SocketSubClients
{
    /// <summary>
    /// Futures subscriptions
    /// </summary>
    public abstract class BinanceSocketClientFutures : IBinanceSocketClientFutures
    {
        private const string aggregatedTradesStreamEndpoint = "@aggTrade";
        private const string klineStreamEndpoint = "@kline";
        private const string symbolMiniTickerStreamEndpoint = "@miniTicker";
        private const string allMiniTickerStreamEndpoint = "!miniTicker@arr";
        private const string symbolTickerStreamEndpoint = "@ticker";
        private const string allTickerStreamEndpoint = "!ticker@arr";
        private const string bookTickerStreamEndpoint = "@bookTicker";
        private const string allBookTickerStreamEndpoint = "!bookTicker";
        private const string liquidationStreamEndpoint = "@forceOrder";
        private const string allLiquidationStreamEndpoint = "!forceOrder@arr";
        private const string partialBookDepthStreamEndpoint = "@depth";
        private const string depthStreamEndpoint = "@depth";

        private const string marginUpdateEvent = "MARGIN_CALL";
        private const string accountUpdateEvent = "ACCOUNT_UPDATE";
        private const string orderUpdateEvent = "ORDER_TRADE_UPDATE";
        private const string listenKeyExpiredEvent = "listenKeyExpired";

        /// <summary>
        /// Log
        /// </summary>
        protected readonly Log Log;
        /// <summary>
        /// BaseClient
        /// </summary>
        protected readonly BinanceSocketClient BaseClient;

        /// <summary>
        /// Base address
        /// </summary>
        protected abstract string BaseAddress { get; }

        internal BinanceSocketClientFutures(Log log, BinanceSocketClient baseClient, BinanceSocketClientOptions options)
        {
            Log = log;
            BaseClient = baseClient;
        }

        #region Aggregate Trade Streams

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedTradeUpdates(string symbol, Action<BinanceStreamAggregatedTrade> onMessage) => SubscribeToAggregatedTradeUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<BinanceStreamAggregatedTrade> onMessage) => await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedTradeUpdates(IEnumerable<string> symbols, Action<BinanceStreamAggregatedTrade> onMessage) => SubscribeToAggregatedTradeUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamAggregatedTrade> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceStreamAggregatedTrade>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + aggregatedTradesStreamEndpoint).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion
        
        #region Kline/Candlestick Streams

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToKlineUpdates(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToKlineUpdatesAsync(symbol, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage).ConfigureAwait(false);


        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToKlineUpdates(IEnumerable<string> symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToKlineUpdatesAsync(symbols, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<BinanceCombinedStream<BinanceStreamKlineData>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + klineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToSymbolMiniTickerUpdates(string symbol, Action<BinanceStreamMiniTick> onMessage) => SubscribeToSymbolMiniTickerUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(string symbol, Action<BinanceStreamMiniTick> onMessage) => await SubscribeToSymbolMiniTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToSymbolMiniTickerUpdates(IEnumerable<string> symbols, Action<BinanceStreamMiniTick> onMessage) => SubscribeToSymbolMiniTickerUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamMiniTick> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceStreamMiniTick>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolMiniTickerStreamEndpoint).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToAllSymbolMiniTickerUpdates(Action<IEnumerable<BinanceStreamMiniTick>> onMessage) => SubscribeToAllSymbolMiniTickerUpdatesAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolMiniTickerUpdatesAsync(Action<IEnumerable<BinanceStreamMiniTick>> onMessage)
        {
            return await Subscribe(allMiniTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Ticker Streams

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToSymbolTickerUpdates(string symbol, Action<BinanceStreamTick> onMessage) => SubscribeToSymbolTickerUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string symbol, Action<BinanceStreamTick> onMessage) => await SubscribeToSymbolTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToSymbolTickerUpdates(IEnumerable<string> symbols, Action<BinanceStreamTick> onMessage) => SubscribeToSymbolTickerUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamTick> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceStreamTick>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolTickerStreamEndpoint).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToAllSymbolTickerUpdates(Action<IEnumerable<BinanceStreamTick>> onMessage) => SubscribeToAllSymbolTickerUpdatesAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolTickerUpdatesAsync(Action<IEnumerable<BinanceStreamTick>> onMessage)
        {
            return await Subscribe(allTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToBookTickerUpdates(string symbol, Action<BinanceStreamBookPrice> onMessage) => SubscribeToBookTickerUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<BinanceStreamBookPrice> onMessage) => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToBookTickerUpdates(IEnumerable<string> symbols, Action<BinanceStreamBookPrice> onMessage) => SubscribeToBookTickerUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamBookPrice> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceStreamBookPrice>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + bookTickerStreamEndpoint).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region All Book Tickers Stream

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToAllBookTickerUpdates(Action<BinanceStreamBookPrice> onMessage) => SubscribeToAllBookTickerUpdatesAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<BinanceStreamBookPrice> onMessage)
        {
            return await Subscribe(allBookTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

        #endregion

        #region Liquidation Order Streams

        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToLiquidationUpdates(string symbol, Action<BinanceFuturesStreamLiquidation> onMessage) => SubscribeToLiquidationUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<BinanceFuturesStreamLiquidation> onMessage) => await SubscribeToLiquidationUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToLiquidationUpdates(IEnumerable<string> symbols, Action<BinanceFuturesStreamLiquidation> onMessage) => SubscribeToLiquidationUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<BinanceFuturesStreamLiquidation> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceFuturesStreamLiquidationData>>(data => onMessage(data.Data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + liquidationStreamEndpoint).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region All Market Liquidation Order Streams

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToAllLiquidationUpdates(Action<BinanceFuturesStreamLiquidation> onMessage) => SubscribeToAllLiquidationUpdatesAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<BinanceFuturesStreamLiquidation> onMessage)
        {
            var handler = new Action<BinanceFuturesStreamLiquidationData>(data => onMessage(data.Data));
            return await Subscribe(allLiquidationStreamEndpoint, false, handler).ConfigureAwait(false);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToPartialOrderBookUpdates(string symbol, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage) => SubscribeToPartialOrderBookUpdatesAsync(symbol, levels, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage) => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToPartialOrderBookUpdates(IEnumerable<string> symbols, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage) => SubscribeToPartialOrderBookUpdatesAsync(symbols, levels, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 250, 500);

            var handler = new Action<BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>>(data =>
            {
                data.Data.Symbol = data.Stream.Split('@')[0];
                onMessage(data.Data);
            });

            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + partialBookDepthStreamEndpoint + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region Diff. Book Depth Streams

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(string symbol, int? updateInterval, Action<IBinanceOrderBook> onMessage) => SubscribeToOrderBookUpdatesAsync(symbol, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<IBinanceOrderBook> onMessage) => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(IEnumerable<string> symbols, int? updateInterval, Action<IBinanceOrderBook> onMessage) => SubscribeToOrderBookUpdatesAsync(symbols, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<IBinanceOrderBook> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 250, 500);
            var handler = new Action<BinanceCombinedStream<BinanceFuturesStreamOrderBookDepth>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + depthStreamEndpoint + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region User Data Streams

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onCrossWalletUpdate">The event handler for whenever a cross wallet has changed</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onPositionUpdate">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToUserDataUpdates(
            string listenKey,
            Action<decimal>? onCrossWalletUpdate,
             Action<IEnumerable<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<IEnumerable<BinanceFuturesStreamBalance>>? onAccountBalanceUpdate,
            Action<IEnumerable<BinanceFuturesStreamPosition>>? onPositionUpdate,
            Action<BinanceFuturesStreamOrderUpdate>? onOrderUpdate,
            Action<BinanceStreamEvent> onListenKeyExpired) => SubscribeToUserDataUpdatesAsync(listenKey, onCrossWalletUpdate, onMarginUpdate, onAccountBalanceUpdate, onPositionUpdate, onOrderUpdate, onListenKeyExpired).Result;

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onCrossWalletUpdate">The event handler for whenever a cross wallet has changed</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <param name="onPositionUpdate">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<decimal>? onCrossWalletUpdate,
            Action<IEnumerable<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<IEnumerable<BinanceFuturesStreamBalance>>? onAccountBalanceUpdate,
            Action<IEnumerable<BinanceFuturesStreamPosition>>? onPositionUpdate,
            Action<BinanceFuturesStreamOrderUpdate>? onOrderUpdate,
            Action<BinanceStreamEvent> onListenKeyExpired)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var handler = new Action<string>(data =>
            {
                var token = JToken.Parse(data);
                var evnt = (string)token["e"];
                switch (evnt)
                {
                    case marginUpdateEvent:
                        {
                            Log.Write(LogVerbosity.Debug, data);

                            onCrossWalletUpdate?.Invoke(token["cw"].ToObject<decimal>());

                            var orders = token["o"];
                            var result = BaseClient.DeserializeInternal<BinanceFuturesStreamMarginUpdate[]>(orders, false);
                            if (result)
                                onMarginUpdate?.Invoke(result.Data);
                            else
                                Log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case accountUpdateEvent:
                        {
                            if (token["a"]["B"] != null)
                            {
                                var balances = token["a"]["B"];
                                var result = BaseClient.DeserializeInternal<BinanceFuturesStreamBalance[]>(balances, false);
                                if (result.Success)
                                    onAccountBalanceUpdate?.Invoke(result.Data);
                                else
                                    Log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account stream: " + result.Error);
                            }

                            if (token["a"]["P"] != null)
                            {
                                var positions = token["a"]["P"];
                                var result = BaseClient.DeserializeInternal<BinanceFuturesStreamPosition[]>(positions, false);
                                if (result.Success)
                                    onPositionUpdate?.Invoke(result.Data);
                                else
                                    Log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account stream: " + result.Error);
                            }

                            break;
                        }
                    case orderUpdateEvent:
                        {
                            Log.Write(LogVerbosity.Debug, data);
                            var orders = token["o"];
                            var result = BaseClient.DeserializeInternal<BinanceFuturesStreamOrderUpdate>(orders, false);
                            if (result)
                                onOrderUpdate?.Invoke(result.Data);
                            else
                                Log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                            break;
                        }
                    case listenKeyExpiredEvent:
                        {
                            Log.Write(LogVerbosity.Debug, data);
                            var result = BaseClient.DeserializeInternal<BinanceStreamEvent>(token, false);
                            if (result)
                                onListenKeyExpired?.Invoke(result.Data);
                            else
                                Log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from the expired listen key event: " + result.Error);
                            break;
                        }
                    default:
                        Log.Write(LogVerbosity.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await Subscribe(listenKey, false, handler).ConfigureAwait(false);
        }

        #endregion

        /// <summary>
        /// Subscribe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="combined"></param>
        /// <param name="onData"></param>
        /// <returns></returns>
        protected async Task<CallResult<UpdateSubscription>> Subscribe<T>(string url, bool combined, Action<T> onData)
        {
            if (combined)
                url = BaseAddress + "stream?streams=" + url;
            else
                url = BaseAddress + "ws/" + url;

            return await BaseClient.SubscribeInternal(url, onData).ConfigureAwait(false);
        }
    }
}
