using Binance.Net.Converters;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Implementation;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using System.Diagnostics;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance websocket Api
    /// </summary>
    public class BinanceSocketClient : ExchangeClient, IBinanceSocketClient
    {
        #region fields
        private static BinanceSocketClientOptions defaultOptions = new BinanceSocketClientOptions();
        private static BinanceSocketClientOptions DefaultOptions
        {
            get
            {
                var result = new BinanceSocketClientOptions()
                {
                    LogVerbosity = defaultOptions.LogVerbosity,
                    BaseAddress = defaultOptions.BaseAddress,
                    LogWriters = defaultOptions.LogWriters,
                    Proxy = defaultOptions.Proxy,
                    RateLimiters = defaultOptions.RateLimiters,
                    RateLimitingBehaviour = defaultOptions.RateLimitingBehaviour,
                    ReconnectTryBehaviour = defaultOptions.ReconnectTryBehaviour,
                    ReconnectTryInterval = defaultOptions.ReconnectTryInterval,
                    BaseSocketCombinedAddress = defaultOptions.BaseSocketCombinedAddress
                };

                if (defaultOptions.ApiCredentials != null)
                    result.ApiCredentials = new ApiCredentials(defaultOptions.ApiCredentials.Key.GetString(), defaultOptions.ApiCredentials.Secret.GetString());

                return result;
            }
        }

        private string baseCombinedAddress;
        private ReconnectBehaviour reconnectBehaviour;
        private TimeSpan reconnectInterval;

        private readonly List<BinanceStream> sockets = new List<BinanceStream>();

        private int lastStreamId;
        private readonly object streamIdLock = new object();
        private const SslProtocols protocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

        private const string DepthStreamEndpoint = "@depth";
        private const string KlineStreamEndpoint = "@kline";
        private const string TradesStreamEndpoint = "@trade";
        private const string AggregatedTradesStreamEndpoint = "@aggTrade";
        private const string SymbolTickerStreamEndpoint = "@ticker";
        private const string AllSymbolTickerStreamEndpoint = "!ticker@arr";
        private const string PartialBookDepthStreamEndpoint = "@depth";

        private const string AccountUpdateEvent = "outboundAccountInfo";
        private const string ExecutionUpdateEvent = "executionReport";
        #endregion

        #region properties
        public IWebsocketFactory SocketFactory { get; set; } = new WebsocketFactory();
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
        public BinanceSocketClient(BinanceSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            Configure(options);
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
            SetAuthenticationProvider(new BinanceAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }

        /// <summary>
        /// Checks the connection to the server and returns how long connecting took
        /// </summary>
        /// <returns>Time to connect in miliseconds</returns>
        public override async Task<CallResult<long>> PingAsync()
        {
            var socket = SocketFactory.CreateWebsocket(log, baseAddress);

            var sw = Stopwatch.StartNew();
            var connect = await socket.Connect().ConfigureAwait(false);
            if (!connect)
                return new CallResult<long>(0, new CantConnectError());
            sw.Stop();
            return new CallResult<long>(sw.ElapsedMilliseconds, null);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToKlineStreamAsync(symbol, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToKlineStreamAsync(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => await SubscribeToKlineStreamAsync(new [] { symbol }, interval, onMessage).ConfigureAwait(false);


        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToKlineStream(string[] symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToKlineStreamAsync(symbols, interval, onMessage).Result;

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToKlineStreamAsync(string[] symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage)
        {
            symbols = symbols.Select(a => a.ToLower() + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await SubscribeCombined(String.Join("/", symbols), onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToDepthStream(string symbol, Action<BinanceStreamDepth> onMessage) => SubscribeToDepthStreamAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToDepthStreamAsync(string symbol, Action<BinanceStreamDepth> onMessage) => await SubscribeToDepthStreamAsync(new [] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToDepthStream(string[] symbol, Action<BinanceStreamDepth> onMessage) => SubscribeToDepthStreamAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToDepthStreamAsync(string[] symbols, Action<BinanceStreamDepth> onMessage)
        {
            symbols = symbols.Select(a => a.ToLower() + DepthStreamEndpoint).ToArray();
            return await SubscribeCombined(String.Join("/", symbols), onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToAggregatedTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToAggregatedTradesStream(string symbol, Action<BinanceStreamAggregatedTrade> onMessage) => SubscribeToAggregatedTradesStreamAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToAggregatedTradesStreamAsync(string symbol, Action<BinanceStreamAggregatedTrade> onMessage) => await SubscribeToAggregatedTradesStreamAsync(new [] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToAggregatedTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToAggregatedTradesStream(string[] symbols, Action<BinanceStreamAggregatedTrade> onMessage) => SubscribeToAggregatedTradesStreamAsync(symbols, onMessage).Result;
        
        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToAggregatedTradesStreamAsync(string[] symbols, Action<BinanceStreamAggregatedTrade> onMessage)
        {
            symbols = symbols.Select(a => a.ToLower() + AggregatedTradesStreamEndpoint).ToArray();
            return await SubscribeCombined(String.Join("/", symbols), onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToTradesStream(string symbol, Action<BinanceStreamTrade> onMessage) => SubscribeToTradesStreamAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToTradesStreamAsync(string symbol, Action<BinanceStreamTrade> onMessage) => await SubscribeToTradesStreamAsync(new [] { symbol }, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToTradesStream(string[] symbols, Action<BinanceStreamTrade> onMessage) => SubscribeToTradesStreamAsync(symbols, onMessage).Result;

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToTradesStreamAsync(string[] symbols, Action<BinanceStreamTrade> onMessage)
        {
            symbols = symbols.Select(a => a.ToLower() + TradesStreamEndpoint).ToArray();
            return await SubscribeCombined(String.Join("/", symbols), onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToSymbolTickerAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToSymbolTicker(string symbol, Action<BinanceStreamTick> onMessage) => SubscribeToSymbolTickerAsync(symbol, onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToSymbolTickerAsync(string symbol, Action<BinanceStreamTick> onMessage)
        {
            symbol = symbol.ToLower();
            return await Subscribe(baseAddress + symbol + SymbolTickerStreamEndpoint, onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToAllSymbolTickerAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToAllSymbolTicker(Action<BinanceStreamTick[]> onMessage) => SubscribeToAllSymbolTickerAsync(onMessage).Result;

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToAllSymbolTickerAsync(Action<BinanceStreamTick[]> onMessage)
        {
            return await Subscribe(baseAddress + AllSymbolTickerStreamEndpoint, onMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToPartialBookDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string symbol, int levels, Action<BinanceStreamOrderBook> onMessage) => SubscribeToPartialBookDepthStreamAsync(symbol, levels, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToPartialBookDepthStreamAsync(string symbol, int levels, Action<BinanceStreamOrderBook> onMessage) => await SubscribeToPartialBookDepthStreamAsync(new [] { symbol }, levels, onMessage).ConfigureAwait(false);

        /// <summary>
        /// Synchronized verion of the <see cref="SubscribeToPartialBookDepthStreamAsync(string[], int, Action{BinanceStreamOrderBook})"/> method
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="levels"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string[] symbols, int levels, Action<BinanceStreamOrderBook> onMessage) => SubscribeToPartialBookDepthStreamAsync(symbols, levels, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToPartialBookDepthStreamAsync(string[] symbols, int levels, Action<BinanceStreamOrderBook> onMessage)
        {
            symbols = symbols.Select(a => a.ToLower() + PartialBookDepthStreamEndpoint + levels).ToArray();
            var socketResult = await CreateSocket(baseCombinedAddress + "stream?streams=" + String.Join("/", symbols)).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                log.Write(LogVerbosity.Debug, $"Data received on sub of {typeof(BinanceStreamOrderBook)}: " + msg);
                var result = Deserialize<BinanceCombinedStream<BinanceStreamOrderBook>>(msg, false);
                if (result.Success)
                {
                    var stream = result.Data.Stream;
                    var symbol = stream.Split('@')[0];
                    result.Data.Data.Symbol = symbol;
                    onMessage?.Invoke(result.Data.Data);
                }
                else
                    log.Write(LogVerbosity.Info, $"Couldn't deserialize data received from combined stream of type {typeof(BinanceStreamOrderBook)}: " + result.Error);
            };

            log.Write(LogVerbosity.Info, "Started combined stream of type " + typeof(BinanceStreamOrderBook));
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage) => SubscribeToUserStreamAsync(listenKey, onAccountInfoMessage, onOrderUpdateMessage).Result;

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">The event handler for whenever an account info update is received</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToUserStreamAsync(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage)
        {
            if (string.IsNullOrEmpty(listenKey))
                return new CallResult<BinanceStreamSubscription>(null, new ArgumentError("ListenKey must be provided"));

            return await CreateUserStream(listenKey, onAccountInfoMessage, onOrderUpdateMessage).ConfigureAwait(false);
        }

        /// <summary>
        /// Unsubscribes from a stream
        /// </summary>
        /// <param name="streamSubscription">The stream subscription received by subscribing</param>
        public void UnsubscribeFromStream(BinanceStreamSubscription streamSubscription)
        {
            lock (sockets)
                sockets.SingleOrDefault(s => s.StreamResult.StreamId == streamSubscription.StreamId)?.Close().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Unsubscribes from all streams
        /// </summary>
        public void UnsubscribeAllStreams()
        {
            lock (sockets)
                sockets.ToList().ForEach(s => s.Close().ConfigureAwait(false).GetAwaiter().GetResult());
        }

        /// <summary>
        /// Dispose this instance
        /// </summary>
        public override void Dispose()
        {
            log.Write(LogVerbosity.Info, "Disposing socket client, closing sockets");
            lock (sockets)
                sockets.ToList().ForEach(s => s.Socket.Close().Wait());
        }

        private async Task<CallResult<BinanceStreamSubscription>> Subscribe<T>(string url, Action<T> onMessage) where T: class
        {
            var socketResult = await CreateSocket(url).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                log.Write(LogVerbosity.Debug, $"Data received on sub of {typeof(T)}: " + msg);
                var result = Deserialize<T>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, $"Couldn't deserialize data received from {typeof(T)} stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, $"Started stream for {typeof(T)}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        private async Task<CallResult<BinanceStreamSubscription>> SubscribeCombined<T>(string streams, Action<T> onMessage)
        {
            var socketResult = await CreateSocket(baseCombinedAddress + "stream?streams=" + streams).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                log.Write(LogVerbosity.Debug, $"Data received on sub of {typeof(T)}: " + msg);
                var result = Deserialize<BinanceCombinedStream<T>>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data.Data);
                else
                    log.Write(LogVerbosity.Info, $"Couldn't deserialize data received from combined stream of type {typeof(T)}: " + result.Error);
            };

            log.Write(LogVerbosity.Info, "Started combined stream of type " + typeof(T));
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        private async Task<CallResult<BinanceStreamSubscription>> CreateUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage)
        {
            var socketResult = await CreateSocket(baseAddress + listenKey).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                if (msg.Contains(AccountUpdateEvent))
                {
                    var result = Deserialize<BinanceStreamAccountInfo>(msg, false);
                    if (result.Success)
                        onAccountInfoMessage?.Invoke(result.Data);
                    else
                        log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account stream: " + result.Error);
                }
                else if (msg.Contains(ExecutionUpdateEvent))
                {
                    log.Write(LogVerbosity.Debug, msg);
                    var result = Deserialize<BinanceStreamOrderUpdate>(msg, false);
                    if (result.Success)
                        onOrderUpdateMessage?.Invoke(result.Data);
                    else
                        log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                }
            };

            log.Write(LogVerbosity.Info, "User stream started");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        private async Task<CallResult<BinanceStream>> CreateSocket(string url)
        {
            try
            {
                var socket = SocketFactory.CreateWebsocket(log, url);
                var socketObject = new BinanceStream() { Socket = socket, StreamResult = new BinanceStreamSubscription() { StreamId = NextStreamId() } };
                socket.SetEnabledSslProtocols(protocols);

                socket.OnClose += () => Socket_OnClose(socketObject);

                socket.OnError += Socket_OnError;
                socket.OnError += socketObject.StreamResult.InvokeError;

                socket.OnOpen += Socket_OnOpen;
                var connected = await socket.Connect().ConfigureAwait(false);
                if (!connected)
                {
                    log.Write(LogVerbosity.Error, "Couldn't open socket stream");
                    return new CallResult<BinanceStream>(null, new CantConnectError());
                }

                log.Write(LogVerbosity.Debug, "Socket connection established");

                lock (sockets)
                    sockets.Add(socketObject);
                return new CallResult<BinanceStream>(socketObject, null);
            }
            catch (Exception e)
            {
                var errorMessage = $"Couldn't open socket stream: {e.Message}";
                log.Write(LogVerbosity.Error, errorMessage);
                return new CallResult<BinanceStream>(null, new CantConnectError());
            }
        }

        private void Configure(BinanceSocketClientOptions options)
        {
            baseCombinedAddress = options.BaseSocketCombinedAddress;
            reconnectBehaviour = options.ReconnectTryBehaviour;
            reconnectInterval = options.ReconnectTryInterval;
        }

        private void Socket_OnOpen()
        {
            log.Write(LogVerbosity.Debug, "Socket opened");
        }

        private void Socket_OnError(Exception e)
        {
            log.Write(LogVerbosity.Error, $"Socket error {e?.Message}");
        }

        private void Socket_OnClose(object sender)
        {
            var con = (BinanceStream)sender;
            if (reconnectBehaviour == ReconnectBehaviour.AutoReconnect && con.TryReconnect)
            {
                log.Write(LogVerbosity.Info, "Connection lost, going to try to reconnect");
                Task.Run(() =>
                {
                    Thread.Sleep((int)Math.Round(reconnectInterval.TotalMilliseconds));
                    if (con.Socket.Connect().Result)
                        log.Write(LogVerbosity.Info, "Reconnected");
                });
            }
            else
            {
                log.Write(LogVerbosity.Info, "Socket closed");
                con.StreamResult.InvokeClosed();
                con.Socket.Dispose();
                lock (sockets)
                    sockets.Remove(con);
            }
        }

        private int NextStreamId()
        {
            lock (streamIdLock)
            {
                lastStreamId++;
                return lastStreamId;
            }
        }
        #endregion
    }
}
