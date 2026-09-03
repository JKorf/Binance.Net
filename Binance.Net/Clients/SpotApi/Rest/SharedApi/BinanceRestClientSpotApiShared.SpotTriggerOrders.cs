using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotSharedApi
    {
        #region Trigger Order Client
        public PlaceSpotTriggerOrderOptions PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var type = GetTriggerOrderParameters(request.PriceDirection, request.OrderPrice, request.OrderSide);
            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                type,
                request.Quantity.QuantityInBaseAsset,
                //timeInForce: request.OrderPrice == null ? TimeInForce.ImmediateOrCancel : TimeInForce.GoodTillCanceled,
                price: request.OrderPrice,
                newClientOrderId: request.ClientOrderId,
                stopPrice: request.TriggerPrice,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data!.Id.ToString()));

        }

        public GetSpotTriggerOrderOptions GetSpotTriggerOrderOptions { get; }
            = new GetSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedSpotTriggerOrder>> GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var result = await _api.Trading.GetOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                id,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(result);

            var (orderType, orderDirection) = ParseTriggerDirections(result.Data!.Type, result.Data.Side);
            // Return
            return HttpResult.Ok(result, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                result.Data.Symbol,
                result.Data.Id.ToString(),
                orderType,
                orderDirection,
                ParseTriggerOrderStatus(result.Data),
                result.Data.StopPrice ?? 0,
                result.Data.CreateTime
                )
            {
                PlacedOrderId = result.Data.Id.ToString(),
                AveragePrice = result.Data.AverageFillPrice,
                OrderPrice = result.Data.Price,
                OrderQuantity = new SharedOrderQuantity(result.Data.Quantity, result.Data.QuoteQuantity),
                QuantityFilled = new SharedOrderQuantity(result.Data.QuantityFilled, result.Data.QuoteQuantityFilled),
                TimeInForce = ParseTimeInForce(result.Data.TimeInForce),
                UpdateTime = result.Data.UpdateTime,
                ClientOrderId = result.Data.ClientOrderId
            });

        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(BinanceOrder data)
        {
            if (data.Status == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (data.Status == OrderStatus.Canceled
                || data.Status == OrderStatus.Rejected
                || data.Status == OrderStatus.Expired
                || data.Status == OrderStatus.ExpiredInMatch)
            {
                return SharedTriggerOrderStatus.CanceledOrRejected;
            }

            if (data.Status == OrderStatus.New
                || data.Status == OrderStatus.PartiallyFilled
                || data.Status == OrderStatus.PendingCancel)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        public CancelSpotTriggerOrderOptions CancelSpotTriggerOrderOptions { get; }
            = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data!.Id.ToString()));

        }

        private SpotOrderType GetTriggerOrderParameters(SharedTriggerPriceDirection orderType, decimal? orderPrice, SharedOrderSide side)
        {
            if (orderType == SharedTriggerPriceDirection.PriceBelow)
            {
                if (side == SharedOrderSide.Buy)
                    // PriceBelow + Enter = TakeProfit Buy order
                    return orderPrice == null ? SpotOrderType.TakeProfit : SpotOrderType.TakeProfitLimit;
                else
                    // PriceBelow + Exit = StopLoss Sell order
                    return orderPrice == null ? SpotOrderType.StopLoss : SpotOrderType.StopLossLimit;
            }

            if (side == SharedOrderSide.Buy)
                // PriceAbove + Enter = StopLoss Buy order
                return orderPrice == null ? SpotOrderType.StopLoss : SpotOrderType.StopLossLimit;
            else
                // PriceAbove + Exit = TakeProfit Sell order
                return orderPrice == null ? SpotOrderType.TakeProfit : SpotOrderType.TakeProfitLimit;
        }

        private (SharedOrderType, SharedTriggerOrderDirection) ParseTriggerDirections(SpotOrderType orderType, OrderSide side)
        {
            if (orderType == SpotOrderType.TakeProfit || orderType == SpotOrderType.TakeProfitLimit)
            {
                if (side == OrderSide.Buy)
                {
                    // TakeProfit + Buy = PriceBelow Enter
                    return (
                        orderType == SpotOrderType.TakeProfit ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Enter);
                }
                else
                {
                    // TakeProfit + Sell = PriceAbove Exit
                    return (
                        orderType == SpotOrderType.TakeProfit ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Exit);
                }
            }

            if (side == OrderSide.Buy)
            {
                // StopLoss + Buy = PriceAbove Enter
                return (
                    orderType == SpotOrderType.StopLoss ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Enter);
            }
            else
            {
                // StopLoss + Sell = PriceBelow Exit
                return (
                    orderType == SpotOrderType.StopLoss ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Exit);
            }
        }
        #endregion
    }
}
