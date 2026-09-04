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
        #region Get Futures Ticker

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        async Task<ICallResult<SharedFuturesTicker>> IGetFuturesTicker.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
            => await GetFuturesTickerAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = _api.ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct);
            var resultMarkPrice = _api.ExchangeData.GetMarkPriceAsync(request.Symbol!.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultTicker, resultMarkPrice).ConfigureAwait(false);

            if (!resultTicker.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker.Result);
            if (!resultMarkPrice.Result.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultMarkPrice.Result);

            return HttpResult.Ok(resultTicker.Result, new SharedFuturesTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, resultTicker.Result.Data.Symbol), 
                resultTicker.Result.Data.Symbol, 
                resultTicker.Result.Data.LastPrice, 
                resultTicker.Result.Data.HighPrice, 
                resultTicker.Result.Data.LowPrice,
                new SharedOrderQuantity(resultTicker.Result.Data.Volume, resultTicker.Result.Data.QuoteVolume),
                resultTicker.Result.Data.PriceChangePercent)
            {
                MarkPrice = resultMarkPrice.Result.Data.MarkPrice,
                IndexPrice = resultMarkPrice.Result.Data.IndexPrice,
                FundingRate = resultMarkPrice.Result.Data.FundingRate,
                NextFundingTime = resultMarkPrice.Result.Data.NextFundingTime == default ? null : resultMarkPrice.Result.Data.NextFundingTime
            });

        }

        #endregion

        #region Get All Futures Tickers

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;

        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        async Task<ICallResult<SharedFuturesTicker[]>> IGetAllFuturesTickers.GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => await GetAllFuturesTickersAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
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
            if (request.TradingMode.HasValue)
                data = data.Where(x => (request.TradingMode == TradingMode.DeliveryLinear ? x.Symbol!.Contains("_") : !x.Symbol!.Contains("_")));

            return HttpResult.Ok(resultTickers.Result, data.Select(x =>
            {
                var markPrice = resultMarkPrices.Result.Data.Single(p => p.Symbol == x.Symbol);
                return new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                    x.Symbol,
                    x.LastPrice,
                    x.HighPrice, 
                    x.LowPrice, 
                    new SharedOrderQuantity(x.Volume, x.QuoteVolume),
                    x.PriceChangePercent)
                {
                    IndexPrice = markPrice.IndexPrice,
                    MarkPrice = markPrice.MarkPrice,
                    FundingRate = markPrice.FundingRate,
                    NextFundingTime = markPrice.NextFundingTime == default ? null : markPrice.NextFundingTime
                };
            }).ToArray());

        }

        #endregion

    }
}
