using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.CommonObjects;
using System.Drawing;
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
        public async Task<WebCallResult<BinanceFuturesOrder>> PlaceOrderAsync(
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
                return new WebCallResult<BinanceFuturesOrder>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;

            var clientOrderId = LibraryHelpers.ApplyBrokerId(newClientOrderId, BinanceExchange.ClientOrderIdFutures, 36, _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
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
            parameters.AddOptionalEnum("priceMatch", priceMatch);
            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("priceProtect", priceProtect?.ToString().ToUpper());

            var request = _definitions.GetOrCreate(HttpMethod.Post, "dapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 0, true);
            var result = await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }


        #endregion

        #region Multiple New Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceFuturesOrder>>>> PlaceMultipleOrdersAsync(
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
                        return new WebCallResult<IEnumerable<CallResult<BinanceFuturesOrder>>>(new ArgumentError(rulesCheck.ErrorMessage!));
                    }

                    order.Quantity = rulesCheck.Quantity;
                    order.Price = rulesCheck.Price;
                    order.StopPrice = rulesCheck.StopPrice;
                }
            }

            var parameters = new ParameterCollection();

            var parameterOrders = new ParameterCollection[orders.Count()];
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new ParameterCollection()
                {
                    { "symbol", order.Symbol },
                    { "newOrderRespType", "RESULT" }
                };

                orderParameters.AddEnum("side", order.Side);
                orderParameters.AddEnum("type", order.Type);
                var clientOrderId = LibraryHelpers.ApplyBrokerId(order.NewClientOrderId, BinanceExchange.ClientOrderIdFutures, 36, _baseClient.ClientOptions.AllowAppendingClientOrderId);
                orderParameters.AddOptionalParameter("quantity", order.Quantity?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", clientOrderId);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalEnum("timeInForce", order.TimeInForce);
                orderParameters.AddOptionalEnum("positionSide", order.PositionSide);
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalEnum("workingType", order.WorkingType);
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                orderParameters.AddOptionalParameter("priceProtect", order.PriceProtect?.ToString().ToUpper());
                orderParameters.AddOptionalEnum("selfTradePreventionMode", order.SelfTradePreventionMode);
                orderParameters.AddOptionalEnum("priceMatch", order.PriceMatch);
                parameterOrders[i] = orderParameters;
                i++;
            }

            parameters.Add("batchOrders", JsonSerializer.Serialize(parameterOrders));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "dapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 5, true);
            var response = await _baseClient.SendAsync<IEnumerable<BinanceFuturesMultipleOrderPlaceResult>>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return response.As<IEnumerable<CallResult<BinanceFuturesOrder>>>(default);

            var result = new List<CallResult<BinanceFuturesOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<BinanceFuturesOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<BinanceFuturesOrder>(item));
            }

            return response.As<IEnumerable<CallResult<BinanceFuturesOrder>>>(result);
        }

        #endregion

        #region Query Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 36, _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "dapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }


        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesOrder>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 36, _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "dapi/v1/order", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            var result = await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "dapi/v1/allOpenOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesCancelAllOrders>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Post, "dapi/v1/countdownCancelAll", BinanceExchange.RateLimiter.FuturesRest, 10, true);
            return await _baseClient.SendAsync<BinanceFuturesCountDownResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CallResult<BinanceFuturesOrder>>>> CancelMultipleOrdersAsync(string symbol, IEnumerable<long>? orderIdList = null, IEnumerable<string>? origClientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderIdList == null && origClientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or origClientOrderIdList must be sent");

            if (orderIdList?.Count() > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (origClientOrderIdList?.Count() > 10)
                throw new ArgumentException("origClientOrderIdList cannot contain more than 10 items");

            var convertClientOrderIdList = origClientOrderIdList?.Select(x => LibraryHelpers.ApplyBrokerId(x, BinanceExchange.ClientOrderIdFutures, 36, _baseClient.ClientOptions.AllowAppendingClientOrderId));

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (origClientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", origClientOrderIdList.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "dapi/v1/batchOrders", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            var response = await _baseClient.SendAsync<IEnumerable<BinanceFuturesMultipleOrderCancelResult>>(request, parameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return response.As<IEnumerable<CallResult<BinanceFuturesOrder>>>(default);

            var result = new List<CallResult<BinanceFuturesOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<BinanceFuturesOrder>(new ServerError(item.Code, item.Message))
                    : new CallResult<BinanceFuturesOrder>(item));
            }

            return response.As<IEnumerable<CallResult<BinanceFuturesOrder>>>(result);
        }

        #endregion

        #region Query Current Open Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceFuturesOrder>> GetOpenOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
                origClientOrderId = LibraryHelpers.ApplyBrokerId(origClientOrderId, BinanceExchange.ClientOrderIdFutures, 36, _baseClient.ClientOptions.AllowAppendingClientOrderId);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "dapi/v1/openOrder", BinanceExchange.RateLimiter.FuturesRest, 1, true);
            return await _baseClient.SendAsync<BinanceFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Current All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            var weight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "dapi/v1/openOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesOrder>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region All Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetOrdersAsync(string? symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 40 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "dapi/v1/allOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesOrder>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region User's Force Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesOrder>>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, DateTime? startTime = null, DateTime? endTime = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalEnum("autoCloseType", closeType);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var weight = symbol == null ? 50 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "dapi/v1/forceOrders", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesOrder>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Account Trade List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceFuturesCoinTrade>>> GetUserTradesAsync(string? symbol = null, string? pair = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? orderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 40 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "dapi/v1/userTrades", BinanceExchange.RateLimiter.FuturesRest, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceFuturesCoinTrade>>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion
    }
}
