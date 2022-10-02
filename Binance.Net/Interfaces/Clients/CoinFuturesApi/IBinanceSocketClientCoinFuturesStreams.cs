using Binance.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot.Socket;

namespace Binance.Net.Interfaces.Clients.CoinFuturesApi
{
    /// <summary>
    /// Binance Coin futures streams
    /// </summary>
    public interface IBinanceSocketClientCoinFuturesStreams : IDisposable
    {
        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#aggregate-trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the aggregated trades update stream for the provided symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#aggregate-trade-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbol and intervals
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the candlestick update stream for the provided symbols and intervals
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="intervals">The intervals of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a specific symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-mini-ticker-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for a list of symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-mini-ticker-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to mini ticker updates stream for all symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#all-market-mini-tickers-stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for a specific symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinance24HPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to ticker updates stream for all symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#all-market-tickers-streams" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinance24HPrice>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to all book ticker update streams
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#all-book-tickers-stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllBookTickerUpdatesAsync(Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the book ticker update stream for the provided symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamBookPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to specific symbol forced liquidations stream
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#liquidation-order-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to list of symbol forced liquidations stream
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#liquidation-order-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to all forced liquidations stream
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#all-market-liquidation-order-streams" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAllLiquidationUpdatesAsync(Action<DataEvent<BinanceFuturesStreamLiquidation>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update</param>
        /// <param name="updateInterval">Update interval in milliseconds</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth updates for the provided symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols to subscribe on</param>
        /// <param name="levels">The amount of entries to be returned in the update of each symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 500. Defaults to 250</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the order book updates for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#diff-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the depth update stream for the provided symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#diff-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 0 or 100, 500 or 1000, depending on endpoint</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IBinanceFuturesEventOrderBook>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the BinanceClient.Futures.UserStreams.StartUserStream method should be called.
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#user-data-streams" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the StartUserStream method</param>
        /// <param name="onLeverageUpdate">The event handler for leverage changed update</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountUpdate">The event handler for whenever an account update is received</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<BinanceFuturesStreamConfigUpdate>>? onLeverageUpdate,
            Action<DataEvent<BinanceFuturesStreamMarginUpdate>>? onMarginUpdate,
            Action<DataEvent<BinanceFuturesStreamAccountUpdate>>? onAccountUpdate,
            Action<DataEvent<BinanceFuturesStreamOrderUpdate>>? onOrderUpdate,
            Action<DataEvent<BinanceStreamEvent>> onListenKeyExpired,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Index price update stream for a single pair
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#index-price-stream" /></para>
        /// </summary>
        /// <param name="pair">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamIndexPrice>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Index price update stream for a list of pairs
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#index-price-stream" /></para>
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesStreamIndexPrice>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#mark-price-stream" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#mark-price-stream" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<IEnumerable<BinanceFuturesCoinStreamMarkPrice>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#continuous-contract-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pair
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#index-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pairs
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#index-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbol
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbols
        /// <para><a href="https://binance-docs.github.io/apidocs/delivery/en/#mark-price-kline-candlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<BinanceStreamIndexKlineData>> onMessage, CancellationToken ct = default);
    }
}
