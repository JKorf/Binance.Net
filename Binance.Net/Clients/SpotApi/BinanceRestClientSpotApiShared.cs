using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotApi : IBinanceRestClientSpotApiShared
    {
        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceSpot";
        public string Exchange => BinanceExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Klines Client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, true, true, 1000, false);
        Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IKlineRestClient)this).GetKlinesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var interval = (Enums.KlineInterval)request.Interval;
                    if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                        return SharedExecutionResult<SharedKline[]>.Error(new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported")));

                    var direction = request.Direction ?? DataDirection.Ascending;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var limit = request.Limit ?? 1000;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

                    // Get data
                    var result = await ExchangeData.GetKlinesAsync(
                        symbol,
                        interval,
                        pageParams.StartTime,
                        pageParams.EndTime,
                        limit,
                        ct: ct
                        ).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedKline[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromTime(pageParams, result.Data!.Max(x => x.OpenTime))
                                : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.OpenTime)),
                            result.Data!.Length,
                            result.Data.Select(x => x.OpenTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams);

                    // Return
                    return SharedExecutionResult<SharedKline[]>.Ok(result,
                        ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                            .ToArray(), nextPageRequest);
                });
        }

        #endregion

        #region Spot Symbol client
        EndpointOptions<GetSymbolsRequest, ISpotSymbolRestClient> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; }
            = new EndpointOptions<GetSymbolsRequest, ISpotSymbolRestClient>(_exchangeName, false);

        Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var result = await ExchangeData.GetExchangeInfoAsync(false, SymbolStatus.Trading, ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedSpotSymbol[]>.Error(result);

                    var resultData = result.Data!.Symbols.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.Status == SymbolStatus.Trading && s.IsSpotTradingAllowed)
                    {
                        MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                        MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                        MinNotionalValue = s.MinNotionalFilter?.MinNotional ?? s.NotionalFilter?.MinNotional,
                        QuantityStep = s.LotSizeFilter?.StepSize,
                        PriceStep = s.PriceFilter?.TickSize
                    }).ToArray();

                    ExchangeSymbolCache.UpdateSymbolInfo(_topicId, resultData);
                    return SharedExecutionResult<SharedSpotSymbol[]>.Ok(result, resultData);
                });

            //var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(request, TradingMode.Spot, SupportedTradingModes);
            //if (validationError != null)
            //    return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            //var result = await ExchangeData.GetExchangeInfoAsync(false, SymbolStatus.Trading, ct: ct).ConfigureAwait(false);
            //if (!result)
            //    return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            //var resultData = result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data.Symbols.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.Status == SymbolStatus.Trading && s.IsSpotTradingAllowed)
            //{
            //    MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
            //    MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
            //    MinNotionalValue = s.MinNotionalFilter?.MinNotional ?? s.NotionalFilter?.MinNotional,
            //    QuantityStep = s.LotSizeFilter?.StepSize,
            //    PriceStep = s.PriceFilter?.TickSize
            //}).ToArray());

            //ExchangeSymbolCache.UpdateSymbolInfo(_topicId, resultData.Data);
            //return resultData;
        }

        async Task<ExchangeResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Ticker client

        GetSpotTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotTickerRestClient)this).GetSpotTickerOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var result = await ExchangeData.GetTickerAsync(request.SymbolName(FormatSymbol), ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedSpotTicker>.Error(result);

                    return SharedExecutionResult<SharedSpotTicker>.Ok(result, new SharedSpotTicker(
                            ExchangeSymbolCache.ParseSymbol(_topicId, result.Data!.Symbol),
                            result.Data.Symbol,
                            result.Data.LastPrice,
                            result.Data.HighPrice,
                            result.Data.LowPrice,
                            result.Data.Volume,
                            result.Data.PriceChangePercent)
                    {
                        QuoteVolume = result.Data.QuoteVolume
                    });
                });
        }

        GetSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetSpotTickersOptions(_exchangeName);
        Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotTickerRestClient)this).GetSpotTickersOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedSpotTicker[]>.Error(result);

                    return SharedExecutionResult<SharedSpotTicker[]>.Ok(result, result.Data!.Select(x =>
                            new SharedSpotTicker(
                                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                                x.Symbol,
                                x.LastPrice,
                                x.HighPrice,
                                x.LowPrice,
                                x.Volume,
                                x.PriceChangePercent)
                            {
                                QuoteVolume = x.QuoteVolume
                            }).ToArray());
                });
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest, IBookTickerRestClient> IBookTickerRestClient.GetBookTickerOptions { get; }
            = new EndpointOptions<GetBookTickerRequest, IBookTickerRestClient>(_exchangeName, false);
        Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IBookTickerRestClient)this).GetBookTickerOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var resultTicker = await ExchangeData.GetBookPriceAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                    if (!resultTicker)
                        return SharedExecutionResult<SharedBookTicker>.Error(resultTicker);

                    return SharedExecutionResult<SharedBookTicker>.Ok(resultTicker, new SharedBookTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, resultTicker.Data!.Symbol),
                        resultTicker.Data.Symbol,
                        resultTicker.Data.BestAskPrice,
                        resultTicker.Data.BestAskQuantity,
                        resultTicker.Data.BestBidPrice,
                        resultTicker.Data.BestBidQuantity));
                });
        }

        #endregion

        #region Recent Trades client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 1000, false);

        Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IRecentTradeRestClient)this).GetRecentTradesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    // Get data
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var result = await ExchangeData.GetRecentTradesAsync(
                        symbol,
                        limit: request.Limit,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedTrade[]>.Error(result);

                    // Return
                    return SharedExecutionResult<SharedTrade[]>.Ok(result, result.Data!.Select(x =>
                        new SharedTrade(request.Symbol, symbol, x.BaseQuantity, x.Price, x.TradeTime)
                        {
                            Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                        }).ToArray());
                });
        }
        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(_exchangeName, true, true, true, 1000, false);
        Task<ExchangeWebResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ITradeHistoryRestClient)this).GetTradeHistoryOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var direction = request.Direction ?? DataDirection.Ascending;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var limit = request.Limit ?? 1000;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);
                    if (pageParams.FromId != null)
                        pageParams.StartTime = null; // If filtering using FromId no timestamps should be set

                    var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
                        symbol,
                        startTime: pageParams.StartTime,
                        endTime: pageParams.EndTime,
                        fromId: pageParams.FromId != null ? long.Parse(pageParams.FromId) : null,
                        limit: limit,
                        ct: ct).ConfigureAwait(false);

                    if (!result)
                        return SharedExecutionResult<SharedTrade[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                        () => direction == DataDirection.Ascending
                            ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                            : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.TradeTime), false),
                        result.Data!.Length,
                        result.Data.Select(x => x.TradeTime),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams);

                    // Return
                    return SharedExecutionResult<SharedTrade[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.TradeTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.TradeTime)
                                {
                                    Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                                }).ToArray(), nextPageRequest);
                });
        }
        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 1, 5000, false);
        Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IOrderBookRestClient)this).GetOrderBookOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var result = await ExchangeData.GetOrderBookAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        limit: request.Limit,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedOrderBook>.Error(result);

                    return SharedExecutionResult<SharedOrderBook>.Ok(result, new SharedOrderBook(result.Data!.Asks, result.Data.Bids));
                });
        }

        #endregion

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Funding, AccountTypeFilter.Spot);

        Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync<GetBalancesRequest, SharedBalance[], GetBalancesOptions>(
                 ((IBalanceRestClient)this).GetBalancesOptions,
                 request,
                 SupportedTradingModes,
                 async () =>
                 {
                     if (request.AccountType == SharedAccountType.Funding)
                     {
                         var result = await Account.GetFundingWalletAsync(ct: ct).ConfigureAwait(false);
                         if (!result)
                             return SharedExecutionResult<SharedBalance[]>.Error(result);

                         return SharedExecutionResult<SharedBalance[]>.Ok(result, result.Data!.Select(x => new SharedBalance(x.Asset, x.Available, x.Available + x.Freeze + x.Locked)).ToArray());
                     }
                     else
                     {
                         var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
                         if (!result)
                             return SharedExecutionResult<SharedBalance[]>.Error(result);

                         return SharedExecutionResult<SharedBalance[]>.Ok(result, result.Data!.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)).ToArray());
                     }
                 });
        }

        #endregion

        #region Spot Order Client

        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.OutputAsset;
        SharedOrderType[] ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market, SharedOrderType.LimitMaker };
        SharedTimeInForce[] ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        string ISpotOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(_exchangeName);
        Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).PlaceSpotOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var result = await Trading.PlaceOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                        request.OrderType == SharedOrderType.Limit ? Enums.SpotOrderType.Limit : request.OrderType == SharedOrderType.Market ? Enums.SpotOrderType.Market : Enums.SpotOrderType.LimitMaker,
                        quantity: request.Quantity?.QuantityInBaseAsset,
                        quoteQuantity: request.Quantity?.QuantityInQuoteAsset,
                        price: request.Price,
                        timeInForce: GetTimeInForce(request.TimeInForce, request.OrderType),
                        newClientOrderId: request.ClientOrderId,
                        ct: ct).ConfigureAwait(false);

                    if (!result)
                        return SharedExecutionResult<SharedId>.Error(result);

                    return SharedExecutionResult<SharedId>.Ok(result, new SharedId(result.Data!.Id.ToString()));
                });
        }

        EndpointOptions<GetOrderRequest, ISpotOrderRestClient> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest, ISpotOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).GetSpotOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedSpotOrder>.Error(new ExchangeWebResult<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id")));

                    var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedSpotOrder>.Error(order);

                    return SharedExecutionResult<SharedSpotOrder>.Ok(order, new SharedSpotOrder(
                            ExchangeSymbolCache.ParseSymbol(_topicId, order.Data!.Symbol),
                            order.Data.Symbol,
                            order.Data.Id.ToString(),
                            ParseOrderType(order.Data.Type),
                            order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            ParseOrderStatus(order.Data.Status),
                            order.Data.CreateTime)
                    {
                        ClientOrderId = order.Data.ClientOrderId,
                        AveragePrice = order.Data.AverageFillPrice,
                        OrderPrice = order.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity == 0 ? null : order.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                        TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                        UpdateTime = order.Data.UpdateTime,
                        TriggerPrice = order.Data.StopPrice,
                        IsTriggerOrder = order.Data.StopPrice != null
                    });
                });
        }

        EndpointOptions<GetOpenOrdersRequest, ISpotOrderRestClient> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; }
            = new EndpointOptions<GetOpenOrdersRequest, ISpotOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                    var orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                    if (!orders)
                        return SharedExecutionResult<SharedSpotOrder[]>.Error(orders);

                    return SharedExecutionResult<SharedSpotOrder[]>.Ok(orders, orders.Data!.Select(x => new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                        x.Symbol,
                        x.Id.ToString(),
                        ParseOrderType(x.Type),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        AveragePrice = x.AverageFillPrice,
                        OrderPrice = x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity == 0 ? null : x.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                        TimeInForce = ParseTimeInForce(x.TimeInForce),
                        UpdateTime = x.UpdateTime,
                        TriggerPrice = x.StopPrice,
                        IsTriggerOrder = x.StopPrice != null
                    }).ToArray());
                });
        }

        GetSpotClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, true, true, true, 1000);
        Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var direction = request.Direction ?? DataDirection.Ascending;
                    var limit = request.Limit ?? 1000;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var pageParams = Pagination.GetPaginationParameters(
                        direction, limit, request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageRequest,
                        direction == DataDirection.Ascending,
                        pageRequest?.FromId != null ? null : TimeSpan.FromDays(1));

                    // Get data
                    var result = await Trading.GetOrdersAsync(
                        symbol,
                        startTime: pageParams.FromId != null ? null : pageParams.StartTime,
                        endTime: pageParams.FromId != null ? null : pageParams.EndTime,
                        limit: limit,
                        orderId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedSpotOrder[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                           () => direction == DataDirection.Ascending
                               ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                               : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.CreateTime)),
                           result.Data!.Length,
                           result.Data.Select(x => x.CreateTime),
                           request.StartTime,
                           request.EndTime ?? DateTime.UtcNow,
                           pageParams,
                           pageRequest?.FromId != null ? null : TimeSpan.FromDays(1));

                    return SharedExecutionResult<SharedSpotOrder[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                            .Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled || x.Status == OrderStatus.Expired)
                            .Select(x => new SharedSpotOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                                x.Symbol,
                                x.Id.ToString(),
                                ParseOrderType(x.Type),
                                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                ParseOrderStatus(x.Status),
                                x.CreateTime)
                            {
                                ClientOrderId = x.ClientOrderId,
                                AveragePrice = x.AverageFillPrice,
                                OrderPrice = x.Price,
                                OrderQuantity = new SharedOrderQuantity(x.Quantity, x.QuoteQuantity == 0 ? null : x.QuoteQuantity),
                                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled),
                                TimeInForce = ParseTimeInForce(x.TimeInForce),
                                UpdateTime = x.UpdateTime,
                                TriggerPrice = x.StopPrice,
                                IsTriggerOrder = x.StopPrice != null
                            })
                            .ToArray(), nextPageRequest);
                });
        }

        EndpointOptions<GetOrderTradesRequest, ISpotOrderRestClient> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; }
            = new EndpointOptions<GetOrderTradesRequest, ISpotOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedUserTrade[]>.Error(new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id")));

                    var orders = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
                    if (!orders)
                        return SharedExecutionResult<SharedUserTrade[]>.Error(orders);

                    return SharedExecutionResult<SharedUserTrade[]>.Ok(orders, orders.Data!.Select(x => new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                        x.Symbol,
                        x.OrderId.ToString(),
                        x.Id.ToString(),
                        x.IsBuyer ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        x.Quantity,
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
                        Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
                    }).ToArray());
                });
        }

        GetSpotUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetSpotUserTradesOptions(_exchangeName, true, true, true, 1000);
        Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).GetSpotUserTradesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var direction = request.Direction ?? DataDirection.Ascending;
                    var limit = request.Limit ?? 1000;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var pageParams = Pagination.GetPaginationParameters(
                        direction, limit, request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageRequest,
                        direction == DataDirection.Ascending,
                        pageRequest?.FromId != null ? null : TimeSpan.FromDays(1));

                    // Get data
                    var result = await Trading.GetUserTradesAsync(
                        symbol,
                        startTime: pageParams.FromId != null ? null : pageParams.StartTime,
                        endTime: pageParams.FromId != null ? null : pageParams.EndTime,
                        limit: limit,
                        fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                        ct: ct
                        ).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedUserTrade[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                        () => direction == DataDirection.Ascending
                            ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                            : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.Timestamp), false),
                        result.Data!.Length,
                        result.Data.Select(x => x.Timestamp),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams,
                        pageRequest?.FromId != null ? null : TimeSpan.FromDays(1));

                    return SharedExecutionResult<SharedUserTrade[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedUserTrade(
                                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol),
                                    x.Symbol,
                                    x.OrderId.ToString(),
                                    x.Id.ToString(),
                                    x.IsBuyer ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                    x.Quantity,
                                    x.Price,
                                    x.Timestamp)
                                {
                                    Fee = x.Fee,
                                    FeeAsset = x.FeeAsset,
                                    Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
                                })
                            .ToArray(), nextPageRequest);
                });
        }

        EndpointOptions<CancelOrderRequest, ISpotOrderRestClient> ISpotOrderRestClient.CancelSpotOrderOptions { get; }
            = new EndpointOptions<CancelOrderRequest, ISpotOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderRestClient)this).CancelSpotOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedId>.Error(new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id")));

                    var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedId>.Error(order);

                    return SharedExecutionResult<SharedId>.Ok(order, new SharedId(order.Data!.Id.ToString()));
                });
        }

        private Enums.TimeInForce? GetTimeInForce(SharedTimeInForce? tif, SharedOrderType type)
        {
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;
            if (type == SharedOrderType.Limit) return TimeInForce.GoodTillCanceled; // Limit orders needs tif

            return null;
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
            if (type == SpotOrderType.Market) return SharedOrderType.Market;
            if (type == SpotOrderType.LimitMaker) return SharedOrderType.LimitMaker;
            if (type == SpotOrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(TimeInForce tif)
        {
            if (tif == TimeInForce.GoodTillCanceled) return SharedTimeInForce.GoodTillCanceled;
            if (tif == TimeInForce.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == TimeInForce.FillOrKill) return SharedTimeInForce.FillOrKill;

            return null;
        }

        #endregion

        #region Spot Client Id Order Client

        EndpointOptions<GetOrderRequest, ISpotOrderClientIdRestClient> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; }
            = new EndpointOptions<GetOrderRequest, ISpotOrderClientIdRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderClientIdRestClient)this).GetSpotOrderByClientOrderIdOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedSpotOrder>.Error(order);

                    return SharedExecutionResult<SharedSpotOrder>.Ok(order, new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, order.Data!.Symbol),
                        order.Data.Symbol,
                        order.Data.Id.ToString(),
                        ParseOrderType(order.Data.Type),
                        order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(order.Data.Status),
                        order.Data.CreateTime)
                    {
                        ClientOrderId = order.Data.ClientOrderId,
                        AveragePrice = order.Data.AverageFillPrice,
                        OrderPrice = order.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, order.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled),
                        TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                        UpdateTime = order.Data.UpdateTime,
                        TriggerPrice = order.Data.StopPrice,
                        IsTriggerOrder = order.Data.StopPrice != null
                    });
                });
        }

        EndpointOptions<CancelOrderRequest, ISpotOrderClientIdRestClient> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; }
            = new EndpointOptions<CancelOrderRequest, ISpotOrderClientIdRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotOrderClientIdRestClient)this).CancelSpotOrderByClientOrderIdOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedId>.Error(order);

                    return SharedExecutionResult<SharedId>.Ok(order, new SharedId(order.Data!.Id.ToString()));
                });
        }
        #endregion

        #region Asset client
        EndpointOptions<GetAssetsRequest, IAssetsRestClient> IAssetsRestClient.GetAssetsOptions { get; }
            = new EndpointOptions<GetAssetsRequest, IAssetsRestClient>(_exchangeName, true);

        Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IAssetsRestClient)this).GetAssetsOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
                    if (!assets)
                        return SharedExecutionResult<SharedAsset[]>.Error(assets);

                    return SharedExecutionResult<SharedAsset[]>.Ok(assets, assets.Data!.Select(x => new SharedAsset(x.Asset)
                    {
                        FullName = x.Name,
                        Networks = x.NetworkList.Select(x => new SharedAssetNetwork(x.Network)
                        {
                            FullName = x.Name,
                            MinConfirmations = x.MinConfirmations,
                            DepositEnabled = x.DepositEnabled,
                            MinWithdrawQuantity = x.WithdrawMin,
                            MaxWithdrawQuantity = x.WithdrawMax,
                            WithdrawEnabled = x.WithdrawEnabled,
                            WithdrawFee = x.WithdrawFee,
                            ContractAddress = x.ContractAddress
                        }).ToArray()
                    }).ToArray());
                });
        }

        EndpointOptions<GetAssetRequest, IAssetsRestClient> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest, IAssetsRestClient>(_exchangeName, false);
        Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IAssetsRestClient)this).GetAssetOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
                    if (!assets)
                        return SharedExecutionResult<SharedAsset>.Error(assets);

                    var asset = assets.Data!.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
                    if (asset == null)
                        return SharedExecutionResult<SharedAsset>.Error(new ExchangeWebResult<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, false, "Asset not found"))));

                    return SharedExecutionResult<SharedAsset>.Ok(assets, new SharedAsset(asset.Asset)
                    {
                        FullName = asset.Name,
                        Networks = asset.NetworkList.Select(x => new SharedAssetNetwork(x.Network)
                        {
                            FullName = x.Name,
                            MinConfirmations = x.MinConfirmations,
                            DepositEnabled = x.DepositEnabled,
                            MinWithdrawQuantity = x.WithdrawMin,
                            MaxWithdrawQuantity = x.WithdrawMax,
                            WithdrawEnabled = x.WithdrawEnabled,
                            WithdrawFee = x.WithdrawFee,
                            ContractAddress = x.ContractAddress
                        }).ToArray()
                    });
                });
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest, IDepositRestClient> IDepositRestClient.GetDepositAddressesOptions { get; }
            = new EndpointOptions<GetDepositAddressesRequest, IDepositRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IDepositRestClient)this).GetDepositAddressesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
                    if (!depositAddresses)
                        return SharedExecutionResult<SharedDepositAddress[]>.Error(depositAddresses);

                    return SharedExecutionResult<SharedDepositAddress[]>.Ok(depositAddresses, new[] {
                            new SharedDepositAddress(depositAddresses.Data!.Asset, depositAddresses.Data.Address)
                            {
                                TagOrMemo = depositAddresses.Data.Tag
                            }
                        });
                });
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(_exchangeName, false, true, true, 1000)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("TravelRuleEndpoint", typeof(bool), "Whether to use the TravelRule endpoint (true) or not (false, default)", true)
            }
        };
        Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync<GetDepositsRequest, SharedDeposit[], GetDepositsOptions>(
                ((IDepositRestClient)this).GetDepositsOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var limit = request.Limit ?? 100;
                    var direction = DataDirection.Descending;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true, TimeSpan.FromDays(90));

                    var traveRule = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "TravelRuleEndpoint");
                    if (traveRule == true)
                    {
                        // Get data
                        var result = await Account.GetTravelRuleDepositHistoryAsync(
                                request.Asset,
                                startTime: pageParams.StartTime,
                                endTime: pageParams.EndTime,
                                limit: limit,
                                offset: pageParams.Offset,
                                ct: ct).ConfigureAwait(false);
                        if (!result)
                            return SharedExecutionResult<SharedDeposit[]>.Error(result);

                        var nextPageRequest = Pagination.GetNextPageRequest(
                            () => Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.InsertTime), false),
                            result.Data!.Length,
                            result.Data.Select(x => x.InsertTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams,
                            TimeSpan.FromDays(90));

                        return SharedExecutionResult<SharedDeposit[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedDeposit(
                                    x.Asset,
                                    x.Quantity,
                                    x.Status == DepositStatus.Success,
                                    x.InsertTime,
                                    ParseTransferStatus(x.Status))
                                {
                                    Confirmations = x.Confirmations.Contains("/") ? int.Parse(x.Confirmations.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0]) : null,
                                    Network = x.Network,
                                    TransactionId = x.TransactionId,
                                    Tag = x.AddressTag,
                                    Id = x.Id
                                }).ToArray(), nextPageRequest);
                    }
                    else
                    {
                        var result = await Account.GetDepositHistoryAsync(
                            request.Asset,
                            startTime: pageParams.StartTime,
                            endTime: pageParams.EndTime,
                            limit: limit,
                            offset: pageParams.Offset,
                            ct: ct).ConfigureAwait(false);
                        if (!result)
                            return SharedExecutionResult<SharedDeposit[]>.Error(result);

                        var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                                : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.InsertTime), false),
                            result.Data!.Length,
                            result.Data.Select(x => x.InsertTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams,
                            TimeSpan.FromDays(90));

                        return SharedExecutionResult<SharedDeposit[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedDeposit(
                                    x.Asset,
                                    x.Quantity,
                                    x.Status == DepositStatus.Success,
                                    x.InsertTime,
                                    ParseTransferStatus(x.Status))
                                {
                                    Confirmations = x.Confirmations.Contains("/") ? int.Parse(x.Confirmations.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0]) : null,
                                    Network = x.Network,
                                    TransactionId = x.TransactionId,
                                    Tag = x.AddressTag,
                                    Id = x.Id
                                }).ToArray(), nextPageRequest);
                    }
                });
        }

        private SharedTransferStatus ParseTransferStatus(DepositStatus status)
        {
            if (status == DepositStatus.Success)
                return SharedTransferStatus.Completed;
            if (status == DepositStatus.Pending || status == DepositStatus.Completed || status == DepositStatus.WaitingUserConfirm)
                return SharedTransferStatus.InProgress;
            if (status == DepositStatus.Rejected || status == DepositStatus.WrongDeposit)
                return SharedTransferStatus.Failed;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(_exchangeName, false, true, true, 1000)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("TravelRuleEndpoint", typeof(bool), "Whether to use the TravelRule endpoint (true) or not (false, default)", true)
            }
        };
        Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync<GetWithdrawalsRequest, SharedWithdrawal[], GetWithdrawalsOptions>(
                ((IWithdrawalRestClient)this).GetWithdrawalsOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var limit = request.Limit ?? 100;
                    var direction = DataDirection.Descending;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true, TimeSpan.FromDays(90));

                    var traveRule = ExchangeParameters.GetValue<bool?>(request.ExchangeParameters, Exchange, "TravelRuleEndpoint");
                    if (traveRule == true)
                    {
                        var result = await Account.GetTravelRuleWithdrawalHistoryAsync(
                            request.Asset,
                            startTime: pageParams.StartTime,
                            endTime: pageParams.EndTime,
                            limit: limit,
                            offset: pageParams.Offset,
                            ct: ct).ConfigureAwait(false);
                        if (!result)
                            return SharedExecutionResult<SharedWithdrawal[]>.Error(result);

                        var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                                : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.ApplyTime), false),
                            result.Data!.Length,
                            result.Data.Select(x => x.ApplyTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams,
                            TimeSpan.FromDays(90));

                        return SharedExecutionResult<SharedWithdrawal[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedWithdrawal(x.Asset, x.Address, x.Quantity, x.Status == WithdrawalStatus.Completed, x.ApplyTime)
                                {
                                    Confirmations = x.ConfirmTimes,
                                    Network = x.Network,
                                    Tag = x.AddressTag,
                                    TransactionId = x.TransactionId,
                                    Fee = x.TransactionFee,
                                    Id = x.Id
                                })
                            .ToArray(), nextPageRequest);
                    }
                    else
                    {
                        var result = await Account.GetWithdrawalHistoryAsync(
                           request.Asset,
                           startTime: pageParams.StartTime,
                           endTime: pageParams.EndTime,
                           limit: limit,
                           offset: pageParams.Offset,
                           ct: ct).ConfigureAwait(false);
                        if (!result)
                            return SharedExecutionResult<SharedWithdrawal[]>.Error(result);

                        var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromId(result.Data!.Max(x => x.Id) + 1)
                                : Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.ApplyTime), false),
                            result.Data!.Length,
                            result.Data.Select(x => x.ApplyTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams,
                            TimeSpan.FromDays(90));

                        return SharedExecutionResult<SharedWithdrawal[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedWithdrawal(x.Asset, x.Address, x.Quantity, x.Status == WithdrawalStatus.Completed, x.ApplyTime)
                                {
                                    Confirmations = x.ConfirmTimes,
                                    Network = x.Network,
                                    Tag = x.AddressTag,
                                    TransactionId = x.TransactionId,
                                    Fee = x.TransactionFee,
                                    Id = x.Id
                                })
                            .ToArray(), nextPageRequest);
                    }
                });
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("TravelRuleQuestionnaire", typeof(BinanceWithdrawQuestionnaire), "Travel rule questionnaire", new BinanceWithdrawQuestionnaireEu())
            }
        };
        Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync<WithdrawRequest, SharedId, WithdrawOptions>(
                ((IWithdrawRestClient)this).WithdrawOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var questionnaire = ExchangeParameters.GetValue<BinanceWithdrawQuestionnaire?>(request.ExchangeParameters, Exchange, "TravelRuleQuestionnaire");
                    if (questionnaire == null)
                    {
                        var withdrawal = await Account.WithdrawAsync(
                            request.Asset,
                            request.Address,
                            request.Quantity,
                            network: request.Network,
                            addressTag: request.AddressTag,
                            ct: ct).ConfigureAwait(false);
                        if (!withdrawal)
                            return SharedExecutionResult<SharedId>.Error(withdrawal);

                        return SharedExecutionResult<SharedId>.Ok(withdrawal, new SharedId(withdrawal.Data!.Id));
                    }
                    else
                    {
                        var withdrawal = await Account.TravelRuleWithdrawAsync(
                            request.Asset,
                            request.Address,
                            request.Quantity,
                            questionnaire,
                            network: request.Network,
                            addressTag: request.AddressTag,
                            ct: ct).ConfigureAwait(false);
                        if (!withdrawal)
                            return SharedExecutionResult<SharedId>.Error(withdrawal);

                        return SharedExecutionResult<SharedId>.Ok(withdrawal, new SharedId(withdrawal.Data!.TravelRuleId.ToString()));
                    }
                });
        }

        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest, IFeeRestClient> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest, IFeeRestClient>(_exchangeName, true);

        Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFeeRestClient)this).GetFeeOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    // Get data
                    var result = await Account.GetTradeFeeAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFee>.Error(result);

                    var symbol = result.Data!.SingleOrDefault();
                    if (symbol == null)
                        return SharedExecutionResult<SharedFee>.Error(new ExchangeWebResult<SharedFee>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol not found"))));

                    // Return
                    return SharedExecutionResult<SharedFee>.Ok(result, new SharedFee(symbol.MakerFee * 100, symbol.TakerFee * 100));
                });
        }
        #endregion

        #region Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotTriggerOrderRestClient)this).PlaceSpotTriggerOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var type = GetTriggerOrderParameters(request.PriceDirection, request.OrderPrice, request.OrderSide);
                    var result = await Trading.PlaceOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        request.OrderSide == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                        type,
                        request.Quantity.QuantityInBaseAsset,
                        //timeInForce: request.OrderPrice == null ? TimeInForce.ImmediateOrCancel : TimeInForce.GoodTillCanceled,
                        price: request.OrderPrice,
                        newClientOrderId: request.ClientOrderId,
                        stopPrice: request.TriggerPrice,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedId>.Error(result);

                    // Return
                    return SharedExecutionResult<SharedId>.Ok(result, new SharedId(result.Data!.Id.ToString()));
                });
        }

        EndpointOptions<GetOrderRequest, ISpotTriggerOrderRestClient> ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; }
            = new EndpointOptions<GetOrderRequest, ISpotTriggerOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotTriggerOrderRestClient)this).GetSpotTriggerOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    if (!long.TryParse(request.OrderId, out var id))
                        throw new ArgumentException($"Invalid order id");

                    var result = await Trading.GetOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        id,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedSpotTriggerOrder>.Error(result);

                    var (orderType, orderDirection) = ParseTriggerDirections(result.Data!.Type, result.Data.Side);
                    // Return
                    return SharedExecutionResult<SharedSpotTriggerOrder>.Ok(result, new SharedSpotTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, result.Data.Symbol),
                        result.Data.Symbol,
                        result.Data.Id.ToString(),
                        orderType,
                        orderDirection,
                        ParseTriggerOrderStatus(result.Data),
                        result.Data.StopPrice ?? 0,
                        result.Data.CreateTime
                        )
                    {
                        PlacedOrderId = result.Data.Id.ToString(),
                        AveragePrice = result.Data.AverageFillPrice,
                        OrderPrice = result.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(result.Data.Quantity, result.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(result.Data.QuantityFilled, result.Data.QuoteQuantityFilled),
                        TimeInForce = ParseTimeInForce(result.Data.TimeInForce),
                        UpdateTime = result.Data.UpdateTime,
                        ClientOrderId = result.Data.ClientOrderId
                    });
                });
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(BinanceOrder data)
        {
            if (data.Status == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (data.Status == OrderStatus.Canceled
                || data.Status == OrderStatus.Rejected
                || data.Status == OrderStatus.Expired
                || data.Status == OrderStatus.ExpiredInMatch)
            {
                return SharedTriggerOrderStatus.CanceledOrRejected;
            }

            if (data.Status == OrderStatus.New
                || data.Status == OrderStatus.PartiallyFilled
                || data.Status == OrderStatus.PendingCancel)
            {
                return SharedTriggerOrderStatus.Active;
            }

            return SharedTriggerOrderStatus.Unknown;
        }

        EndpointOptions<CancelOrderRequest, ISpotTriggerOrderRestClient> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; }
            = new EndpointOptions<CancelOrderRequest, ISpotTriggerOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ISpotTriggerOrderRestClient)this).CancelSpotTriggerOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedId>.Error(new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id")));

                    var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedId>.Error(order);

                    return SharedExecutionResult<SharedId>.Ok(order, new SharedId(order.Data!.Id.ToString()));
                });
        }


        private SpotOrderType GetTriggerOrderParameters(SharedTriggerPriceDirection orderType, decimal? orderPrice, SharedOrderSide side)
        {
            if (orderType == SharedTriggerPriceDirection.PriceBelow)
            {
                if (side == SharedOrderSide.Buy)
                    // PriceBelow + Enter = TakeProfit Buy order
                    return orderPrice == null ? SpotOrderType.TakeProfit : SpotOrderType.TakeProfitLimit;
                else
                    // PriceBelow + Exit = StopLoss Sell order
                    return orderPrice == null ? SpotOrderType.StopLoss : SpotOrderType.StopLossLimit;
            }

            if (side == SharedOrderSide.Buy)
                // PriceAbove + Enter = StopLoss Buy order
                return orderPrice == null ? SpotOrderType.StopLoss : SpotOrderType.StopLossLimit;
            else
                // PriceAbove + Exit = TakeProfit Sell order
                return orderPrice == null ? SpotOrderType.TakeProfit : SpotOrderType.TakeProfitLimit;
        }

        private (SharedOrderType, SharedTriggerOrderDirection) ParseTriggerDirections(SpotOrderType orderType, OrderSide side)
        {
            if (orderType == SpotOrderType.TakeProfit || orderType == SpotOrderType.TakeProfitLimit)
            {
                if (side == OrderSide.Buy)
                {
                    // TakeProfit + Buy = PriceBelow Enter
                    return (
                        orderType == SpotOrderType.TakeProfit ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Enter);
                }
                else
                {
                    // TakeProfit + Sell = PriceAbove Exit
                    return (
                        orderType == SpotOrderType.TakeProfit ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Exit);
                }
            }

            if (side == OrderSide.Buy)
            {
                // StopLoss + Buy = PriceAbove Enter
                return (
                    orderType == SpotOrderType.StopLoss ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Enter);
            }
            else
            {
                // StopLoss + Sell = PriceBelow Exit
                return (
                    orderType == SpotOrderType.StopLoss ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Exit);
            }
        }
        #endregion

        #region Transfer client

        TransferOptions ITransferRestClient.TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Funding,
            SharedAccountType.Spot,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin,
            SharedAccountType.Option
            ]);
        Task<ExchangeWebResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ITransferRestClient)this).TransferOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var transferType = GetTransferType(request);
                    if (transferType == null)
                        return SharedExecutionResult<SharedId>.Error(new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination")));

                    // Get data
                    var transfer = await Account.TransferAsync(
                        transferType.Value,
                        request.Asset,
                        request.Quantity,
                        request.FromSymbol,
                        request.ToSymbol,
                        ct: ct).ConfigureAwait(false);
                    if (!transfer)
                        return SharedExecutionResult<SharedId>.Error(transfer);

                    return SharedExecutionResult<SharedId>.Ok(transfer, new SharedId(transfer.Data!.TransactionId.ToString()));
                });
        }

        private UniversalTransferType? GetTransferType(TransferRequest request)
        {
            if (request.FromAccountType == SharedAccountType.Funding)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.FundingToMargin;
                if (request.ToAccountType == SharedAccountType.PerpetualInverseFutures || request.ToAccountType == SharedAccountType.DeliveryInverseFutures) return UniversalTransferType.FundingToCoinFutures;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.FundingToUsdFutures;
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.FundingToOption;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.FundingToMain;
            }
            else if (request.FromAccountType == SharedAccountType.Spot)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.MainToMargin;
                if (request.ToAccountType == SharedAccountType.IsolatedMargin) return UniversalTransferType.MainToIsolatedMargin;
                if (request.ToAccountType == SharedAccountType.PerpetualInverseFutures || request.ToAccountType == SharedAccountType.DeliveryInverseFutures) return UniversalTransferType.MainToCoinFutures;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.MainToUsdFutures;
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.MainToOption;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.MainToFunding;
            }
            else if (request.FromAccountType == SharedAccountType.CrossMargin)
            {
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.MarginToOption;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.MarginToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.MarginToMain;
                if (request.ToAccountType == SharedAccountType.PerpetualInverseFutures || request.ToAccountType == SharedAccountType.DeliveryInverseFutures) return UniversalTransferType.MarginToCoinFutures;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.MarginToUsdFutures;
                if (request.ToAccountType == SharedAccountType.IsolatedMargin) return UniversalTransferType.MarginToIsolatedMargin;
            }
            else if (request.FromAccountType == SharedAccountType.IsolatedMargin)
            {
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.IsolatedMarginToMain;
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.IsolatedMarginToMargin;
                if (request.ToAccountType == SharedAccountType.IsolatedMargin) return UniversalTransferType.IsolatedMarginToIsolatedMargin;
            }
            else if (request.FromAccountType == SharedAccountType.PerpetualLinearFutures || request.FromAccountType == SharedAccountType.DeliveryLinearFutures)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.UsdFuturesToMargin;
                if (request.ToAccountType == SharedAccountType.Option) return UniversalTransferType.UsdFuturesToOption;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.UsdFuturesToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.UsdFuturesToMain;
            }
            else if (request.FromAccountType == SharedAccountType.PerpetualInverseFutures || request.FromAccountType == SharedAccountType.DeliveryInverseFutures)
            {
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.CoinFuturesToMargin;
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.CoinFuturesToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.CoinFuturesToMain;
            }
            else if (request.FromAccountType == SharedAccountType.Option)
            {
                if (request.ToAccountType == SharedAccountType.Funding) return UniversalTransferType.OptionToFunding;
                if (request.ToAccountType == SharedAccountType.Spot) return UniversalTransferType.OptionToMain;
                if (request.ToAccountType == SharedAccountType.CrossMargin) return UniversalTransferType.OptionToMargin;
                if (request.ToAccountType == SharedAccountType.PerpetualLinearFutures || request.ToAccountType == SharedAccountType.DeliveryLinearFutures) return UniversalTransferType.OptionToUsdFutures;
            }

            return null;
        }

        #endregion
    }
}
