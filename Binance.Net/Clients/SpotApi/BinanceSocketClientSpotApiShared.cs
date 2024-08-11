using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotApi : IBinanceSocketClientSpotApiShared
    {
        public string Exchange => BinanceExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickerUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedTicker>>> handler, CancellationToken ct)
        {
            var result = await ExchangeData.SubscribeToAllMiniTickerUpdatesAsync(update => handler(update.As(update.Data.Select(x => new SharedTicker
            {
                Symbol = x.Symbol,
                HighPrice = x.HighPrice,
                LastPrice = x.LastPrice,
                LowPrice = x.LowPrice
            }))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.SubscribeToMiniTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker
            {
                Symbol = update.Data.Symbol,
                HighPrice = update.Data.HighPrice,
                LastPrice = update.Data.LastPrice,
                LowPrice = update.Data.LowPrice
            })), ct).ConfigureAwait(false);

            return result;
        }
    }
}
