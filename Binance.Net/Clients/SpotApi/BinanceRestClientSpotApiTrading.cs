﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.ExtensionMethods;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Futures.AlgoOrders;
using Binance.Net.Objects.Models.Spot;
using Binance.Net.Objects.Models.Spot.Blvt;
using Binance.Net.Objects.Models.Spot.BSwap;
using Binance.Net.Objects.Models.Spot.Convert;
using Binance.Net.Objects.Models.Spot.ConvertTransfer;
using Binance.Net.Objects.Models.Spot.Margin;
using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Binance.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BinanceRestClientSpotApiTrading : IBinanceRestClientSpotApiTrading
    {
        private const string api = "api";
        private const string signedVersion = "3";

        private const string marginApi = "sapi";
        private const string marginVersion = "1";

        private const string BlvtApi = "sapi";
        private const string blvtVersion = "1";

        private const string bSwapApi = "sapi";
        private const string bSwapVersion = "1";

        private const string convertApi = "sapi";
        private const string convertVersion = "1";

        // Orders
        private const string openOrdersEndpoint = "openOrders";
        private const string allOrdersEndpoint = "allOrders";
        private const string newOrderEndpoint = "order";
        private const string cancelReplaceOrderEndpoint = "order/cancelReplace";
        private const string newTestOrderEndpoint = "order/test";
        private const string queryOrderEndpoint = "order";
        private const string cancelOrderEndpoint = "order";
        private const string cancelAllOpenOrderEndpoint = "openOrders";
        private const string myTradesEndpoint = "myTrades";

        // OCO orders
        private const string newOcoOrderEndpoint = "order/oco";
        private const string cancelOcoOrderEndpoint = "orderList";
        private const string getOcoOrderEndpoint = "orderList";
        private const string getAllOcoOrderEndpoint = "allOrderList";
        private const string getOpenOcoOrderEndpoint = "openOrderList";

        // Margin
        private const string newMarginOrderEndpoint = "margin/order";
        private const string cancelMarginOrderEndpoint = "margin/order";
        private const string myMarginTradesEndpoint = "margin/myTrades";
        private const string allMarginOrdersEndpoint = "margin/allOrders";
        private const string openMarginOrdersEndpoint = "margin/openOrders";
        private const string cancelOpenMarginOrdersEndpoint = "margin/openOrders";
        private const string queryMarginOrderEndpoint = "margin/order";

        // Margin OCO
        private const string newMarginOCOOrderEndpoint = "margin/order/oco";
        private const string cancelMarginOCOOrderEndpoint = "margin/orderList";
        private const string getMarginOCOOrderEndpoint = "margin/orderList";
        private const string allMarginOCOOrderEndpoint = "margin/allOrderList";
        private const string openMarginOCOOrderEndpoint = "margin/openOrderList";

        // Blvt
        private const string blvtSubscribeEndpoint = "blvt/subscribe";
        private const string blvtRedeemEndpoint = "blvt/redeem";
        private const string blvtSubscriptionRecordsEndpoint = "blvt/subscribe/record";
        private const string blvtRedeemRecordsEndpoint = "blvt/redeem/record";

        // BSwap        
        private const string bSwapPoolLiquidityEndpoint = "bswap/liquidity";
        private const string bSwapAddLiquidityEndpoint = "bswap/liquidityAdd";
        private const string bSwapRemoveLiquidityEndpoint = "bswap/liquidityRemove";
        private const string bSwapLiquidityOperationsEndpoint = "bswap/liquidityOps";
        private const string bSwapQuoteEndpoint = "bswap/quote";
        private const string bSwapSwapEndpoint = "bswap/swap";
        private const string bSwapSwapRecordsEndpoint = "bswap/swap";
        private const string bSwapAddLiquidityPreviewEndpoint = "bswap/addLiquidityPreview";
        private const string bSwapRemoveLiquidityPreviewEndpoint = "bswap/removeLiquidityPreview";

        // C2C
        private const string c2cTradeHistoryEndpoint = "c2c/orderMatch/listUserOrderHistory";

        // Pay
        private const string payTradeHistoryEndpoint = "pay/transactions";

        // Convert
        private const string convertQuoteRequestEndpoint = "convert/getQuote";
        private const string convertAcceptQuoteEndpoint = "convert/acceptQuote";
        private const string convertOrderStatusEndpoint = "convert/orderStatus";
        private const string convertTradeHistoryEndpoint = "convert/tradeFlow";

        // Convert transfer
        private const string convertTransferEndpoint = "asset/convert-transfer";
        private const string convertTransferHistoryEndpoint = "asset/convert-transfer/queryByPage";

        private readonly BinanceRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal BinanceRestClientSpotApiTrading(ILogger logger, BinanceRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Test New Order 

        /// <inheritdoc />
        public async Task<WebCallResult<BinancePlacedOrder>> PlaceTestOrderAsync(string symbol,
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
            return await _baseClient.PlaceOrderInternal(_baseClient.GetUrl(newTestOrderEndpoint, api, signedVersion),
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
                ct).ConfigureAwait(false);
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
            var result = await _baseClient.PlaceOrderInternal(_baseClient.GetUrl(newOrderEndpoint, api, signedVersion),
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
            symbol.ValidateBinanceSymbol();
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("cancelRestrictions", EnumConverter.GetString(cancelRestriction));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceOrderBase>(_baseClient.GetUrl(cancelOrderEndpoint, api, signedVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
            if (result)
                    _baseClient.InvokeOrderCanceled(new OrderId() { SourceObject = result.Data, Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        #endregion

        #region Cancel all Open Orders on a Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllOrdersAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrderBase>>(_baseClient.GetUrl(cancelAllOpenOrderEndpoint, api, signedVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
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
            symbol.ValidateBinanceSymbol();

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

            var parameters = new Dictionary<string, object>
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

            var result = await _baseClient.SendRequestInternal<BinanceReplaceOrderResult>(_baseClient.GetUrl(cancelReplaceOrderEndpoint, api, signedVersion), HttpMethod.Post, ct, parameters, true, weight: 1).ConfigureAwait(false);
           
            if (result && result.Data.NewOrderResult == OrderOperationResult.Success)
                _baseClient.InvokeOrderPlaced(new OrderId() { SourceObject = result.Data, Id = result.Data.NewOrderResponse!.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }
        #endregion
         
        #region Query Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrder>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceOrder>(_baseClient.GetUrl(queryOrderEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Current Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenOrdersAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol?.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrl(openOrdersEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: symbol == null ? 40 : 3).ConfigureAwait(false);
        }

        #endregion

        #region All Orders 

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrl(allOrdersEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
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
            symbol.ValidateBinanceSymbol();

            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, null, price, stopPrice, null, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinanceOrderOcoList>(new ArgumentError(rulesCheck.ErrorMessage!));
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

            return await _baseClient.SendRequestInternal<BinanceOrderOcoList>(_baseClient.GetUrl(newOcoOrderEndpoint, api, signedVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> CancelOcoOrderAsync(string symbol, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceOrderOcoList>(_baseClient.GetUrl(cancelOcoOrderEndpoint, api, signedVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrderOcoList>> GetOcoOrderAsync(long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceOrderOcoList>(_baseClient.GetUrl(getOcoOrderEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 2).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOcoOrdersAsync(long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrderOcoList>>(_baseClient.GetUrl(getAllOcoOrderEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderOcoList>>> GetOpenOcoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrderOcoList>>(_baseClient.GetUrl(getOpenOcoOrderEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 3).ConfigureAwait(false);
        }

        #endregion

        #region Get user trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTrade>>> GetUserTradesAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceTrade>>(_baseClient.GetUrl(myTradesEndpoint, api, signedVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
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
                null,
                null,
                null,
                selfTradePreventionMode,
                autoRepayAtCancel,
                receiveWindow,
                weight: 6,
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
            symbol.ValidateBinanceSymbol();

            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceOrderBase>(_baseClient.GetUrl(cancelMarginOrderEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true, weight: 10).ConfigureAwait(false);
            if (result)
                _baseClient.InvokeOrderCanceled(new OrderId { Id = result.Data.Id.ToString(CultureInfo.InvariantCulture) });
            return result;
        }

        #endregion

        #region Margin Account Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrderBase>>> CancelAllMarginOrdersAsync(string symbol, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrderBase>>(_baseClient.GetUrl(cancelOpenMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Order

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceOrder>> GetMarginOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId should be provided");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceOrder>(_baseClient.GetUrl(queryMarginOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's Open Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetOpenMarginOrdersAsync(string? symbol = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol?.ValidateBinanceSymbol();
            if (isIsolated == true && symbol == null)
                throw new ArgumentException("Symbol must be provided for isolated margin");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated);

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrl(openMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Query Margin Account's All Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceOrder>>> GetMarginOrdersAsync(string symbol, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, bool? isIsolated = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 500);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceOrder>>(_baseClient.GetUrl(allMarginOrdersEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 200).ConfigureAwait(false);
        }
        #endregion

        #region Query Margin Account's Trade List

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceTrade>>> GetMarginUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? fromId = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceTrade>>(_baseClient.GetUrl(myMarginTradesEndpoint, "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
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
            symbol.ValidateBinanceSymbol();
            var rulesCheck = await _baseClient.CheckTradeRules(symbol, quantity, null, price, stopPrice, null, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                _logger.Log(LogLevel.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinanceMarginOrderOcoList>(new ArgumentError(rulesCheck.ErrorMessage!));
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

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrl(newMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Post, ct, parameters, true, weight: 6).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> CancelMarginOcoOrderAsync(string symbol, bool? isIsolated = null, long? orderListId = null, string? listClientOrderId = null, string? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            if (!orderListId.HasValue && string.IsNullOrEmpty(listClientOrderId))
                throw new ArgumentException("Either orderListId or listClientOrderId must be sent");

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("listClientOrderId", listClientOrderId);
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrl(cancelMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Delete, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Query OCO

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceMarginOrderOcoList>> GetMarginOcoOrderAsync(string? symbol = null, bool? isIsolated = null, long? orderListId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderListId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderListId or origClientOrderId must be sent");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated.ToString());
            parameters.AddOptionalParameter("orderListId", orderListId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceMarginOrderOcoList>(_baseClient.GetUrl(getMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Query all OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (fromId != null && (startTime != null || endTime != null))
                throw new ArgumentException("Start/end time can only be provided without fromId parameter");

            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginOrderOcoList>>(_baseClient.GetUrl(allMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 200).ConfigureAwait(false);
        }

        #endregion

        #region Query Open OCO

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceMarginOrderOcoList>>> GetMarginOpenOcoOrdersAsync(string? symbol = null, bool? isIsolated = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("isIsolated", isIsolated?.ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceMarginOrderOcoList>>(_baseClient.GetUrl(openMarginOCOOrderEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens

        #region Leveraged tokens subscribe

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBlvtSubscribeResult>> SubscribeLeveragedTokenAsync(string tokenName, decimal cost, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "tokenName", tokenName },
                { "cost", cost.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBlvtSubscribeResult>(_baseClient.GetUrl(blvtSubscribeEndpoint, BlvtApi, blvtVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Leveraged tokens subscription records

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtSubscription>>> GetLeveragedTokensSubscriptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("tokenName", tokenName);
            parameters.AddOptionalParameter("id", id?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBlvtSubscription>>(_baseClient.GetUrl(blvtSubscriptionRecordsEndpoint, BlvtApi, blvtVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Leveraged tokens Redeem 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBlvtRedeemResult>> RedeemLeveragedTokenAsync(string tokenName, decimal quantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "tokenName", tokenName },
                { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBlvtRedeemResult>(_baseClient.GetUrl(blvtRedeemEndpoint, BlvtApi, blvtVersion), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Get Leveraged tokens redemption records

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBlvtRedemption>>> GetLeveragedTokensRedemptionRecordsAsync(string? tokenName = null, long? id = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("tokenName", tokenName);
            parameters.AddOptionalParameter("id", id?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBlvtRedemption>>(_baseClient.GetUrl(blvtRedeemRecordsEndpoint, BlvtApi, blvtVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Liquidity pools

        #region Add liquid pool liquidity

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapOperationResult>> AddToLiquidityPoolAsync(int poolId, string asset, decimal quantity, LiquidityType? type = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"asset", asset},
                {"quantity", quantity.ToString(CultureInfo.InvariantCulture)}
            };
            parameters.AddOptionalParameter("type", type == null ? null : JsonConvert.SerializeObject(type.Value, new LiquidityTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapOperationResult>(_baseClient.GetUrl(bSwapAddLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Remove liquid pool liquidity

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapOperationResult>> RemoveFromLiquidityPoolAsync(int poolId, string asset, LiquidityType type, decimal shareQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"asset", asset},
                {"type", JsonConvert.SerializeObject(type, new LiquidityTypeConverter(false))},
                {"shareAmount", shareQuantity.ToString(CultureInfo.InvariantCulture)}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapOperationResult>(_baseClient.GetUrl(bSwapRemoveLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Get liquid pool liquidity operation records

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapOperation>>> GetLiquidityPoolOperationRecordsAsync(long? operationId = null, int? poolId = null, BSwapOperation? operation = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("operationId", operationId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("poolId", poolId);
            parameters.AddOptionalParameter("operation", operation.HasValue ? JsonConvert.SerializeObject(new BSwapOperationConverter(false)) : null);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapOperation>>(_baseClient.GetUrl(bSwapLiquidityOperationsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }

        #endregion

        #region Request liquid pool swap quote

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapQuote>> GetLiquidityPoolSwapQuoteAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"quoteAsset", quoteAsset},
                {"baseAsset", baseAsset},
                {"quoteQty", quoteQuantity.ToString(CultureInfo.InvariantCulture)}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapQuote>(_baseClient.GetUrl(bSwapQuoteEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #region Liquid pool swap 

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapResult>> LiquidityPoolSwapAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"quoteAsset", quoteAsset},
                {"baseAsset", baseAsset},
                {"quoteQty",quoteQuantity.ToString(CultureInfo.InvariantCulture)}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapResult>(_baseClient.GetUrl(bSwapSwapEndpoint, bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Get liquid pool swap history
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapRecord>>> GetLiquidityPoolSwapHistoryAsync(long? swapId = null, BSwapStatus? status = null, string? quoteAsset = null, string? baseAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 100);

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("swapId", swapId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("status", status.HasValue ? JsonConvert.SerializeObject(status.Value, new BSwapStatusConverter(false)) : null);
            parameters.AddOptionalParameter("baseAsset", baseAsset);
            parameters.AddOptionalParameter("quoteAsset", quoteAsset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapRecord>>(_baseClient.GetUrl(bSwapSwapRecordsEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }

        #endregion

        #region Add liquidity pool preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapPreviewResult>> AddToLiquidityPoolPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"quoteAsset", asset},
                {"type", JsonConvert.SerializeObject(type, new LiquidityTypeConverter(false))},
                {"quoteQty", quantity.ToString(CultureInfo.InvariantCulture)},
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapPreviewResult>(_baseClient.GetUrl(bSwapAddLiquidityPreviewEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #region Remove liquidity pool preview

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapPreviewResult>> RemoveFromLiquidityPoolPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"poolId", poolId},
                {"quoteAsset", asset},
                {"type", JsonConvert.SerializeObject(type, new LiquidityTypeConverter(false))},
                {"shareAmount", quantity.ToString(CultureInfo.InvariantCulture)}
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapPreviewResult>(_baseClient.GetUrl(bSwapRemoveLiquidityPreviewEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 150).ConfigureAwait(false);
        }

        #endregion

        #region Get info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapPoolLiquidity>>> GetLiquidityPoolInfoAsync(int? poolId = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("poolId", poolId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapPoolLiquidity>>(_baseClient.GetUrl(bSwapPoolLiquidityEndpoint, bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: poolId == null ? 10 : 1).ConfigureAwait(false);
        }

        #endregion

        #region Get Unclaimed Liquidity Pools Rewards

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapUnclaimedRewards>> GetUnclaimedLiquidityPoolsRewardsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapUnclaimedRewards>(_baseClient.GetUrl("bswap/unclaimedRewards", bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Claim Liquidity Pools Rewards

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceBSwapClaimResult>> ClaimLiquidityPoolsRewardsAsync(int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceBSwapClaimResult>(_baseClient.GetUrl("bswap/claimRewards", bSwapApi, bSwapVersion), HttpMethod.Post, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #region Get Claim History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceBSwapRewardHistory>>> GetLiquidityPoolsClaimHistoryAsync(long? poolId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("poolId", poolId);
            parameters.AddOptionalParameter("assetRewards", asset);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinanceBSwapRewardHistory>>(_baseClient.GetUrl("bswap/claimedHistory", bSwapApi, bSwapVersion), HttpMethod.Get, ct, parameters, true, weight: 1000).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region C2C

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinanceC2CUserTrade>>> GetC2CTradeHistoryAsync(OrderSide side, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("tradeType", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("startTimestamp", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTimestamp", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("rows", pageSize);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinanceC2CUserTrade>>>(_baseClient.GetUrl(c2cTradeHistoryEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
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
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendRequestInternal<BinanceResult<IEnumerable<BinancePayTrade>>>(_baseClient.GetUrl(payTradeHistoryEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 3000).ConfigureAwait(false);
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

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("fromAsset", quoteAsset);
            parameters.AddParameter("toAsset", baseAsset);
            parameters.AddOptionalParameter("fromAmount", quoteQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("toAmount", baseQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("walletType", walletType != null ? JsonConvert.SerializeObject(walletType, new WalletTypeConverter(false)) : null);
            parameters.AddOptionalParameter("validTime", EnumConverter.GetString(validTime));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceConvertQuote>(_baseClient.GetUrl(convertQuoteRequestEndpoint, convertApi, convertVersion), HttpMethod.Post, ct, parameters, true, weight: 200).ConfigureAwait(false);
        }

        #endregion

        #region Convert Accept Quote

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertResult>> ConvertAcceptQuoteAsync(string quoteId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("quoteId", quoteId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceConvertResult>(_baseClient.GetUrl(convertAcceptQuoteEndpoint, convertApi, convertVersion), HttpMethod.Post, ct, parameters, true, weight: 500).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Order Status

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertOrderStatus>> GetConvertOrderStatusAsync(string? orderId = null, string? quoteId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && quoteId == null || orderId != null && quoteId != null)
                throw new ArgumentException("Either orderId or quoteId must be sent, but not both");

            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("quoteId", quoteId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceConvertOrderStatus>(_baseClient.GetUrl(convertOrderStatusEndpoint, convertApi, convertVersion), HttpMethod.Get, ct, parameters, true, weight: 100).ConfigureAwait(false);
        }

        #endregion

        #region Get Convert Trade History

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceListResult<Objects.Models.Spot.Convert.BinanceConvertTrade>>> GetConvertTradeHistoryAsync(DateTime startTime, DateTime endTime, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceListResult<Objects.Models.Spot.Convert.BinanceConvertTrade>>(_baseClient.GetUrl(convertTradeHistoryEndpoint, convertApi, convertVersion), HttpMethod.Get, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Convert Transfer
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceConvertTransferResult>> ConvertTransferAsync(string clientTransferId, string asset, decimal quantity, string targetAsset, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "clientTranId", clientTransferId },
                { "asset", asset },
                { "amount", quantity },
                { "targetAsset", targetAsset }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            return await _baseClient.SendRequestInternal<BinanceConvertTransferResult>(_baseClient.GetUrl(convertTransferEndpoint, marginApi, marginVersion), HttpMethod.Post, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BinanceQueryRecords<BinanceConvertTransferRecord>>> GetConvertTransferHistoryAsync(DateTime startTime, DateTime endTime, long? transferId = null, string? asset = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "startTime", DateTimeConverter.ConvertToMilliseconds(startTime) },
                { "endTime", DateTimeConverter.ConvertToMilliseconds(endTime) },
            };
            parameters.AddOptionalParameter("tranId", transferId);
            parameters.AddOptionalParameter("asset", asset);
            parameters.AddOptionalParameter("current", page);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceQueryRecords<BinanceConvertTransferRecord>>(_baseClient.GetUrl(convertTransferHistoryEndpoint, marginApi, marginVersion), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
        }
        #endregion

        #region Get Prevented Trades
        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BinancePreventedTrade>>> GetPreventedTradesAsync(string symbol, long? preventedMatchId = null, long? orderId = null, long? fromPreventedMatchId = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("preventedMatchId", preventedMatchId);
            parameters.AddOptionalParameter("orderId", orderId);
            parameters.AddOptionalParameter("fromPreventedMatchId", fromPreventedMatchId);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<IEnumerable<BinancePreventedTrade>>(_baseClient.GetUrl("myPreventedMatches", "api", "3"), HttpMethod.Get, ct, parameters, true, weight: 5).ConfigureAwait(false);
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
            var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "duration", duration },
            };
            parameters.AddOptionalParameter("clientAlgoId", clientOrderId);
            parameters.AddOptionalParameter("limitPrice", limitPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceAlgoOrderResult>(_baseClient.GetUrl("algo/spot/newOrderTwap", "sapi", "1"), HttpMethod.Post, ct, parameters, true, weight: 3000).ConfigureAwait(false);
        }
        #endregion

        #region Cancel algo order
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoResult>> CancelAlgoOrderAsync(long algoOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "algoId", algoOrderId },
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceAlgoResult>(_baseClient.GetUrl("algo/spot/order", "sapi", "1"), HttpMethod.Delete, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #region Get Open Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrders>> GetOpenAlgoOrdersAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceAlgoOrders>(_baseClient.GetUrl("algo/spot/openOrders", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #region Get historical Algo Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoOrders>> GetClosedAlgoOrdersAsync(string? symbol = null, OrderSide? side = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceAlgoOrders>(_baseClient.GetUrl("algo/spot/historicalOrders", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion

        #region Get Algo sub Orders
        /// <inheritdoc />
        public async Task<WebCallResult<BinanceAlgoSubOrderList>> GetAlgoSubOrdersAsync(long algoId, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "algoId", algoId }
            };
            parameters.AddOptionalParameter("page", page);
            parameters.AddOptionalParameter("pageSize", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.SendRequestInternal<BinanceAlgoSubOrderList>(_baseClient.GetUrl("algo/spot/subOrders", "sapi", "1"), HttpMethod.Get, ct, parameters, true, weight: 1).ConfigureAwait(false);
        }
        #endregion
        #endregion
    }
}
