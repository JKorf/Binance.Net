using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceSocketClientSpotApi : IBinanceSocketClientSpotApiShared
    {
        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceSpot";
        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        #region Tickers client
        SubscribeTickersOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITickersSocketClient)this).SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await ExchangeData.SubscribeToAllRollingWindowTickerUpdatesAsync(TimeSpan.FromDays(1), update => handler(update.ToType(update.Data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.PriceChangePercent)
            {
                QuoteVolume = x.QuoteVolume
            }).ToArray())), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume, update.Data.PriceChangePercent)
            {
                QuoteVolume = update.Data.QuoteVolume
            })), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Trade client

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } 
            = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200,
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("Aggregated", typeof(bool), "Whether to subscribe to aggregated trade updates instead of individual trade updates", true)
            }
        };
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            if (ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "Aggregated") == true)
            {
                var result = await ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(symbols, update => handler(update.ToType(new[] 
                { 
                    new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol), update.Data.Symbol, update.Data.Quantity, update.Data.Price, update.Data.TradeTime)
                {
                    Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                } })), ct).ConfigureAwait(false);

                return result;
            }
            else
            {
                var result = await ExchangeData.SubscribeToTradeUpdatesAsync(symbols, update => handler(update.ToType(new[] 
                {
                    new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol), update.Data.Symbol,update.Data.Quantity, update.Data.Price, update.Data.TradeTime)
                {
                    Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                } })), ct).ConfigureAwait(false);

                return result;
            }
        }

        #endregion

        #region Book Ticker client

        SubscribeBookTickerOptions IBookTickerSocketClient.SubscribeBookTickerOptions { get; }
            = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<WebSocketResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await ExchangeData.SubscribeToBookTickerUpdatesAsync(symbols, update => handler(update.ToType(new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol), update.Data.Symbol, update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; }
            = new SubscribeBalanceOptions(_exchangeName, true);
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await Account.SubscribeToUserDataUpdatesAsync(
                onAccountPositionMessage: update => handler(update.ToType(update.Data.Balances.Select(x =>
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.Available,
                        x.Total)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Spot Order client

        SubscribeSpotOrderOptions ISpotOrderSocketClient.SubscribeSpotOrderOptions { get; } 
            = new SubscribeSpotOrderOptions(_exchangeName, true);
        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((ISpotOrderSocketClient)this).SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var result = await Account.SubscribeToUserDataUpdatesAsync(
                onOrderUpdateMessage: update => handler(update.ToType(new[] {
                    new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.Id.ToString(),
                        ParseOrderType(update.Data.Type),
                        update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Status),
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        OrderPrice = update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(update.Data.Quantity, update.Data.QuoteQuantity == 0 ? null : update.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(update.Data.QuantityFilled, update.Data.QuoteQuantityFilled),
                        UpdateTime = update.Data.UpdateTime,
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
                        TimeInForce = update.Data.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        TriggerPrice = update.Data.StopPrice == 0 ? null : update.Data.StopPrice,
                        IsTriggerOrder = update.Data.StopPrice > 0,
                        LastTrade = update.Data.LastQuantityFilled == 0 ? null : new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol), update.Data.Symbol, update.Data.Id.ToString(), update.Data.TradeId.ToString(), update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, update.Data.LastQuantityFilled, update.Data.LastPriceFilled, update.Data.UpdateTime)
                        {
                            Role = update.Data.BuyerIsMaker ? SharedRole.Maker : SharedRole.Taker,
                            ClientOrderId = update.Data.ClientOrderId
                        }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == Enums.OrderStatus.Canceled || status == OrderStatus.Rejected || status == OrderStatus.Expired)
                return SharedOrderStatus.Canceled;
            if (status == Enums.OrderStatus.PendingNew
                || status == Enums.OrderStatus.PendingCancel
                || status == Enums.OrderStatus.New
                || status == Enums.OrderStatus.PartiallyFilled)
            {
                return SharedOrderStatus.Open;
            }
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(SpotOrderType type)
        {
            if (type == SpotOrderType.Market || type == SpotOrderType.TakeProfit || type == SpotOrderType.StopLoss)
                return SharedOrderType.Market;

            if (type == SpotOrderType.Limit || type == SpotOrderType.LimitMaker || type == SpotOrderType.TakeProfitLimit || type == SpotOrderType.StopLossLimit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await ExchangeData.SubscribeToKlineUpdatesAsync(symbols, (KlineInterval)request.Interval, update => handler(update.ToType(
                new SharedKline(ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, update.Data.Symbol), update.Data.Symbol, update.Data.Data.OpenTime, update.Data.Data.ClosePrice, update.Data.Data.HighPrice, update.Data.Data.LowPrice, update.Data.Data.OpenPrice, update.Data.Data.Volume))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 10, 20 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, validationError);

            var symbols = request.SymbolNames(FormatSymbol);
            var result = await ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(symbols, request.Limit ?? 20, 100, update => handler(update.ToType(new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

            return result;
        }
        #endregion
    }
}
