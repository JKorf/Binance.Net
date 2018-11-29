using Binance.Net.Converters;
using Binance.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
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
    public class BinanceSocketClient : SocketClient//, IBinanceSocketClient
    {
        #region fields
        private static BinanceSocketClientOptions defaultOptions = new BinanceSocketClientOptions();
        private static BinanceSocketClientOptions DefaultOptions => defaultOptions.Copy();

        private string baseCombinedAddress;

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

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToKlineStreamAsync(symbol, interval, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the candlestick update stream for the provided symbol
        ///// </summary>
        ///// <param name="symbol">The symbol</param>
        ///// <param name="interval">The interval of the candlesticks</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineStreamAsync(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => await SubscribeToKlineStreamAsync(new [] { symbol }, interval, onMessage).ConfigureAwait(false);


        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToKlineStream(string[] symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage) => SubscribeToKlineStreamAsync(symbols, interval, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the candlestick update stream for the provided symbols
        ///// </summary>
        ///// <param name="symbols">The symbols</param>
        ///// <param name="interval">The interval of the candlesticks</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineStreamAsync(string[] symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage)
        {
            var handler = new Action<BinanceCombinedStream<BinanceStreamKlineData>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + KlineStreamEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToDepthStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToDepthStream(string symbol, Action<BinanceOrderBook> onMessage) => SubscribeToDepthStreamAsync(symbol, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the depth update stream for the provided symbol
        ///// </summary>
        ///// <param name="symbol">The symbol</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToDepthStreamAsync(string symbol, Action<BinanceOrderBook> onMessage) => await SubscribeToDepthStreamAsync(new [] { symbol }, onMessage).ConfigureAwait(false);

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToDepthStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToDepthStream(string[] symbol, Action<BinanceOrderBook> onMessage) => SubscribeToDepthStreamAsync(symbol, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the depth update stream for the provided symbols
        ///// </summary>
        ///// <param name="symbols">The symbols</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToDepthStreamAsync(string[] symbols, Action<BinanceOrderBook> onMessage)
        {
            var handler = new Action<BinanceCombinedStream<BinanceOrderBook>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + DepthStreamEndpoint).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToAggregatedTradesStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedTradesStream(string symbol, Action<BinanceStreamAggregatedTrade> onMessage) => SubscribeToAggregatedTradesStreamAsync(symbol, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the aggregated trades update stream for the provided symbol
        ///// </summary>
        ///// <param name="symbol">The symbol</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradesStreamAsync(string symbol, Action<BinanceStreamAggregatedTrade> onMessage) => await SubscribeToAggregatedTradesStreamAsync(new [] { symbol }, onMessage).ConfigureAwait(false);

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToAggregatedTradesStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAggregatedTradesStream(string[] symbols, Action<BinanceStreamAggregatedTrade> onMessage) => SubscribeToAggregatedTradesStreamAsync(symbols, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the aggregated trades update stream for the provided symbols
        ///// </summary>
        ///// <param name="symbols">The symbols</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradesStreamAsync(string[] symbols, Action<BinanceStreamAggregatedTrade> onMessage)
        {
            var handler = new Action<BinanceCombinedStream<BinanceStreamAggregatedTrade>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + AggregatedTradesStreamEndpoint).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToTradesStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToTradesStream(string symbol, Action<BinanceStreamTrade> onMessage) => SubscribeToTradesStreamAsync(symbol, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the trades update stream for the provided symbol
        ///// </summary>
        ///// <param name="symbol">The symbol</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradesStreamAsync(string symbol, Action<BinanceStreamTrade> onMessage) => await SubscribeToTradesStreamAsync(new [] { symbol }, onMessage).ConfigureAwait(false);

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToTradesStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToTradesStream(string[] symbols, Action<BinanceStreamTrade> onMessage) => SubscribeToTradesStreamAsync(symbols, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the trades update stream for the provided symbols
        ///// </summary>
        ///// <param name="symbols">The symbols</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradesStreamAsync(string[] symbols, Action<BinanceStreamTrade> onMessage)
        {
            var handler = new Action<BinanceCombinedStream<BinanceStreamTrade>>(data => onMessage(data.Data));
            symbols = symbols.Select(a => a.ToLower() + TradesStreamEndpoint).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToSymbolTickerAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToSymbolTicker(string symbol, Action<BinanceStreamTick> onMessage) => SubscribeToSymbolTickerAsync(symbol, onMessage).Result;

        ///// <summary>
        ///// Subscribes to ticker updates stream for a specific symbol
        ///// </summary>
        ///// <param name="symbol">The symbol to subscribe to</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerAsync(string symbol, Action<BinanceStreamTick> onMessage)
        {
            return await Subscribe(symbol.ToLower() + SymbolTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToAllSymbolTickerAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAllSymbolTicker(Action<BinanceStreamTick[]> onMessage) => SubscribeToAllSymbolTickerAsync(onMessage).Result;

        ///// <summary>
        ///// Subscribes to ticker updates stream for all symbols
        ///// </summary>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolTickerAsync(Action<BinanceStreamTick[]> onMessage)
        {
            return await Subscribe(AllSymbolTickerStreamEndpoint, false, onMessage).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToPartialBookDepthStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToPartialBookDepthStream(string symbol, int levels, Action<BinanceOrderBook> onMessage) => SubscribeToPartialBookDepthStreamAsync(symbol, levels, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the depth updates for the provided symbol
        ///// </summary>
        ///// <param name="symbol">The symbol to subscribe on</param>
        ///// <param name="levels">The amount of entries to be returned in the update</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialBookDepthStreamAsync(string symbol, int levels, Action<BinanceOrderBook> onMessage) => await SubscribeToPartialBookDepthStreamAsync(new [] { symbol }, levels, onMessage).ConfigureAwait(false);

        ///// <summary>
        ///// Synchronized verion of the <see cref="SubscribeToPartialBookDepthStreamAsync(string[], int, Action{BinanceOrderBook})"/> method
        ///// </summary>
        ///// <param name="symbols"></param>
        ///// <param name="levels"></param>
        ///// <param name="onMessage"></param>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToPartialBookDepthStream(string[] symbols, int levels, Action<BinanceOrderBook> onMessage) => SubscribeToPartialBookDepthStreamAsync(symbols, levels, onMessage).Result;

        ///// <summary>
        ///// Subscribes to the depth updates for the provided symbols
        ///// </summary>
        ///// <param name="symbols">The symbols to subscribe on</param>
        ///// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        ///// <param name="onMessage">The event handler for the received data</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialBookDepthStreamAsync(string[] symbols, int levels, Action<BinanceOrderBook> onMessage)
        {
            var handler = new Action<BinanceCombinedStream<BinanceOrderBook>>(data =>
            {
                data.Data.Symbol = data.Stream.Split('@')[0];
                onMessage(data.Data);
            });

            symbols = symbols.Select(a => a.ToLower() + PartialBookDepthStreamEndpoint + levels).ToArray();
            return await Subscribe(String.Join("/", symbols), true, handler).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Synchronized version of the <see cref="SubscribeToUserStreamAsync"/> method
        ///// </summary>
        ///// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage) => SubscribeToUserStreamAsync(listenKey, onAccountInfoMessage, onOrderUpdateMessage).Result;

        ///// <summary>
        ///// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        ///// </summary>
        ///// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        ///// <param name="onAccountInfoMessage">The event handler for whenever an account info update is received</param>
        ///// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        ///// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        ///// using the <see cref="UnsubscribeFromStream(BinanceStreamSubscription)"/> method</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserStreamAsync(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage)
        {
            if (string.IsNullOrEmpty(listenKey))
                return new CallResult<UpdateSubscription>(null, new ArgumentError("ListenKey must be provided"));

            var handler = new Action<string>(data =>
            {
                var token = JToken.Parse(data);
                var evnt = (string)token["e"];
                if (evnt == AccountUpdateEvent)
                {
                    var result = Deserialize<BinanceStreamAccountInfo>(token, false);
                    if (result.Success)
                        onAccountInfoMessage?.Invoke(result.Data);
                    else
                        log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from account stream: " + result.Error);
                }
                else if (evnt == ExecutionUpdateEvent)
                {
                    log.Write(LogVerbosity.Debug, data);
                    var result = Deserialize<BinanceStreamOrderUpdate>(token, false);
                    if (result.Success)
                        onOrderUpdateMessage?.Invoke(result.Data);
                    else
                        log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from order stream: " + result.Error);
                }
                else
                {
                    log.Write(LogVerbosity.Warning, $"Received unknown user data event {evnt}: " + data);
                }
            });

            return await Subscribe(listenKey, false, handler).ConfigureAwait(false);
        }

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(string url, bool combined, Action<T> onData)
        {
            if (combined)
                url = baseCombinedAddress + "stream?streams=" + url;
            else
                url = baseAddress + url;

            var connectResult = await CreateAndConnectSocket(url, onData);
            if (!connectResult.Success)
                return new CallResult<UpdateSubscription>(null, connectResult.Error);

            return new CallResult<UpdateSubscription>(new UpdateSubscription(connectResult.Data), null);
        }
        
        private async Task<CallResult<SocketSubscription>> CreateAndConnectSocket<T>(string url, Action<T> onMessage)
        {
            var socket = CreateSocket(url);
            var subscription = new SocketSubscription(socket);            
            subscription.MessageHandlers.Add(DataHandlerName, (subs, data) => DataHandler(subs, data, onMessage));

            var connectResult = await ConnectSocket(subscription);
            if (!connectResult.Success)
                return new CallResult<SocketSubscription>(null, connectResult.Error);

            socket.ShouldReconnect = true;
            return new CallResult<SocketSubscription>(subscription, null);
        }
        
        private bool DataHandler<T>(SocketSubscription subscription, JToken data, Action<T> handler)
        {
            if (typeof(T) == typeof(string))
            {
                handler((T)Convert.ChangeType(data.ToString(), typeof(T)));
                return true;
            }

            var desResult = Deserialize<T>(data, false);
            if (!desResult.Success)
            {
                log.Write(LogVerbosity.Info, $"Couldn't deserialize data received from stream of type {typeof(T)}: " + desResult.Error);
                return false;
            }

            handler(desResult.Data);
            return true;
        }

        private void Configure(BinanceSocketClientOptions options)
        {
            baseCombinedAddress = options.BaseSocketCombinedAddress;
        }

        protected override bool SocketReconnect(SocketSubscription subscription, TimeSpan disconnectedTime)
        {
            return true;
        }
        #endregion
    }
}
