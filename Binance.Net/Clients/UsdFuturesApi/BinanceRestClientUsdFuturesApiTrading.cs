using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models.Futures;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using CryptoExchange.Net.CommonObjects;
using System.Drawing;
using System.Text.Json;

namespace Binance.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BinanceRestClientUsdFuturesApiTrading : IBinanceRestClientUsdFuturesApiTrading
    {
        private readonly ILogger _logger;
        private static readonly RequestDefinitionCache _definitions = new();

        private readonly BinanceRestClientUsdFuturesApi _baseClient;
        private readonly string _spotBaseAddress; 

        internal BinanceRestClientUsdFuturesApiTrading(ILogger logger, BinanceRestClientUsdFuturesApi baseClient)
        {
            _logger = logger;
            _baseClient = baseClient;
            _spotBaseAddress = _baseClient.ClientOptions.Environment.SpotRestAddress;
        }

        #region New Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceUsdFuturesOrder>> PlaceOrderAsync(
            string symbol,
            Enums.OrderSide side,
            FuturesOrderType type,
            decimal? quantity,
            decimal? price = null,
            Enums.PositionSide? positionSide = null,
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
            DateTime? goodTillDate = null,
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
                return new WebCallResult<BinanceUsdFuturesOrder>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;

            var clientOrderId = LibraryHelpers.ApplyBrokerId(newClientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("activationPrice", activationPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("callbackRate", callbackRate?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("workingType", workingType);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString().ToLower());
            parameters.AddOptionalParameter("closePosition", closePosition?.ToString().ToLower());
            parameters.AddOptionalEnum("newOrderRespType", orderResponseType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("priceProtect", priceProtect?.ToString().ToUpper());
            parameters.AddOptionalEnum("priceMatch", priceMatch);
            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalMilliseconds("goodTillDate", goodTillDate);

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 0, true);
            var result = await _baseClient.SendAsync<BinanceUsdFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId
                {
                    SourceObject = result.Data,
                    Id = result.Data.Id.ToString(CultureInfo.InvariantCulture)
                });
            return result;
        }

        #endregion

        #region Multiple New Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>> PlaceMultipleOrdersAsync(
            IEnumerable<BinanceFuturesBatchOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            if (_baseClient.ApiOptions.TradeRulesBehaviour != TradeRulesBehaviour.None)
            {
                foreach (var order in orders)
                {
                    var rulesCheck = await _baseClient.CheckTradeRules(order.Symbol, order.Quantity, null, order.Price, order.StopPrice, order.Type, ct).ConfigureAwait(false);
                    if (!rulesCheck.Passed)
                    {
                        _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                        return new WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(new ArgumentError(rulesCheck.ErrorMessage!));
                    }

                    order.Quantity = rulesCheck.Quantity;
                    order.Price = rulesCheck.Price;
                    order.StopPrice = rulesCheck.StopPrice;
                }
            }

            var parameters = new ParameterCollection();
            var parameterOrders = new List<Dictionary<string, object>>();
            int i = 0;
            foreach (var order in orders)
            {
                var clientOrderId = LibraryHelpers.ApplyBrokerId(order.NewClientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

                var orderParameters = new ParameterCollection()
                {
                    { "symbol", order.Symbol },
                    { "newOrderRespType", "RESULT" }
                };
                orderParameters.AddEnum("side", order.Side);
                orderParameters.AddEnum("type", order.Type);
                orderParameters.AddOptionalParameter("quantity", order.Quantity?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", clientOrderId);
                orderParameters.AddOptionalEnum("timeInForce", order.TimeInForce);
                orderParameters.AddOptionalEnum("positionSide", order.PositionSide);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalEnum("workingType", order.WorkingType);
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                orderParameters.AddOptionalParameter("priceProtect", order.PriceProtect?.ToString().ToUpper());
                orderParameters.AddOptionalEnum("priceMatch", order.PriceMatch);
                orderParameters.AddOptionalEnum("selfTradePreventionMode", order.SelfTradePreventionMode);
                parameterOrders.Add(orderParameters);
                i++;
            }

            parameters.Add("batchOrders", JsonSerializer.Serialize(parameterOrders));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            var response = await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesMultipleOrderPlaceResult>>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return response.As<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(default);

            var result = new List<CallResult<BinanceUsdFuturesOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<BinanceUsdFuturesOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<BinanceUsdFuturesOrder>(item));
            }

            return response.As<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(result);
        }

        #endregion

        #region Query Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceUsdFuturesOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceUsdFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Order Edit History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrderEditHistory>>> GetOrderEditHistoryAsync(string symbol, long? orderId = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (clientOrderId != null)
                clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", clientOrderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/orderAmendment", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesOrderEditHistory>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceUsdFuturesOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            var result = await _baseClient.SendAsync<BinanceUsdFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            if (result)
            {
                _baseClient.InvokeOrderCanceled(new OrderId
                {
                    SourceObject = result.Data,
                    Id = result.Data.Id.ToString(CultureInfo.InvariantCulture)
                });
            }
            return result;
        }

        #endregion

        #region Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesCancelAllOrders>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/allOpenOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesCancelAllOrders>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceUsdFuturesOrder>> EditOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", EnumConverter.GetString(side) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Put, "fapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceUsdFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>> EditMultipleOrdersAsync(
            IEnumerable<BinanceFuturesBatchEditOrder> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var parameterOrders = new List<Dictionary<string, object>>();
            int i = 0;
            foreach (var order in orders)
            {
                var clientOrderId = order.ClientOrderId;
                if (clientOrderId != null)
                    clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

                var orderParameters = new ParameterCollection()
                {
                    { "symbol", order.Symbol },
                    { "quantity", order.Quantity.ToString(CultureInfo.InvariantCulture) },
                    { "price", order.Price.ToString(CultureInfo.InvariantCulture) },
                };
                orderParameters.AddEnum("side", order.Side);
                orderParameters.AddOptionalParameter("orderId", order.OrderId?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("origClientOrderId", clientOrderId);
                parameterOrders.Add(orderParameters);
                i++;
            }

            parameters.Add("batchOrders", JsonSerializer.Serialize(parameterOrders));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Put, "fapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            var response = await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesMultipleOrderPlaceResult>>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return response.As<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(default);

            var result = new List<CallResult<BinanceUsdFuturesOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<BinanceUsdFuturesOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<BinanceUsdFuturesOrder>(item));
            }

            return response.As<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(result);
        }

        #endregion

        #region Auto-Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "countdownTime", (int)countDownTime.TotalMilliseconds }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/countdownCancelAll", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesCountDownResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>> CancelMultipleOrdersAsync(string symbol, List<long>? orderIdList = null, List<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderIdList == null && origClientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or origClientOrderIdList must be sent");

            if (orderIdList?.Count > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (origClientOrderIdList?.Count > 10)
                throw new ArgumentException("origClientOrderIdList cannot contain more than 10 items");

            var convertClientOrderIdList = origClientOrderIdList?.Select(x => LibraryHelpers.ApplyBrokerId(x, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride));
            
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (origClientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", convertClientOrderIdList.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            var response = await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesMultipleOrderCancelResult>>(request, parameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return response.As<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(default);

            var result = new List<CallResult<BinanceUsdFuturesOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<BinanceUsdFuturesOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<BinanceUsdFuturesOrder>(item));
            }

            return response.As<IEnumerable<CallResult<BinanceUsdFuturesOrder>>>(result);
        }

        #endregion

        #region Query Current Open Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceUsdFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/openOrder", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceUsdFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Current All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceUsdFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            var weight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/openOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesOrder>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceUsdFuturesOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/allOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region User's Force Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceUsdFuturesOrder>>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalEnum("autoCloseType", closeType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var weight = symbol == null ? 50 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/forceOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceUsdFuturesOrder>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Account Trade List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesUsdtTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? orderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/userTrades", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesUsdtTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Futures Algo

        #region Place VP Order
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrderResult>> PlaceVolumeParticipationOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            OrderUrgency urgency,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? limitPrice = null,
            PositionSide? positionSide = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "urgency", EnumConverter.GetString(urgency) },
            };
            parameters.AddEnum("side", side);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/algo/futures/newOrderVp", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendToAddressAsync<BinanceAlgoOrderResult>(_spotBaseAddress, request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Place TWAP Order
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            int duration,
            string? clientOrderId = null,
            bool? reduceOnly = null,
            decimal? limitPrice = null,
            PositionSide? positionSide = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            clientOrderId = LibraryHelpers.ApplyBrokerId(clientOrderId, BinanceExchange.ClientOrderIdFutures, 32, _baseClient.ClientOptions.AllowBrokerOverride);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "duration", duration },
            };
            parameters.AddEnum("side", side);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("reduceOnly", reduceOnly);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/algo/futures/newOrderTwap", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendToAddressAsync<BinanceAlgoOrderResult>(_spotBaseAddress, request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Cancel algo order
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "algoId", algoOrderId },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/algo/futures/order", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendToAddressAsync<BinanceAlgoResult>(_spotBaseAddress, request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/algo/futures/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendToAddressAsync<BinanceAlgoOrders>(_spotBaseAddress, request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get historical Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null,long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/algo/futures/historicalOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendToAddressAsync<BinanceAlgoOrders>(_spotBaseAddress, request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Algo sub Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "algoId", algoId }
            };
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/algo/futures/subOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendToAddressAsync<BinanceAlgoSubOrderList>(_spotBaseAddress, request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePositionV3>>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/positionRisk", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            var result = await _baseClient.SendAsync<IEnumerable<BinancePositionV3>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Convert Quote Request

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesConvertQuote>> ConvertQuoteRequestAsync(string fromAsset, string toAsset, decimal? fromQuantity = null, decimal? toQuantity = null, ValidTime? validTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("fromAsset", fromAsset);
            parameters.Add("toAsset", toAsset);
            parameters.AddOptional("fromAmount", fromQuantity);
            parameters.AddOptional("toAmount", toQuantity);
            if (validTime != null)
            {
                var time = validTime == ValidTime.TenSeconds ? "10s" : validTime == ValidTime.ThirtySeconds ? "30s" : validTime == ValidTime.OneMinute ? "1m" : "2m";
                parameters.Add("validTime", time);
            }
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v1/convert/getQuote", BinanceExchange.RateLimiter.FuturesRest, 50, true);
            var result = await _baseClient.SendAsync<BinanceFuturesConvertQuote>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Convert Accept Quote

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesQuoteResult>> ConvertAcceptQuoteAsync(string quoteId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("quoteId", quoteId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v1/convert/acceptQuote", BinanceExchange.RateLimiter.FuturesRest, 200, true);
            var result = await _baseClient.SendAsync<BinanceFuturesQuoteResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Convert Order Status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesConvertStatus>> GetConvertOrderStatusAsync(string? quoteId = null, string? orderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("quoteId", quoteId);
            parameters.AddOptional("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/convert/orderStatus", BinanceExchange.RateLimiter.FuturesRest, 50, true);
            var result = await _baseClient.SendAsync<BinanceFuturesConvertStatus>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
