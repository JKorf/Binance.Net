using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceSocketClientUsdFuturesSharedApi
    {
        #region Subscribe To Position Updates

        public SubscribePositionOptions SubscribePositionOptions { get; } 
            = new SubscribePositionOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.Account.SubscribeToUserDataUpdatesAsync(
                onAccountUpdate: update => handler(update.ToType(update.Data.UpdateData.Positions.Select(x =>
                    new SharedPosition(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        new SharedOrderQuantity(Math.Abs(x.Quantity)),
                        update.Data.EventTime)
                    {
                        AverageOpenPrice = x.EntryPrice,
                        PositionMode = x.PositionSide == PositionSide.Both ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.PositionSide == Enums.PositionSide.Both ? (x.Quantity > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                        UnrealizedPnl = x.UnrealizedPnl
                    }).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
