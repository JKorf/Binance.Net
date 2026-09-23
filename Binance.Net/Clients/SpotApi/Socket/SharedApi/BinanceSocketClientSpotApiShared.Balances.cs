using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotSharedApi
    {
        #region Subscribe To Balance Updates

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; }
            = new SubscribeBalanceOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.Account.SubscribeToUserDataUpdatesAsync(
                onAccountPositionMessage: update => handler(update.ToType(update.Data.Balances.Select(x =>
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.Available,
                        x.Total)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
