using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.ConvertTransfer;
using Binance.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net.CommonObjects;
using System;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceRestClientSpotApiTrading : IBinanceRestClientSpotApiTrading
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
        public async Task<WebCallResult<BinanceTestOrderCommission>> PlaceTestOrderAsync(string symbol,
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
                return new WebCallResult<BinanceTestOrderCommission>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;
            quoteQuantity = rulesCheck.QuoteQuantity;

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new SpotOrderTypeConverter(false)) }
            };
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.AddOptionalParameter("strategyId", strategyId);
            parameters.AddOptionalParameter("strategyType", strategyType);
            parameters.AddOptional("computeCommissionRates", computeFeeRates?.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            parameters.AddOptionalParameter("selfTradePreventionMode", EnumConverter.GetString(selfTradePreventionMode));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = computeFeeRates == true ? 20 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/order/test", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<BinanceTestOrderCommission>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region New Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceOrderAsync(string symbol,
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
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await _baseClient.PlaceOrderInternal(_baseClient.GetUrl("order", "api", "3"),
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
                receiveWindow,
                1,
                BinanceExchange.RateLimiter.SpotRestUid,
                ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId() { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderBase>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, CancelRestriction? cancelRestriction = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("cancelRestrictions", EnumConverter.GetString(cancelRestriction));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v3/order", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result = await _baseClient.SendAsync<BinanceOrderBase>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                    _baseClient.InvokeOrderCanceled(new OrderId() { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        #endregion

        #region Cancel all Open Orders on a Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v3/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrderBase>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Replace order
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceReplaceOrderResult>> ReplaceOrderAsync(string symbol,
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
                return new WebCallResult<BinanceReplaceOrderResult>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;
            quoteQuantity = rulesCheck.QuoteQuantity;

            string clientOrderId = newClientOrderId ?? ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new SpotOrderTypeConverter(false)) },
                { "cancelReplaceMode", EnumConverter.GetString(cancelReplaceMode) }
            };
            parameters.AddOptionalParameter("cancelNewClientOrderId", newCancelClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("cancelOrderId", cancelOrderId);
            parameters.AddOptionalParameter("strategyId", strategyId);
            parameters.AddOptionalParameter("strategyType", strategyType);
            parameters.AddOptionalParameter("cancelOrigClientOrderId", cancelClientOrderId);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.AddOptionalParameter("cancelRestrictions", EnumConverter.GetString(cancelRestriction));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/order/cancelReplace", BinanceExchange.RateLimiter.SpotRestIp, 4, true);
            var result = await _baseClient.SendAsync<BinanceReplaceOrderResult>(request, parameters, ct).ConfigureAwait(false);

            if (result && result.Data.NewOrderResult == OrderOperationResult.Success)
                _baseClient.InvokeOrderPlaced(new OrderId() { SourceObject = result.Data, Id = result.Data.NewOrderResponse!.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }
        #endregion
         
        #region Query Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/order", BinanceExchange.RateLimiter.SpotRestIp, 4, true);
            return await _baseClient.SendAsync<BinanceOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Current Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/openOrders", BinanceExchange.RateLimiter.SpotRestIp, symbol == null ? 80 : 6, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region All Orders 

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/allOrders", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> PlaceOcoOrderAsync(string symbol,
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
                return new WebCallResult<BinanceOrderOcoList>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity!.Value;
            price = rulesCheck.Price!.Value;
            stopPrice = rulesCheck.StopPrice!.Value;

            limitClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);
            stopClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("limitStrategyId", limitStrategyId);
            parameters.AddOptionalParameter("limitStrategyType", limitStrategyType);
            parameters.AddOptionalParameter("trailingDelta", trailingDelta);
            parameters.AddOptionalParameter("stopStrategyId", stopStrategyId);
            parameters.AddOptionalParameter("stopStrategyType", stopStrategyType);
            parameters.AddOptionalParameter("selfTradePreventionMode", EnumConverter.GetString(selfTradePreventionMode));
            parameters.AddOptionalParameter("stopLimitPrice", stopLimitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("limitClientOrderId", limitClientOrderId);
            parameters.AddOptionalParameter("stopClientOrderId", stopClientOrderId);
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQuantity);
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQuantity);
            parameters.AddOptionalParameter("stopLimitTimeInForce", stopLimitTimeInForce == null ? null : JsonConvert.SerializeObject(stopLimitTimeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/order/oco", BinanceExchange.RateLimiter.SpotRestUid, 2, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> PlaceOcoOrderListAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            SpotOrderType aboveOrderType,
            SpotOrderType belowOrderType,

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
            aboveClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);
            belowClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "aboveType", EnumConverter.GetString(aboveOrderType) },
                { "belowType", EnumConverter.GetString(belowOrderType) },
            };

            parameters.AddOptional("aboveClientOrderId", aboveClientOrderId);
            parameters.AddOptional("aboveIcebergQty", aboveIcebergQuantity);
            parameters.AddOptional("abovePrice", abovePrice);
            parameters.AddOptional("aboveStopPrice", aboveStopPrice);
            parameters.AddOptional("aboveTrailingDelta", aboveTrailingDelta);
            parameters.AddOptionalEnum("aboveTimeInForce", aboveTimeInForce);
            parameters.AddOptional("aboveStrategyId", aboveStrategyId);
            parameters.AddOptional("aboveStrategyType", aboveStrategyType);

            parameters.AddOptional("belowClientOrderId", belowClientOrderId);
            parameters.AddOptional("belowIcebergQty", belowIcebergQuantity);
            parameters.AddOptional("belowPrice", belowPrice);
            parameters.AddOptional("belowStopPrice", belowStopPrice);
            parameters.AddOptional("belowTrailingDelta", belowTrailingDelta);
            parameters.AddOptionalEnum("belowTimeInForce", belowTimeInForce);
            parameters.AddOptional("belowStrategyId", belowStrategyId);
            parameters.AddOptional("belowStrategyType", belowStrategyType);

            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/orderList/oco", BinanceExchange.RateLimiter.SpotRestUid, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> CancelOcoOrderAsync(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v3/orderList", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> GetOcoOrderAsync(long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/orderList", BinanceExchange.RateLimiter.SpotRestIp, 4, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOcoOrdersAsync(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/allOrderList", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrderOcoList>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOpenOcoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/openOrderList", BinanceExchange.RateLimiter.SpotRestIp, 6, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrderOcoList>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OTO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> PlaceOtoOrderListAsync(
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
            workingClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);
            pendingClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("workingType", workingOrderType);
            parameters.AddEnum("workingSide", workingSide);
            parameters.Add("workingQuantity", workingQuantity);
            parameters.Add("workingPrice", workingPrice);
            parameters.Add("pendingQuantity", pendingQuantity);
            parameters.AddEnum("pendingSide", pendingSide);
            parameters.AddEnum("pendingType", pendingOrderType);

            parameters.AddOptional("listClientOrderId", listClientOrderId);
            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptional("workingClientOrderId", workingClientOrderId);
            parameters.AddOptional("workingIcebergQty", workingIcebergQuantity);
            parameters.AddOptionalEnum("workingTimeInForce", workingTimeInForce);
            parameters.AddOptional("workingStrategyId", workingStrategyId);
            parameters.AddOptional("workingStrategyType", workingStrategyType);

            parameters.AddOptional("pendingClientOrderId", pendingClientOrderId);
            parameters.AddOptional("pendingPrice", pendingPrice);
            parameters.AddOptional("pendingStopPrice", pendingStopPrice);
            parameters.AddOptional("pendingTrailingDelta", pendingTrailingDelta);
            parameters.AddOptional("pendingIcebergQty", pendingIcebergQuantity);
            parameters.AddOptionalEnum("pendingTimeInForce", pendingTimeInForce);
            parameters.AddOptional("pendingStrategyId", pendingStrategyId);
            parameters.AddOptional("pendingStrategyType", pendingStrategyType);

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/orderList/oto", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region New OTOCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> PlaceOtocoOrderListAsync(
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
            workingClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);
            pendingAboveClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);
            pendingBelowClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("workingType", workingOrderType);
            parameters.AddEnum("workingSide", workingSide);
            parameters.Add("workingQuantity", workingQuantity);
            parameters.Add("workingPrice", workingPrice);
            parameters.Add("pendingQuantity", pendingQuantity);
            parameters.AddEnum("pendingSide", pendingSide);
            parameters.AddEnum("pendingAboveType", pendingAboveOrderType);
            parameters.AddEnum("pendingBelowType", pendingBelowOrderType);

            parameters.AddOptional("listClientOrderId", listClientOrderId);
            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptional("workingClientOrderId", workingClientOrderId);
            parameters.AddOptional("workingIcebergQty", workingIcebergQuantity);
            parameters.AddOptionalEnum("workingTimeInForce", workingTimeInForce);
            parameters.AddOptional("workingStrategyId", workingStrategyId);
            parameters.AddOptional("workingStrategyType", workingStrategyType);

            parameters.AddOptional("pendingAboveClientOrderId", pendingAboveClientOrderId);
            parameters.AddOptional("pendingAbovePrice", pendingAbovePrice);
            parameters.AddOptional("pendingAboveStopPrice", pendingAboveStopPrice);
            parameters.AddOptional("pendingAboveTrailingDelta", pendingAboveTrailingDelta);
            parameters.AddOptional("pendingAboveIcebergQty", pendingAboveIcebergQuantity);
            parameters.AddOptionalEnum("pendingAboveTimeInForce", pendingAboveTimeInForce);
            parameters.AddOptional("pendingAboveStrategyId", pendingAboveStrategyId);
            parameters.AddOptional("pendingAboveStrategyType", pendingAboveStrategyType);

            parameters.AddOptional("pendingBelowClientOrderId", pendingBelowClientOrderId);
            parameters.AddOptional("pendingBelowPrice", pendingBelowPrice);
            parameters.AddOptional("pendingBelowStopPrice", pendingBelowStopPrice);
            parameters.AddOptional("pendingBelowTrailingDelta", pendingBelowTrailingDelta);
            parameters.AddOptional("pendingBelowIcebergQty", pendingBelowIcebergQuantity);
            parameters.AddOptionalEnum("pendingBelowTimeInForce", pendingBelowTimeInForce);
            parameters.AddOptional("pendingBelowStrategyId", pendingBelowStrategyId);
            parameters.AddOptional("pendingBelowStrategyType", pendingBelowStrategyType);

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/orderList/otoco", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get user trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTrade>>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/myTrades", BinanceExchange.RateLimiter.SpotRestIp, 20, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceTrade>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Margin Account New Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
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
            var result = await _baseClient.PlaceOrderInternal(_baseClient.GetUrl("margin/order", "sapi", "1"),
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
                receiveWindow,
                weight: 6,
                BinanceExchange.RateLimiter.SpotRestUid,
                ct).ConfigureAwait(false);

            if (result)
                _baseClient.InvokeOrderPlaced(new OrderId { Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        #endregion

        #region Margin Account Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderBase>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/margin/order", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            var result = await _baseClient.SendAsync<BinanceOrderBase>(request, parameters, ct).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        #endregion

        #region Margin Account Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/margin/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrderBase>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrder>> GetMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId should be provided");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/order", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Open Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenMarginOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            if (isIsolated == true && symbol == null)
                throw new ArgumentException("Symbol must be provided for isolated margin");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrder>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's All Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/allOrders", BinanceExchange.RateLimiter.SpotRestIp, 200, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceOrder>>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Account's Trade List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMarginUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/myTrades", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account New OCO Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> PlaceMarginOCOOrderAsync(string symbol,
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
                return new WebCallResult<BinanceMarginOrderOcoList>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity!.Value;
            price = rulesCheck.Price!.Value;
            stopPrice = rulesCheck.StopPrice!.Value;

            limitClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);
            stopClientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("stopLimitPrice", stopLimitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("sideEffectType", sideEffectType == null ? null : JsonConvert.SerializeObject(sideEffectType, new SideEffectTypeConverter(false)));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("limitClientOrderId", limitClientOrderId);
            parameters.AddOptionalParameter("stopClientOrderId", stopClientOrderId);
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopLimitTimeInForce", stopLimitTimeInForce == null ? null : JsonConvert.SerializeObject(stopLimitTimeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("autoRepayAtCancel", autoRepayAtCancel);
            parameters.AddOptionalParameter("selfTradePreventionMode", EnumConverter.GetString(selfTradePreventionMode));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/margin/order/oco", BinanceExchange.RateLimiter.SpotRestUid, 6, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/margin/orderList", BinanceExchange.RateLimiter.SpotRestUid, 1, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/orderList", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<BinanceMarginOrderOcoList>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/allOrderList", BinanceExchange.RateLimiter.SpotRestIp, 200, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceMarginOrderOcoList>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/margin/openOrderList", BinanceExchange.RateLimiter.SpotRestIp, 10, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceMarginOrderOcoList>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens

        #region Leveraged tokens subscribe

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBlvtSubscribeResult>> SubscribeLeveragedTokenAsync(string tokenName, decimal cost, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "tokenName", tokenName },
                { "cost", cost.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/blvt/subscribe", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceBlvtSubscribeResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Leveraged tokens subscription records

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtSubscription>>> GetLeveragedTokensSubscriptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("tokenName", tokenName);
            parameters.AddOptionalParameter("id", id?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/blvt/subscribe/record", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBlvtSubscription>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens Redeem 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBlvtRedeemResult>> RedeemLeveragedTokenAsync(string tokenName, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "tokenName", tokenName },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/blvt/redeem", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceBlvtRedeemResult > (request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Leveraged tokens redemption records

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtRedemption>>> GetLeveragedTokensRedemptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("tokenName", tokenName);
            parameters.AddOptionalParameter("id", id?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/blvt/redeem/record", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<IEnumerable<BinanceBlvtRedemption>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region C2C

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceC2CUserTrade>>> GetC2CTradeHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("tradeType", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("startTimestamp", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTimestamp", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("rows", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/c2c/orderMatch/listUserOrderHistory", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            var result =  await _baseClient.SendAsync<BinanceResult<IEnumerable<BinanceC2CUserTrade>>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<BinanceC2CUserTrade>>(default);

            if (result.Data?.Code != 0)
                return result.AsError<IEnumerable<BinanceC2CUserTrade>>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Pay

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePayTrade>>> GetPayTradeHistoryAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/pay/transactions", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            var result = await _baseClient.SendAsync<BinanceResult<IEnumerable<BinancePayTrade>>>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return result.As<IEnumerable<BinancePayTrade>>(default);

            if (result.Data?.Code != 0)
                return result.AsError<IEnumerable<BinancePayTrade>>(new ServerError(result.Data!.Code, result.Data!.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Convert

        #region Convert Quote Request

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertQuote>> ConvertQuoteRequestAsync(string quoteAsset, string baseAsset, decimal? quoteQuantity = null, decimal? baseQuantity = null, WalletType? walletType = null, ValidTime? validTime = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (quoteQuantity == null && baseQuantity == null || quoteQuantity != null && baseQuantity != null)
                throw new ArgumentException("Either quoteQuantity or baseQuantity must be sent, but not both");

            var parameters = new ParameterCollection();
            parameters.AddParameter("fromAsset", quoteAsset);
            parameters.AddParameter("toAsset", baseAsset);
            parameters.AddOptionalParameter("fromAmount", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("toAmount", baseQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("walletType", walletType != null ? JsonConvert.SerializeObject(walletType, new WalletTypeConverter(false)) : null);
            parameters.AddOptionalParameter("validTime", EnumConverter.GetString(validTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/convert/getQuote", BinanceExchange.RateLimiter.SpotRestUid, 200, true);
            return await _baseClient.SendAsync<BinanceConvertQuote>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Convert Accept Quote

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertResult>> ConvertAcceptQuoteAsync(string quoteId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("quoteId", quoteId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/convert/acceptQuote", BinanceExchange.RateLimiter.SpotRestUid, 500, true);
            return await _baseClient.SendAsync<BinanceConvertResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Order Status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertOrderStatus>> GetConvertOrderStatusAsync(string? orderId = null, string? quoteId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && quoteId == null || orderId != null && quoteId != null)
                throw new ArgumentException("Either orderId or quoteId must be sent, but not both");

            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("quoteId", quoteId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/convert/orderStatus", BinanceExchange.RateLimiter.SpotRestUid, 100, true);
            return await _baseClient.SendAsync<BinanceConvertOrderStatus>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Trade History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceListResult<BinanceConvertTrade>>> GetConvertTradeHistoryAsync(DateTime startTime, DateTime endTime, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/convert/tradeFlow", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceListResult<BinanceConvertTrade>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Get Prevented Trades
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePreventedTrade>>> GetPreventedTradesAsync(string symbol, long? preventedMatchId = null, long? orderId = null, long? fromPreventedMatchId = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("preventedMatchId", preventedMatchId);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("fromPreventedMatchId", fromPreventedMatchId);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = preventedMatchId == null ? 20 : 2;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/myPreventedMatches", BinanceExchange.RateLimiter.SpotRestIp, weight, true);
            return await _baseClient.SendAsync<IEnumerable<BinancePreventedTrade>>(request, parameters, ct, weight).ConfigureAwait(false);
        }
        #endregion

        #region Spot Algo

        #region Place TWAP
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrderResult>> PlaceTimeWeightedAveragePriceOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            int duration,
            string? clientOrderId = null,
            decimal? limitPrice = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            clientOrderId ??= ExchangeHelpers.AppendRandomString(_baseClient._brokerId, 32);

            var parameters = new ParameterCollection()
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "duration", duration },
            };
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "sapi/v1/algo/spot/newOrderTwap", BinanceExchange.RateLimiter.SpotRestUid, 3000, true);
            return await _baseClient.SendAsync<BinanceAlgoOrderResult>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "sapi/v1/algo/spot/order", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoResult>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/algo/spot/openOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoOrders>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get historical Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/algo/spot/historicalOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoOrders>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "sapi/v1/algo/spot/subOrders", BinanceExchange.RateLimiter.SpotRestIp, 1, true);
            return await _baseClient.SendAsync<BinanceAlgoSubOrderList>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion
        #endregion
    }
}
