using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal partial class BinanceRestClientCoinFuturesApi : IBinanceRestClientCoinFuturesApiShared
    {
        public string Exchange => BinanceExchange.ExchangeName;

        async Task<WebCallResult<IEnumerable<SharedKline>>> IKlineClient.GetKlinesAsync(KlineRequest request, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval.TotalSeconds;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new WebCallResult<IEnumerable<SharedKline>>(new ArgumentError("Interval not supported"));

            var result = await ExchangeData.GetKlinesAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.FuturesType),
                interval,
                request.StartTime,
                request.EndTime,
                request.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedKline>>(default);

            return result.As(result.Data.Select(x => new SharedKline
            {
                BaseVolume = x.Volume,
                ClosePrice = x.ClosePrice,
                HighPrice = x.HighPrice,
                LowPrice = x.LowPrice,
                OpenPrice = x.OpenPrice,
                OpenTime = x.OpenTime
            }));
        }

        async Task<WebCallResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolClient.GetSymbolsAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedFuturesSymbol>>(default);

            return result.As(result.Data.Symbols.Select(s => new SharedFuturesSymbol
            {
                BaseAsset = s.BaseAsset,
                QuoteAsset = s.QuoteAsset,
                Name = s.Name,
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                QuantityStep = s.LotSizeFilter?.StepSize,
                PriceStep = s.PriceFilter?.TickSize,
                ContractSize = 1,
                DeliveryTime = s.DeliveryDate
            }));
        }

        async Task<WebCallResult<SharedTicker>> ITickerClient.GetTickerAsync(TickerRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync(symbol: FormatSymbol(request.BaseAsset, request.QuoteAsset, request.FuturesType), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<SharedTicker>(default);

            var ticker = result.Data.Single();
            return result.As(new SharedTicker
            {
                HighPrice = ticker.HighPrice,
                LastPrice = ticker.LastPrice,
                LowPrice = ticker.LowPrice,
            });
        }

        async Task<WebCallResult<IEnumerable<SharedTrade>>> ITradeClient.GetTradesAsync(TradeRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.FuturesType),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedTrade>>(default);

            return result.As(result.Data.Select(x => new SharedTrade
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Timestamp = x.TradeTime
            }));
        }
    }
}
