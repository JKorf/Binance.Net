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
        #region Subscribe To Balance Updates

        public SubscribeBalanceOptions SubscribeBalanceOptions { get; } 
            = new SubscribeBalanceOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.Account.SubscribeToUserDataUpdatesAsync(
                onAccountUpdate: update => handler(update.ToType(update.Data.UpdateData.Balances.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.WalletBalance,
                        x.WalletBalance)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
