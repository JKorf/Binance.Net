using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SocketSubClient;
using Binance.Net.Objects;
using Binance.Net.Objects.Blvt;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.MarketStream;
using Binance.Net.Objects.Spot.UserStream;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Binance.Net.SocketSubClients
{
    /// <summary>
    /// Spot streams
    /// </summary>
    public class BinanceSocketClientBlvt : IBinanceSocketClientBlvt
    {
        #region fields

        private const string bltvInfoEndpoint = "@tokenNav";
        private const string bltvKlineEndpoint = "@nav_kline";

        #endregion

        private readonly BinanceSocketClient _baseClient;
        private readonly Log _log;
        private readonly string _baseAddress;

        internal BinanceSocketClientBlvt(Log log, BinanceSocketClient baseClient, BinanceSocketClientOptions options)
        {
            _log = log;
            _baseClient = baseClient;
            _baseAddress = options.BaseAddressUsdtFutures;
        }

        #region Blvt info update
        /// <summary>
        /// Subscribes to leveraged token info updates
        /// </summary>
        /// <param name="token">The token to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token,
            Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage)
            => SubscribeToBlvtInfoUpdatesAsync(new List<string> { token }, onMessage);

        /// <summary>
        /// Subscribes to leveraged token info updates
        /// </summary>
        /// <param name="tokens">The tokens to subscribe to</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage)
        {
            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + bltvInfoEndpoint).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceBlvtInfoUpdate>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.TokenName)));
            return await Subscribe(string.Join("/", tokens), true, handler).ConfigureAwait(false);
        }

        #endregion

        #region Blvt kline update
        /// <summary>
        /// Subscribes to leveraged token kline updates
        /// </summary>
        /// <param name="token">The token to subscribe to</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token,
            KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage) =>
            SubscribeToBlvtKlineUpdatesAsync(new List<string> { token }, interval, onMessage);

        /// <summary>
        /// Subscribes to leveraged token kline updates
        /// </summary>
        /// <param name="tokens">The tokens to subscribe to</param>
        /// <param name="interval">The kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage)
        {
            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + bltvKlineEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await Subscribe(string.Join("/", tokens), true, handler).ConfigureAwait(false);
        }

        #endregion

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(string url, bool combined, Action<DataEvent<T>> onData)
        {
            if (combined)
                url = _baseAddress + "stream?streams=" + url;
            else
                url = _baseAddress + "ws/" + url;

            return await _baseClient.SubscribeInternal(url, onData).ConfigureAwait(false);
        }
    }
}
