﻿using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Binance USD futures exchange data websocket API
    /// </summary>
    public interface IBinanceSocketClientUsdFuturesApiExchangeData
    {
        /// <summary>
        /// Gets the order book for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/market-data/websocket-api" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the order book for, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<CallResult<BinanceResponse<BinanceFuturesOrderBook>>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the price of a symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/market-data/websocket-api/Symbol-Price-Ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        Task<CallResult<BinanceResponse<BinancePrice>>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the price of all symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/market-data/websocket-api/Symbol-Price-Ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Price of symbol</returns>
        Task<CallResult<BinanceResponse<BinancePrice[]>>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for a symbol.
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/market-data/websocket-api/Symbol-Order-Book-Ticker" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to get book price for, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<CallResult<BinanceResponse<BinanceBookPrice>>> GetBookPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the best price/quantity on the order book for all symbols.
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/market-data/websocket-api/Symbol-Order-Book-Ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of book prices</returns>
        Task<CallResult<BinanceResponse<BinanceBookPrice[]>>> GetBookPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Aggregate-Trade-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to individual trade update. NOTE: This endpoint stream isn't document and therefor might be changed or removed without prior notice
        /// </summary>
        /// <param name="symbol">Symbol to subscribe, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="filterOutNonTradeUpdates">Filter out any update which isn't a trade. Occasionally different updates (like INSURANCE_FUND updates) will occur on this stream. By default these are ignored</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamTrade>> onMessage, bool filterOutNonTradeUpdates = true, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to individual trade update. NOTE: This endpoint stream isn't document and therefor might be changed or removed without prior notice
        /// </summary>
        /// <param name="symbols">Symbols to subscribe, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="filterOutNonTradeUpdates">Filter out any update which isn't a trade. Occasionally different updates (like INSURANCE_FUND updates) will occur on this stream. By default these are ignored</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamTrade>> onMessage, bool filterOutNonTradeUpdates = true, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Aggregate-Trade-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Mark-Price-Stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Mark-Price-Stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="premiumIndex">Whether you want to subscribe to premium index k-lines</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, bool premiumIndex = false, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol and intervals
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="premiumIndex">Whether you want to subscribe to premium index k-lines</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, bool premiumIndex = false, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="premiumIndex">Whether you want to subscribe to premium index k-lines</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, bool premiumIndex = false, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols and intervals
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Kline-Candlestick-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="premiumIndex">Whether you want to subscribe to premium index k-lines</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, bool premiumIndex = false, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Continuous-Contract-Kline-Candlestick-Streams" /></para>
        /// </summary>
        /// <param name="pair">The pair, for example `ETHUSDT`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamContinuousKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Continuous-Contract-Kline-Candlestick-Streams" /></para>
        /// </summary>
        /// <param name="pairs">The pairs, for example `ETHUSDT`</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamContinuousKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Individual-Symbol-Mini-Ticker-Stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Individual-Symbol-Mini-Ticker-Stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/All-Market-Mini-Tickers-Stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IBinanceMiniTick[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Individual-Symbol-Ticker-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Individual-Symbol-Ticker-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/All-Market-Tickers-Streams" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IBinance24HPrice[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/All-Book-Tickers-Stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Individual-Symbol-Book-Ticker-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Individual-Symbol-Book-Ticker-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Liquidation-Order-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Liquidation-Order-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/All-Market-Liquidation-Order-Streams" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Partial-Book-Depth-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on, for example `ETHUSDT`</param>
        /// <param name="levels">The amount of entries to be returned in the update, 5, 10 or 20</param>
        /// <param name="updateInterval">Update interval in milliseconds, 100, 250 or 500</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Partial-Book-Depth-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on, for example `ETHUSDT`</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Diff-Book-Depth-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Diff-Book-Depth-Streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols, for example `ETHUSDT`</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to contract/symbol updates
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Contract-Info-Stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(Action<DataEvent<BinanceFuturesStreamSymbolUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a all symbols
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Mark-Price-Stream-for-All-market" /></para>
        /// </summary>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMarkPriceUpdatesAsync(int? updateInterval, Action<DataEvent<BinanceFuturesUsdtStreamMarkPrice[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to composite index updates stream for a symbol
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Composite-Index-Symbol-Information-Streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToCompositeIndexUpdatesAsync(string symbol,
            Action<DataEvent<BinanceFuturesStreamCompositeIndex>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to asset index updates stream
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Multi-Assets-Mode-Asset-Index" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAssetIndexUpdatesAsync(Action<DataEvent<BinanceFuturesStreamAssetIndexUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to asset index updates for a single
        /// <para><a href="https://developers.binance.com/docs/derivatives/usds-margined-futures/websocket-market-streams/Multi-Assets-Mode-Asset-Index" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAssetIndexUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamAssetIndexUpdate>> onMessage, CancellationToken ct = default);
    }
}
