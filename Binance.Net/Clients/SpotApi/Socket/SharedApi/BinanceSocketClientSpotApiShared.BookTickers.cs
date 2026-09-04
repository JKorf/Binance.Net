using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotSharedApi
    {
        #region Subscribe To Book Ticker Updates

        public SubscribeBookTickerOptions SubscribeBookTickerOptions { get; }
            = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await _api.ExchangeData.SubscribeToBookTickerUpdatesAsync(symbols, update => handler(
                update.ToType(
                    new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), 
                        update.Data.Symbol,
                        update.Data.BestAskPrice,
                        new SharedOrderQuantity(update.Data.BestAskQuantity),
                        update.Data.BestBidPrice,
                        new SharedOrderQuantity(update.Data.BestBidQuantity)))), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
