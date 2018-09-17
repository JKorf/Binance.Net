using System;
using System.Threading.Tasks;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiter;

namespace Binance.Net.Interfaces
{
    public interface IBinanceSocketClient
    {
        IWebsocketFactory SocketFactory { get; set; }
        IRequestFactory RequestFactory { get; set; }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        void SetApiCredentials(string apiKey, string apiSecret);

        /// <summary>
        /// Checks the connection to the server and returns how long connecting took
        /// </summary>
        /// <returns>Time to connect in miliseconds</returns>
        Task<CallResult<long>> PingAsync();

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToKlineStream(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToKlineStreamAsync(string symbol, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToKlineStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToKlineStream(string[] symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToKlineStreamAsync(string[] symbols, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToDepthStream(string symbol, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToDepthStreamAsync(string symbol, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToDepthStream(string[] symbol, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToDepthStreamAsync(string[] symbols, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToAggregatedTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToAggregatedTradesStream(string symbol, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToAggregatedTradesStreamAsync(string symbol, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToAggregatedTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToAggregatedTradesStream(string[] symbols, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToAggregatedTradesStreamAsync(string[] symbols, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToTradesStream(string symbol, Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToTradesStreamAsync(string symbol, Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToTradesStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToTradesStream(string[] symbols, Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Subscribes to the trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToTradesStreamAsync(string[] symbols, Action<BinanceStreamTrade> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceSocketClient.SubscribeToSymbolTickerAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToSymbolTicker(string symbol, Action<BinanceStreamTick> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToSymbolTickerAsync(string symbol, Action<BinanceStreamTick> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceSocketClient.SubscribeToAllSymbolTickerAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToAllSymbolTicker(Action<BinanceStreamTick[]> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToAllSymbolTickerAsync(Action<BinanceStreamTick[]> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="SubscribeToPartialBookDepthStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string symbol, int levels, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToPartialBookDepthStreamAsync(string symbol, int levels, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Synchronized verion of the <see cref="SubscribeToPartialBookDepthStreamAsync(string[], int, Action{BinanceStreamOrderBook})"/> method
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="levels"></param>
        /// <param name="onMessage"></param>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToPartialBookDepthStream(string[] symbols, int levels, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToPartialBookDepthStreamAsync(string[] symbols, int levels, Action<BinanceOrderBook> onMessage);

        /// <summary>
        /// Synchronized version of the <see cref="BinanceSocketClient.SubscribeToUserStreamAsync"/> method
        /// </summary>
        /// <returns></returns>
        CallResult<BinanceStreamSubscription> SubscribeToUserStream(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="BinanceClient.StartUserStream"/> method should be called.
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onAccountInfoMessage">The event handler for whenever an account info update is received</param>
        /// <param name="onOrderUpdateMessage">The event handler for whenever an order status update is received</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is closed and can close this specific stream 
        /// using the <see cref="BinanceSocketClient.UnsubscribeFromStream"/> method</returns>
        Task<CallResult<BinanceStreamSubscription>> SubscribeToUserStreamAsync(string listenKey, Action<BinanceStreamAccountInfo> onAccountInfoMessage, Action<BinanceStreamOrderUpdate> onOrderUpdateMessage);

        /// <summary>
        /// Unsubscribes from a stream
        /// </summary>
        /// <param name="streamSubscription">The stream subscription received by subscribing</param>
        void UnsubscribeFromStream(BinanceStreamSubscription streamSubscription);

        /// <summary>
        /// Unsubscribes from all streams
        /// </summary>
        void UnsubscribeAllStreams();

        /// <summary>
        /// Dispose this instance
        /// </summary>
        void Dispose();

        /// <summary>
        /// Adds a rate limiter to the client. There are 2 choices, the <see cref="RateLimiterTotal"/> and the <see cref="RateLimiterPerEndpoint"/>.
        /// </summary>
        /// <param name="limiter">The limiter to add</param>
        void AddRateLimiter(IRateLimiter limiter);

        /// <summary>
        /// Removes all rate limiters from this client
        /// </summary>
        void RemoveRateLimiters();

        /// <summary>
        /// Ping to see if the server is reachable
        /// </summary>
        /// <returns>The roundtrip time of the ping request</returns>
        CallResult<long> Ping();
    }
}