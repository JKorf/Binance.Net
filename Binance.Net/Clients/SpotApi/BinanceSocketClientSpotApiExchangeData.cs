using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.Socket;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Socket;
using CryptoExchange.Net.Objects.Sockets;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceSocketClientSpotApiExchangeData : IBinanceSocketClientSpotApiExchangeData
    {
        private readonly ILogger _logger;
        private readonly BinanceSocketClientSpotApi _client;

        #region constructor/destructor

        internal BinanceSocketClientSpotApiExchangeData(ILogger logger, BinanceSocketClientSpotApi client)
        {
            _client = client;
            _logger = logger;
        }

        #endregion

        #region Queries

        #region Ping

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<object>>> PingAsync(CancellationToken ct = default)
        {
            return await _client.QueryAsync<object>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ping", new Dictionary<string, object>(), ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Server Time

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<DateTime>>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _client.QueryAsync<BinanceCheckTime>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"time", new Dictionary<string, object>(), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsError<BinanceResponse<DateTime>>(result.Error!);

            return result.As(new BinanceResponse<DateTime>
            {
                Ratelimits = result.Data!.Ratelimits!,
                Result = result.Data!.Result!.ServerTime!
            });
        }

        #endregion

        #region Get Exchange Info

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceExchangeInfo>>> GetExchangeInfoAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", symbols);
            var result = await _client.QueryAsync<BinanceExchangeInfo>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"exchangeInfo", parameters, weight: 20, ct: ct).ConfigureAwait(false);
            if (!result)
                return result;

            _client._exchangeInfo = result.Data.Result;
            _client._lastExchangeInfoUpdate = DateTime.UtcNow;
            _logger.Log(LogLevel.Information, "Trade rules updated");
            return result;
        }

        #endregion

        #region Get Orderbook

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceOrderBook>>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            int weight = limit <= 100 ? 5 : limit <= 500 ? 25 : limit <= 1000 ? 50 : 250;
            var result = await _client.QueryAsync<BinanceOrderBook>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"depth", parameters, weight: weight, ct: ct).ConfigureAwait(false);
            if (result)
                result.Data.Result.Symbol = symbol;
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceRecentTradeQuote[]>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<BinanceRecentTradeQuote[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.recent", parameters, weight: 25, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceRecentTradeQuote[]>>> GetTradeHistoryAsync(string symbol, long? fromId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);
            return await _client.QueryAsync<BinanceRecentTradeQuote[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.historical", parameters, false, weight: 25, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Aggregated Trades

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceStreamAggregatedTrade[]>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("fromId", fromId);
            return await _client.QueryAsync<BinanceStreamAggregatedTrade[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.aggregate", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceSpotKline[]>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _client.QueryAsync<BinanceSpotKline[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"klines", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get UI Klines

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceSpotKline[]>>> GetUIKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _client.QueryAsync<BinanceSpotKline[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"uiKlines", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Average Price

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceAveragePrice>>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
            return await _client.QueryAsync<BinanceAveragePrice>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"avgPrice", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<Binance24HPrice[]>>> GetTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", symbols?.ToArray());
            var symbolCount = symbols?.Count();
            int weight = symbolCount == null || symbolCount > 100 ? 80 : symbolCount <= 20 ? 2 : 40;
            return await _client.QueryAsync<Binance24HPrice[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker.24hr", parameters, false, weight: weight, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rolling Window Tickers

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceRollingWindowTick[]>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", symbols.ToArray());
            var symbolCount = symbols.Count();
            int weight = Math.Min(symbolCount * 4, 200);
            return await _client.QueryAsync<BinanceRollingWindowTick[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker", parameters, false, weight: weight, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceBookPrice[]>>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbols", symbols?.ToArray());
            return await _client.QueryAsync<BinanceBookPrice[]>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker.book", parameters, false, weight: 4, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams

        #region Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default) =>
            await SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamTrade>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BinanceStreamTrade>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });

            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@trade").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "trade", symbols, handler, ct).ConfigureAwait(false);
        }

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToTradeUpdatesPerfAsync(IEnumerable<string> symbols, Func<BinanceStreamTrade, ValueTask> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@trade"));
            return _client.SubscribeHighPerfAsync<BinanceCombinedStream<BinanceStreamTrade>, BinanceStreamTrade>(_client.BaseAddress, topics.ToArray(), callback, ct: ct);
        }

        #endregion

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default) =>
            await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAggregatedTrade>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamAggregatedTrade>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BinanceStreamAggregatedTrade>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@aggTrade")
                .ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "aggTrade", symbols, handler, ct).ConfigureAwait(false);
        }

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToAggregatedTradeUpdatesPerfAsync(IEnumerable<string> symbols, Func<BinanceStreamAggregatedTrade, ValueTask> callback, CancellationToken ct)
        {
            var topics = symbols.Select(x => x.ToLowerInvariant() + "@aggTrade");
            return _client.SubscribeHighPerfAsync<BinanceCombinedStream<BinanceStreamAggregatedTrade>, BinanceStreamAggregatedTrade>(_client.BaseAddress, topics.ToArray(), callback, ct);
        }

        #endregion

        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol,
            IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols,
            KlineInterval interval, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols,
            IEnumerable<KlineInterval> intervals, Action<DataEvent<IBinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamKlineData>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<IBinanceStreamKlineData>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            symbols = symbols.SelectMany(a =>
                intervals.Select(i =>
                    a.ToLower(CultureInfo.InvariantCulture) + "@kline" + "_" +
                    EnumConverter.GetString(i))).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "kline", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Mini Ticker Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol,
            Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default) =>
            await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<IBinanceMiniTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamMiniTick>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<IBinanceMiniTick>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@miniTicker")
                .ToArray();

            return await _client.SubscribeAsync(_client.BaseAddress, "24hrMiniTicker", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(
            Action<DataEvent<IBinanceMiniTick[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamMiniTick[]>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<IBinanceMiniTick[]>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithDataTimestamp(data.Data.Max(x => x.EventTime))
                    );
            });
            return await _client.SubscribeAsync(_client.BaseAddress, "24hrMiniTicker", new[] { "!miniTicker@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Market Rolling Window Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToRollingWindowTickerUpdatesAsync(string symbol, TimeSpan windowSize,
            Action<DataEvent<BinanceStreamRollingWindowTick>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamRollingWindowTick>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BinanceStreamRollingWindowTick>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
            return await _client.SubscribeAsync(_client.BaseAddress, windowString + "Ticker", new[] { $"{symbol.ToLowerInvariant()}@ticker_{windowString}" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Rolling Window Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllRollingWindowTickerUpdatesAsync(TimeSpan windowSize,
            Action<DataEvent<BinanceStreamRollingWindowTick[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamRollingWindowTick[]>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BinanceStreamRollingWindowTick[]>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithDataTimestamp(data.Data.Max(x => x.EventTime))
                    );
            });
            var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
            return await _client.SubscribeAsync(_client.BaseAddress, windowString + "Ticker", new[] { $"!ticker_{windowString}@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Book Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default) =>
            await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols,
            Action<DataEvent<BinanceStreamBookPrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamBookPrice>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BinanceStreamBookPrice>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@bookTicker").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "bookTicker", symbols, handler, ct).ConfigureAwait(false);
        }

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToBookTickerUpdatesPerfAsync(IEnumerable<string> symbols, Func<BinanceStreamBookPrice, ValueTask> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@bookTicker"));
            return _client.SubscribeHighPerfAsync<BinanceCombinedStream<BinanceStreamBookPrice>, BinanceStreamBookPrice>(_client.BaseAddress, topics.ToArray(), callback, ct);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol,
            int levels, int? updateInterval, Action<DataEvent<IBinanceOrderBook>> onMessage, CancellationToken ct = default) =>
            await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage, ct)
                .ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(
            IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<IBinanceOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceOrderBook>>((receiveTime, originalData, data) =>
            {
                data.Data.Symbol = data.Stream.Split('@')[0];
                onMessage(
                    new DataEvent<IBinanceOrderBook>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "depthUpdate", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Diff. Depth Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol,
            int? updateInterval, Action<DataEvent<IBinanceEventOrderBook>> onMessage, CancellationToken ct = default) =>
            await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols,
            int? updateInterval, Action<DataEvent<IBinanceEventOrderBook>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceEventOrderBook>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<IBinanceEventOrderBook>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + "@depth" +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "depthUpdate", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamTick>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<IBinanceTick>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@ticker").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, "24hrTicker", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IBinanceTick[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamTick[]>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<IBinanceTick[]>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithDataTimestamp(data.Data.Max(x => x.EventTime))
                    );
            });
            return await _client.SubscribeAsync(_client.BaseAddress, "24hrTicker", new[] { "!ticker@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Average Price Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAveragePriceUpdatesAsync(string symbol,
            Action<DataEvent<BinanceStreamAveragePrice>> onMessage, CancellationToken ct = default) =>
            await SubscribeToAveragePriceUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAveragePriceUpdatesAsync(
            IEnumerable<string> symbols, Action<DataEvent<BinanceStreamAveragePrice>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, BinanceCombinedStream<BinanceStreamAveragePrice>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<BinanceStreamAveragePrice>(data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@avgPrice")
                .ToArray();

            return await _client.SubscribeAsync(_client.BaseAddress, "avgPrice", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion
        #endregion

    }
}
