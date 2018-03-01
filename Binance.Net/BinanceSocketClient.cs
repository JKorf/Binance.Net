using Binance.Net.Converters;
using Binance.Net.Implementations;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using SuperSocket.ClientEngine;

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
        private readonly List<BinanceStream> sockets = new List<BinanceStream>();

        private int lastStreamId;
        private readonly object streamIdLock = new object();
        private SslProtocols protocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

        private const string DepthStreamEndpoint = "@depth";
        private const string KlineStreamEndpoint = "@kline";
        private const string TradesStreamEndpoint = "@aggTrade";
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
        /// Create a new instance of BinanceSocketClient
        /// </summary>
        public BinanceSocketClient():this(defaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient using provided credentials. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret associated with the key</param>
        public BinanceSocketClient(BinanceSocketClientOptions options): base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            Configure(options);
        }
        #endregion 

        #region methods

        public void SetDefaultOptions(BinanceSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKline> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(baseWebsocketAddress + symbol + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)));
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamKline>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started kline stream for {symbol}: {interval}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToDepthStream(string symbol, Action<BinanceStreamDepth> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(baseWebsocketAddress + symbol + DepthStreamEndpoint);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamDepth>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started depth stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToTradesStream(string symbol, Action<BinanceStreamTrade> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(baseWebsocketAddress + symbol + TradesStreamEndpoint);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTrade>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started trade stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToSymbolTicker(string symbol, Action<BinanceStreamTick> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(baseWebsocketAddress + symbol + SymbolTickerStreamEndpoint);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTick>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started symbol ticker stream for {symbol}");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToAllSymbolTicker(Action<BinanceStreamTick[]> onMessage)
        {
            var socketResult = CreateSocket(baseWebsocketAddress + AllSymbolTickerStreamEndpoint);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTick[]>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started all symbol ticker stream");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Subscribes to the depth updates
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string symbol, int levels, Action<BinanceOrderBook> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(baseWebsocketAddress + symbol + PartialBookDepthStreamEndpoint + levels.ToString());
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceOrderBook>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started partial book depth stream");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">The event handler for whenever an account info update is received</param>
        /// <param name="OnOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public CallResult<BinanceStreamSubscription> SubscribeToUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> OnOrderUpdateMessage)
        {
            if (string.IsNullOrEmpty(listenKey))
                return new CallResult<BinanceStreamSubscription>(null, new ArgumentError("ListenKey must be provided"));
            
            return CreateUserStream(listenKey, onAccountInfoMessage, OnOrderUpdateMessage);
        }

        /// <summary>
        /// Unsubscribes from a stream
        /// </summary>
        /// <param name="streamSubscription">The stream subscription received by subscribing</param>
        public void UnsubscribeFromStream(BinanceStreamSubscription streamSubscription)
        {
            lock (sockets)
                sockets.SingleOrDefault(s => s.StreamResult.StreamId == streamSubscription.StreamId)?.Socket.Close();
        }

        /// <summary>
        /// Unsubscribes from all streams
        /// </summary>
        public void UnsubscribeAllStreams()
        {
            lock (sockets)
                sockets.ToList().ForEach(s => s.Socket.Close());
        }

        /// <summary>
        /// Dispose this instance
        /// </summary>
        public override void Dispose()
        {
            lock (sockets)
                sockets.ToList().ForEach(s => s.Socket.Close());
        }

        private CallResult<BinanceStreamSubscription> CreateUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage)
        {
            var socketResult = CreateSocket(baseWebsocketAddress + listenKey);
            if (!socketResult.Success)
                return new CallResult<BinanceStreamSubscription>(null, socketResult.Error);

            socketResult.Data.Socket.OnMessage += (o, s) =>
            {
                if(s.Message.Contains(AccountUpdateEvent))
                    onAccountInfoMessage?.Invoke(JsonConvert.DeserializeObject<BinanceStreamAccountInfo>(s.Message));
                else if (s.Message.Contains(ExecutionUpdateEvent))
                    onOrderUpdateMessage?.Invoke(JsonConvert.DeserializeObject<BinanceStreamOrderUpdate>(s.Message));
            };

            log.Write(LogVerbosity.Debug, "User stream started");
            return new CallResult<BinanceStreamSubscription>(socketResult.Data.StreamResult, null);
        }


        private CallResult<BinanceStream> CreateSocket(string url)
        {
            try
            {
                var socket = SocketFactory.CreateWebsocket(url);
                var socketObject = new BinanceStream() { Socket = socket, StreamResult = new BinanceStreamSubscription() { StreamId = NextStreamId() } };
                socket.SetEnabledSslProtocols(protocols); 

                socket.OnClose += (obj, args) => Socket_OnClose(socketObject, args);
                socket.OnClose += (obj, args) => socketObject.StreamResult.InvokeClosed();

                socket.OnError += (obj, args) => Socket_OnError(socketObject, args);
                socket.OnError += (obj, args) => socketObject.StreamResult.InvokeError(args.Exception);

                socket.OnOpen += (obj, args) => Socket_OnOpen(socketObject, args);
                socket.Connect();
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
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            log.Write(LogVerbosity.Debug, $"Socket opened");
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            log.Write(LogVerbosity.Error, $"Socket error {e.Exception?.Message}");
        }

        private void Socket_OnClose(object sender, EventArgs e)
        {
            log.Write(LogVerbosity.Debug, "Socket closed");
            lock (sockets)
                sockets.Remove((BinanceStream)sender);
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
