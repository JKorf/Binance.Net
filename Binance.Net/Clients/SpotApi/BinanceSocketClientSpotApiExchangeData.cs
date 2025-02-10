using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
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
            var parameters = new Dictionary<string, object>();
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
            var parameters = new Dictionary<string, object>();
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
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceRecentTradeQuote>>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<IEnumerable<BinanceRecentTradeQuote>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.recent", parameters, weight: 25, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceRecentTradeQuote>>>> GetTradeHistoryAsync(string symbol, long? fromId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("fromId", fromId);
            return await _client.QueryAsync<IEnumerable<BinanceRecentTradeQuote>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.historical", parameters, false, weight: 25, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Aggregated Trades

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceStreamAggregatedTrade>>>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("fromId", fromId);
            return await _client.QueryAsync<IEnumerable<BinanceStreamAggregatedTrade>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"trades.aggregate", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceSpotKline>>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("interval", EnumConverter.GetString(interval));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _client.QueryAsync<IEnumerable<BinanceSpotKline>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"klines", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get UI Klines

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceSpotKline>>>> GetUIKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("interval", EnumConverter.GetString(interval));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            return await _client.QueryAsync<IEnumerable<BinanceSpotKline>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"uiKlines", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Average Price

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceAveragePrice>>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _client.QueryAsync<BinanceAveragePrice>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"avgPrice", parameters, false, weight: 2, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<Binance24HPrice>>>> GetTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            var symbolCount = symbols?.Count();
            int weight = symbolCount == null || symbolCount > 100 ? 80 : symbolCount <= 20 ? 2 : 40;
            return await _client.QueryAsync<IEnumerable<Binance24HPrice>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker.24hr", parameters, false, weight: weight, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Rolling Window Tickers

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceRollingWindowTick>>>> GetRollingWindowTickersAsync(IEnumerable<string> symbols, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            var symbolCount = symbols.Count();
            int weight = Math.Min(symbolCount * 4, 200);
            return await _client.QueryAsync<IEnumerable<BinanceRollingWindowTick>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker", parameters, false, weight: weight, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceBookPrice>>>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbols", symbols);
            return await _client.QueryAsync<IEnumerable<BinanceBookPrice>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"ticker.book", parameters, false, weight: 4, ct: ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTrade>>>(data => 
            onMessage(data.As(data.Data.Data)
            .WithStreamId(data.Data.Stream)
            .WithSymbol(data.Data.Data.Symbol)
            .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@trade").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamAggregatedTrade>>>(data => 
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@aggTrade")
                .ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data => 
                onMessage(data.As<IBinanceStreamKlineData>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.SelectMany(a =>
                intervals.Select(i =>
                    a.ToLower(CultureInfo.InvariantCulture) + "@kline" + "_" +
                    EnumConverter.GetString(i))).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamMiniTick>>>(data => 
                onMessage(data.As<IBinanceMiniTick>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@miniTicker")
                .ToArray();

            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Mini Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllMiniTickerUpdatesAsync(
            Action<DataEvent<IEnumerable<IBinanceMiniTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamMiniTick>>>>(data => 
                onMessage(data.As<IEnumerable<IBinanceMiniTick>>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { "!miniTicker@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Market Rolling Window Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToRollingWindowTickerUpdatesAsync(string symbol, TimeSpan windowSize,
            Action<DataEvent<BinanceStreamRollingWindowTick>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamRollingWindowTick>>>(data => 
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { $"{symbol.ToLowerInvariant()}@ticker_{windowString}" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Rolling Window Tickers Stream

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllRollingWindowTickerUpdatesAsync(TimeSpan windowSize,
            Action<DataEvent<IEnumerable<BinanceStreamRollingWindowTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamRollingWindowTick>>>>(data => 
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            var windowString = windowSize < TimeSpan.FromDays(1) ? windowSize.TotalHours + "h" : windowSize.TotalDays + "d";
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { $"!ticker_{windowString}@arr" }, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamBookPrice>>>(data => 
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@bookTicker").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceOrderBook>>>(data =>
            {
                data.Data.Data.Symbol = data.Data.Stream.Split('@')[0];
                onMessage(data.As<IBinanceOrderBook>(data.Data.Data)
                    .WithStreamId(data.Data.Stream)
                    .WithSymbol(data.Data.Data.Symbol));
            });

            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceEventOrderBook>>>(data => 
                onMessage(data.As<IBinanceEventOrderBook>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a =>
                a.ToLower(CultureInfo.InvariantCulture) + "@depth" +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Individual Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default) => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IBinanceTick>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamTick>>>(data => 
                onMessage(data.As<IBinanceTick>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@ticker").ToArray();
            return await _client.SubscribeAsync(_client.BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Market Tickers Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTickerUpdatesAsync(Action<DataEvent<IEnumerable<IBinanceTick>>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<BinanceCombinedStream<IEnumerable<BinanceStreamTick>>>>(data => 
                onMessage(data.As<IEnumerable<IBinanceTick>>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await _client.SubscribeAsync(_client.BaseAddress, new[] { "!ticker@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Blvt info update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(string token,
            Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToBlvtInfoUpdatesAsync(new List<string> { token }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtInfoUpdatesAsync(IEnumerable<string> tokens, Action<DataEvent<BinanceBlvtInfoUpdate>> onMessage, CancellationToken ct = default)
        {
            var address = _client.ClientOptions.Environment.BlvtSocketAddress ?? throw new Exception("No url found for Blvt stream, check the `BlvtSocketAddress` in the client environment");

            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + "@tokenNav").ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceBlvtInfoUpdate>>>(data => 
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.TokenName)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            return await _client.SubscribeAsync(address.AppendPath("lvt-p"), tokens, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Blvt kline update
        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(string token,
            KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default) =>
            SubscribeToBlvtKlineUpdatesAsync(new List<string> { token }, interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBlvtKlineUpdatesAsync(IEnumerable<string> tokens, KlineInterval interval, Action<DataEvent<BinanceStreamKlineData>> onMessage, CancellationToken ct = default)
        {
            var address = _client.ClientOptions.Environment.BlvtSocketAddress ?? throw new Exception("No url found for Blvt stream, check the `BlvtSocketAddress` in the client environment");

            tokens = tokens.Select(a => a.ToUpper(CultureInfo.InvariantCulture) + "@nav_kline" + "_" + EnumConverter.GetString(interval)).ToArray();
            var handler = new Action<DataEvent<BinanceCombinedStream<BinanceStreamKlineData>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            return await _client.SubscribeAsync(address.AppendPath("lvt-p"), tokens, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}
