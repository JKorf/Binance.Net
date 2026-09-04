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
        #region Subscribe To Ticker Updates

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeTickerSocket.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
            => await SubscribeToTickerUpdatesAsync(request, x => handler(x.ToType<SharedTicker>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickerOptions SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await _api.ExchangeData.SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(
                new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol, 
                    update.Data.LastPrice,
                    update.Data.HighPrice,
                    update.Data.LowPrice,
                    new SharedOrderQuantity(update.Data.Volume, update.Data.QuoteVolume),
                    update.Data.PriceChangePercent)
            {
            })), ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Subscribe To All Tickers Updates

        async Task<WebSocketResult<UpdateSubscription>> ISubscribeAllTickersSocket.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedTicker[]>> handler, CancellationToken ct)
            => await SubscribeToAllTickersUpdatesAsync(request, x => handler(x.ToType<SharedTicker[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeTickersOptions SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await _api.ExchangeData.SubscribeToAllTickerUpdatesAsync(update =>
            {
                IEnumerable<IBinance24HPrice> data = update.Data;
                if (request.TradingMode != null)
                    data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? !x.Symbol.Contains("_") : x.Symbol.Contains("_"));

                if (!data.Any())
                    return;

                handler(update.ToType(data.Select(x => 
                    new SharedSpotTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.LastPrice,
                        x.HighPrice,
                        x.LowPrice,
                        new SharedOrderQuantity(x.Volume, x.QuoteVolume),
                        x.PriceChangePercent)
                {
                }).ToArray()));
            }, ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
