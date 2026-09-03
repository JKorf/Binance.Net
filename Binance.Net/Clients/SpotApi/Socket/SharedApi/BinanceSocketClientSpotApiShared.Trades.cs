using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotSharedApi
    {
        #region Trade client

        public SubscribeTradeOptions SubscribeTradeOptions { get; } 
            = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200,
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("Aggregated", typeof(bool), "Whether to subscribe to aggregated trade updates instead of individual trade updates", true)
            }
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            if (ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "Aggregated") == true)
            {
                var result = await _api.ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(symbols, update => handler(update.ToType(new[] 
                { 
                    new SharedTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        new SharedOrderQuantity(update.Data.Quantity),
                        update.Data.Price, 
                        update.Data.TradeTime)
                {
                    Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                } })), ct).ConfigureAwait(false);

                return result;
            }
            else
            {
                var result = await _api.ExchangeData.SubscribeToTradeUpdatesAsync(symbols, update => handler(update.ToType(new[] 
                {
                    new SharedTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        new SharedOrderQuantity(update.Data.Quantity),
                        update.Data.Price,
                        update.Data.TradeTime)
                {
                    Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                } })), ct).ConfigureAwait(false);

                return result;
            }
        }

        #endregion
    }
}
