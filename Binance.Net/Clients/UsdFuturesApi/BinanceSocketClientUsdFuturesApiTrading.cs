using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Futures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal class BinanceSocketClientUsdFuturesApiTrading : IBinanceSocketClientUsdFuturesApiTrading
    {
        private readonly BinanceSocketClientUsdFuturesApi _client;
        private readonly ILogger _logger;

        internal BinanceSocketClientUsdFuturesApiTrading(ILogger logger, BinanceSocketClientUsdFuturesApi client)
        {
            _client = client;
            _logger = logger;
        }

        #region Queries

        #region Place Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> PlaceOrderAsync(string symbol,
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

            string clientOrderId = BinanceHelpers.ApplyBrokerId(newClientOrderId, BinanceExchange.ClientOrderIdFutures, 32);

            var parameters = new ParameterCollection();
            parameters.AddParameter("symbol", symbol);
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
            parameters.AddOptionalParameter("priceProtect", priceProtect?.ToString().ToUpper());
            parameters.AddOptionalEnum("priceMatch", priceMatch);
            parameters.AddOptionalEnum("selfTradePreventionMode", selfTradePreventionMode);
            parameters.AddOptionalMilliseconds("goodTillDate", goodTillDate);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceUsdFuturesOrder>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"order.place", parameters, true, true, weight: 0, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> EditOrderAsync(string symbol, OrderSide side, decimal quantity, decimal price, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "side", EnumConverter.GetString(side) },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "price", price.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceUsdFuturesOrder>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"order.modify", parameters, true, true, weight: 1, ct: ct).ConfigureAwait(false);

        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceUsdFuturesOrder>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"order.cancel", parameters, true, true, weight: 1, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceUsdFuturesOrder>>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceUsdFuturesOrder>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"order.status", parameters, true, true, weight: 1, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<IEnumerable<BinancePositionV3>>>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));
            
            return await _client.QueryAsync<IEnumerable<BinancePositionV3>>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-fapi/v1"), $"v2/account.position", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams
        #endregion
    }
}
