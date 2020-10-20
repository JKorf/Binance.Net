using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Blvt;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Binance.Net.Interfaces.SocketSubClient
{
    /// <summary>
    /// Leveraged token endpoints
    /// </summary>
    public interface IBinanceSocketClientBlvt
    {
        /// <summary>
        /// Subscribes to leveraged token info updates
        /// </summary>
        /// <param name="token">The token to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token,
            Action<BinanceCombinedStream<BinanceBlvtInfoUpdate>> onMessage);

        /// <summary>
        /// Subscribes to leveraged token info updates
        /// </summary>
        /// <param name="tokens">The tokens to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<BinanceCombinedStream<BinanceBlvtInfoUpdate>> onMessage);

        /// <summary>
        /// Subscribes to leveraged token kline updates
        /// </summary>
        /// <param name="token">The token to subscribe to</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token,
            KlineInterval interval, Action<BinanceCombinedStream<BinanceStreamKlineData>> onMessage);

        /// <summary>
        /// Subscribes to leveraged token kline updates
        /// </summary>
        /// <param name="tokens">The tokens to subscribe to</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<BinanceCombinedStream<BinanceStreamKlineData>> onMessage);
    }
}