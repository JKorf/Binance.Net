using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Interfaces.SocketSubClient
{
    /// <summary>
    /// Base interface for socket client
    /// </summary>
    public interface IBinanceSocketClientBase
    {
        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAggregatedTradeUpdates(string symbol, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAggregatedTradeUpdates(IEnumerable<string> symbols, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamAggregatedTrade> onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToKlineUpdates(string symbol, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToKlineUpdates(IEnumerable<string> symbols, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<IBinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToSymbolMiniTickerUpdates(string symbol, Action<IBinanceMiniTick> onMessage);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(string symbol, Action<IBinanceMiniTick> onMessage);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToSymbolMiniTickerUpdates(IEnumerable<string> symbols, Action<IBinanceMiniTick> onMessage);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<IBinanceMiniTick> onMessage);

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAllSymbolMiniTickerUpdates(Action<IEnumerable<IBinanceMiniTick>> onMessage);

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolMiniTickerUpdatesAsync(Action<IEnumerable<IBinanceMiniTick>> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToSymbolTickerUpdates(string symbol, Action<IBinanceTick> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string symbol, Action<IBinanceTick> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToSymbolTickerUpdates(IEnumerable<string> symbols, Action<IBinanceTick> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(IEnumerable<string> symbols, Action<IBinanceTick> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAllSymbolTickerUpdates(Action<IEnumerable<IBinanceTick>> onMessage);

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllSymbolTickerUpdatesAsync(Action<IEnumerable<IBinanceTick>> onMessage);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToBookTickerUpdates(string symbol, Action<BinanceStreamBookPrice> onMessage);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<BinanceStreamBookPrice> onMessage);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToBookTickerUpdates(IEnumerable<string> symbols, Action<BinanceStreamBookPrice> onMessage);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<BinanceStreamBookPrice> onMessage);

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToAllBookTickerUpdates(Action<BinanceStreamBookPrice> onMessage);

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<BinanceStreamBookPrice> onMessage);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToPartialOrderBookUpdates(string symbol, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToPartialOrderBookUpdates(IEnumerable<string> symbols, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(string symbol, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(IEnumerable<string> symbols, int? updateInterval, Action<IBinanceOrderBook> onMessage);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<IBinanceOrderBook> onMessage);

    }
}
