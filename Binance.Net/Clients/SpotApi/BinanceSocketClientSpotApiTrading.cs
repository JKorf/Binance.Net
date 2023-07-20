using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Converters;
using System.Globalization;
using System;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects;
using Microsoft.Extensions.Logging;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceSocketClientSpotApiTrading : IBinanceSocketClientSpotApiTrading
    {
        private readonly BinanceSocketClientSpotApi _client;
        private readonly ILogger _logger;

        #region constructor/destructor

        internal BinanceSocketClientSpotApiTrading(ILogger logger, BinanceSocketClientSpotApi client)
        {
            _logger = logger;
            _client = client;
        }

        #endregion

        #region Queries

        #region Place Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinancePlacedOrder>>> PlaceOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null)
        {
            // Check trade rules
            var rulesCheck = await _client.CheckTradeRules(symbol, quantity, quoteQuantity, price, stopPrice, type).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new CallResult<BinanceResponse<BinancePlacedOrder>>(new ArgumentError(rulesCheck.ErrorMessage!));
            }

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", EnumConverter.GetString(side));
            parameters.AddParameter("type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("timeInForce", EnumConverter.GetString(timeInForce));
            parameters.AddOptionalParameter("price", rulesCheck.Price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quantity", rulesCheck.Quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", rulesCheck.QuoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("stopPrice", rulesCheck.StopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailingDelta", trailingDelta?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("strategyId", strategyId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("strategyType", strategyType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("selfTradePreventionMode", EnumConverter.GetString(selfTradePreventionMode));
            return await _client.QueryAsync<BinancePlacedOrder>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"order.place", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Place Test Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinancePlacedOrder>>> PlaceTestOrderAsync(string symbol,
            OrderSide side,
            SpotOrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            int? trailingDelta = null,
            int? strategyId = null,
            int? strategyType = null,
            SelfTradePreventionMode? selfTradePreventionMode = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddParameter("side", EnumConverter.GetString(side));
            parameters.AddParameter("type", EnumConverter.GetString(type));
            parameters.AddOptionalParameter("timeInForce", EnumConverter.GetString(timeInForce));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("trailingDelta", trailingDelta?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("strategyId", strategyId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("strategyType", strategyType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("selfTradePreventionMode", EnumConverter.GetString(selfTradePreventionMode));
            return await _client.QueryAsync<BinancePlacedOrder>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"order.test", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceOrder>>> GetOrderAsync(string symbol, int? orderId = null, string? clientOrderId = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("origClientOrderId", clientOrderId);
            return await _client.QueryAsync<BinanceOrder>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"order.status", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceOrder>>> CancelOrderAsync(string symbol, int? orderId = null, string? clientOrderId = null, string? newClientOrderId = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            return await _client.QueryAsync<BinanceOrder>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"order.cancel", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Replace Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceReplaceOrderResult>>> ReplaceOrderAsync(string symbol,
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
            int? strategyType = null)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", StaticConverters.OrderSideConverter(ref side) },
                { "type", StaticConverters.OrderTypeConverter(ref type) },
                { "cancelReplaceMode", EnumConverter.GetString(cancelReplaceMode) }
            };
            parameters.AddOptionalParameter("cancelNewClientOrderId", newCancelClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("cancelOrderId", cancelOrderId);
            parameters.AddOptionalParameter("strategyId", strategyId);
            parameters.AddOptionalParameter("strategyType", strategyType);
            parameters.AddOptionalParameter("cancelOrigClientOrderId", cancelClientOrderId);
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : StaticConverters.TimeInForceConverter(ref timeInForce));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : StaticConverters.OrderResponseTypeConverter(ref orderResponseType));
            parameters.AddOptionalParameter("trailingDelta", trailingDelta?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceReplaceOrderResult>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"order.cancelReplace", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceOrder>>>> GetOpenOrdersAsync(string? symbol = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            return await _client.QueryAsync<IEnumerable<BinanceOrder>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"openOrders.status", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceOrder>>>> CancelAllOrdersAsync(string symbol)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            return await _client.QueryAsync<IEnumerable<BinanceOrder>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"openOrders.cancelAll", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Place Oco Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> PlaceOcoOrderAsync(string symbol,
            OrderSide side,
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
            decimal? limitIcebergQty = null,
            int? stopStrategyId = null,
            int? stopStrategyType = null,
            int? stopIcebergQty = null,
            SelfTradePreventionMode? selfTradePreventionMode = null)
        {
            symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", StaticConverters.OrderSideConverter(ref side) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
                { "stopPrice", stopPrice.ToString(CultureInfo.InvariantCulture) }
            };

            parameters.AddOptionalParameter("limitStrategyId", limitStrategyId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limitStrategyType", limitStrategyType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limitClientOrderId", limitClientOrderId);
            parameters.AddOptionalParameter("limitIcebergQty", limitIcebergQuantity?.ToString(CultureInfo.InvariantCulture));

            parameters.AddOptionalParameter("trailingDelta", trailingDelta?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("selfTradePreventionMode", EnumConverter.GetString(selfTradePreventionMode));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);

            parameters.AddOptionalParameter("stopLimitPrice", stopLimitPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopStrategyId", stopStrategyId);
            parameters.AddOptionalParameter("stopStrategyType", stopStrategyType);
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopIcebergQty", stopIcebergQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopClientOrderId", stopClientOrderId);
            parameters.AddOptionalParameter("stopLimitTimeInForce", stopLimitTimeInForce == null ? null : StaticConverters.TimeInForceConverter(ref stopLimitTimeInForce));

            return await _client.QueryAsync<BinanceOrderOcoList>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"orderList.place", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Oco Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> GetOcoOrderAsync(long? orderId = null, string? clientOrderId = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("orderListId", orderId);
            parameters.AddOptionalParameter("origClientOrderId", clientOrderId);
            return await _client.QueryAsync<BinanceOrderOcoList>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"orderList.status", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Oco Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceOrderOcoList>>> CancelOcoOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, string? newClientOrderId = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderListId", orderId);
            parameters.AddOptionalParameter("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);
            return await _client.QueryAsync<BinanceOrderOcoList>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"orderList.cancel", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Oco Orders

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceOrderOcoList>>>> GetOpenOcoOrdersAsync()
        {
            return await _client.QueryAsync<IEnumerable<BinanceOrderOcoList>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"openOrderLists.status", new Dictionary<string, object>(), true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Order History

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceOrder>>>> GetOrdersAsync(string symbol, long? fromOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", fromOrderId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<IEnumerable<BinanceOrder>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"allOrders", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get OCO Order History

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceOrderOcoList>>>> GetOcoOrdersAsync(long? fromOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("fromId", fromOrderId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<IEnumerable<BinanceOrderOcoList>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"allOrderLists", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinanceTrade>>>> GetUserTradesAsync(string symbol, long? orderId = null, long? fromOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("fromId", fromOrderId);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<IEnumerable<BinanceTrade>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"myTrades", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinancePreventedTrade>>>> GetPreventedTradesAsync(string symbol, long? preventedTradeId = null, long? orderId = null, long? fromPreventedTradeId = null, int? limit = null)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("symbol", symbol);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("preventedMatchId", preventedTradeId);
            parameters.AddOptionalParameter("fromPreventedMatchId", fromPreventedTradeId);
            parameters.AddOptionalParameter("limit", limit);
            return await _client.QueryAsync<IEnumerable<BinancePreventedTrade>>(_client.ClientOptions.Environment.SpotSocketApiAddress.AppendPath("ws-api/v3"), $"myPreventedMatches", parameters, true, true).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
