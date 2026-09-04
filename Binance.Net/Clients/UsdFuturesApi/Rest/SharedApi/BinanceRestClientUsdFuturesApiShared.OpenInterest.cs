using Binance.Net.Clients.SpotApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceRestClientUsdFuturesSharedApi
    {
        #region Get Open Interest

        public GetOpenInterestOptions GetOpenInterestOptions { get; } = new GetOpenInterestOptions(_exchangeName, false);
        async Task<ICallResult<SharedOpenInterest>> IGetOpenInterest.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
            => await GetOpenInterestAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedOpenInterest>> GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            var validationError = GetOpenInterestOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOpenInterest>(Exchange, validationError);

            var result = await _api.ExchangeData.GetOpenInterestAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOpenInterest>(result);

            return HttpResult.Ok(result, new SharedOpenInterest(new SharedOrderQuantity(result.Data.OpenInterest)));

        }

        #endregion

    }
}
