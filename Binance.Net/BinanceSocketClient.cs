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

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance websocket Api
    /// </summary>
    public class BinanceSocketClient: ExchangeClient
    {
        #region fields
        private static BinanceSocketClientOptions defaultOptions = new BinanceSocketClientOptions();

        private string baseWebsocketAddress;
        private ReconnectBehaviour reconnectBehaviour;
        private TimeSpan reconnectInterval;

        private readonly List<BinanceStream> sockets = new List<BinanceStream>();

        private int lastStreamId;
        private readonly object streamIdLock = new object();
        private SslProtocols protocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

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
        public BinanceSocketClient():this(defaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceSocketClient(BinanceSocketClientOptions options): base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
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
        /// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKline> onMessage) => SubscribeToKlineStreamAsync(symbol, interval, onMessage).Result;
        
        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToKlineStreamAsync(string symbol, KlineInterval interval, Action<BinanceStreamKline> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = await CreateSocket(baseWebsocketAddress + symbol + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceStreamKline>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from kline stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, $"Started kline stream for {symbol}: {interval}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
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
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToDepthStreamAsync(string symbol, Action<BinanceStreamDepth> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = await CreateSocket(baseWebsocketAddress + symbol + DepthStreamEndpoint).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceStreamDepth>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from depth stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, $"Started depth stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
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
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToAggregatedTradesStreamAsync(string symbol, Action<BinanceStreamAggregatedTrade> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = await CreateSocket(baseWebsocketAddress + symbol + AggregatedTradesStreamEndpoint).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceStreamAggregatedTrade>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from trade stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, $"Started trade stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
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
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToTradesStreamAsync(string symbol, Action<BinanceStreamTrade> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = await CreateSocket(baseWebsocketAddress + symbol + TradesStreamEndpoint).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceStreamTrade>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from trade stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, $"Started trade stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
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
            var socketResult = await CreateSocket(baseWebsocketAddress + symbol + SymbolTickerStreamEndpoint).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceStreamTick>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from depth stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, $"Started symbol ticker stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
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
            var socketResult = await CreateSocket(baseWebsocketAddress + AllSymbolTickerStreamEndpoint).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceStreamTick[]>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from all ticker stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, "Started all symbol ticker stream");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToPartialBookDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string symbol, int levels, Action<BinanceOrderBook> onMessage) => SubscribeToPartialBookDepthStreamAsync(symbol, levels, onMessage).Result;

        /// <summary>
        /// Subscribes to the depth updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<BinanceStreamSubscription>> SubscribeToPartialBookDepthStreamAsync(string symbol, int levels, Action<BinanceOrderBook> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = await CreateSocket(baseWebsocketAddress + symbol + PartialBookDepthStreamEndpoint + levels).ConfigureAwait(false);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (msg) =>
            {
                var result = Deserialize<BinanceOrderBook>(msg, false);
                if (result.Success)
                    onMessage?.Invoke(result.Data);
                else
                    log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from depth stream: " + result.Error);
            };

            log.Write(LogVerbosity.Info, "Started partial book depth stream");
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
                sockets.SingleOrDefault(s => s.StreamResult.StreamId == streamSubscription.StreamId)?.Close().Wait();
        }

        /// <summary>
        /// Unsubscribes from all streams
        /// </summary>
        public void UnsubscribeAllStreams()
        {
            lock (sockets)
                sockets.ToList().ForEach(s => s.Close().Wait());
        }

        /// <summary>
        /// Dispose this instance
        /// </summary>
        public override void Dispose()
        {
            log.Write(LogVerbosity.Info, "Disposing socket client, closing sockets");
            lock (sockets)
                sockets.ToList().ForEach(s => s.Socket.Close());
        }

        private async Task<CallResult<BinanceStreamSubscription>> CreateUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage)
        {
            var socketResult = await CreateSocket(baseWebsocketAddress + listenKey).ConfigureAwait(false);
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
                var socket = SocketFactory.CreateWebsocket(url);
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
            baseWebsocketAddress = options.BaseSocketAddress;
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
