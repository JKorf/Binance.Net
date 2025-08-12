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
using CryptoExchange.Net.Objects.Errors;

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

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Not authorized to execute the request", "-1002"),
                new ErrorInfo(ErrorType.RequestRateLimited, false, "Request rate limit reached", "-1003"),

                new ErrorInfo(ErrorType.OrderRateLimited, false, "Order rate limit reached", "-1015"),
                new ErrorInfo(ErrorType.TimestampInvalid, false, "Request timestamp invalid, check time sync", "-1021"),
                new ErrorInfo(ErrorType.SignatureInvalid, false, "Signature invalid, check your API credentials", "-1022"),
                new ErrorInfo(ErrorType.ConnectionRateLimited, false, "Request rate limit reached", "-1034"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid characters in parameter", "-1100"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Too many parameters specified", "-1101"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Mandatory parameter missing or incorrect format", "-1102"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unknown parameter", "-1103"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unread parameter", "-1104"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Empty parameter", "-1105"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter not required", "-1106"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter overflow", "-1108"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid precision", "-1111"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce sent when not required", "-1114"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "TimeInForce has invalid value", "-1115"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid order type", "-1116"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid side", "-1117"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id not sent", "-1118"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id not sent", "-1119"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid interval", "-1120"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid symbol", "-1121"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid symbol status", "-1122"),
                new ErrorInfo(ErrorType.InvalidListenKey, false, "Invalid listen key", "-1125"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Lookup period too big", "-1127"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter combination invalid", "-1128"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter value", "-1130"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid strategy type value", "-1134"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid ticker type value", "-1139"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid cancel restrictions", "-1145"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Duplicate symbols specified", "-1151"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO order type not supported", "-1158"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO buy limit price must be below", "-1165"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO sell limit price must be above", "-1166"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "One of OCO order should be market order", "-1168"),
                new ErrorInfo(ErrorType.SubscriptionRateLimited, false, "One of OCO order should be market", "-1191"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO buy stop loss price must be above", "-1196"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO sell stop loss price must be below", "-1197"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO buy take profit price must be above", "-1198"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OCO sell take profit price must be below", "-1199"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "-2013"),
                new ErrorInfo(ErrorType.SignatureInvalid, false, "API key format invalid", "-2014"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API-key, IP, or permissions for action", "-2015"),
                new ErrorInfo(ErrorType.UnknownOrder, false, "Order archived", "-2026"),
                new ErrorInfo(ErrorType.DuplicateSubscription, false, "User subscription already active", "-2035"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid client order id", "-2039")
            ],
            [
                new ErrorEvaluator("-1013", (code, msg) => {
                    var topic = msg?.Replace("Filter failure: ", "");
                    return topic switch
                    {
                        "NOTIONAL" => new ErrorInfo(ErrorType.QuantityInvalid, false, "Price * quantity is not within range of the minNotional and maxNotional", code),
                        "LOT_SIZE" => new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity is too high, too low, and/or not following the step size rule for the symbol", code),
                        "MIN_NOTIONAL" => new ErrorInfo(ErrorType.QuantityInvalid, false, "Price * quantity is too low to be a valid order for the symbol", code),
                        "MARKET_LOT_SIZE" => new ErrorInfo(ErrorType.QuantityInvalid, false, "MARKET order's quantity is too high, too low, and/or not following the step size rule for the symbol", code),
                        "PERCENT_PRICE" => new ErrorInfo(ErrorType.PriceInvalid, false, "Price is X% too high or X% too low from the average weighted price over the last Y minutes", code),
                        "PRICE_FILTER" => new ErrorInfo(ErrorType.PriceInvalid, false, "Price is too high, too low, and/or not following the tick size rule for the symbol", code),
                        "ICEBERG_PARTS" => new ErrorInfo(ErrorType.InvalidParameter, false, "ICEBERG order would break into too many parts; icebergQty is too small", code),
                        "MAX_POSITION" => new ErrorInfo(ErrorType.Unknown, false, "The account's position has reached the maximum defined limit", code),
                        "MAX_NUM_ORDERS" => new ErrorInfo(ErrorType.OrderRateLimited, false, "Account has too many open orders on the symbol", code),
                        "MAX_NUM_ALGO_ORDERS" => new ErrorInfo(ErrorType.OrderRateLimited, false, "Account has too many open stop loss and/or take profit orders on the symbol", code),
                        "MAX_NUM_ICEBERG_ORDERS" => new ErrorInfo(ErrorType.OrderRateLimited, false, "Account has too many open iceberg orders on the symbol", code),
                        "TRAILING_DELTA" => new ErrorInfo(ErrorType.InvalidParameter, false, "TrailingDelta is not within the defined range of the filter for that order type", code),
                        "EXCHANGE_MAX_NUM_ORDERS" => new ErrorInfo(ErrorType.OrderRateLimited, false, "Account has too many open orders on the exchange", code),
                        "EXCHANGE_MAX_NUM_ALGO_ORDERS" => new ErrorInfo(ErrorType.OrderRateLimited, false, "Account has too many open stop loss and/or take profit orders on the exchange", code),
                        "EXCHANGE_MAX_NUM_ICEBERG_ORDERS" => new ErrorInfo(ErrorType.OrderRateLimited, false, "Account has too many open iceberg orders on the exchange", code),
                        _ => new ErrorInfo(ErrorType.Unknown, false, msg ?? "Unknown", code),
                    };
                }),
                new ErrorEvaluator(["-1010", "-2010", "-2011", "-2038"], (code, msg) => {
                    msg = msg?.TrimEnd(['.']);
                    return msg switch
                    {
                        "Unknown order sent" => new ErrorInfo(ErrorType.UnknownOrder, false, msg, code),
                        "Duplicate order sent" => new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Client order id already in use", code),
                        "Market is closed" => new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol is not currently in trading mode", code),
                        "Account has insufficient balance for requested action" => new ErrorInfo(ErrorType.BalanceInsufficient, false, msg, code),
                        "Order amend is not supported for this symbol" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "This action is disabled on this account" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "This account may not place or cancel orders" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order would trigger immediately" => new ErrorInfo(ErrorType.StopParametersInvalid, false, msg, code),
                        "OCO orders are not supported for this symbol" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order cancel-replace is not supported for this symbol" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "This symbol is not permitted for this account" => new ErrorInfo(ErrorType.Unauthorized, false, msg, code),
                        "This symbol is restricted for this account" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order was not canceled due to cancel restrictions" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        "Order amend (quantity increase) is not supported" => new ErrorInfo(ErrorType.InvalidOperation, false, msg, code),
                        _ => new ErrorInfo(ErrorType.InvalidParameter, false, msg ?? "Unknown error", code)
                    };
                })
            ]
        );
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
                return new WebCallResult<BinancePlacedOrder>(ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
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
                return BinanceTradeRuleResult.CreateFailed("", "Unable to retrieve trading rules, validation failed");

            return BinanceHelpers.ValidateTradeRules(_logger, ApiOptions.TradeRulesBehaviour, _exchangeInfo, symbol, quantity, quoteQuantity, price, stopPrice, type);
        }

        internal async Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.TimestampInvalid && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
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
            if (!result && result.Error!.ErrorType == ErrorType.TimestampInvalid && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
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
                return new ServerError(null, ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new ServerError(null, ErrorInfo.Unknown, exception: exception);

            if (code == null)
                return new ServerError(null, new ErrorInfo(ErrorType.Unknown, false, msg));

            var errorInfo = GetErrorInfo(code.Value, msg);
            return new ServerError(code.Value.ToString(), errorInfo, exception);
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

            return new BinanceRateLimitError(code.Value, msg);
        }
    }
}
