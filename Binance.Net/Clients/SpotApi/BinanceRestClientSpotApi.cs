using Binance.Net.Clients.MessageHandlers;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;
using System.Net.Http.Headers;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBinanceRestClientSpotApi" />
    internal partial class BinanceRestClientSpotApi : RestApiClient<BinanceEnvironment, BinanceAuthenticationProvider, BinanceCredentials>, IBinanceRestClientSpotApi
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private static readonly ParameterSerializationSettings _serializationSettings = new ParameterSerializationSettings()
        {
            Decimal = DecimalSerialization.String
        };

        #region fields 
        /// <inheritdoc />
        public new BinanceRestApiOptions ApiOptions => (BinanceRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new BinanceRestOptions ClientOptions => (BinanceRestOptions)base.ClientOptions;
        /// <inheritdoc />
        protected override IRestMessageHandler MessageHandler { get; } = new BinanceRestMessageHandler(BinanceErrors.SpotErrors);


        internal BinanceExchangeInfo? _exchangeInfo;
        internal DateTime? _lastExchangeInfoUpdate;

        protected override ErrorMapping ErrorMapping => BinanceErrors.SpotErrors;
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
        protected override BinanceAuthenticationProvider CreateAuthenticationProvider(BinanceCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

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
            PegPriceType? pegPriceType = null,
            int? pegOffsetValue = null,
            PegOffsetType? pegOffsetType = null,
            int? receiveWindow = null,
            int weight = 1,
            IDictionary<string, object>? additionalParameters = null,
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

            var clientOrderId = LibraryHelpers.ApplyBrokerId(
                newClientOrderId, 
                LibraryHelpers.GetClientReference(() => ClientOptions.BrokerId, Exchange, "Spot"),
                36,
                ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(_serializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity);
            parameters.Add("quoteOrderQty", quoteQuantity);
            parameters.Add("newClientOrderId", clientOrderId);
            parameters.Add("price", price);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("stopPrice", stopPrice);
            parameters.Add("icebergQty", icebergQty);
            parameters.Add("sideEffectType", sideEffectType);
            parameters.Add("isIsolated", isIsolated);
            parameters.Add("newOrderRespType", orderResponseType);
            parameters.Add("trailingDelta", trailingDelta);
            parameters.Add("strategyId", strategyId);
            parameters.Add("strategyType", strategyType);
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.Add("autoRepayAtCancel", autoRepayAtCancel);
            parameters.Add("recvWindow", receiveWindow ?? ClientOptions.ReceiveWindow.TotalMilliseconds);
            parameters.Add("pegPriceType", pegPriceType);
            parameters.Add("pegOffsetValue", pegOffsetValue);
            parameters.Add("pegOffsetType", pegOffsetType);
            parameters.ApplyOptionalParameters(additionalParameters);

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

        internal async Task<WebCallResult> SendAsync(RequestDefinition definition, IParameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, IParameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, IParameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        #endregion

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        public IBinanceRestClientSpotApiShared SharedClient => this;
    }
}
