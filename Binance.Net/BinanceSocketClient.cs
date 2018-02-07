using Binance.Net.Converters;
using Binance.Net.Events;
using Binance.Net.Implementations;
using Binance.Net.Interfaces;
using Binance.Net.Logging;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Binance.Net.Errors;
using SuperSocket.ClientEngine;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance websocket Api
    /// </summary>
    public class BinanceSocketClient: BinanceAbstractClient, IDisposable
    {
        #region fields
        private const string BaseWebsocketAddress = "wss://stream.binance.com:9443/ws/";

        private readonly List<BinanceStream> sockets = new List<BinanceStream>();

        private int lastStreamId;
        private readonly object streamIdLock = new object();
        private Action<BinanceStreamAccountInfo> accountInfoCallback;
        private Action<BinanceStreamOrderUpdate> orderUpdateCallback;
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
        public BinanceSocketClient()
        {
        }

        /// <summary>
        /// Create a new instance of BinanceSocketClient using provided credentials. Api keys can be managed at https://www.binance.com/userCenter/createApi.html
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret associated with the key</param>
        public BinanceSocketClient(string apiKey, string apiSecret)
        {
            SetApiCredentials(apiKey, apiSecret);
        }

        ~BinanceSocketClient()
        {
            Dispose(false);
        }

        #endregion 

        #region methods
        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream id. This stream id can be used to close this specific stream using the <see cref="UnsubscribeFromStream(int)"/> method</returns>
        public BinanceApiResult<BinanceStreamSubscription> SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKline> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(BaseWebsocketAddress + symbol + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)));
            if (!socketResult.Success)
                return new BinanceApiResult<BinanceStreamSubscription>() {Error = socketResult.Error};

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamKline>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started kline stream for {symbol}: {interval}");
            return new BinanceApiResult<BinanceStreamSubscription>() { Data = socketResult.Data.StreamResult, Success = true };
        }

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream id. This stream id can be used to close this specific stream using the <see cref="UnsubscribeFromStream(int)"/> method</returns>
        public BinanceApiResult<BinanceStreamSubscription> SubscribeToDepthStream(string symbol, Action<BinanceStreamDepth> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(BaseWebsocketAddress + symbol + DepthStreamEndpoint);
            if (!socketResult.Success)
                return new BinanceApiResult<BinanceStreamSubscription>() { Error = socketResult.Error };

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamDepth>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started depth stream for {symbol}");
            return new BinanceApiResult<BinanceStreamSubscription>() { Data = socketResult.Data.StreamResult, Success = true };
        }

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream id. This stream id can be used to close this specific stream using the <see cref="UnsubscribeFromStream(int)"/> method</returns>
        public BinanceApiResult<BinanceStreamSubscription> SubscribeToTradesStream(string symbol, Action<BinanceStreamTrade> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(BaseWebsocketAddress + symbol + TradesStreamEndpoint);
            if (!socketResult.Success)
                return new BinanceApiResult<BinanceStreamSubscription>() { Error = socketResult.Error };

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTrade>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started trade stream for {symbol}");
            return new BinanceApiResult<BinanceStreamSubscription>() { Data = socketResult.Data.StreamResult, Success = true };
        }

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream id. This stream id can be used to close this specific stream using the <see cref="UnsubscribeFromStream(int)"/> method</returns>
        public BinanceApiResult<BinanceStreamSubscription> SubscribeToSymbolTicker(string symbol, Action<BinanceStreamTick> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(BaseWebsocketAddress + symbol + SymbolTickerStreamEndpoint);
            if (!socketResult.Success)
                return new BinanceApiResult<BinanceStreamSubscription>() { Error = socketResult.Error };

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTick>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started symbol ticker stream for {symbol}");
            return new BinanceApiResult<BinanceStreamSubscription>() { Data = socketResult.Data.StreamResult, Success = true };
        }

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream id. This stream id can be used to close this specific stream using the <see cref="UnsubscribeFromStream(int)"/> method</returns>
        public BinanceApiResult<BinanceStreamSubscription> SubscribeToAllSymbolTicker(Action<BinanceStreamTick[]> onMessage)
        {
            var socketResult = CreateSocket(BaseWebsocketAddress + AllSymbolTickerStreamEndpoint);
            if (!socketResult.Success)
                return new BinanceApiResult<BinanceStreamSubscription>() { Error = socketResult.Error };

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceStreamTick[]>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started all symbol ticker stream");
            return new BinanceApiResult<BinanceStreamSubscription>() { Data = socketResult.Data.StreamResult, Success = true };
        }

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream id. This stream id can be used to close this specific stream using the <see cref="UnsubscribeFromStream(int)"/> method</returns>
        public BinanceApiResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string symbol, int levels, Action<BinanceOrderBook> onMessage)
        {
            symbol = symbol.ToLower();
            var socketResult = CreateSocket(BaseWebsocketAddress + symbol + PartialBookDepthStreamEndpoint + levels.ToString());
            if (!socketResult.Success)
                return new BinanceApiResult<BinanceStreamSubscription>() { Error = socketResult.Error };

            socketResult.Data.Socket.OnMessage += (o, s) => onMessage(JsonConvert.DeserializeObject<BinanceOrderBook>(s.Message));

            log.Write(LogVerbosity.Debug, $"Started partial book depth stream");
            return new BinanceApiResult<BinanceStreamSubscription>() { Data = socketResult.Data.StreamResult, Success = true };
        }

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onMessage">The event handler for the data received</param>
        /// <returns>bool indicating success</returns>
        public BinanceApiResult<bool> SubscribeToAccountUpdateStream(string listenKey, Action<BinanceStreamAccountInfo> onMessage)
        {
            if (string.IsNullOrEmpty(listenKey))
                return ThrowErrorMessage<bool>(BinanceErrors.GetError(BinanceErrorKey.NoListenKey));

            accountInfoCallback = onMessage;
            return CreateUserStream(listenKey);
        }

        /// <summary>
        /// Subscribes to the order update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onMessage">The event handler for the data received</param>
        /// <returns>bool indicating success</returns>
        public BinanceApiResult<bool> SubscribeToOrderUpdateStream(string listenKey, Action<BinanceStreamOrderUpdate> onMessage)
        {
            if (string.IsNullOrEmpty(listenKey))
                return ThrowErrorMessage<bool>(BinanceErrors.GetError(BinanceErrorKey.NoListenKey));

            orderUpdateCallback = onMessage;
            return CreateUserStream(listenKey);
        }

        /// <summary>
        /// Unsubscribes from the account update stream
        /// </summary>
        /// <returns></returns>
        public void UnsubscribeFromAccountUpdateStream()
        {
            accountInfoCallback = null;

            // Close the socket if we're not listening for anything
            if (orderUpdateCallback == null)
            {
                lock (sockets)
                    sockets.SingleOrDefault(s => s.UserStream)?.Socket.Close();
            }
        }

        /// <summary>
        /// Unsubscribes from the order update stream
        /// </summary>
        /// <returns></returns>
        public void UnsubscribeFromOrderUpdateStream()
        {
            orderUpdateCallback = null;

            // Close the socket if we're not listening for anything
            if (accountInfoCallback == null)
            {
                lock (sockets)
                    sockets.SingleOrDefault(s => s.UserStream)?.Socket.Close();
            }
        }

        /// <summary>
        /// Unsubscribes from a stream with the provided stream subscription
        /// </summary>
        /// <param name="streamId">SteamId of the stream</param>
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

            orderUpdateCallback = null;
            accountInfoCallback = null;
        }

        /// <summary>
        /// Dispose this instance
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnUserMessage(string data)
        {
            if (data.Contains(AccountUpdateEvent))
                accountInfoCallback?.Invoke(JsonConvert.DeserializeObject<BinanceStreamAccountInfo>(data));
            else if (data.Contains(ExecutionUpdateEvent))
                orderUpdateCallback?.Invoke(JsonConvert.DeserializeObject<BinanceStreamOrderUpdate>(data));
        }

        private BinanceApiResult<bool> CreateUserStream(string listenKey)
        {
            lock (sockets)
                if (sockets.Any(s => s.UserStream))
                    return new BinanceApiResult<bool>() {Data = true, Success = true};

            var socketResult = CreateSocket(BaseWebsocketAddress + listenKey);
            if (!socketResult.Success)
                return new BinanceApiResult<bool>() { Data = false, Success = false, Error = socketResult.Error};

            socketResult.Data.UserStream = true;
            socketResult.Data.Socket.OnMessage += (o, s) => OnUserMessage(s.Message);
            log.Write(LogVerbosity.Debug, "User stream started");
            return new BinanceApiResult<bool>() { Data = true, Success = true};
        }


        private BinanceApiResult<BinanceStream> CreateSocket(string url)
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
                return new BinanceApiResult<BinanceStream>() {Data = socketObject, Success = true};
            }
            catch (Exception e)
            {
                var errorMessage = $"Couldn't open socket stream: {e.Message}";
                log.Write(LogVerbosity.Error, errorMessage);
                return ThrowErrorMessage<BinanceStream>(BinanceErrors.GetError(BinanceErrorKey.CantConnectToServer), errorMessage);
            }
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
        
        private void Dispose(bool disposing)
        {
            lock (sockets)
                sockets.ToList().ForEach(s => s.Socket.Close());
        }
        #endregion
    }
}
