using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
using Newtonsoft.Json.Linq;

namespace Binance.Net.SocketSubClients
{
    /// <summary>
    /// COIN-M futures streams
    /// </summary>
    public class BinanceSocketClientFuturesCoin: BinanceSocketClientFutures, IBinanceSocketClientFuturesCoin
    {
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
        /// <summary>
        /// Base address
        /// </summary>
        protected override string BaseAddress { get; }

        internal BinanceSocketClientFuturesCoin(Log log, BinanceSocketClient baseClient,
            BinanceSocketClientOptions options) : base(log, baseClient)
        {
            BaseAddress = options.BaseAddressCoinFutures;
        }

        #region Kline/Candlestick Streams

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override CallResult<UpdateSubscription> SubscribeToKlineUpdates(string symbol, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage) => SubscribeToKlineUpdatesAsync(symbol, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage) => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage).ConfigureAwait(false);


        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override CallResult<UpdateSubscription> SubscribeToKlineUpdates(IEnumerable<string> symbols, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage) => SubscribeToKlineUpdatesAsync(symbols, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<BinanceCombinedStream<BinanceFuturesStreamCoinKlineData>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + klineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region Index Price Stream

        /// <summary>
        /// Subscribes to the Index price update stream for a single pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(string pair, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage) => SubscribeToIndexPriceUpdatesAsync(pair, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the Index price update stream for a single pair
        /// </summary>
        /// <param name="pair">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage) => await SubscribeToIndexPriceUpdatesAsync(new[] { pair }, updateInterval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the Index price update stream for a list of pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(IEnumerable<string> pairs, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage) => SubscribeToIndexPriceUpdatesAsync(pairs, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the Index price update stream for a list of pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage)
        {
            pairs.ValidateNotNull(nameof(pairs));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var internalHandler = new Action<JToken>(data => HandlePossibleSingleData(data, onMessage));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) + indexPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await Subscribe(string.Join("/", pairs), true, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #region Mark Price Stream

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(string symbol, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage) => SubscribeToMarkPriceUpdatesAsync(symbol, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage) => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(IEnumerable<string> symbols, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage) => SubscribeToMarkPriceUpdatesAsync(symbols, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);
            
            var internalHandler = new Action<JToken>(data => HandlePossibleSingleData(data, onMessage));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + markPriceStreamEndpoint + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await Subscribe(string.Join("/", symbols), true, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #region Continuous contract kline/Candlestick Streams

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToContinuousContractKlineUpdates(string pair, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToContinuousContractKlineUpdatesAsync(pair, contractType, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => await SubscribeToContinuousContractKlineUpdatesAsync(new[] { pair }, contractType, interval, onMessage).ConfigureAwait(false);


        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToContinuousContractKlineUpdates(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToContinuousContractKlineUpdatesAsync(pairs, contractType, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<BinanceCombinedStream<BinanceStreamKlineData>>(data => onMessage(data.Data));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      "_" +
                                      JsonConvert.SerializeObject(contractType, new ContractTypeConverter(false)).ToLower() +
                                      continuousKlineStreamEndpoint + 
                                      "_" + 
                                      JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(string.Join("/", pairs), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region Index kline/Candlestick Streams

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexKlineUpdates(string pair, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage) => SubscribeToIndexKlineUpdatesAsync(pair, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage) => await SubscribeToIndexKlineUpdatesAsync(new[] { pair }, interval, onMessage).ConfigureAwait(false);


        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToIndexKlineUpdates(IEnumerable<string> pairs, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage) => SubscribeToIndexKlineUpdatesAsync(pairs, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage)
        {
            pairs.ValidateNotNull(nameof(pairs));
            var handler = new Action<BinanceCombinedStream<BinanceStreamIndexKlineData>>(data => onMessage(data.Data));
            pairs = pairs.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                      indexKlineStreamEndpoint +
                                      "_" +
                                      JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(string.Join("/", pairs), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region Mark price kline/Candlestick Streams

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceKlineUpdates(string symbol, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage) => SubscribeToMarkPriceKlineUpdatesAsync(symbol, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage) => await SubscribeToMarkPriceKlineUpdatesAsync(new[] { symbol }, interval, onMessage).ConfigureAwait(false);


        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToMarkPriceKlineUpdates(IEnumerable<string> symbols, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage) => SubscribeToMarkPriceKlineUpdatesAsync(symbols, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            var handler = new Action<BinanceCombinedStream<BinanceStreamIndexKlineData>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) +
                                          markKlineStreamEndpoint +
                                         "_" +
                                         JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
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
        public override CallResult<UpdateSubscription> SubscribeToSymbolMiniTickerUpdates(string symbol, Action<IBinanceMiniTick> onMessage) => SubscribeToSymbolMiniTickerUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(string symbol, Action<IBinanceMiniTick> onMessage) => await SubscribeToSymbolMiniTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override CallResult<UpdateSubscription> SubscribeToSymbolMiniTickerUpdates(IEnumerable<string> symbols, Action<IBinanceMiniTick> onMessage) => SubscribeToSymbolMiniTickerUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<IBinanceMiniTick> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceStreamCoinMiniTick>>(data => onMessage(data.Data));
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
        public override CallResult<UpdateSubscription> SubscribeToAllSymbolMiniTickerUpdates(Action<IEnumerable<IBinanceMiniTick>> onMessage) => SubscribeToAllSymbolMiniTickerUpdatesAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolMiniTickerUpdatesAsync(Action<IEnumerable<IBinanceMiniTick>> onMessage)
        {
            var handler = new Action<IEnumerable<BinanceStreamCoinMiniTick>>(onMessage);
            return await Subscribe(allMiniTickerStreamEndpoint, false, handler).ConfigureAwait(false);
        }
        #endregion
        
        #region Individual Symbol Ticker Streams

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override CallResult<UpdateSubscription> SubscribeToSymbolTickerUpdates(string symbol, Action<IBinanceTick> onMessage) => SubscribeToSymbolTickerUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string symbol, Action<IBinanceTick> onMessage) => await SubscribeToSymbolTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override CallResult<UpdateSubscription> SubscribeToSymbolTickerUpdates(IEnumerable<string> symbols, Action<IBinanceTick> onMessage) => SubscribeToSymbolTickerUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(IEnumerable<string> symbols, Action<IBinanceTick> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<BinanceCombinedStream<BinanceStreamCoinTick>>(data => onMessage(data.Data));
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
        public override CallResult<UpdateSubscription> SubscribeToAllSymbolTickerUpdates(Action<IEnumerable<IBinanceTick>> onMessage) => SubscribeToAllSymbolTickerUpdatesAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public override async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolTickerUpdatesAsync(Action<IEnumerable<IBinanceTick>> onMessage)
        {
            var handler = new Action<IEnumerable<BinanceStreamCoinTick>>(data => onMessage(data));
            return await Subscribe(allTickerStreamEndpoint, false, handler).ConfigureAwait(false);
        }

        #endregion

        private void HandlePossibleSingleData<T>(JToken data, Action<IEnumerable<T>> onMessage)
        {
            var internalData = data["data"];
            if (internalData == null)
                return;

            if (internalData.Type == JTokenType.Array)
            {
                var deserialized = BaseClient
                    .DeserializeInternal<BinanceCombinedStream<IEnumerable<T>>>(
                        data);
                if (!deserialized)
                    return;

                onMessage(deserialized.Data.Data);
            }
            else
            {
                var deserialized = BaseClient
                    .DeserializeInternal<BinanceCombinedStream<T>>(
                        data);
                if (!deserialized)
                    return;

                onMessage(new[] { deserialized.Data.Data });
            }
        }
    }
}
