using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
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
        public ApiType[] SupportedApiTypes => new[] { ApiType.Spot };

        #region Tickers client
        SubscriptionOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscriptionOptions("SubscribeTickersRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(ApiType apiType, Action<ExchangeEvent<IEnumerable<SharedSpotTicker>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITickersSocketClient)this).SubscribeAllTickersOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await ExchangeData.SubscribeToAllMiniTickerUpdatesAsync(update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x => new SharedSpotTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Ticker client
        SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await ExchangeData.SubscribeToMiniTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await ExchangeData.SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent<IEnumerable<SharedTrade>>(Exchange, new[] { new SharedTrade(update.Data.Price, update.Data.Quantity, update.Data.TradeTime) })), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await ExchangeData.SubscribeToBookTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Balance client
        SubscriptionOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions("SubscribeBalanceRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(ApiType apiType, Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var listenKey = await Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return new ExchangeResult<UpdateSubscription>(Exchange, listenKey.As<UpdateSubscription>(default));

            var result = await Account.SubscribeToUserDataUpdatesAsync(listenKey.Data.Result,
                onAccountPositionMessage: update => handler(update.AsExchangeEvent(Exchange, update.Data.Balances.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)))), 
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Spot Order client

        SubscriptionOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } = new SubscriptionOptions("SubscribeSpotOrderRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(Action<ExchangeEvent<IEnumerable<SharedSpotOrder>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var listenKey = await Account.StartUserStreamAsync().ConfigureAwait(false);
            if (!listenKey)
                return new ExchangeResult<UpdateSubscription>(Exchange, listenKey.As<UpdateSubscription>(default));

            var result = await Account.SubscribeToUserDataUpdatesAsync(listenKey.Data.Result,
                onOrderUpdateMessage: update => handler(update.AsExchangeEvent<IEnumerable<SharedSpotOrder>>(Exchange, new[] { 
                    new SharedSpotOrder(
                        update.Data.Symbol,
                        update.Data.Id.ToString(),
                        update.Data.Type == Enums.SpotOrderType.Limit ? SharedOrderType.Limit : update.Data.Type == Enums.SpotOrderType.Market ? SharedOrderType.Market : update.Data.Type == Enums.SpotOrderType.LimitMaker ? SharedOrderType.LimitMaker : SharedOrderType.Other,
                        update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        update.Data.Status == Enums.OrderStatus.Canceled ? SharedOrderStatus.Canceled : (update.Data.Status == Enums.OrderStatus.New || update.Data.Status == Enums.OrderStatus.PartiallyFilled) ? SharedOrderStatus.Open : SharedOrderStatus.Filled,
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
                        TimeInForce = update.Data.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : update.Data.TimeInForce == Enums.TimeInForce.GoodTillDate ? SharedTimeInForce.GoodTillDate : SharedTimeInForce.GoodTillCanceled,
                        LastTrade = update.Data.LastQuantityFilled == 0 ? null : new SharedUserTrade(update.Data.Symbol, update.Data.Id.ToString(), update.Data.TradeId.ToString(), update.Data.LastQuantityFilled, update.Data.LastPriceFilled, update.Data.UpdateTime)
                        {
                            Role = update.Data.BuyerIsMaker ? SharedRole.Maker : SharedRole.Taker
                        }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await ExchangeData.SubscribeToKlineUpdatesAsync(symbol, interval, update => handler(update.AsExchangeEvent(Exchange, new SharedKline(update.Data.Data.OpenTime, update.Data.Data.ClosePrice, update.Data.Data.HighPrice, update.Data.Data.LowPrice, update.Data.Data.OpenPrice, update.Data.Data.Volume))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 10, 20 });
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(symbol, request.Limit ?? 20, 100, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion
    }
}
