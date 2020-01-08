using Binance.Net.Converters;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Sockets;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance websocket Api
    /// </summary>
    public class BinanceSocketClient : SocketClient, IBinanceSocketClient
    {
        #region fields
        private static BinanceSocketClientOptions defaultOptions = new BinanceSocketClientOptions();
        private static BinanceSocketClientOptions DefaultOptions => defaultOptions.Copy();

        private readonly string baseCombinedAddress;

        private const string DepthStreamEndpoint = "@depth";
        private const string BookTickerStreamEndpoint = "@bookTicker";
        private const string KlineStreamEndpoint = "@kline";
        private const string TradesStreamEndpoint = "@trade";
        private const string AggregatedTradesStreamEndpoint = "@aggTrade";
        private const string SymbolTickerStreamEndpoint = "@ticker";
        private const string AllSymbolTickerStreamEndpoint = "!ticker@arr";
        private const string PartialBookDepthStreamEndpoint = "@depth";

        private const string AccountUpdateEvent = "outboundAccountInfo";
        private const string ExecutionUpdateEvent = "executionReport";
        private const string OcoOrderUpdateEvent = "listStatus";
        private const string AccountPositionUpdateEvent = "outboundAccountPosition";
        private const string BalanceUpdateEvent = "balanceUpdate";
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BinanceSocketClient with default options
        /// </summary>
        public BinanceSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceSocketClient(BinanceSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials, ArrayParametersSerialization.MultipleValues))
        {
            baseCombinedAddress = options.BaseSocketCombinedAddress;
        }
        #endregion 

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(BinanceSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new BinanceAuthenticationProvider(new ApiCredentials(apiKey, apiSecret), ArrayParametersSerialization.MultipleValues));
        }

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
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => await SubscribeToKlineUpdatesAsync(new [] { symbol }, interval, onMessage).ConfigureAwait(false);


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
            foreach(var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<BinanceCombinedStream<BinanceStreamKlineData>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(string symbol, int? updateInterval, Action<BinanceOrderBook> onMessage) => SubscribeToOrderBookUpdatesAsync(symbol, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<BinanceOrderBook> onMessage) => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(IEnumerable<string> symbols, int? updateInterval, Action<BinanceOrderBook> onMessage) => SubscribeToOrderBookUpdatesAsync(symbols, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<BinanceOrderBook> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            updateInterval?.ValidateIntValues(nameof(updateInterval), 0, 100, 1000);
            var handler = new Action<BinanceCombinedStream<BinanceOrderBook>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + DepthStreamEndpoint + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToBookTickerUpdates(string symbol, Action<BinanceBookTick> onMessage) => SubscribeToBookTickerUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<BinanceBookTick> onMessage) => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToBookTickerUpdates(IEnumerable<string> symbols, Action<BinanceBookTick> onMessage) => SubscribeToBookTickerUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<BinanceBookTick> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<BinanceCombinedStream<BinanceBookTick>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + BookTickerStreamEndpoint).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

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
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<BinanceCombinedStream<BinanceStreamAggregatedTrade>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + AggregatedTradesStreamEndpoint).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToTradeUpdates(string symbol, Action<BinanceStreamTrade> onMessage) => SubscribeToTradeUpdatesAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<BinanceStreamTrade> onMessage) => await SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToTradeUpdates(IEnumerable<string> symbols, Action<BinanceStreamTrade> onMessage) => SubscribeToTradeUpdatesAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamTrade> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            var handler = new Action<BinanceCombinedStream<BinanceStreamTrade>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + TradesStreamEndpoint).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

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
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string symbol, Action<BinanceStreamTick> onMessage)
        {
            symbol.ValidateBinanceSymbol();
            return await Subscribe(symbol.ToLower() + SymbolTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

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
            return await Subscribe(AllSymbolTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToPartialOrderBookUpdates(string symbol, int levels, int? updateInterval, Action<BinanceOrderBook> onMessage) => SubscribeToPartialOrderBookUpdatesAsync(symbol, levels, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<BinanceOrderBook> onMessage) => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToPartialOrderBookUpdates(IEnumerable<string> symbols, int levels, int? updateInterval, Action<BinanceOrderBook> onMessage) => SubscribeToPartialOrderBookUpdatesAsync(symbols, levels, updateInterval, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000. Defaults to 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<BinanceOrderBook> onMessage)
        {
            symbols.ValidateNotNull(nameof(symbols));
            foreach (var symbol in symbols)
                symbol.ValidateBinanceSymbol();

            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<BinanceCombinedStream<BinanceOrderBook>>(data =>
            {
                data.Data.Symbol = data.Stream.Split('@')[0];
                onMessage(data.Data);
            });

            symbols = symbols.Select(a => a.ToLower() + PartialBookDepthStreamEndpoint + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await Subscribe(string.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">The event handler for whenever an account info update is received</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public CallResult<UpdateSubscription> SubscribeToUserDataUpdates(
            string listenKey, 
            Action<BinanceStreamAccountInfo>? onAccountInfoMessage, 
            Action<BinanceStreamOrderUpdate>? onOrderUpdateMessage,
            Action<BinanceStreamOrderList>? onOcoOrderUpdateMessage,
            Action<IEnumerable<BinanceStreamBalance>>? onAccountPositionMessage,
            Action<BinanceStreamBalanceUpdate>? onAccountBalanceUpdate) => SubscribeToUserDataUpdatesAsync(listenKey, onAccountInfoMessage, onOrderUpdateMessage, onOcoOrderUpdateMessage, onAccountPositionMessage, onAccountBalanceUpdate).Result;

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">The event handler for whenever an account info update is received</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <param name="onOcoOrderUpdateMessage">The event handler for whenever an oco order status update is received</param>
        /// <param name="onAccountPositionMessage">The event handler for whenever an account position update is received. Account position updates are a list of changed funds</param>
        /// <param name="onAccountBalanceUpdate">The event handler for whenever a deposit or withdrawal has been processed and the account balance has changed</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey, 
            Action<BinanceStreamAccountInfo>? onAccountInfoMessage, 
            Action<BinanceStreamOrderUpdate>? onOrderUpdateMessage,
            Action<BinanceStreamOrderList>? onOcoOrderUpdateMessage,
            Action<IEnumerable<BinanceStreamBalance>>? onAccountPositionMessage,
            Action<BinanceStreamBalanceUpdate>? onAccountBalanceUpdate)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var handler = new Action<string>(data =>
            {
                var token = JToken.Parse(data);
                var evnt = (string)token["e"];
                switch (evnt)
                {
                    case AccountUpdateEvent:
                    {
                        var result = Deserialize<BinanceStreamAccountInfo>(token, false);
                        if (result.Success)
                            onAccountInfoMessage?.Invoke(result.Data);
                        else
                            log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account stream: " + result.Error);
                        break;
                    }
                    case ExecutionUpdateEvent:
                    {
                        log.Write(LogVerbosity.Debug, data);
                        var result = Deserialize<BinanceStreamOrderUpdate>(token, false);
                        if (result)
                            onOrderUpdateMessage?.Invoke(result.Data);
                        else
                            log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                        break;
                    }
                    case OcoOrderUpdateEvent:
                    {
                        log.Write(LogVerbosity.Debug, data);
                        var result = Deserialize<BinanceStreamOrderList>(token, false);
                        if (result)
                            onOcoOrderUpdateMessage?.Invoke(result.Data);
                        else
                            log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from oco order stream: " + result.Error);
                        break;
                    }
                    case AccountPositionUpdateEvent:
                    {
                        log.Write(LogVerbosity.Debug, data);
                        var result = Deserialize<IEnumerable<BinanceStreamBalance>>(token["B"], false);
                        if (result)
                            onAccountPositionMessage?.Invoke(result.Data);
                        else
                            log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account position stream: " + result.Error);
                        break;
                    }
                    case BalanceUpdateEvent:
                    {
                        log.Write(LogVerbosity.Debug, data);
                        var result = Deserialize<BinanceStreamBalanceUpdate>(token, false);
                        if (result)
                            onAccountBalanceUpdate?.Invoke(result.Data);
                        else
                            log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account position stream: " + result.Error);
                        break;
                    }
                    default:
                        log.Write(LogVerbosity.Warning, $"Received unknown user data event {evnt}: " + data);
                        break;
                }
            });

            return await Subscribe(listenKey, false, handler).ConfigureAwait(false);
        }

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(string url, bool combined, Action<T> onData)
        {
            if (combined)
                url = baseCombinedAddress + "stream?streams=" + url;
            else
                url = BaseAddress + url;

            return await Subscribe(url, null, url + NextId(), false, onData).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            return Task.FromResult(true);
        }
        #endregion
    }
}
