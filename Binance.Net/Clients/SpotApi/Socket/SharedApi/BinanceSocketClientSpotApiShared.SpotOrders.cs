using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotSharedApi
    {
        #region Subscribe To Spot Order Updates

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } 
            = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.Account.SubscribeToUserDataUpdatesAsync(
                onOrderUpdateMessage: update => handler(update.ToType(new[] {
                    new SharedSpotOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.Id.ToString(),
                        ParseOrderType(update.Data.Type),
                        update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Status),
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        OrderPrice = update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(update.Data.Quantity, update.Data.QuoteQuantity == 0 ? null : update.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(update.Data.QuantityFilled, update.Data.QuoteQuantityFilled),
                        UpdateTime = update.Data.UpdateTime,
                        TimeInForce = update.Data.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        TriggerPrice = update.Data.StopPrice == 0 ? null : update.Data.StopPrice,
                        IsTriggerOrder = update.Data.StopPrice > 0,
                        LastTrade = update.Data.LastQuantityFilled == 0 ? null : 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                                update.Data.Symbol, 
                                update.Data.Id.ToString(),
                                update.Data.TradeId.ToString(), 
                                update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, 
                                new SharedOrderQuantity(update.Data.LastQuantityFilled), 
                                update.Data.LastPriceFilled, update.Data.UpdateTime)
                            {
                                Fee = update.Data.Fee,
                                FeeAsset = update.Data.FeeAsset,
                                Role = update.Data.BuyerIsMaker ? SharedRole.Maker : SharedRole.Taker,
                                ClientOrderId = update.Data.ClientOrderId
                            }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == Enums.OrderStatus.Canceled || status == OrderStatus.Rejected || status == OrderStatus.Expired)
                return SharedOrderStatus.Canceled;
            if (status == Enums.OrderStatus.PendingNew
                || status == Enums.OrderStatus.PendingCancel
                || status == Enums.OrderStatus.New
                || status == Enums.OrderStatus.PartiallyFilled)
            {
                return SharedOrderStatus.Open;
            }
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(SpotOrderType type)
        {
            if (type == SpotOrderType.Market || type == SpotOrderType.TakeProfit || type == SpotOrderType.StopLoss)
                return SharedOrderType.Market;

            if (type == SpotOrderType.Limit || type == SpotOrderType.LimitMaker || type == SpotOrderType.TakeProfitLimit || type == SpotOrderType.StopLossLimit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        #region Place Spot Order

        public SharedFeeDeductionType SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        public SharedFeeAssetType SpotFeeAssetType => SharedFeeAssetType.OutputAsset;
        public SharedOrderType[] SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        public SharedTimeInForce[] SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        public SharedQuantitySupport SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        public string GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        async Task<ICallResult<SharedId>> IPlaceSpotOrder.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
            => await PlaceSpotOrderAsync(request, ct).ConfigureAwait(false);

        PlaceSpotOrderOptions IPlaceSpotOrder.PlaceSpotOrderOptions
            => PlaceSpotOrderOptions;

        public PlaceSpotOrderSocketOptions PlaceSpotOrderOptions { get; } = new PlaceSpotOrderSocketOptions(_exchangeName);
        public async Task<QueryResult<SharedId>> PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Limit ? Enums.SpotOrderType.Limit : request.OrderType == SharedOrderType.Market ? Enums.SpotOrderType.Market : Enums.SpotOrderType.LimitMaker,
                quantity: request.Quantity?.QuantityInBaseAsset,
                quoteQuantity: request.Quantity?.QuantityInQuoteAsset,
                price: request.Price,
                timeInForce: GetTimeInForce(request.TimeInForce, request.OrderType),
                newClientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result.Success)
                return QueryResult.Fail<SharedId>(result);

            return QueryResult.Ok(result, new SharedId(result.Data!.Result.Id.ToString()));

        }

        #endregion

        #region Cancel Spot Order

        CancelSpotOrderOptions ICancelSpotOrder.CancelSpotOrderOptions
            => CancelSpotOrderOptions;

        public CancelSpotOrderSocketOptions CancelSpotOrderOptions { get; }
            = new CancelSpotOrderSocketOptions(_exchangeName, true);
        async Task<ICallResult<SharedId>> ICancelSpotOrder.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelSpotOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<QueryResult<SharedId>> CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return QueryResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return QueryResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return QueryResult.Fail<SharedId>(order);

            return QueryResult.Ok(order, new SharedId(order.Data!.Result.Id.ToString()));
        }

        #endregion

        private Enums.TimeInForce? GetTimeInForce(SharedTimeInForce? tif, SharedOrderType type)
        {
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;
            if (type == SharedOrderType.Limit) return TimeInForce.GoodTillCanceled; // Limit orders needs tif

            return null;
        }

    }
}
