using Binance.Net.Clients.UsdFuturesApi;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Caching;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System.Collections.Concurrent;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal partial class BinanceRestClientCoinFuturesSharedApi
    {
        #region Get Ticker

        async Task<ICallResult<SharedTicker>> IGetTicker.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await ((IGetTickerRest)this).GetTickerAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker>> IGetTickerRest.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var result = await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker>(result);

            return HttpResult.Ok<SharedTicker>(result, result.Data);
        }

        GetTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions => GetTickerOptions;

        public GetTickerOptions GetTickerOptions { get; } = new GetTickerOptions(_exchangeName);

        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = _api.ExchangeData.GetTickersAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct);
            var resultMarkPrice = _api.ExchangeData.GetMarkPricesAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct);
            await Task.WhenAll(resultTicker, resultMarkPrice).ConfigureAwait(false);
            if (!resultTicker.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result);
            if (!resultMarkPrice.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultMarkPrice.Result);

            var ticker = resultTicker.Result.Data.SingleOrDefault();
            var mark = resultMarkPrice.Result.Data.SingleOrDefault();

            if (ticker == null || mark == null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol not found")));

            return HttpResult.Ok(resultTicker.Result, new SharedFuturesTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, ticker.Symbol),
                ticker.Symbol,
                ticker.LastPrice,
                ticker.HighPrice,
                ticker.LowPrice,
                new SharedOrderQuantity(ticker.Volume, null, ticker.QuoteVolume),
                ticker.PriceChangePercent)
            {
                IndexPrice = mark.IndexPrice,
                MarkPrice = mark.MarkPrice,
                FundingRate = mark.FundingRate,
                NextFundingTime = mark.NextFundingTime == default ? null : mark.NextFundingTime
            });

        }

        #endregion

        #region Get All Tickers

        async Task<ICallResult<SharedTicker[]>> IGetAllTickers.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await ((IGetAllTickersRest)this).GetAllTickersAsync(request, ct).ConfigureAwait(false);

        async Task<HttpResult<SharedTicker[]>> IGetAllTickersRest.GetAllTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var result = await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTicker[]>(result);

            return HttpResult.Ok<SharedTicker[]>(result, result.Data);
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllTickersOptions;

        public GetAllTickersOptions GetAllTickersOptions { get; } = new GetAllTickersOptions(_exchangeName);

        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var resultTickers = _api.ExchangeData.GetTickersAsync(ct: ct);
            var resultMarkPrices = _api.ExchangeData.GetMarkPricesAsync(ct: ct);
            await Task.WhenAll(resultTickers, resultMarkPrices).ConfigureAwait(false);
            if (!resultTickers.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultTickers.Result);
            if (!resultMarkPrices.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultMarkPrices.Result);

            IEnumerable<IBinance24HPrice> data = resultTickers.Result.Data;
            if (request.TradingMode != null)
                data = data.Where(x => request.TradingMode == TradingMode.PerpetualInverse ? x.Symbol!.Contains("_PERP") : !x.Symbol!.Contains("_PERP"));

            return HttpResult.Ok(resultTickers.Result, data.Select(x =>
            {
                var markPrice = resultMarkPrices.Result.Data.Single(p => p.Symbol == x.Symbol);
                return new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice,
                    x.LowPrice,
                    new SharedOrderQuantity(x.Volume, null, x.QuoteVolume),
                    x.PriceChangePercent)
                {
                    IndexPrice = markPrice.IndexPrice,
                    MarkPrice = markPrice.MarkPrice,
                    FundingRate = markPrice.FundingRate,
                    NextFundingTime = markPrice.NextFundingTime
                };
            }).ToArray());

        }

        #endregion

    }
}
