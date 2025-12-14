using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceSocketClientUsdFuturesApi : IBinanceSocketClientUsdFuturesApiShared
    {
        private const string _topicId = "BinanceUsdFutures";
        public string Exchange => BinanceExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes => new[] { TradingMode.DeliveryLinear, TradingMode.PerpetualLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Ticker client

        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions()
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToTickerUpdatesAsync(symbols, update => handler(update.ToType(new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume, update.Data.PriceChangePercent)
            {
                QuoteVolume = update.Data.QuoteVolume
            })), ct: ct).ConfigureAwait(false);            

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Tickers client

        SubscribeTickersOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions();
        async Task<ExchangeResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITickersSocketClient)this).SubscribeAllTickersOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await ExchangeData.SubscribeToAllTickerUpdatesAsync(update =>
            {
                IEnumerable<IBinance24HPrice> data = update.Data;
                if (request.TradingMode != null)
                    data = data.Where(x => request.TradingMode == TradingMode.PerpetualLinear ? !x.Symbol.Contains("_") : x.Symbol.Contains("_"));

                if (!data.Any())
                    return;

                handler(update.ToType(data.Select(x => new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.PriceChangePercent)
                {
                    QuoteVolume = x.QuoteVolume
                }).ToArray()));
            }, ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Trade client

        EndpointOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new EndpointOptions<SubscribeTradeRequest>(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToAggregatedTradeUpdatesAsync(symbols, update => handler(update.ToType(new[]
            { 
                new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.Quantity, update.Data.Price, update.Data.TradeTime)
            {
                Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
            } })), ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new EndpointOptions<SubscribeBookTickerRequest>(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToBookTickerUpdatesAsync(symbols, update => handler(update.ToType(new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);
            
            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Balance client
        EndpointOptions<SubscribeBalancesRequest> IBalanceSocketClient.SubscribeBalanceOptions { get; } = new EndpointOptions<SubscribeBalancesRequest>(false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SubscribeBalancesRequest.ListenKey), typeof(string), "The listenkey for starting the user stream", "123123123")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await Account.SubscribeToUserDataUpdatesAsync(request.ListenKey!,
                onAccountUpdate: update => handler(update.ToType(update.Data.UpdateData.Balances.Select(x => new SharedBalance(x.Asset, x.WalletBalance, x.WalletBalance)).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Futures Order client

        EndpointOptions<SubscribeFuturesOrderRequest> IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new EndpointOptions<SubscribeFuturesOrderRequest>(false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SubscribeFuturesOrderRequest.ListenKey), typeof(string), "The listenkey for starting the user stream", "123123123")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderSocketClient)this).SubscribeFuturesOrderOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await Account.SubscribeToUserDataUpdatesAsync(request.ListenKey!,
                onOrderUpdate: update => handler(update.ToType(new[] {
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.UpdateData.Symbol), update.Data.UpdateData.Symbol,
                        update.Data.UpdateData.OrderId.ToString(),
                        ParseOrderType(update.Data.UpdateData.Type),
                        update.Data.UpdateData.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        update.Data.UpdateData.Status == Enums.OrderStatus.Canceled ? SharedOrderStatus.Canceled : (update.Data.UpdateData.Status == Enums.OrderStatus.New || update.Data.UpdateData.Status == Enums.OrderStatus.PartiallyFilled) ? SharedOrderStatus.Open : SharedOrderStatus.Filled,
                        update.Data.UpdateData.UpdateTime)
                    {
                        ClientOrderId = update.Data.UpdateData.ClientOrderId,
                        OrderPrice = update.Data.UpdateData.Price == 0 ? null : update.Data.UpdateData.Price,
                        OrderQuantity = new SharedOrderQuantity(update.Data.UpdateData.Quantity, contractQuantity: update.Data.UpdateData.Quantity),
                        QuantityFilled = new SharedOrderQuantity(update.Data.UpdateData.AccumulatedQuantityOfFilledTrades, contractQuantity : update.Data.UpdateData.AccumulatedQuantityOfFilledTrades),
                        UpdateTime = update.Data.UpdateData.UpdateTime,
                        Fee = update.Data.UpdateData.Fee,
                        FeeAsset = update.Data.UpdateData.FeeAsset,
                        AveragePrice = update.Data.UpdateData.AveragePrice == 0 ? null : update.Data.UpdateData.AveragePrice,
                        PositionSide = update.Data.UpdateData.PositionSide == Enums.PositionSide.Long ? SharedPositionSide.Long : update.Data.UpdateData.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : null,
                        ReduceOnly = update.Data.UpdateData.IsReduce,
                        TimeInForce = update.Data.UpdateData.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.UpdateData.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        TriggerPrice = update.Data.UpdateData.StopPrice == 0 ? null : update.Data.UpdateData.StopPrice,
                        IsTriggerOrder = update.Data.UpdateData.StopPrice > 0,
                        IsCloseOrder = update.Data.UpdateData.IsClosePositionOrder,
                        LastTrade = update.Data.UpdateData.QuantityOfLastFilledTrade == 0 ? null : new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.UpdateData.Symbol), update.Data.UpdateData.Symbol, update.Data.UpdateData.OrderId.ToString(), update.Data.UpdateData.TradeId.ToString(), update.Data.UpdateData.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell, update.Data.UpdateData.QuantityOfLastFilledTrade, update.Data.UpdateData.PriceLastFilledTrade, update.Data.UpdateData.UpdateTime)
                        {
                            ClientOrderId = update.Data.UpdateData.ClientOrderId,
                            Role = update.Data.UpdateData.BuyerIsMaker ? SharedRole.Maker : SharedRole.Taker
                        }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        private SharedOrderType ParseOrderType(FuturesOrderType type)
        {
            if (type == FuturesOrderType.Market || type == FuturesOrderType.StopMarket || type == FuturesOrderType.TakeProfitMarket || type == FuturesOrderType.TrailingStopMarket)
                return SharedOrderType.Market;

            if (type == FuturesOrderType.Limit || type == FuturesOrderType.Stop || type == FuturesOrderType.TakeProfit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, ArgumentError.Invalid(nameof(SubscribeKlineRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToKlineUpdatesAsync(symbols, interval, update => handler(update.ToType(
                new SharedKline(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.Data.OpenTime, update.Data.Data.ClosePrice, update.Data.Data.HighPrice, update.Data.Data.LowPrice, update.Data.Data.OpenPrice, update.Data.Data.Volume))), false, false, ct).ConfigureAwait(false);
            
            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 10, 20 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await ExchangeData.SubscribeToPartialOrderBookUpdatesAsync(symbols, request.Limit ?? 20, 100, update => handler(update.ToType(new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            
            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Position client
        EndpointOptions<SubscribePositionRequest> IPositionSocketClient.SubscribePositionOptions { get; } = new EndpointOptions<SubscribePositionRequest>(false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SubscribePositionRequest.ListenKey), typeof(string), "The listenkey for starting the user stream", "123123123")
            }
        };
        async Task<ExchangeResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = ((IPositionSocketClient)this).SubscribePositionOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await Account.SubscribeToUserDataUpdatesAsync(request.ListenKey!,
                onAccountUpdate: update => handler(update.ToType(update.Data.UpdateData.Positions.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, Math.Abs(x.Quantity), update.Data.EventTime)
                {
                    AverageOpenPrice = x.EntryPrice,
                    PositionSide = x.PositionSide == Enums.PositionSide.Both ? (x.Quantity > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                    UnrealizedPnl = x.UnrealizedPnl
                }).ToArray())),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion
    }
}
