using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SocketSubClient;
using Binance.Net.Objects;
using Binance.Net.Objects.Blvt;
using Binance.Net.Objects.Spot;
using Binance.Net.Objects.Spot.MarketStream;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;

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
        private readonly string? _baseAddress;

        internal BinanceSocketClientBlvt(Log log, BinanceSocketClient baseClient, BinanceSocketClientOptions options)
        {
            _log = log;
            _baseClient = baseClient;
            _baseAddress = options.BaseAddressUsdtFutures;
        }

        #region Blvt info update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token,
            Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage)
            => SubscribeToBlvtInfoUpdatesAsync(new List<string> { token }, onMessage);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage)
        {
            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + bltvInfoEndpoint).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceBlvtInfoUpdate>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.TokenName)));
            return await Subscribe(tokens, handler).ConfigureAwait(false);
        }

        #endregion

        #region Blvt kline update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token,
            KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage) =>
            SubscribeToBlvtKlineUpdatesAsync(new List<string> { token }, interval, onMessage);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage)
        {
            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + bltvKlineEndpoint + "_" + JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false))).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => onMessage(data.As(data.Data.Data, data.Data.Data.Symbol)));
            return await Subscribe(tokens, handler).ConfigureAwait(false);
        }

        #endregion

        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(IEnumerable<string> topics, Action<DataEvent<T>> onData)
        {
            if (_baseAddress == null)
                throw new ArgumentNullException("BaseAddress", "No API address provided for the futures API, check the client options");

            return await _baseClient.SubscribeInternal(_baseAddress + "stream", topics, onData).ConfigureAwait(false);
        }
    }
}
