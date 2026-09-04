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
        #region Get Balances

        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Funding, AccountTypeFilter.Spot);

        async Task<ICallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
            => await GetBalancesAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Funding)
            {
                var result = await _api.Account.GetFundingWalletAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data!.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.Available, 
                        x.Available + x.Freeze + x.Locked)).ToArray());
            }
            else
            {
                var result = await _api.Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data!.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.Available, 
                        x.Total)).ToArray());
            }

        }

        #endregion

    }
}
