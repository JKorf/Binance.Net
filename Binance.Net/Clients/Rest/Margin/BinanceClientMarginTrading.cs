using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.Rest.Margin;
using Binance.Net.Objects.Shared;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.Rest.Margin
{
    /// <summary>
    /// Spot system endpoints
    /// </summary>
    public class BinanceClientMarginTrading : IBinanceClientMarginTrading
    {
        private const string marginApi = "sapi";
        private const string marginVersion = "1";

        // Margin
        private const string newMarginOrderEndpoint = "margin/order";
        private const string cancelMarginOrderEndpoint = "margin/order";
        private const string myMarginTradesEndpoint = "margin/myTrades";
        private const string allMarginOrdersEndpoint = "margin/allOrders";
        private const string openMarginOrdersEndpoint = "margin/openOrders";
        private const string cancelOpenMarginOrdersEndpoint = "margin/openOrders";
        private const string queryMarginOrderEndpoint = "margin/order";

        // OCO
        private const string newMarginOCOOrderEndpoint = "margin/order/oco";
        private const string cancelMarginOCOOrderEndpoint = "margin/orderList";
        private const string getMarginOCOOrderEndpoint = "margin/orderList";
        private const string allMarginOCOOrderEndpoint = "margin/allOrderList";
        private const string openMarginOCOOrderEndpoint = "margin/openOrderList";


        private readonly Log _log;

        private readonly BinanceClientMargin _baseClient;

        internal BinanceClientMarginTrading(Log log, BinanceClientMargin baseClient)
        {
            _log = log;
            _baseClient = baseClient;
        }


        #region Margin Account New Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceMarginOrderAsync(string symbol,
            OrderSide side,
            OrderType type,
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
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var result = await _baseClient.PlaceOrderInternal(_baseClient.GetUrl(newMarginOrderEndpoint, marginApi, marginVersion),
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
                receiveWindow,
                ct).ConfigureAwait(false);

            if (result)
                _baseClient.InvokeOrderPlaced(result.Data);
            return result;
        }

        #endregion

        #region Margin Account Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderBase>> CancelMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, string? newClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceOrderBase>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceOrderBase>(_baseClient.GetUrl(cancelMarginOrderEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(result.Data);
            return result;
        }

        #endregion

        #region Margin Account Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelOpenMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceOrderBase>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrderBase>>(_baseClient.GetUrl(cancelOpenMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrder>> GetMarginAccountOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId should be provided");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceOrder>(_baseClient.GetUrl(queryMarginOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Open Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginAccountOpenOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol?.ValidateBinanceSymbol();
            if (isIsolated == true && symbol == null)
                throw new ArgumentException("Symbol must be provided for isolated margin");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrl(openMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's All Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginAccountOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceOrder>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrl(allMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Account's Trade List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMarginAccountUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceTrade>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime.HasValue ? JsonConvert.SerializeObject(startTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime.HasValue ? JsonConvert.SerializeObject(endTime.Value, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceTrade>>(_baseClient.GetUrl(myMarginTradesEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Margin Account New OCO Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> PlaceMarginOCOOrderAsync(string symbol,
            OrderSide side,
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
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginOrderOcoList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, price, stopPrice, null, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _log.Write(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinanceMarginOrderOcoList>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity!.Value;
            price = rulesCheck.Price!.Value;
            stopPrice = rulesCheck.StopPrice!.Value;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) },
                { "timestamp", _baseClient.GetTimestamp() }
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
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrl(newMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginOrderOcoList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrl(cancelMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinanceMarginOrderOcoList>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrl(getMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", startTime != null ? JsonConvert.SerializeObject(startTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? JsonConvert.SerializeObject(endTime, new TimestampConverter()) : null);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginOrderOcoList>>(_baseClient.GetUrl(allMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await _baseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", _baseClient.GetTimestamp() }
            };
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginOrderOcoList>>(_baseClient.GetUrl(openMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion
    }
}
