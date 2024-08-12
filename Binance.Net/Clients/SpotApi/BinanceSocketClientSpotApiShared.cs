using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Models.Socket;
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
            var result = await ExchangeData.SubscribeToAllMiniTickerUpdatesAsync(update => handler(update.As(update.Data.Select(x => new SharedTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice)))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.SubscribeToMiniTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker(update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(TradeSubscribeRequest request, Action<DataEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.As<IEnumerable<SharedTrade>>(new[] { new SharedTrade(update.Data.Price, update.Data.Quantity, update.Data.TradeTime) })), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(BookTickerSubscribeRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.As(new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var listenKey = await Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return listenKey.As<UpdateSubscription>(default);

            var result = await Account.SubscribeToUserDataUpdatesAsync(listenKey.Data.Result,
                onAccountPositionMessage: update => handler(update.As(update.Data.Balances.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)))), 
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToOrderUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var listenKey = await Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return listenKey.As<UpdateSubscription>(default);

            var result = await Account.SubscribeToUserDataUpdatesAsync(listenKey.Data.Result,
                onOrderUpdateMessage: update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] { 
                    new SharedSpotOrder(
                        update.Data.Symbol,
                        update.Data.Id.ToString(),
                        update.Data.Type == Enums.SpotOrderType.Limit ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Limit : update.Data.Type == Enums.SpotOrderType.Market ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Market : update.Data.Type == Enums.SpotOrderType.LimitMaker ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.LimitMaker : CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Other,
                        update.Data.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                        update.Data.Status == Enums.OrderStatus.Canceled ? CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.Canceled : (update.Data.Status == Enums.OrderStatus.New || update.Data.Status == Enums.OrderStatus.PartiallyFilled) ? CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.PartiallyFilled : CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.Filled,
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        Price = update.Data.Price,
                        Quantity = update.Data.Quantity,
                        QuantityFilled = update.Data.QuantityFilled,
                        QuoteQuantity = update.Data.QuoteQuantity,
                        QuoteQuantityFilled = update.Data.QuoteQuantityFilled,
                        UpdateTime = update.Data.UpdateTime,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
                        TimeInForce = update.Data.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.ImmediateOrCancel : update.Data.TimeInForce == Enums.TimeInForce.FillOrKill ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.FillOrKill : update.Data.TimeInForce == Enums.TimeInForce.GoodTillDate ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.GoodTillDate : CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.GoodTillCanceled
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
    }
}
