using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using CryptoExchange.Net.Objects.Errors;
using System.Text.Json;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc />
    internal class BinanceRestClientCoinFuturesApiTrading : IBinanceRestClientCoinFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new();
        private readonly ILogger _logger;

        private readonly BinanceRestClientCoinFuturesApi _baseClient;

        internal BinanceRestClientCoinFuturesApiTrading(ILogger logger, BinanceRestClientCoinFuturesApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
        }

        #region New Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            FuturesOrderType type,
            decimal? quantity,
            decimal? price = null,
            PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            string? newClientOrderId = null,
            decimal? stopPrice = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? closePosition = null,
            OrderResponseType? orderResponseType = null,
            bool? priceProtect = null,
            PriceMatch? priceMatch = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (closePosition == true && positionSide != null)
            {
                if (positionSide == Enums.PositionSide.Short && side == Enums.OrderSide.Sell)
                    throw new ArgumentException("Can't close short position with order side sell");
                if (positionSide == Enums.PositionSide.Long && side == Enums.OrderSide.Buy)
                    throw new ArgumentException("Can't close long position with order side buy");
            }

            if (orderResponseType == OrderResponseType.Full)
                throw new ArgumentException("OrderResponseType.Full is not supported in Futures");

            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, null, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return HttpResult.Fail<BinanceFuturesOrder>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;

            var clientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
            };

            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("positionSide", positionSide);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("activationPrice", activationPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("callbackRate", callbackRate?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("workingType", workingType);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString().ToLower());
            parameters.AddOptionalParameter("closePosition", closePosition?.ToString().ToLower());
            parameters.Add("newOrderRespType", orderResponseType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.Add("priceMatch", priceMatch);
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("priceProtect", priceProtect?.ToString().ToUpper());

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "dapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 0, true);
            var result = await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }


        #endregion

        #region Multiple New Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<BinanceFuturesOrder>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<BinanceFuturesBatchOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (orders.Count() <= 0 || orders.Count() > 5)
                throw new ArgumentException("Order list should be at least 1 and max 5 orders");

            if (_baseClient.ApiOptions.TradeRulesBehaviour != TradeRulesBehaviour.None)
            {
                foreach (var order in orders)
                {
                    var rulesCheck = await _baseClient.CheckTradeRules(order.Symbol, order.Quantity, null, order.Price, order.StopPrice, order.Type, ct).ConfigureAwait(false);
                    if (!rulesCheck.Passed)
                    {
                        _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                        return HttpResult.Fail<CallResult<BinanceFuturesOrder>[]>(BinanceExchange.Metadata.Id, ArgumentError.Invalid(rulesCheck.ErrorParameter!, rulesCheck.ErrorMessage!));
                    }

                    order.Quantity = rulesCheck.Quantity;
                    order.Price = rulesCheck.Price;
                    order.StopPrice = rulesCheck.StopPrice;
                }
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);

            var parameterOrders = new Parameters[orders.Count()];
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new Parameters(BinanceExchange._parameterSerializationSettings)
                {
                    { "symbol", order.Symbol },
                    { "newOrderRespType", "RESULT" }
                };

                orderParameters.Add("side", order.Side);
                orderParameters.Add("type", order.Type);
                var clientOrderId = LibraryHelpers.ApplyBrokerId(
                    order.NewClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
                orderParameters.AddOptionalParameter("quantity", order.Quantity?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", clientOrderId);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.Add("timeInForce", order.TimeInForce);
                orderParameters.Add("positionSide", order.PositionSide);
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.Add("workingType", order.WorkingType);
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                orderParameters.AddOptionalParameter("priceProtect", order.PriceProtect?.ToString().ToUpper());
                orderParameters.Add("selfTradePreventionMode", order.SelfTradePreventionMode);
                orderParameters.Add("priceMatch", order.PriceMatch);
                parameterOrders[i] = orderParameters;
                i++;
            }

#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
            parameters.Add("batchOrders", JsonSerializer.Serialize(parameterOrders, SerializerOptions.WithConverters(BinanceExchange._serializerContext)));
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "dapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            var response = await _baseClient.SendAsync<BinanceFuturesMultipleOrderPlaceResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return HttpResult.Fail<CallResult<BinanceFuturesOrder>[]>(response);

            var result = new List<CallResult<BinanceFuturesOrder>>();
            foreach (var item in response.Data!)
            {
                result.Add(item.Code != 0
                    ? CallResult.Fail<BinanceFuturesOrder>(new ServerError(item.Code.ToString(), _baseClient.GetErrorInfo(item.Code, item.Message)))
                    : CallResult.Ok<BinanceFuturesOrder>(item));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail(response, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")), result.ToArray());

            return HttpResult.Ok(response, result.ToArray());
        }

        #endregion

        #region Query Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }


        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "dapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            var result = await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCancelAllOrders>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "dapi/v1/allOpenOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesCancelAllOrders>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Auto-Cancel All Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "countdownTime", (int)countDownTime.TotalMilliseconds }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "dapi/v1/countdownCancelAll", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesCountDownResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<BinanceFuturesOrder>[]>> CancelMultipleOrdersAsync(string symbol, IEnumerable<long>? orderIdList = null, IEnumerable<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderIdList == null && origClientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or origClientOrderIdList must be sent");

            if (orderIdList?.Count() > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (origClientOrderIdList?.Count() > 10)
                throw new ArgumentException("origClientOrderIdList cannot contain more than 10 items");

            var convertClientOrderIdList = origClientOrderIdList?.Select(x => LibraryHelpers.ApplyBrokerId(
                    x,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId));

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (origClientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", origClientOrderIdList.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "dapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            var response = await _baseClient.SendAsync<BinanceFuturesMultipleOrderCancelResult[]>(request, parameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return HttpResult.Fail<CallResult<BinanceFuturesOrder>[]>(response);

            var result = new List<CallResult<BinanceFuturesOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? CallResult.Fail<BinanceFuturesOrder>(new ServerError(item.Code.ToString(), _baseClient.GetErrorInfo(item.Code, item.Message)))
                    : CallResult.Ok<BinanceFuturesOrder>(item));
            }

            return HttpResult.Ok(response, result.ToArray());
        }

        #endregion

        #region Query Current Open Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/openOrder", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Current All Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            var weight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/openOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region All Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder[]>> GetOrdersAsync(string? symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/allOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region User's Force Orders

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesOrder[]>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.Add("autoCloseType", closeType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var weight = symbol == null ? 50 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/forceOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Account Trade List

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesCoinTrade[]>> GetUserTradesAsync(string? symbol = null, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? orderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "dapi/v1/userTrades", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<BinanceFuturesCoinTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New Conditional Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesConditionalOrder>> PlaceConditionalOrderAsync(
            string symbol,
            Enums.OrderSide side,
            ConditionalOrderType type,
            decimal? quantity,
            decimal? price = null,
            Enums.PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            string? clientOrderId = null,
            decimal? triggerPrice = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? closePosition = null,
            bool? priceProtect = null,
            PriceMatch? priceMatch = null,
            SelfTradePreventionMode? selfTradePreventionMode = null,
            DateTime? goodTillDate = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var clientOrderIdInt = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings)
            {
                { "algoType", "CONDITIONAL" },
                { "symbol", symbol }
            };
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("clientAlgoId", clientOrderIdInt);
            parameters.Add("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("positionSide", positionSide);
            parameters.Add("triggerPrice", triggerPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("activatePrice", activationPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("callbackRate", callbackRate?.ToString(CultureInfo.InvariantCulture));
            parameters.Add("workingType", workingType);
            parameters.Add("reduceOnly", reduceOnly?.ToString().ToLower());
            parameters.Add("closePosition", closePosition?.ToString().ToLower());
            parameters.Add("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.Add("priceProtect", priceProtect?.ToString().ToUpper());
            parameters.Add("priceMatch", priceMatch);
            parameters.Add("selfTradePreventionMode", selfTradePreventionMode);
            parameters.Add("goodTillDate", goodTillDate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "dapi/v1/algoOrder", BinanceExchange.RateLimiter.FuturesRest, 0, true);
            var result = await _baseClient.SendAsync<BinanceFuturesConditionalOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Conditional Order
        /// <inheritdoc />
        public async Task<HttpResult<BinanceAlgoResult>> CancelConditionalOrderAsync(
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (clientOrderId != null)
            {
                clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings);
            parameters.Add("algoId", orderId);
            parameters.Add("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/dapi/v1/algoOrder", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoResult>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Cancel All Conditional Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceResult>> CancelAllConditionalOrdersAsync(
            string symbol,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings); ;
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/dapi/v1/algoOpenOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceResult>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Conditional Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesConditionalOrder[]>> GetOpenConditionalOrdersAsync(
            string? symbol = null,
            string? algoType = null,
            long? orderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings); ;
            parameters.Add("algoType", algoType);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/dapi/v1/openAlgoOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesConditionalOrder[]>(request, parameters, ct, weight: weight).ConfigureAwait(false);
        }
        #endregion

        #region Get Conditional Order

        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesConditionalOrder>> GetConditionalOrderAsync(
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {

            if (clientOrderId != null)
            {
                clientOrderId = LibraryHelpers.ApplyBrokerId(
                    clientOrderId,
                    LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange, "Futures"),
                    36,
                    _baseClient.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings); ;
            parameters.Add("algoId", orderId);
            parameters.Add("clientAlgoId", clientOrderId);
            parameters.Add("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/dapi/v1/algoOrder", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesConditionalOrder>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Conditional Orders
        /// <inheritdoc />
        public async Task<HttpResult<BinanceFuturesConditionalOrder[]>> GetConditionalOrdersAsync(
            string symbol,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? page = null,
            int? limit = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BinanceExchange._parameterSerializationSettings); ;
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("limit", limit);
            parameters.Add("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/dapi/v1/allAlgoOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<BinanceFuturesConditionalOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
    }
}
