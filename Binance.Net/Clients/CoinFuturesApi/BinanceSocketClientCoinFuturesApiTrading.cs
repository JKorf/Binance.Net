using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Futures;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal class BinanceSocketClientCoinFuturesApiTrading : IBinanceSocketClientCoinFuturesApiTrading
    {
        private readonly BinanceSocketClientCoinFuturesApi _client;
        private readonly ILogger _logger;

        internal BinanceSocketClientCoinFuturesApiTrading(ILogger logger, BinanceSocketClientCoinFuturesApi client)
        {
            _client = client;
            _logger = logger;
        }

        #region Queries

        #region Place Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceFuturesOrder>>> PlaceOrderAsync(string symbol,
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

            string clientOrderId = LibraryHelpers.ApplyBrokerId(
                    newClientOrderId,
                    LibraryHelpers.GetClientReference(() => _client.ClientOptions.BrokerId, _client.Exchange, "Futures"),
                    36,
                    _client.ClientOptions.AllowAppendingClientOrderId);

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
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceFuturesOrder>(_client.ClientOptions.Environment.CoinFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"order.place", parameters, true, true, weight: 0, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceFuturesOrder>>> EditOrderAsync(string symbol, OrderSide side, decimal quantity, decimal? price = null, PriceMatch? priceMatch = null, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _client.ClientOptions.BrokerId, _client.Exchange, "Futures"),
                    36,
                    _client.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
            };
            parameters.AddEnum("side", side);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalEnum("priceMatch", priceMatch);
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceFuturesOrder>(_client.ClientOptions.Environment.UsdFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"order.modify", parameters, true, true, weight: 1, ct: ct).ConfigureAwait(false);

        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceFuturesOrder>>> CancelOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (!orderId.HasValue && string.IsNullOrEmpty(origClientOrderId))
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _client.ClientOptions.BrokerId, _client.Exchange, "Futures"),
                    36,
                    _client.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceFuturesOrder>(_client.ClientOptions.Environment.CoinFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"order.cancel", parameters, true, true, weight: 1, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinanceFuturesOrder>>> GetOrderAsync(string symbol, long? orderId = null, string? origClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderId == null && origClientOrderId == null)
                throw new ArgumentException("Either orderId or origClientOrderId must be sent");

            if (origClientOrderId != null)
            {
                origClientOrderId = LibraryHelpers.ApplyBrokerId(
                    origClientOrderId,
                    LibraryHelpers.GetClientReference(() => _client.ClientOptions.BrokerId, _client.Exchange, "Futures"),
                    36,
                    _client.ClientOptions.AllowAppendingClientOrderId);
            }

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("orderId", orderId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("origClientOrderId", origClientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinanceFuturesOrder>(_client.ClientOptions.Environment.CoinFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"order.status", parameters, true, true, weight: 1, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<CallResult<BinanceResponse<BinancePositionDetailsCoin[]>>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture));

            return await _client.QueryAsync<BinancePositionDetailsCoin[]>(_client.ClientOptions.Environment.CoinFuturesSocketApiAddress!.AppendPath("ws-dapi/v1"), $"account.position", parameters, true, true, weight: 5, ct: ct).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region Streams
        #endregion
    }
}
