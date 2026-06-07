using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net.Objects.Errors;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BinanceRestClientSpotApiTrading : IBinanceRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        private readonly BinanceRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal BinanceRestClientSpotApiTrading(ILogger logger, BinanceRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Test New Order 

        /// <inheritdoc />
        public async Task<HttpResult<BinanceTestOrderCommission>> PlaceTestOrderAsync(string symbol,
            Enums.OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? computeFeeRates = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (quoteQuantity != null && type != SpotOrderType.Market)
                throw new ArgumentException("quoteQuantity is only valid for market orders");

            if (quantity == null && quoteQuantity == null || quantity != null && quoteQuantity != null)
                throw new ArgumentException("1 of either should be specified, quantity or quoteOrderQuantity");

            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, quoteQuantity, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return HttpResult.Fail<BinanceTestOrderCommission>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;
            quoteQuantity = rulesCheck.QuoteQuantity;

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
            };
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("timeInForce", timeInForce);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("newOrderRespType", orderResponseType);
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.AddOptionalParameter("strategyId", strategyId);
            parameters.AddOptionalParameter("strategyType", strategyType);
            parameters.Add("computeCommissionRates", computeFeeRates?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = computeFeeRates == true ? 20 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/order/test", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<BinanceTestOrderCommission>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region New Order

        /// <inheritdoc />
        public async Task<HttpResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
            Enums.OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            PegPriceType? pegPriceType = null,
            int? pegOffsetValue = null,
            PegOffsetType? pegOffsetType = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await _baseClient.PlaceOrderInternal("/api/v3/order",
                BinanceExchange.RateLimiter.SpotRestUid,
                symbol,
                side,
                type,
                quantity,
                quoteQuantity,
                newClientOrderId,
                price,
                timeInForce,
                stopPrice,
                icebergQty,
                null,
                null,
                orderResponseType,
                trailingDelta,
                strategyId,
                strategyType,
                selfTradePreventionMode,
                null,
                pegPriceType,
                pegOffsetValue,
                pegOffsetType,
                receiveWindow,
                1,
                ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderBase>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, CancelRestriction? cancelRestriction = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null) 
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }
            
            if (newClientOrderId != null)
            {
                newClientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.Add("cancelRestrictions", cancelRestriction);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "api/v3/order", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceOrderBase>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel all Open Orders on a Symbol

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderBase[]>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "api/v3/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderBase[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Replace order
        /// <inheritdoc />
        public async Task<HttpResult<BinanceReplaceOrderResult>> ReplaceOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            CancelReplaceMode cancelReplaceMode,
            long? cancelOrderId = null,
            string? cancelClientOrderId = null,
            string? newCancelClientOrderId = null,
            string? newClientOrderId = null,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            OrderResponseType? orderResponseType = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            CancelRestriction? cancelRestriction = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (cancelOrderId == null && cancelClientOrderId == null || cancelOrderId != null && cancelClientOrderId != null)
                throw new ArgumentException("1 of either should be specified, cancelOrderId or cancelClientOrderId");

            if (quoteQuantity != null && type != SpotOrderType.Market)
                throw new ArgumentException("quoteQuantity is only valid for market orders");

            if (quantity == null && quoteQuantity == null || quantity != null && quoteQuantity != null)
                throw new ArgumentException("1 of either should be specified, quantity or quoteOrderQuantity");


            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, quoteQuantity, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return HttpResult.Fail<BinanceReplaceOrderResult>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;
            quoteQuantity = rulesCheck.QuoteQuantity;

            var clientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("cancelReplaceMode", cancelReplaceMode);
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.AddOptionalParameter("cancelNewClientOrderId", newCancelClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("cancelOrderId", cancelOrderId);
            parameters.AddOptionalParameter("strategyId", strategyId);
            parameters.AddOptionalParameter("strategyType", strategyType);
            parameters.AddOptionalParameter("cancelOrigClientOrderId", cancelClientOrderId);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("timeInForce", timeInForce);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("newOrderRespType", orderResponseType);
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.Add("cancelRestrictions", cancelRestriction);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/order/cancelReplace", BinanceExchange.RateLimiter.SpotRestIp, 4, true, tryParseOnNonSuccess: true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceReplaceOrderResult>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceReplaceOrderResult>(result);

            if (result.Data?.Data == null)
            {
                if (result.Data == null)
                    return HttpResult.Fail<BinanceReplaceOrderResult>(result, new ServerError(ErrorInfo.Unknown));

                // A general API error
                return HttpResult.Fail<BinanceReplaceOrderResult>(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            }

            if (result.Data.Data.NewOrderResult == OrderOperationResult.NotAttempted)
                // Not attempted because cancel failed
                return HttpResult.Fail<BinanceReplaceOrderResult>(result, new ServerError(result.Data.Data.CancelResponse!.Code!.Value, _baseClient.GetErrorInfo(result.Data.Data.CancelResponse.Code.Value, result.Data.Data.CancelResponse.Message)), result.Data.Data);

            if (result.Data.Data.NewOrderResult == OrderOperationResult.Failure)
                // New order attempted; if cancel failed this still takes priority since cancelReplaceMode was AllowFailure
                return HttpResult.Fail<BinanceReplaceOrderResult>(result, new ServerError(result.Data.Data.NewOrderResponse!.Code!.Value, _baseClient.GetErrorInfo(result.Data.Data.NewOrderResponse.Code.Value, result.Data.Data.NewOrderResponse.Message)), result.Data.Data);

            return HttpResult.Ok(result, result.Data.Data);
        }
        #endregion

        #region Amend Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceAmendedOrderResult>> AmendOrderAsync(
            string symbol,
            decimal newQty,
            long? orderId = null,
            string? origClientOrderId = null,
            string? newClientOrderId = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (newClientOrderId != null)
            {
                newClientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "newQty", newQty.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "api/v3/order/amend/keepPriority", BinanceExchange.RateLimiter.SpotRestIp, 4, true);
            return await _baseClient.SendAsync<BinanceAmendedOrderResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/order", BinanceExchange.RateLimiter.SpotRestIp, 4, true);
            return await _baseClient.SendAsync<BinanceOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Current Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrder[]>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/openOrders", BinanceExchange.RateLimiter.SpotRestIp, symbol == null ? 80 : 6, true);
            return await _baseClient.SendAsync<BinanceOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Orders 

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrder[]>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/allOrders", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<BinanceOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList>> PlaceOcoOrderAsync(string symbol,
            Enums.OrderSide side,
            decimal quantity,
            decimal price,
            decimal stopPrice,
            decimal? stopLimitPrice = null,
            string? listClientOrderId = null,
            string? limitClientOrderId = null,
            string? stopClientOrderId = null,
            decimal? limitIcebergQuantity = null,
            decimal? stopIcebergQuantity = null,
            TimeInForce? stopLimitTimeInForce = null,
            int? trailingDelta = null,
            int? limitStrategyId = null,
            int? limitStrategyType = null,
            int? stopStrategyId = null,
            int? stopStrategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, null, price, stopPrice, null, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return HttpResult.Fail<BinanceOrderOcoList>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity!.Value;
            price = rulesCheck.Price!.Value;
            stopPrice = rulesCheck.StopPrice!.Value;

            limitClientOrderId = LibraryHelpers.ApplyBrokerId(
                    limitClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            stopClientOrderId = LibraryHelpers.ApplyBrokerId(
                    stopClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("side", side);

            parameters.AddOptionalParameter("limitStrategyId", limitStrategyId);
            parameters.AddOptionalParameter("limitStrategyType", limitStrategyType);
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.AddOptionalParameter("stopStrategyId", stopStrategyId);
            parameters.AddOptionalParameter("stopStrategyType", stopStrategyType);
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("stopLimitPrice", stopLimitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("limitClientOrderId", limitClientOrderId);
            parameters.AddOptionalParameter("stopClientOrderId", stopClientOrderId);
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQuantity);
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQuantity);
            parameters.Add("stopLimitTimeInForce", stopLimitTimeInForce);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/order/oco", BinanceExchange.RateLimiter.SpotRestUid, 2, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList>> PlaceOcoOrderListAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            SpotOrderType aboveOrderType,
            SpotOrderType belowOrderType,
            string? listClientOrderId = null,

            string? aboveClientOrderId = null,
            decimal? aboveIcebergQuantity = null,
            decimal? abovePrice = null,
            decimal? aboveStopPrice = null,
            decimal? aboveTrailingDelta = null,
            TimeInForce? aboveTimeInForce = null,
            int? aboveStrategyId = null,
            int? aboveStrategyType = null,

            string? belowClientOrderId = null,
            decimal? belowIcebergQuantity = null,
            decimal? belowPrice = null,
            decimal? belowStopPrice = null,
            decimal? belowTrailingDelta = null,
            TimeInForce? belowTimeInForce = null,
            int? belowStrategyId = null,
            int? belowStrategyType = null,

            SelfTradePreventionMode? selfTradePreventionMode = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            aboveClientOrderId = LibraryHelpers.ApplyBrokerId(
                    aboveClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            belowClientOrderId = LibraryHelpers.ApplyBrokerId(
                    belowClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.Add("aboveType", aboveOrderType);
            parameters.Add("belowType", belowOrderType);
            parameters.Add("side", side);

            parameters.Add("listClientOrderId", listClientOrderId);
            parameters.Add("aboveClientOrderId", aboveClientOrderId);
            parameters.Add("aboveIcebergQty", aboveIcebergQuantity);
            parameters.Add("abovePrice", abovePrice);
            parameters.Add("aboveStopPrice", aboveStopPrice);
            parameters.Add("aboveTrailingDelta", aboveTrailingDelta);
            parameters.Add("aboveTimeInForce", aboveTimeInForce);
            parameters.Add("aboveStrategyId", aboveStrategyId);
            parameters.Add("aboveStrategyType", aboveStrategyType);

            parameters.Add("belowClientOrderId", belowClientOrderId);
            parameters.Add("belowIcebergQty", belowIcebergQuantity);
            parameters.Add("belowPrice", belowPrice);
            parameters.Add("belowStopPrice", belowStopPrice);
            parameters.Add("belowTrailingDelta", belowTrailingDelta);
            parameters.Add("belowTimeInForce", belowTimeInForce);
            parameters.Add("belowStrategyId", belowStrategyId);
            parameters.Add("belowStrategyType", belowStrategyType);

            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/orderList/oco", BinanceExchange.RateLimiter.SpotRestUid, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList>> CancelOcoOrderAsync(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            if (listClientOrderId != null)
            {
                listClientOrderId = LibraryHelpers.ApplyBrokerId(
                    listClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "api/v3/orderList", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList>> GetOcoOrderAsync(long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/orderList", BinanceExchange.RateLimiter.SpotRestIp, 4, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList[]>> GetOcoOrdersAsync(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/allOrderList", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList[]>> GetOpenOcoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/openOrderList", BinanceExchange.RateLimiter.SpotRestIp, 6, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OTO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList>> PlaceOtoOrderListAsync(
            string symbol,

            OrderSide workingSide,
            SpotOrderType workingOrderType,
            decimal workingQuantity,
            decimal workingPrice,

            decimal pendingQuantity,
            OrderSide pendingSide,
            SpotOrderType pendingOrderType,

            string? listClientOrderId = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,

            string? workingClientOrderId = null,
            decimal? workingIcebergQuantity = null,
            TimeInForce? workingTimeInForce = null,
            int? workingStrategyId = null,
            int? workingStrategyType = null,

            string? pendingClientOrderId = null,
            decimal? pendingPrice = null,
            decimal? pendingStopPrice = null,
            decimal? pendingTrailingDelta = null,
            decimal? pendingIcebergQuantity = null,
            TimeInForce? pendingTimeInForce = null,
            int? pendingStrategyId = null,
            int? pendingStrategyType = null,

            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            workingClientOrderId = LibraryHelpers.ApplyBrokerId(
                    workingClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            pendingClientOrderId = LibraryHelpers.ApplyBrokerId(
                    pendingClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("workingType", workingOrderType);
            parameters.Add("workingSide", workingSide);
            parameters.Add("workingQuantity", workingQuantity);
            parameters.Add("workingPrice", workingPrice);
            parameters.Add("pendingQuantity", pendingQuantity);
            parameters.Add("pendingSide", pendingSide);
            parameters.Add("pendingType", pendingOrderType);

            parameters.Add("listClientOrderId", listClientOrderId);
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.Add("workingClientOrderId", workingClientOrderId);
            parameters.Add("workingIcebergQty", workingIcebergQuantity);
            parameters.Add("workingTimeInForce", workingTimeInForce);
            parameters.Add("workingStrategyId", workingStrategyId);
            parameters.Add("workingStrategyType", workingStrategyType);

            parameters.Add("pendingClientOrderId", pendingClientOrderId);
            parameters.Add("pendingPrice", pendingPrice);
            parameters.Add("pendingStopPrice", pendingStopPrice);
            parameters.Add("pendingTrailingDelta", pendingTrailingDelta);
            parameters.Add("pendingIcebergQty", pendingIcebergQuantity);
            parameters.Add("pendingTimeInForce", pendingTimeInForce);
            parameters.Add("pendingStrategyId", pendingStrategyId);
            parameters.Add("pendingStrategyType", pendingStrategyType);

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/orderList/oto", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OTOCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderOcoList>> PlaceOtocoOrderListAsync(
            string symbol,

            OrderSide workingSide,
            SpotOrderType workingOrderType,
            decimal workingQuantity,
            decimal workingPrice,

            decimal pendingQuantity,
            OrderSide pendingSide,
            SpotOrderType pendingAboveOrderType,
            SpotOrderType pendingBelowOrderType,

            string? listClientOrderId = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,

            string? workingClientOrderId = null,
            decimal? workingIcebergQuantity = null,
            TimeInForce? workingTimeInForce = null,
            int? workingStrategyId = null,
            int? workingStrategyType = null,

            string? pendingAboveClientOrderId = null,
            decimal? pendingAbovePrice = null,
            decimal? pendingAboveStopPrice = null,
            decimal? pendingAboveTrailingDelta = null,
            decimal? pendingAboveIcebergQuantity = null,
            TimeInForce? pendingAboveTimeInForce = null,
            int? pendingAboveStrategyId = null,
            int? pendingAboveStrategyType = null,

            string? pendingBelowClientOrderId = null,
            decimal? pendingBelowPrice = null,
            decimal? pendingBelowStopPrice = null,
            decimal? pendingBelowTrailingDelta = null,
            decimal? pendingBelowIcebergQuantity = null,
            TimeInForce? pendingBelowTimeInForce = null,
            int? pendingBelowStrategyId = null,
            int? pendingBelowStrategyType = null,

            int? receiveWindow = null,
            CancellationToken ct = default)
        {

            workingClientOrderId = LibraryHelpers.ApplyBrokerId(
                    workingClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            pendingAboveClientOrderId = LibraryHelpers.ApplyBrokerId(
                    pendingAboveClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            pendingBelowClientOrderId = LibraryHelpers.ApplyBrokerId(
                    pendingBelowClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("workingType", workingOrderType);
            parameters.Add("workingSide", workingSide);
            parameters.Add("workingQuantity", workingQuantity);
            parameters.Add("workingPrice", workingPrice);
            parameters.Add("pendingQuantity", pendingQuantity);
            parameters.Add("pendingSide", pendingSide);
            parameters.Add("pendingAboveType", pendingAboveOrderType);
            parameters.Add("pendingBelowType", pendingBelowOrderType);

            parameters.Add("listClientOrderId", listClientOrderId);
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.Add("workingClientOrderId", workingClientOrderId);
            parameters.Add("workingIcebergQty", workingIcebergQuantity);
            parameters.Add("workingTimeInForce", workingTimeInForce);
            parameters.Add("workingStrategyId", workingStrategyId);
            parameters.Add("workingStrategyType", workingStrategyType);

            parameters.Add("pendingAboveClientOrderId", pendingAboveClientOrderId);
            parameters.Add("pendingAbovePrice", pendingAbovePrice);
            parameters.Add("pendingAboveStopPrice", pendingAboveStopPrice);
            parameters.Add("pendingAboveTrailingDelta", pendingAboveTrailingDelta);
            parameters.Add("pendingAboveIcebergQty", pendingAboveIcebergQuantity);
            parameters.Add("pendingAboveTimeInForce", pendingAboveTimeInForce);
            parameters.Add("pendingAboveStrategyId", pendingAboveStrategyId);
            parameters.Add("pendingAboveStrategyType", pendingAboveStrategyType);

            parameters.Add("pendingBelowClientOrderId", pendingBelowClientOrderId);
            parameters.Add("pendingBelowPrice", pendingBelowPrice);
            parameters.Add("pendingBelowStopPrice", pendingBelowStopPrice);
            parameters.Add("pendingBelowTrailingDelta", pendingBelowTrailingDelta);
            parameters.Add("pendingBelowIcebergQty", pendingBelowIcebergQuantity);
            parameters.Add("pendingBelowTimeInForce", pendingBelowTimeInForce);
            parameters.Add("pendingBelowStrategyId", pendingBelowStrategyId);
            parameters.Add("pendingBelowStrategyType", pendingBelowStrategyType);

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/orderList/otoco", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<BinanceTrade[]>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = orderId == null ? 20 : 5;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/myTrades", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<BinanceTrade[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }
        #endregion

        #region Margin Account New Order

        /// <inheritdoc />
        public async Task<HttpResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQuantity = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            OrderResponseType? orderResponseType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? autoRepayAtCancel = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await _baseClient.PlaceOrderInternal("/sapi/v1/margin/order",
                BinanceExchange.RateLimiter.SpotRestUid,
                symbol,
                side,
                type,
                quantity,
                quoteQuantity,
                newClientOrderId,
                price,
                timeInForce,
                stopPrice,
                icebergQuantity,
                sideEffectType,
                isIsolated,
                orderResponseType,
                null,
                null,
                null,
                selfTradePreventionMode,
                autoRepayAtCancel,
                null,
                null,
                null,
                receiveWindow,
                weight: 6,
                ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Margin Account Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderBase>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            if (newClientOrderId != null)
            {
                newClientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "sapi/v1/margin/order", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            var result = await _baseClient.SendAsync<BinanceOrderBase>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Margin Account Cancel All Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrderBase[]>> CancelAllMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "sapi/v1/margin/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderBase[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrder>> GetMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId should be provided");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/order", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Open Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrder[]>> GetOpenMarginOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            if (isIsolated == true && symbol == null)
                throw new ArgumentException("Symbol must be provided for isolated margin");

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's All Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceOrder[]>> GetMarginOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/allOrders", BinanceExchange.RateLimiter.SpotRestIp, 200, true);
            return await _baseClient.SendAsync<BinanceOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Account's Trade List

        /// <inheritdoc />
        public async Task<HttpResult<BinanceTrade[]>> GetMarginUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null,
            int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/myTrades", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account New OCO Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarginOrderOcoList>> PlaceMarginOCOOrderAsync(string symbol,
            Enums.OrderSide side,
            decimal price,
            decimal stopPrice,
            decimal quantity,
            decimal? stopLimitPrice = null,
            TimeInForce? stopLimitTimeInForce = null,
            decimal? stopIcebergQuantity = null,
            decimal? limitIcebergQuantity = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            string? listClientOrderId = null,
            string? limitClientOrderId = null,
            string? stopClientOrderId = null,
            OrderResponseType? orderResponseType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            bool? autoRepayAtCancel = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, null, price, stopPrice, null, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return HttpResult.Fail<BinanceMarginOrderOcoList>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity!.Value;
            price = rulesCheck.Price!.Value;
            stopPrice = rulesCheck.StopPrice!.Value;

            limitClientOrderId = LibraryHelpers.ApplyBrokerId(
                    limitClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            stopClientOrderId = LibraryHelpers.ApplyBrokerId(
                    stopClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.Add("side", side);
            parameters.AddOptionalParameter("stopLimitPrice", stopLimitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.Add("sideEffectType", sideEffectType);
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("limitClientOrderId", limitClientOrderId);
            parameters.AddOptionalParameter("stopClientOrderId", stopClientOrderId);
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("newOrderRespType", orderResponseType);
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("stopLimitTimeInForce", stopLimitTimeInForce);
            parameters.AddOptionalParameter("autoRepayAtCancel", autoRepayAtCancel);
            parameters.Add("selfTradePreventionMode",selfTradePreventionMode);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/margin/order/oco", BinanceExchange.RateLimiter.SpotRestUid, 6, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            if (listClientOrderId != null)
            {
                listClientOrderId = LibraryHelpers.ApplyBrokerId(
                    listClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            if (newClientOrderId != null)
            {
                newClientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "sapi/v1/margin/orderList", BinanceExchange.RateLimiter.SpotRestUid, 1, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/orderList", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarginOrderOcoList[]>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/allOrderList", BinanceExchange.RateLimiter.SpotRestIp, 200, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<HttpResult<BinanceMarginOrderOcoList[]>> GetMarginOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/margin/openOrderList", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region C2C

        /// <inheritdoc />
        public async Task<HttpResult<BinanceC2CUserTrade[]>> GetC2CTradeHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("tradeType", side);
            parameters.AddOptionalParameter("startTimestamp", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTimestamp", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("rows", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/c2c/orderMatch/listUserOrderHistory", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinanceC2CUserTrade[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinanceC2CUserTrade[]>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinanceC2CUserTrade[]>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Pay

        /// <inheritdoc />
        public async Task<HttpResult<BinancePayTrade[]>> GetPayTradeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/pay/transactions", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            var result = await _baseClient.SendAsync<BinanceResult<BinancePayTrade[]>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BinancePayTrade[]>(result);

            if (result.Data?.Code != 0)
                return HttpResult.Fail<BinancePayTrade[]>(result, new ServerError(result.Data!.Code.ToString(), _baseClient.GetErrorInfo(result.Data!.Code, result.Data!.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Convert

        #region Convert Quote Request

        /// <inheritdoc />
        public async Task<HttpResult<BinanceConvertQuote>> ConvertQuoteRequestAsync(string quoteAsset, string baseAsset, decimal? quoteQuantity = null, decimal? baseQuantity = null, WalletType? walletType = null, ValidTime? validTime = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (quoteQuantity == null && baseQuantity == null || quoteQuantity != null && baseQuantity != null)
                throw new ArgumentException("Either quoteQuantity or baseQuantity must be sent, but not both");

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddParameter("fromAsset", quoteAsset);
            parameters.AddParameter("toAsset", baseAsset);
            parameters.AddOptionalParameter("fromAmount", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("toAmount", baseQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("walletType", walletType == null ? null : walletType == WalletType.Spot ? "SPOT" : "FUNDING");
            parameters.Add("validTime", validTime);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/convert/getQuote", BinanceExchange.RateLimiter.SpotRestUid, 200, true);
            return await _baseClient.SendAsync<BinanceConvertQuote>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert Accept Quote

        /// <inheritdoc />
        public async Task<HttpResult<BinanceConvertResult>> ConvertAcceptQuoteAsync(string quoteId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddParameter("quoteId", quoteId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/convert/acceptQuote", BinanceExchange.RateLimiter.SpotRestUid, 500, true);
            return await _baseClient.SendAsync<BinanceConvertResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Order Status

        /// <inheritdoc />
        public async Task<HttpResult<BinanceConvertOrderStatus>> GetConvertOrderStatusAsync(string? orderId = null, string? quoteId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && quoteId == null || orderId != null && quoteId != null)
                throw new ArgumentException("Either orderId or quoteId must be sent, but not both");

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("quoteId", quoteId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/convert/orderStatus", BinanceExchange.RateLimiter.SpotRestUid, 100, true);
            return await _baseClient.SendAsync<BinanceConvertOrderStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Trade History

        /// <inheritdoc />
        public async Task<HttpResult<BinanceListResult<BinanceConvertTrade>>> GetConvertTradeHistoryAsync(DateTime startTime, DateTime endTime, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/convert/tradeFlow", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceListResult<BinanceConvertTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Get Prevented Trades
        /// <inheritdoc />
        public async Task<HttpResult<BinancePreventedTrade[]>> GetPreventedTradesAsync(string symbol, long? preventedMatchId = null, long? orderId = null, long? fromPreventedMatchId = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("preventedMatchId", preventedMatchId);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("fromPreventedMatchId", fromPreventedMatchId);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = preventedMatchId == null ? 20 : 2;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/myPreventedMatches", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<BinancePreventedTrade[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }
        #endregion

        #region Spot Algo

        #region Place TWAP
        /// <inheritdoc />
        public async Task<HttpResult<BinanceAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            int duration,
            string? clientOrderId = null,
            decimal? limitPrice = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Spot"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "duration", duration },
            };
            parameters.Add("side", side);
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "sapi/v1/algo/spot/newOrderTwap", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceAlgoOrderResult>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Cancel algo order
        /// <inheritdoc />
        public async Task<HttpResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "algoId", algoOrderId },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "sapi/v1/algo/spot/order", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoResult>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Algo Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/algo/spot/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoOrders>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get historical Algo Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.Add("side", side);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/algo/spot/historicalOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoOrders>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Algo sub Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "algoId", algoId }
            };
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "sapi/v1/algo/spot/subOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoSubOrderList>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
        #endregion
    }
}
