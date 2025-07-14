using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using Binance.Net.Converters;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBinanceRestClientSpotApi" />
    internal partial class BinanceRestClientSpotApi : RestApiClient, IBinanceRestClientSpotApi
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        #region fields 
        /// <inheritdoc />
        public new BinanceRestApiOptions ApiOptions => (BinanceRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new BinanceRestOptions ClientOptions => (BinanceRestOptions)base.ClientOptions;


        internal BinanceExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public IBinanceRestClientSpotApiAgent Agent { get; }
        /// <inheritdoc />
        public string ExchangeName => "Binance";
        #endregion


        #region constructor/destructor
        internal BinanceRestClientSpotApi(ILogger logger, HttpClient? httpClient, BinanceRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            Account = new BinanceRestClientSpotApiAccount(this);
            ExchangeData = new BinanceRestClientSpotApiExchangeData(logger, this);
            Trading = new BinanceRestClientSpotApiTrading(logger, this);
            Agent = new BinanceRestClientSpotApiAgent(this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
            ArraySerialization = ArrayParametersSerialization.MultipleValues;
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(BinanceExchange._serializerContext));
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BinanceExchange._serializerContext));

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        #region helpers

        internal async Task<WebCallResult<BinancePlacedOrder>> PlaceOrderInternal(string path,
            IRateLimitGate gate,
            string symbol,
            Enums.OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? autoRepayAtCancel = null,
            int? receiveWindow = null,
            int weight = 1,
            CancellationToken ct = default)
        {
            if (quoteQuantity != null && type != SpotOrderType.Market)
                throw new ArgumentException("quoteQuantity is only valid for market orders");

            if (quantity == null && quoteQuantity == null || quantity != null && quoteQuantity != null)
                throw new ArgumentException("1 of either should be specified, quantity or quoteOrderQuantity");

            var rulesCheck = await CheckTradeRules(symbol, quantity, quoteQuantity, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinancePlacedOrder>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;
            quoteQuantity = rulesCheck.QuoteQuantity;

            var clientOrderId = LibraryHelpers.ApplyBrokerId(newClientOrderId, BinanceExchange.ClientOrderIdSpot, 36, ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("sideEffectType", sideEffectType);
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalEnum("newOrderRespType", orderResponseType);
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.AddOptionalParameter("strategyId", strategyId);
            parameters.AddOptionalParameter("strategyType", strategyType);
            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("autoRepayAtCancel", autoRepayAtCancel);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, path, gate, 1, true);
            return await SendAsync<BinancePlacedOrder>(request, parameters, ct, weight: weight).ConfigureAwait(false);
        }

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, SpotOrderType? type, CancellationToken ct)
        {
            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(quantity, quoteQuantity, price, stopPrice);

            if (_exchangeInfo == null || _lastExchangeInfoUpdate == null || (DateTime.UtcNow - _lastExchangeInfoUpdate.Value).TotalMinutes > ApiOptions.TradeRulesUpdateInterval.TotalMinutes)
                await ExchangeData.GetExchangeInfoAsync(ct: ct).ConfigureAwait(false);

            if (_exchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");

            return BinanceHelpers.ValidateTradeRules(_logger, ApiOptions.TradeRulesBehaviour, _exchangeInfo, symbol, quantity, quoteQuantity, price, stopPrice, type);
        }

        internal async Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                _timeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }

        #endregion

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        public IBinanceRestClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(null, "Unknown request error", exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new ServerError(null, "Unknown request error", exception: exception);

            if (code == null)
                return new ServerError(null, msg, exception);

            return new ServerError(code.Value, msg, exception);
        }

        /// <inheritdoc />
        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            var error = GetRateLimitError(accessor);
            var retryAfterHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("Retry-After", StringComparison.InvariantCultureIgnoreCase));
            if (retryAfterHeader.Value?.Any() != true)
                return error;

            var value = retryAfterHeader.Value.First();
            if (!int.TryParse(value, out var seconds))
                return error;

            if (seconds == 0)
            {
                var now = DateTime.UtcNow;
                seconds = (int)(new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Utc).AddMinutes(1) - now).TotalSeconds + 1;
            }

            error.RetryAfter = DateTime.UtcNow.AddSeconds(seconds);
            return error;
        }

        private BinanceRateLimitError GetRateLimitError(IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new BinanceRateLimitError(accessor.GetOriginalString());

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new BinanceRateLimitError(accessor.GetOriginalString());

            if (code == null)
                return new BinanceRateLimitError(msg);

            return new BinanceRateLimitError(code.Value, msg, null);
        }
    }
}
