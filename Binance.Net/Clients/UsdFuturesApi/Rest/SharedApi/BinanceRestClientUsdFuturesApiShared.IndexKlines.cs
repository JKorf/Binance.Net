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
        #region Get Index Price Klines

        public GetIndexPriceKlinesOptions GetIndexPriceKlinesOptions { get; } = new GetIndexPriceKlinesOptions(_exchangeName, true, true, true, 1000, false);

        async Task<ICallResult<SharedFuturesKline[]>> IGetIndexPriceKlines.GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetIndexPriceKlinesAsync(request, pageRequest, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedFuturesKline[]>> GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetIndexPriceKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, validationError);

            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return HttpResult.Fail<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var direction = request.Direction ?? DataDirection.Ascending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            var result = await _api.ExchangeData.GetMarkPriceKlinesAsync(
                symbol,
                interval,
                limit,
                pageParams.StartTime,
                pageParams.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => direction == DataDirection.Ascending
                        ? Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime))
                        : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                    result.Data.Length,
                    result.Data.Select(x => x.OpenTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            // Return
            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFuturesKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice))
                    .ToArray(), nextPageRequest);

        }

        #endregion

    }
}
