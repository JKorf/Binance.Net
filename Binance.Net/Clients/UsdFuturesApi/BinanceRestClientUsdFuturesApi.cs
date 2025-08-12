using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.SharedApis;
using Binance.Net.Converters;
using CryptoExchange.Net.Objects.Errors;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc cref="IBinanceRestClientUsdFuturesApi" />
    internal partial class BinanceRestClientUsdFuturesApi : RestApiClient, IBinanceRestClientUsdFuturesApi
    {
        #region fields 
        /// <inheritdoc />
        public new BinanceRestApiOptions ApiOptions => (BinanceRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new BinanceRestOptions ClientOptions => (BinanceRestOptions)base.ClientOptions;

        internal BinanceFuturesUsdtExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("USD Futures Api");

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Not authorized to execute the request", "-1002"),
                new ErrorInfo(ErrorType.RequestRateLimited, false, "Request rate limit reached", "-1003"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid order combination", "-1014"),
                new ErrorInfo(ErrorType.OrderRateLimited, false, "Order rate limit reached", "-1015"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Operation not supported", "-1020"),
                new ErrorInfo(ErrorType.TimestampInvalid, false, "Request timestamp invalid, check time sync", "-1021"),
                new ErrorInfo(ErrorType.SignatureInvalid, false, "Signature invalid, check your API credentials", "-1022"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Start time bigger than end time filter", "-1023"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid characters in parameter", "-1100"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Too many parameters specified", "-1101"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Mandatory parameter missing or incorrect format", "-1102"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unknown parameter", "-1103"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Unread parameter", "-1104"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Empty parameter", "-1105"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter not required", "-1106"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid asset", "-1108"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid symbol type", "-1110"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid precision", "-1111"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Withdrawal amount must be negative", "-1113"),
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
                new ErrorInfo(ErrorType.InvalidParameter, false, "Asset not supported", "-1126"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Lookup period too big", "-1127"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter combination invalid", "-1128"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter value", "-1130"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid newOrderRespType", "-1136"),

                new ErrorInfo(ErrorType.Unknown, false, "Batch cancel failed", "-2012"),
                new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "-2013"),
                new ErrorInfo(ErrorType.SignatureInvalid, false, "API key format invalid", "-2014"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API-key, IP, or permissions for action", "-2015"),
                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance for order", "-2018"),
                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient margin", "-2019"),
                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Position not sufficient", "-2024"),

                new ErrorInfo(ErrorType.OrderRateLimited, false, "Max open orders reached", "-2025"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order type not supported with reduce only", "-2026"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Max leverage ratio exceeded", "-2027"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Min leverage ratio exceeded", "-2028"),

                new ErrorInfo(ErrorType.PriceInvalid, false, "Price less than 0", "-4001"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "Price greater than max price", "-4002"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity less than 0", "-4003"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity less than min quantity", "-4004"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity greater than max quantity", "-4005"),
                new ErrorInfo(ErrorType.StopParametersInvalid, false, "Stop price less than 0", "-4006"),
                new ErrorInfo(ErrorType.StopParametersInvalid, false, "Stop price greater than max price", "-4007"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "Price not increased by tick size", "-4014"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Client order id invalid length", "-4015"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "Price higher than multiplier cap", "-4016"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity not increased by step size", "-4023"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "Price lower than multiplier floor", "-4024"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid leverage", "-4028"),
                new ErrorInfo(ErrorType.OrderRateLimited, false, "Max stop orders reached", "-4045"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Margin type can't be changed with open orders", "-4047"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Margin type can't be changed with open positions", "-4048"),
                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient cross margin balance", "-4050"),
                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient isolated margin balance", "-4051"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Quantity should be positive", "-4055"),
                new ErrorInfo(ErrorType.SignatureInvalid, false, "Invalid RSA public key", "-4057"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid position side", "-4060"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid position side", "-4061"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Reduce only error", "-4062"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Position side can't be changed with open orders", "-4067"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Position side can't be changed with open positions", "-4068"),
                new ErrorInfo(ErrorType.AllOrdersFailed, false, "Batch orders failed", "-4083"),
                new ErrorInfo(ErrorType.Unauthorized, false, "User only has reduce only permissions", "-4087"),
                new ErrorInfo(ErrorType.Unauthorized, false, "No order placement permissions", "-4088"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Inactive account", "-4109"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid transaction id length", "-4114"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Duplicate transaction id", "-4115"),
                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "-4116"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid activation price", "-4135"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Reduce only must be true for closing position", "-4138"),
                new ErrorInfo(ErrorType.StopParametersInvalid, false, "Take profit or stop order will be triggered immediately", "-4142"),
                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "-4144"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Leverage reduction not allowed with open positions", "-4161"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Order notional value too small", "-4164"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "(Stop)price too low", "-4184"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Cooling off period", "-4192"),
                new ErrorInfo(ErrorType.Unauthorized, false, "KYC level insufficient", "-4202"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Leverage setting not allowed within period", "-4203"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Leverage setting not allowed within period", "-4205"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage setting not allowed in location", "-4206"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage setting not allowed on symbol", "-4209"),
                new ErrorInfo(ErrorType.StopParametersInvalid, false, "Stop price higher than price multiplier cap", "-4210"),
                new ErrorInfo(ErrorType.StopParametersInvalid, false, "Stop price lower than price multiplier floor", "-4211"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Feature not allowed in your region", "-4402"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage not allowed in your region", "-4403"),

                new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "FillOrKill order could not be filled immediately", "-5021"),
                new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "PostOnly order could not be placed", "-5022"),
                new ErrorInfo(ErrorType.InvalidOperation, false, "Symbol not in trading status", "-5024"),
                new ErrorInfo(ErrorType.OrderTypeInvalid, false, "Only limit orders supported", "-5025"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Maximum modify order limit exceeded", "-5026"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Order notional value too small", "-5029"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid price match", "-5037"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid price match type", "-5038")
            ]
        );
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceRestClientUsdFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceRestClientUsdFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceRestClientUsdFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public IBinanceRestClientUsdFuturesApiAgent Agent { get; }
        /// <inheritdoc />
        public string ExchangeName => "Binance";
        #endregion

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BinanceExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        #region constructor/destructor
        internal BinanceRestClientUsdFuturesApi(ILogger logger, HttpClient? httpClient, BinanceRestOptions options)
            : base(logger, httpClient, options.Environment.UsdFuturesRestAddress!, options, options.UsdFuturesOptions)
        {
            Account = new BinanceRestClientUsdFuturesApiAccount(this);
            ExchangeData = new BinanceRestClientUsdFuturesApiExchangeData(logger, this);
            Trading = new BinanceRestClientUsdFuturesApiTrading(logger, this);
            Agent = new BinanceRestClientUsdFuturesApiAgent(this);

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

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result = result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, FuturesOrderType type, CancellationToken ct)
        {
            var outputQuantity = quantity;
            var outputQuoteQuantity = quoteQuantity;
            var outputPrice = price;
            var outputStopPrice = stopPrice;

            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, quoteQuantity, outputPrice, outputStopPrice);

            if (_exchangeInfo == null || _lastExchangeInfoUpdate == null || (DateTime.UtcNow - _lastExchangeInfoUpdate.Value).TotalMinutes > ApiOptions.TradeRulesUpdateInterval.TotalMinutes)
                await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);

            if (_exchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("", "Unable to retrieve trading rules, validation failed");

            var symbolData = _exchangeInfo.Symbols.SingleOrDefault(s => string.Equals(s.Name, symbol, StringComparison.CurrentCultureIgnoreCase));
            if (symbolData == null)
                return BinanceTradeRuleResult.CreateFailed("symbol", $"Trade rules check failed: Symbol {symbol} not found");

            if (!symbolData.OrderTypes.Contains(type))
                return BinanceTradeRuleResult.CreateFailed("orderType", $"Trade rules check failed: {type} order type not allowed for {symbol}");

            if (symbolData.LotSizeFilter != null || symbolData.MarketLotSizeFilter != null && type == FuturesOrderType.Market)
            {
                var minQty = symbolData.LotSizeFilter?.MinQuantity;
                var maxQty = symbolData.LotSizeFilter?.MaxQuantity;
                var stepSize = symbolData.LotSizeFilter?.StepSize;
                if (type == FuturesOrderType.Market && symbolData.MarketLotSizeFilter != null)
                {
                    minQty = symbolData.MarketLotSizeFilter.MinQuantity;
                    if (symbolData.MarketLotSizeFilter.MaxQuantity != 0)
                        maxQty = symbolData.MarketLotSizeFilter.MaxQuantity;

                    if (symbolData.MarketLotSizeFilter.StepSize != 0)
                        stepSize = symbolData.MarketLotSizeFilter.StepSize;
                }

                if (minQty.HasValue && quantity.HasValue)
                {
                    outputQuantity = BinanceHelpers.ClampQuantity(minQty.Value, maxQty!.Value, stepSize!.Value, quantity.Value);
                    if (outputQuantity != quantity.Value)
                    {
                        if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed("quantity", $"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");
                        }

                        _logger.Log(LogLevel.Information, $"Quantity clamped from {quantity} to {outputQuantity}");
                    }
                }
            }

            if (symbolData.MinNotionalFilter != null && outputQuoteQuantity != null)
            {
                if (quoteQuantity < symbolData.MinNotionalFilter.MinNotional)
                {
                    if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        return BinanceTradeRuleResult.CreateFailed(
                            "quoteQuantity",
                            $"Trade rules check failed: MinNotional filter failed. Order value: {quoteQuantity}, minimal order value: {symbolData.MinNotionalFilter.MinNotional}");

                    outputQuoteQuantity = symbolData.MinNotionalFilter.MinNotional;
                    _logger.Log(LogLevel.Information, $"QuoteQuantity adjusted from {quoteQuantity} to {outputQuoteQuantity} based on min notional filter");
                }
            }

            if (price == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, null, outputStopPrice);

            if (symbolData.PriceFilter != null)
            {
                if (symbolData.PriceFilter.MaxPrice != 0 && symbolData.PriceFilter.MinPrice != 0)
                {
                    outputPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice, symbolData.PriceFilter.MaxPrice, price.Value);
                    if (outputPrice != price)
                    {
                        if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed("price", $"Trade rules check failed: Price filter max/min failed. Original price: {price}, Closest allowed: {outputPrice}");

                        _logger.Log(LogLevel.Information, $"price clamped from {price} to {outputPrice}");
                    }

                    if (stopPrice != null)
                    {
                        outputStopPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice,
                            symbolData.PriceFilter.MaxPrice, stopPrice.Value);
                        if (outputStopPrice != stopPrice)
                        {
                            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                                return BinanceTradeRuleResult.CreateFailed(
                                    "stopPrice",
                                    $"Trade rules check failed: Stop price filter max/min failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");

                            _logger.Log(LogLevel.Information,
                                $"Stop price clamped from {stopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }

                if (symbolData.PriceFilter.TickSize != 0)
                {
                    var beforePrice = outputPrice;
                    outputPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, price.Value);
                    if (outputPrice != beforePrice)
                    {
                        if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed("price", $"Trade rules check failed: Price filter tick failed. Original price: {price}, Closest allowed: {outputPrice}");

                        _logger.Log(LogLevel.Information, $"price rounded from {beforePrice} to {outputPrice}");
                    }

                    if (stopPrice != null)
                    {
                        var beforeStopPrice = outputStopPrice;
                        outputStopPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, stopPrice.Value);
                        if (outputStopPrice != beforeStopPrice)
                        {
                            if (ApiOptions.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                                return BinanceTradeRuleResult.CreateFailed(
                                    "stopPrice", 
                                    $"Trade rules check failed: Stop price filter tick failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");

                            _logger.Log(LogLevel.Information,
                                $"Stop price floored from {beforeStopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, outputPrice, outputStopPrice);
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

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        public IBinanceRestClientUsdFuturesApiShared SharedClient => this;

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
