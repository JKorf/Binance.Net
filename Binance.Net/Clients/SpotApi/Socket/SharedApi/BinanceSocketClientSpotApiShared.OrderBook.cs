using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotSharedApi
    {
        #region Subscribe To Order Book Updates

        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await _api.ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(symbols, request.Limit ?? 20, 100, update => handler(
                update.ToType(
                    new SharedOrderBook(SharedQuantityType.BaseAsset, update.Data.LastUpdateId, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Subscribe To Incremental Order Book Updates

        public SubscribeIncrementalOrderBookOptions SubscribeIncrementalOrderBookOptions { get; }
            = new SubscribeIncrementalOrderBookOptions(_exchangeName, false, [5000], SharedOrderBookSubscriptionType.OnlyIncremental)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync
            (SubscribeOrderBookRequest request, Action<DataEvent<SharedIncrementalOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeIncrementalOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await _api.ExchangeData.SubscribeToOrderBookUpdatesAsync(symbols, 100, update => handler(
                update.ToType(
                    new SharedIncrementalOrderBook(SharedQuantityType.BaseAsset, update.Data.FirstUpdateId, update.Data.LastUpdateId, update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }

        #endregion


    }
}
