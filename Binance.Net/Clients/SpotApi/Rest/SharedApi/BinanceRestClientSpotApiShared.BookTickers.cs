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
        #region Get Book Ticker

        public GetBookTickerOptions GetBookTickerOptions { get; }
            = new GetBookTickerOptions(_exchangeName, false);
        async Task<ICallResult<SharedBookTicker>> IGetBookTicker.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
            => await GetBookTickerAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedBookTicker>> GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await _api.ExchangeData.GetBookPriceAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, resultTicker.Data!.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice,
                new SharedOrderQuantity(resultTicker.Data.BestAskQuantity),
                resultTicker.Data.BestBidPrice,
                new SharedOrderQuantity(resultTicker.Data.BestBidQuantity)));

        }

        #endregion

    }
}
