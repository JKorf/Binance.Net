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
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;

namespace Binance.Net.SocketSubClients
{
    /// <summary>
    /// USDT-M futures streams
    /// </summary>
    public class BinanceSocketClientFuturesUsdt: BinanceSocketClientFutures, IBinanceSocketClientFuturesUsdt
    {
        private const string klineStreamEndpoint = "@kline";
        private const string markPriceStreamEndpoint = "@markPrice";
        private const string allMarkPriceStreamEndpoint = "!markPrice@arr";
        private const string symbolMiniTickerStreamEndpoint = "@miniTicker";
        private const string allMiniTickerStreamEndpoint = "!miniTicker@arr";
        private const string symbolTickerStreamEndpoint = "@ticker";
        private const string allTickerStreamEndpoint = "!ticker@arr";
        private const string compositeIndexEndpoint = "@compositeIndex";
        /// <summary>
        /// Base address
        /// </summary>
        protected override string? BaseAddress { get; }

        internal BinanceSocketClientFuturesUsdt(Log log, BinanceSocketClient baseClient,
            BinanceSocketClientOptions options) : base(log, baseClient)
        {
            BaseAddress = options.BaseAddressUsdtFutures;
        }

        #region Mark Price Stream

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage) => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage).ConfigureAwait(false);
                
        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesUsdtStreamMarkPrice>>>(data => onMessage(data.As(data.Data.Data, data.Data.Stream)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + markPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await Subscribe(symbols, handler).ConfigureAwait(false);
        }

        #endregion
        
        #region Mark Price Stream for All market

        /// <summary>
        /// Subscribes to the Mark price update stream for a all symbols
        /// </summary>
        /// /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamMarkPrice>>> onMessage)
        {
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceFuturesStreamMarkPrice>>>>(data => onMessage(data.As<IEnumerable<BinanceFuturesStreamMarkPrice>>(data.Data.Data, data.Data.Stream)));
            return await Subscribe(new[] { allMarkPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "") }, handler).ConfigureAwait(false);
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
        public override async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol and intervals
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols and interval
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols and intervals
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As<IBinanceStreamKlineData>(data.Data.Data)));
            symbols = symbols.SelectMany(a => intervals.Select(i => a.ToLower(CultureInfo.InvariantCulture) + klineStreamEndpoint + "_" + JsonConvert.SerializeObject(i, new KlineIntervalConverter(false)))).ToArray();
            return await Subscribe(symbols, handler).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage) => await SubscribeToSymbolMiniTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamMiniTick>>>(data => onMessage(data.As<IBinanceMiniTick>(data.Data.Data, data.Data.Stream)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolMiniTickerStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream
        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamMiniTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceMiniTick>>(data.Data.Data, data.Data.Stream)));
            return await Subscribe(new[] { allMiniTickerStreamEndpoint }, handler).ConfigureAwait(false);
        }
        #endregion

        #region Individual Symbol Ticker Streams

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> onMessage) => await SubscribeToSymbolTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceTick>> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTick>>>(data => onMessage(data.As<IBinanceTick>(data.Data.Data, data.Data.Stream)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + symbolTickerStreamEndpoint).ToArray();
            return await Subscribe(symbols, handler).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <summary>
        /// Subscribes to composite index updates stream for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToCompositeIndexUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamCompositeIndex>> onMessage)
        {
            var action = new Action<DataEvent<BinanceCombinedStream<BinanceFuturesStreamCompositeIndex>>>(data => {
                onMessage(data.As(data.Data.Data, data.Data.Stream));
            });
            return await Subscribe(new[] { symbol + compositeIndexEndpoint }, action).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> onMessage)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamTick>>>>(data => onMessage(data.As<IEnumerable<IBinanceTick>>(data.Data.Data, data.Data.Stream)));
            return await Subscribe(new[] { allTickerStreamEndpoint }, handler).ConfigureAwait(false);
        }

        #endregion
    }
}
