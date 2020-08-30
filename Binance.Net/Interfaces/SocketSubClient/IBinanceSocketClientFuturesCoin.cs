using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Futures.MarketStream;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Interfaces.SocketSubClient
{
    /// <summary>
    /// COIN-M futures streams
    /// </summary>
    public interface IBinanceSocketClientFuturesCoin: IBinanceSocketClientFutures
    {
        /// <summary>
        /// Subscribes to the Index price update stream for a single pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(string pair, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Index price update stream for a single pair
        /// </summary>
        /// <param name="pair">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string pair, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Index price update stream for a list of pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToIndexPriceUpdates(IEnumerable<string> pairs, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Index price update stream for a list of pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> pairs, int? updateInterval, Action<IEnumerable<BinanceFuturesStreamIndexPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(string symbol, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a single symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToMarkPriceUpdates(IEnumerable<string> symbols, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage);

        /// <summary>
        /// Subscribes to the Mark price update stream for a list of symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 1000 or 3000. Defaults to 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<IEnumerable<BinanceFuturesCoinStreamMarkPrice>> onMessage);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToContinuousContractKlineUpdates(string pair, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(string pair, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToContinuousContractKlineUpdates(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the continuous contract candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="contractType">The contract type</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToContinuousContractKlineUpdatesAsync(IEnumerable<string> pairs, ContractType contractType, KlineInterval interval, Action<BinanceStreamKlineData> onMessage);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToIndexKlineUpdates(string pair, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pair
        /// </summary>
        /// <param name="pair">The pair</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string pair, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToIndexKlineUpdates(IEnumerable<string> pairs, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the index candlestick update stream for the provided pairs
        /// </summary>
        /// <param name="pairs">The pairs</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(IEnumerable<string> pairs, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToMarkPriceKlineUpdates(string symbol, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string symbol, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        CallResult<UpdateSubscription> SubscribeToMarkPriceKlineUpdates(IEnumerable<string> symbols, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);

        /// <summary>
        /// Subscribes to the mark price candlestick update stream for the provided symbols
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">The interval of the candlesticks</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<BinanceStreamIndexKlineData> onMessage);
    }
}