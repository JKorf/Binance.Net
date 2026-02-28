using Binance.Net.Enums;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Coin futures exchange data websocket API
    /// </summary>
    public interface IBinanceSocketClientCoinFuturesApiExchangeData
    {
        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Aggregate-Trade-Streams" /><br />
        /// Endpoint:<br />
        /// @aggTrade
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Aggregate-Trade-Streams" /><br />
        /// Endpoint:<br />
        /// @aggTrade
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to the aggregated trades update stream in high-performance mode<br />
        /// More info about high-performance mode can be found here: <a href="https://cryptoexchange.jkorf.dev/client-libs/features/performance" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Aggregate-Trade-Streams" /><br />
        /// Endpoint:<br />
        /// @aggTrade
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected</returns>
        Task<CallResult<HighPerfUpdateSubscription>> SubscribeToAggregatedTradeUpdatesPerfAsync(IEnumerable<string> symbols, Action<BinanceStreamMinimalTrade> onMessage, CancellationToken ct);

        /// <summary>
        /// Subscribe to individual trade update. NOTE: This endpoint stream isn't document and therefor might be changed or removed without prior notice
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to individual trade update. NOTE: This endpoint stream isn't document and therefor might be changed or removed without prior notice
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to the trades updates stream in high-performance mode. NOTE: This endpoint stream isn't document and therefor might be changed or removed without prior notice<br />
        /// More info about high-performance mode can be found here: <a href="https://cryptoexchange.jkorf.dev/client-libs/features/performance" />
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected</returns>
        Task<CallResult<HighPerfUpdateSubscription>> SubscribeToTradeUpdatesPerfAsync(IEnumerable<string> symbols, Action<BinanceStreamMinimalTrade> onMessage, CancellationToken ct);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @kline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol and intervals
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @kline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @kline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols and intervals
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @kline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Mini-Ticker-Stream" /><br />
        /// Endpoint:<br />
        /// @miniTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Mini-Ticker-Stream" /><br />
        /// Endpoint:<br />
        /// @miniTicker
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream in high-performance mode<br />
        /// More info about high-performance mode can be found here: <a href="https://cryptoexchange.jkorf.dev/client-libs/features/performance" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Mini-Ticker-Stream" /><br />
        /// Endpoint:<br />
        /// @miniTicker
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected</returns>
        Task<CallResult<HighPerfUpdateSubscription>> SubscribeToMiniTickerUpdatesPerfAsync(IEnumerable<string> symbols, Action<BinanceStreamMinimalCoinTick> onMessage, CancellationToken ct);

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/All-Market-Mini-Tickers-Stream" /><br />
        /// Endpoint:<br />
        /// !miniTicker@arr
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IBinanceMiniTick[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Ticker-Streams" /><br />
        /// Endpoint:<br />
        /// @ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Ticker-Streams" /><br />
        /// Endpoint:<br />
        /// @ticker
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://binance-docs.github.io/apidocs/delivery/en/#all-market-tickers-streams" /><br />
        /// Endpoint:<br />
        /// !ticker@arr
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IBinance24HPrice[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/All-Market-Tickers-Streams" /><br />
        /// Endpoint:<br />
        /// !bookTicker
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Book-Ticker-Streams" /><br />
        /// Endpoint:<br />
        /// @bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Book-Ticker-Streams" /><br />
        /// Endpoint:<br />
        /// @bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to the book ticker update stream in high-performance mode<br />
        /// More info about high-performance mode can be found here: <a href="https://cryptoexchange.jkorf.dev/client-libs/features/performance" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Individual-Symbol-Book-Ticker-Streams" /><br />
        /// Endpoint:<br />
        /// @bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected</returns>
        Task<CallResult<HighPerfUpdateSubscription>> SubscribeToBookTickerUpdatesPerfAsync(IEnumerable<string> symbols, Action<BinanceStreamBookPrice> onMessage, CancellationToken ct);

        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Liquidation-Order-Streams" /><br />
        /// Endpoint:<br />
        /// @forceOrder
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Liquidation-Order-Streams" /><br />
        /// Endpoint:<br />
        /// @forceOrder
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/All-Market-Liquidation-Order-Streams" /><br />
        /// Endpoint:<br />
        /// !forceOrder@arr
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Partial-Book-Depth-Streams" /><br />
        /// Endpoint:<br />
        /// @depth&lt;levels&gt;
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `BTCUSD_PERP`</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Partial-Book-Depth-Streams" /><br />
        /// Endpoint:<br />
        /// @depth&lt;levels&gt;
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on, for example `BTCUSD_PERP`</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribes to the depth updates stream in high-performance mode<br />
        /// More info about high-performance mode can be found here: <a href="https://cryptoexchange.jkorf.dev/client-libs/features/performance" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Partial-Book-Depth-Streams" /><br />
        /// Endpoint:<br />
        /// @depth&lt;levels&gt;
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected</returns>
        Task<CallResult<HighPerfUpdateSubscription>> SubscribeToPartialOrderBookUpdatesPerfAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<BinanceFuturesStreamMinimalBookUpdate> onMessage, CancellationToken ct);

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Diff-Book-Depth-Streams" /><br />
        /// Endpoint:<br />
        /// @depth
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Diff-Book-Depth-Streams" /><br />
        /// Endpoint:<br />
        /// @depth
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);
        /// <summary>
        /// Subscribe to the depth update stream in high-performance mode<br />
        /// More info about high-performance mode can be found here: <a href="https://cryptoexchange.jkorf.dev/client-libs/features/performance" />
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Diff-Book-Depth-Streams" /><br />
        /// Endpoint:<br />
        /// @depth
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected</returns>
        Task<CallResult<HighPerfUpdateSubscription>> SubscribeToOrderBookUpdatesPerfAsync(IEnumerable<string> symbols, int? updateInterval, Action<BinanceFuturesStreamMinimalBookUpdate> onMessage, CancellationToken ct);

        /// <summary>
        /// Subscribes to the Index price update stream for a single pair
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Index-Price-Stream" /><br />
        /// Endpoint:<br />
        /// @indexPrice
        /// </para>
        /// </summary>
        /// <param name="pair">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<DataEvent<BinanceFuturesStreamIndexPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Index price update stream for a list of pairs
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Index-Price-Stream" /><br />
        /// Endpoint:<br />
        /// @indexPrice
        /// </para>
        /// </summary>
        /// <param name="pairs">The pairs, for example `BTCUSD`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<DataEvent<BinanceFuturesStreamIndexPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Mark-Price-Stream" /><br />
        /// Endpoint:<br />
        /// @markPrice
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<BinanceFuturesCoinStreamMarkPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        ///Subscribe to the Mark price update stream for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Mark-Price-of-All-Symbols-of-a-Pair" /><br />
        /// Endpoint:<br />
        /// !markPrice@arr
        /// </para>
        /// </summary>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(Action<DataEvent<BinanceFuturesCoinStreamMarkPrice[]>> onMessage, int? updateInterval = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Mark-Price-of-All-Symbols-of-a-Pair" /><br />
        /// Endpoint:<br />
        /// @markPrice
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<BinanceFuturesCoinStreamMarkPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Continuous-Contract-Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @continuousKline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="pair">The pair, for example `BTCUSD`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Continuous-Contract-Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @continuousKline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="pairs">The pairs, for example `BTCUSD`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pair
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Index-Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @indexPriceKline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="pair">The pair, for example `BTCUSD`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pairs
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Index-Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @indexPriceKline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="pairs">The pairs, for example `BTCUSD`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Mark-Price-Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @markPriceKline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Mark-Price-Kline-Candlestick-Streams" /><br />
        /// Endpoint:<br />
        /// @markPriceKline_&lt;interval&gt;
        /// </para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `BTCUSD_PERP`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to contract/symbol updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/derivatives/coin-margined-futures/websocket-market-streams/Contract-Info-Stream" /><br />
        /// Endpoint:<br />
        /// !contractInfo
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<BinanceFuturesStreamSymbolUpdate>> onMessage, CancellationToken ct = default);
    }
}
