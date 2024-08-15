using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceRestClientUsdFuturesApi : IBinanceRestClientUsdFuturesApiShared
    {
        public string Exchange => BinanceExchange.ExchangeName;

        //GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; }
        //    = new GetKlinesOptions(
        //        true,
        //        false,
        //        SharedKlineInterval.FiveMinutes,
        //        SharedKlineInterval.FifteenMinutes,
        //        SharedKlineInterval.OneHour,
        //        SharedKlineInterval.FifteenMinutes,
        //        SharedKlineInterval.OneDay,
        //        SharedKlineInterval.OneWeek,
        //        SharedKlineInterval.OneMonth);

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var result = await ExchangeData.GetKlinesAsync(
                request.GetSymbol(FormatSymbol),
                interval,
                fromTimestamp ?? request.StartTime,
                request.EndTime,
                request.Limit ?? 1000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult< IEnumerable<SharedKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == (request.Limit ?? 1000))
                nextToken = new DateTimeToken(result.Data.Max(o => o.OpenTime).AddSeconds((int)interval));
            if (nextToken?.LastTime >= request.EndTime)
                nextToken = null;

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)), nextToken);
        }

        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetSymbolsAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult< IEnumerable<SharedFuturesSymbol>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Symbols.Select(s => new SharedFuturesSymbol(s.BaseAsset, s.QuoteAsset, s.Name)
            {
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                QuantityStep = s.LotSizeFilter?.StepSize,
                PriceStep = s.PriceFilter?.TickSize,
                ContractSize = 1,
                DeliveryTime = s.DeliveryDate
            }));
        }

        async Task<ExchangeWebResult<SharedTicker>> ITickerRestClient.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickerAsync(request.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTicker>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedTicker(result.Data.Symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedTicker>>> ITickerRestClient.GetTickersAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTicker>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedTicker>>(Exchange, result.Data.Select(x => new SharedTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice)));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
                request.GetSymbol((baseAsset, quoteAsset, apiType) => FormatSymbol(baseAsset, quoteAsset)), // Don't pass api type; need only the pair
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult< IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.TradeTime)));
        }
    }
}
