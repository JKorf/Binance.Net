using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.UsdFuturesApi
{
    internal partial class BinanceRestClientUsdFuturesApi : IBinanceRestClientUsdFuturesApiShared
    {
        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceUsdFutures";
        public string Exchange => BinanceExchange.ExchangeName;

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.DeliveryLinear, TradingMode.PerpetualLinear };
        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Klines client

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
                                ? Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime))
                                : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                            result.Data.Length,
                            result.Data.Select(x => x.OpenTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams);

                    // Return
                    return SharedExecutionResult<SharedKline[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                            .ToArray(), nextPageRequest);


                });
        }

        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, true, true, 1000, false);

        Task<ExchangeWebResult<SharedFuturesKline[]>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var interval = (Enums.KlineInterval)request.Interval;
                    if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                        return SharedExecutionResult<SharedFuturesKline[]>.Error(new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported")));

                    var direction = request.Direction ?? DataDirection.Ascending;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var limit = request.Limit ?? 1000;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

                    var result = await ExchangeData.GetMarkPriceKlinesAsync(
                        symbol,
                        interval,
                        limit,
                        pageParams.StartTime,
                        pageParams.EndTime,
                        ct: ct
                        ).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFuturesKline[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime))
                                : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                            result.Data.Length,
                            result.Data.Select(x => x.OpenTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams);

                    // Return
                    return SharedExecutionResult<SharedFuturesKline[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedFuturesKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice))
                            .ToArray(), nextPageRequest);


                });
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetSymbolsRequest, IFuturesSymbolRestClient> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest, IFuturesSymbolRestClient>(_exchangeName, false);
        Task<ExchangeWebResult<SharedFuturesSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFuturesSymbol[]>.Error(result);

                    var data = result.Data.Symbols.Where(x => x.ContractType != null);
                    if (request.TradingMode != null)
                        data = data.Where(x => FilterContractType(request.TradingMode.Value, x.ContractType!.Value));
                    var resultData = data.Select(s =>
                    new SharedFuturesSymbol(s.ContractType == ContractType.Perpetual ? TradingMode.PerpetualLinear : TradingMode.DeliveryLinear,
                    s.BaseAsset,
                    s.QuoteAsset,
                    s.Name,
                    s.Status == SymbolStatus.Trading)
                    {
                        MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                        MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                        QuantityStep = s.LotSizeFilter?.StepSize,
                        PriceStep = s.PriceFilter?.TickSize,
                        MinNotionalValue = s.MinNotionalFilter?.MinNotional,
                        ContractSize = 1,
                        DeliveryTime = s.DeliveryDate.Year == 2100 ? null : s.DeliveryDate
                    }).ToArray();

                    ExchangeSymbolCache.UpdateSymbolInfo(_topicId, resultData);
                    return SharedExecutionResult<SharedFuturesSymbol[]>.Ok(result, resultData);


                });
        }

        private bool FilterContractType(TradingMode mode, ContractType type)
        {
            var isPerp = type == ContractType.Perpetual || type == ContractType.PerpetualDelivering || type == ContractType.PerpetualTradFi;
            if (mode == TradingMode.PerpetualLinear)
                return isPerp;

            return !isPerp;
        }

        async Task<ExchangeResult<SharedSymbol[]>> IFuturesSymbolRestClient.GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<SharedSymbol[]>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<SharedSymbol[]>(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, baseAsset));
        }

        async Task<ExchangeResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbol));
        }

        async Task<ExchangeResult<bool>> IFuturesSymbolRestClient.SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId))
            {
                var symbols = await ((IFuturesSymbolRestClient)this).GetFuturesSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols)
                    return new ExchangeResult<bool>(Exchange, symbols.Error!);
            }

            return new ExchangeResult<bool>(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, symbolName));
        }
        #endregion

        #region Ticker client

        GetFuturesTickerOptions IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTickerRestClient)this).GetFuturesTickerOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var resultTicker = ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct);
                    var resultMarkPrice = ExchangeData.GetMarkPriceAsync(request.Symbol!.GetSymbol(FormatSymbol), ct);
                    await Task.WhenAll(resultTicker, resultMarkPrice).ConfigureAwait(false);

                    if (!resultTicker.Result)
                        return SharedExecutionResult<SharedFuturesTicker>.Error(resultTicker.Result);
                    if (!resultMarkPrice.Result)
                        return SharedExecutionResult<SharedFuturesTicker>.Error(resultMarkPrice.Result);

                    return SharedExecutionResult<SharedFuturesTicker>.Ok(resultTicker.Result, new SharedFuturesTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, resultTicker.Result.Data.Symbol), resultTicker.Result.Data.Symbol, resultTicker.Result.Data.LastPrice, resultTicker.Result.Data.HighPrice, resultTicker.Result.Data.LowPrice, resultTicker.Result.Data.Volume, resultTicker.Result.Data.PriceChangePercent)
                    {
                        MarkPrice = resultMarkPrice.Result.Data.MarkPrice,
                        IndexPrice = resultMarkPrice.Result.Data.IndexPrice,
                        FundingRate = resultMarkPrice.Result.Data.FundingRate,
                        NextFundingTime = resultMarkPrice.Result.Data.NextFundingTime == default ? null : resultMarkPrice.Result.Data.NextFundingTime
                    });


                });
        }

        GetFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new GetFuturesTickersOptions(_exchangeName);
        Task<ExchangeWebResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTickerRestClient)this).GetFuturesTickersOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var resultTickers = ExchangeData.GetTickersAsync(ct: ct);
                    var resultMarkPrices = ExchangeData.GetMarkPricesAsync(ct: ct);
                    await Task.WhenAll(resultTickers, resultMarkPrices).ConfigureAwait(false);
                    if (!resultTickers.Result)
                        return SharedExecutionResult<SharedFuturesTicker[]>.Error(resultTickers.Result);
                    if (!resultMarkPrices.Result)
                        return SharedExecutionResult<SharedFuturesTicker[]>.Error(resultMarkPrices.Result);

                    IEnumerable<IBinance24HPrice> data = resultTickers.Result.Data;
                    if (request.TradingMode.HasValue)
                        data = data.Where(x => (request.TradingMode == TradingMode.DeliveryLinear ? x.Symbol!.Contains("_") : !x.Symbol!.Contains("_")));

                    return SharedExecutionResult<SharedFuturesTicker[]>.Ok(resultTickers.Result, data.Select(x =>
                    {
                        var markPrice = resultMarkPrices.Result.Data.Single(p => p.Symbol == x.Symbol);
                        return new SharedFuturesTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.PriceChangePercent)
                        {
                            IndexPrice = markPrice.IndexPrice,
                            MarkPrice = markPrice.MarkPrice,
                            FundingRate = markPrice.FundingRate,
                            NextFundingTime = markPrice.NextFundingTime == default ? null : markPrice.NextFundingTime
                        };
                    }).ToArray());


                });
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest, IBookTickerRestClient> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest, IBookTickerRestClient>(_exchangeName, false);
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
                        ExchangeSymbolCache.ParseSymbol(_topicId, resultTicker.Data.Symbol),
                        resultTicker.Data.Symbol,
                        resultTicker.Data.BestAskPrice,
                        resultTicker.Data.BestAskQuantity,
                        resultTicker.Data.BestBidPrice,
                        resultTicker.Data.BestBidQuantity));


                });
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 1000, false);
        Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IRecentTradeRestClient)this).GetRecentTradesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var result = await ExchangeData.GetRecentTradesAsync(
                        symbol,
                        limit: request.Limit,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedTrade[]>.Error(result);

                    return SharedExecutionResult<SharedTrade[]>.Ok(result, result.Data.Select(x => new SharedTrade(
                        request.Symbol, symbol, x.BaseQuantity, x.Price, x.TradeTime)
                    {
                        Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                    }).ToArray());


                });
        }

        #endregion

        #region Futures Order Client

        SharedFeeDeductionType IFuturesOrderRestClient.FuturesFeeDeductionType => SharedFeeDeductionType.AddToCost;
        SharedFeeAssetType IFuturesOrderRestClient.FuturesFeeAssetType => SharedFeeAssetType.QuoteAsset;

        SharedOrderType[] IFuturesOrderRestClient.FuturesSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        SharedTimeInForce[] IFuturesOrderRestClient.FuturesSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport IFuturesOrderRestClient.FuturesSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset);

        string IFuturesOrderRestClient.GenerateClientOrderId() => ExchangeHelpers.RandomString(20);

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(_exchangeName, false);
        Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Trading.PlaceOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                        request.OrderType == SharedOrderType.Limit ? Enums.FuturesOrderType.Limit : Enums.FuturesOrderType.Market,
                        quantity: request.Quantity?.QuantityInBaseAsset ?? request.Quantity?.QuantityInContracts,
                        price: request.Price,
                        positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                        reduceOnly: request.ReduceOnly,
                        timeInForce: GetTimeInForce(request.OrderType, request.TimeInForce),
                        newClientOrderId: request.ClientOrderId,
                        ct: ct).ConfigureAwait(false);

                    if (!result)
                        return SharedExecutionResult<SharedId>.Error(result);

                    return SharedExecutionResult<SharedId>.Ok(result, new SharedId(result.Data.Id.ToString()));


                });
        }

        EndpointOptions<GetOrderRequest, IFuturesOrderRestClient> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest, IFuturesOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).GetFuturesOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedFuturesOrder>.Error(new ExchangeWebResult<SharedFuturesOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id")));

                    var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedFuturesOrder>.Error(order);

                    return SharedExecutionResult<SharedFuturesOrder>.Ok(order, new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol), order.Data.Symbol,
                        order.Data.Id.ToString(),
                        ParseOrderType(order.Data.Type),
                        order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(order.Data.Status),
                        order.Data.CreateTime)
                    {
                        ClientOrderId = order.Data.ClientOrderId,
                        AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                        OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, contractQuantity: order.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled, contractQuantity: order.Data.QuantityFilled),
                        TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                        UpdateTime = order.Data.UpdateTime,
                        PositionSide = order.Data.PositionSide == PositionSide.Both ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        ReduceOnly = order.Data.ReduceOnly,
                        TriggerPrice = order.Data.StopPrice,
                        IsTriggerOrder = order.Data.StopPrice > 0
                    });


                });
        }

        EndpointOptions<GetOpenOrdersRequest, IFuturesOrderRestClient> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest, IFuturesOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var symbol = request.Symbol?.GetSymbol(FormatSymbol);
                    var orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
                    if (!orders)
                        return SharedExecutionResult<SharedFuturesOrder[]>.Error(orders);

                    return SharedExecutionResult<SharedFuturesOrder[]>.Ok(orders, orders.Data.Select(x => new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol,
                        x.Id.ToString(),
                        ParseOrderType(x.Type),
                        x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Status),
                        x.CreateTime)
                    {
                        ClientOrderId = x.ClientOrderId,
                        AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                        OrderPrice = x.Price == 0 ? null : x.Price,
                        OrderQuantity = new SharedOrderQuantity(x.Quantity, contractQuantity: x.Quantity),
                        QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled, x.QuantityFilled),
                        TimeInForce = ParseTimeInForce(x.TimeInForce),
                        UpdateTime = x.UpdateTime,
                        PositionSide = x.PositionSide == PositionSide.Both ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        ReduceOnly = x.ReduceOnly,
                        TriggerPrice = x.StopPrice,
                        IsTriggerOrder = x.StopPrice > 0
                    }).ToArray());


                });
        }

        GetFuturesClosedOrdersOptions IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new GetFuturesClosedOrdersOptions(_exchangeName, true, true, true, 1000);
        Task<ExchangeWebResult<SharedFuturesOrder[]>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions,
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
                        pageRequest?.FromId != null ? null : TimeSpan.FromDays(7));

                    // Get data
                    var result = await Trading.GetOrdersAsync(
                        symbol,
                        startTime: pageParams.FromId != null ? null : pageParams.StartTime,
                        endTime: pageParams.FromId != null ? null : pageParams.EndTime,
                        limit: limit,
                        orderId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFuturesOrder[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                           () => direction == DataDirection.Ascending
                               ? Pagination.NextPageFromId(result.Data.Max(x => x.Id) + 1)
                               : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.CreateTime)),
                           result.Data.Length,
                           result.Data.Select(x => x.CreateTime),
                           request.StartTime,
                           request.EndTime ?? DateTime.UtcNow,
                           pageParams,
                           pageRequest?.FromId != null ? null : TimeSpan.FromDays(7));

                    // Return
                    return SharedExecutionResult<SharedFuturesOrder[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                        .Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled || x.Status == OrderStatus.Expired)
                        .Select(x =>
                            new SharedFuturesOrder(
                                ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol,
                                x.Id.ToString(),
                                ParseOrderType(x.Type),
                                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                ParseOrderStatus(x.Status),
                                x.CreateTime)
                            {
                                ClientOrderId = x.ClientOrderId,
                                AveragePrice = x.AveragePrice == 0 ? null : x.AveragePrice,
                                OrderPrice = x.Price == 0 ? null : x.Price,
                                OrderQuantity = new SharedOrderQuantity(x.Quantity, contractQuantity: x.Quantity),
                                QuantityFilled = new SharedOrderQuantity(x.QuantityFilled, x.QuoteQuantityFilled, x.QuantityFilled),
                                TimeInForce = ParseTimeInForce(x.TimeInForce),
                                UpdateTime = x.UpdateTime,
                                PositionSide = x.PositionSide == PositionSide.Both ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                                ReduceOnly = x.ReduceOnly,
                                TriggerPrice = x.StopPrice,
                                IsTriggerOrder = x.StopPrice > 0
                            }).ToArray(), nextPageRequest);


                });
        }

        EndpointOptions<GetOrderTradesRequest, IFuturesOrderRestClient> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest, IFuturesOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedUserTrade[]>.Error(new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id")));

                    var orders = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
                    if (!orders)
                        return SharedExecutionResult<SharedUserTrade[]>.Error(orders);

                    return SharedExecutionResult<SharedUserTrade[]>.Ok(orders, orders.Data.Select(x => new SharedUserTrade(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol,
                        x.OrderId.ToString(),
                        x.Id.ToString(),
                        x.Buyer ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        x.Quantity,
                        x.Price,
                        x.Timestamp)
                    {
                        Fee = x.Fee,
                        FeeAsset = x.FeeAsset,
                        Role = x.Maker ? SharedRole.Maker : SharedRole.Taker
                    }).ToArray());


                });
        }

        GetFuturesUserTradesOptions IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new GetFuturesUserTradesOptions(_exchangeName, true, true, true, 1000);
        Task<ExchangeWebResult<SharedUserTrade[]>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions,
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
                        pageRequest?.FromId != null ? null : TimeSpan.FromDays(7));

                    var result = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol),
                        startTime: pageParams.FromId != null ? null : pageParams.StartTime,
                        endTime: pageParams.FromId != null ? null : pageParams.EndTime,
                        limit: limit,
                        orderId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                        ct: ct
                        ).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedUserTrade[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                        () => direction == DataDirection.Ascending
                            ? Pagination.NextPageFromId(result.Data.Max(x => x.Id) + 1)
                            : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.Timestamp), false),
                        result.Data.Length,
                        result.Data.Select(x => x.Timestamp),
                        request.StartTime,
                        request.EndTime ?? DateTime.UtcNow,
                        pageParams,
                        pageRequest?.FromId != null ? null : TimeSpan.FromDays(7));

                    return SharedExecutionResult<SharedUserTrade[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedUserTrade(
                                    ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol,
                                    x.OrderId.ToString(),
                                    x.Id.ToString(),
                                    x.Buyer ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                    x.Quantity,
                                    x.Price,
                                    x.Timestamp)
                                {
                                    Price = x.Price,
                                    Quantity = x.Quantity,
                                    Fee = x.Fee,
                                    FeeAsset = x.FeeAsset,
                                    Role = x.Maker ? SharedRole.Maker : SharedRole.Taker
                                }).ToArray(), nextPageRequest);


                });
        }

        EndpointOptions<CancelOrderRequest, IFuturesOrderRestClient> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest, IFuturesOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedId>.Error(new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id")));

                    var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedId>.Error(order);

                    return SharedExecutionResult<SharedId>.Ok(order, new SharedId(order.Data.Id.ToString()));


                });
        }

        EndpointOptions<GetPositionsRequest, IFuturesOrderRestClient> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest, IFuturesOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedPosition[]>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).GetPositionsOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Account.GetPositionInformationAsync(symbol: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedPosition[]>.Error(result);

                    IEnumerable<BinancePositionDetailsUsdt> data = result.Data;
                    if (request.TradingMode.HasValue)
                        data = data.Where(x => request.TradingMode == TradingMode.DeliveryLinear ? x.Symbol!.Contains("_") : !x.Symbol!.Contains("_"));

                    var resultTypes = request.Symbol == null && request.TradingMode == null ? SupportedTradingModes : request.Symbol != null ? new[] { request.Symbol!.TradingMode } : new[] { request.TradingMode!.Value };
                    return SharedExecutionResult<SharedPosition[]>.Ok(result, data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, Math.Abs(x.Quantity), x.UpdateTime)
                    {
                        UnrealizedPnl = x.UnrealizedPnl,
                        LiquidationPrice = x.LiquidationPrice == 0 ? null : x.LiquidationPrice,
                        Leverage = x.Leverage,
                        AverageOpenPrice = x.EntryPrice,
                        PositionMode = x.PositionSide == PositionSide.Both ? SharedPositionMode.OneWay : SharedPositionMode.HedgeMode,
                        PositionSide = x.PositionSide == PositionSide.Both ? (x.Quantity >= 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
                    }).ToArray());


                });
        }

        EndpointOptions<ClosePositionRequest, IFuturesOrderRestClient> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest, IFuturesOrderRestClient>(_exchangeName, true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.PositionSide), typeof(SharedPositionSide), "The position side to close", SharedPositionSide.Long),
                new ParameterDescription(nameof(ClosePositionRequest.Quantity), typeof(decimal), "Quantity of the position is required", 0.1m)
            }
        };
        Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderRestClient)this).ClosePositionOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var positionMode = await Account.GetPositionModeAsync().ConfigureAwait(false);
                    if (!positionMode)
                        return SharedExecutionResult<SharedId>.Error(positionMode);

                    var result = await Trading.PlaceOrderAsync(
                        symbol,
                        request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                        FuturesOrderType.Market,
                        request.Quantity,
                        positionSide: !positionMode.Data.IsHedgeMode ? null : request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                        reduceOnly: positionMode.Data.IsHedgeMode ? null : true,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedId>.Error(result);

                    return SharedExecutionResult<SharedId>.Ok(result, new SharedId(result.Data.Id.ToString()));


                });
        }

        private TimeInForce? GetTimeInForce(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (tif == SharedTimeInForce.ImmediateOrCancel) return TimeInForce.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return TimeInForce.FillOrKill;
            if (tif == SharedTimeInForce.GoodTillCanceled) return TimeInForce.GoodTillCanceled;
            if (type == SharedOrderType.Limit) return TimeInForce.GoodTillCanceled; // Limit order always needs tif

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

        private SharedOrderType ParseOrderType(FuturesOrderType type)
        {
            if (type == FuturesOrderType.Market) return SharedOrderType.Market;
            if (type == FuturesOrderType.Limit) return SharedOrderType.Limit;

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

        #region Futures Client Id Order Client

        EndpointOptions<GetOrderRequest, IFuturesOrderClientIdRestClient> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest, IFuturesOrderClientIdRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderClientIdRestClient.GetFuturesOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderClientIdRestClient)this).GetFuturesOrderByClientOrderIdOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedFuturesOrder>.Error(order);

                    return SharedExecutionResult<SharedFuturesOrder>.Ok(order, new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol), order.Data.Symbol,
                        order.Data.Id.ToString(),
                        ParseOrderType(order.Data.Type),
                        order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(order.Data.Status),
                        order.Data.CreateTime)
                    {
                        ClientOrderId = order.Data.ClientOrderId,
                        AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                        OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, contractQuantity: order.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled, order.Data.QuantityFilled),
                        TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                        UpdateTime = order.Data.UpdateTime,
                        PositionSide = order.Data.PositionSide == PositionSide.Both ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        ReduceOnly = order.Data.ReduceOnly,
                        TriggerPrice = order.Data.StopPrice,
                        IsTriggerOrder = order.Data.StopPrice > 0
                    });


                });
        }

        EndpointOptions<CancelOrderRequest, IFuturesOrderClientIdRestClient> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest, IFuturesOrderClientIdRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> IFuturesOrderClientIdRestClient.CancelFuturesOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesOrderClientIdRestClient)this).CancelFuturesOrderByClientOrderIdOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedId>.Error(order);

                    return SharedExecutionResult<SharedId>.Ok(order, new SharedId(order.Data.Id.ToString()));


                });
        }
        #endregion

        #region Leverage client
        SharedLeverageSettingMode ILeverageRestClient.LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        EndpointOptions<GetLeverageRequest, ILeverageRestClient> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest, ILeverageRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ILeverageRestClient)this).GetLeverageOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Account.GetPositionInformationAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedLeverage>.Error(result);

                    if (!result.Data.Any())
                        return SharedExecutionResult<SharedLeverage>.Error(new ExchangeWebResult<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol not found"))));

                    return SharedExecutionResult<SharedLeverage>.Ok(result, new SharedLeverage(result.Data.First().Leverage)
                    {
                        Side = request.PositionSide
                    });


                });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName);
        Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((ILeverageRestClient)this).SetLeverageOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Account.ChangeInitialLeverageAsync(symbol: request.Symbol!.GetSymbol(FormatSymbol), (int)request.Leverage, ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedLeverage>.Error(result);

                    return SharedExecutionResult<SharedLeverage>.Ok(result, new SharedLeverage(result.Data.Leverage));


                });
        }
        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, new[] { 5, 10, 20, 50, 100, 500, 1000 }, false);
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

                    return SharedExecutionResult<SharedOrderBook>.Ok(result, new SharedOrderBook(result.Data.Asks, result.Data.Bids));


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

                    // Get data
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
                            ? Pagination.NextPageFromId(result.Data.Max(x => x.Id) + 1)
                            : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.TradeTime), false),
                        result.Data.Length,
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

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, true, true, 1000, false);

        Task<ExchangeWebResult<SharedFuturesKline[]>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var interval = (Enums.KlineInterval)request.Interval;
                    if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                        return SharedExecutionResult<SharedFuturesKline[]>.Error(new ExchangeWebResult<SharedFuturesKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported")));

                    var direction = request.Direction ?? DataDirection.Ascending;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var limit = request.Limit ?? 1000;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

                    var result = await ExchangeData.GetMarkPriceKlinesAsync(
                        symbol,
                        interval,
                        limit,
                        pageParams.StartTime,
                        pageParams.EndTime,
                        ct: ct
                        ).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFuturesKline[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime))
                                : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.OpenTime)),
                            result.Data.Length,
                            result.Data.Select(x => x.OpenTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams);

                    // Return
                    return SharedExecutionResult<SharedFuturesKline[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedFuturesKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice))
                            .ToArray(), nextPageRequest);


                });
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest, IOpenInterestRestClient> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest, IOpenInterestRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IOpenInterestRestClient)this).GetOpenInterestOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await ExchangeData.GetOpenInterestAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedOpenInterest>.Error(result);

                    return SharedExecutionResult<SharedOpenInterest>.Ok(result, new SharedOpenInterest(result.Data.OpenInterest));


                });
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, true, 1000, false);

        Task<ExchangeWebResult<SharedFundingRate[]>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFundingRateRestClient)this).GetFundingRateHistoryOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var direction = request.Direction ?? DataDirection.Ascending;
                    var symbol = request.Symbol!.GetSymbol(FormatSymbol);
                    var limit = request.Limit ?? 1000;
                    var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

                    // Get data
                    var result = await ExchangeData.GetFundingRatesAsync(
                        symbol,
                        startTime: pageParams.StartTime,
                        endTime: pageParams.EndTime,
                        limit: limit,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFundingRate[]>.Error(result);

                    var nextPageRequest = Pagination.GetNextPageRequest(
                            () => direction == DataDirection.Ascending
                                ? Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.FundingTime))
                                : Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.FundingTime)),
                            result.Data.Length,
                            result.Data.Select(x => x.FundingTime),
                            request.StartTime,
                            request.EndTime ?? DateTime.UtcNow,
                            pageParams);

                    // Return
                    return SharedExecutionResult<SharedFundingRate[]>.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.FundingTime, request.StartTime, request.EndTime, direction)
                            .Select(x =>
                                new SharedFundingRate(x.FundingRate, x.FundingTime))
                            .ToArray(), nextPageRequest);


                });
        }
        #endregion

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Futures);

        Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IBalanceRestClient)this).GetBalancesOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedBalance[]>.Error(result);

                    return SharedExecutionResult<SharedBalance[]>.Ok(result, result.Data.Select(x => new SharedBalance(x.Asset, x.AvailableBalance, x.WalletBalance)).ToArray());


                });
        }

        #endregion

        #region Position Mode client
        SharedPositionModeSelection IPositionModeRestClient.PositionModeSettingType => SharedPositionModeSelection.PerAccount;

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions(_exchangeName);
        Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IPositionModeRestClient)this).GetPositionModeOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Account.GetPositionModeAsync(ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedPositionModeResult>.Error(result);

                    return SharedExecutionResult<SharedPositionModeResult>.Ok(result, new SharedPositionModeResult(result.Data.IsHedgeMode ? SharedPositionMode.HedgeMode : SharedPositionMode.OneWay));


                });
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions(_exchangeName);
        Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IPositionModeRestClient)this).SetPositionModeOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Account.ModifyPositionModeAsync(request.PositionMode == SharedPositionMode.HedgeMode, ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedPositionModeResult>.Error(result);

                    return SharedExecutionResult<SharedPositionModeResult>.Ok(result, new SharedPositionModeResult(request.PositionMode));


                });
        }
        #endregion

        #region Listen Key client

        EndpointOptions<StartListenKeyRequest, IListenKeyRestClient> IListenKeyRestClient.StartOptions { get; } = new EndpointOptions<StartListenKeyRequest, IListenKeyRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<string>> IListenKeyRestClient.StartListenKeyAsync(StartListenKeyRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IListenKeyRestClient)this).StartOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    // Get data
                    var result = await Account.StartUserStreamAsync(ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<string>.Error(result);

                    return SharedExecutionResult<string>.Ok(result, result.Data);


                });
        }
        EndpointOptions<KeepAliveListenKeyRequest, IListenKeyRestClient> IListenKeyRestClient.KeepAliveOptions { get; } = new EndpointOptions<KeepAliveListenKeyRequest, IListenKeyRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<string>> IListenKeyRestClient.KeepAliveListenKeyAsync(KeepAliveListenKeyRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IListenKeyRestClient)this).KeepAliveOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    // Get data
                    var result = await Account.KeepAliveUserStreamAsync(request.ListenKey, ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<string>.Error(result);

                    return SharedExecutionResult<string>.Ok(result, request.ListenKey);


                });
        }

        EndpointOptions<StopListenKeyRequest, IListenKeyRestClient> IListenKeyRestClient.StopOptions { get; } = new EndpointOptions<StopListenKeyRequest, IListenKeyRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<string>> IListenKeyRestClient.StopListenKeyAsync(StopListenKeyRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IListenKeyRestClient)this).StopOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    // Get data
                    var result = await Account.StopUserStreamAsync(request.ListenKey, ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<string>.Error(result);

                    return SharedExecutionResult<string>.Ok(result, request.ListenKey);


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
                    var result = await Account.GetUserCommissionRateAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedFee>.Error(result);

                    // Return
                    return SharedExecutionResult<SharedFee>.Ok(result, new SharedFee(result.Data.MakerCommissionRate * 100, result.Data.TakerCommissionRate * 100));


                });
        }
        #endregion

        #region Trigger Order Client
        PlaceFuturesTriggerOrderOptions IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(PlaceFuturesTriggerOrderRequest.PositionMode), typeof(SharedPositionMode), "PositionMode the account is in", SharedPositionMode.OneWay)
            }
        };
        Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTriggerOrderRestClient)this).PlaceFuturesTriggerOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {
                    var (type, side) = GetTriggerOrderParameters(request.PriceDirection, request.OrderPrice, request.OrderDirection);

                    var result = await Trading.PlaceOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        side,
                        type,
                        request.Quantity.QuantityInBaseAsset ?? request.Quantity.QuantityInContracts,
                        timeInForce: request.OrderPrice == null ? null : TimeInForce.GoodTillCanceled,
                        price: request.OrderPrice,
                        stopPrice: request.TriggerPrice,
                        newClientOrderId: request.ClientOrderId,
                        positionSide: request.PositionMode == SharedPositionMode.OneWay ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                        workingType: GetWorkingType(request),
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<SharedId>.Error(result);

                    // Return
                    return SharedExecutionResult<SharedId>.Ok(result, new SharedId(result.Data.Id.ToString()));


                });
        }

        private WorkingType? GetWorkingType(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.TriggerPriceType == null)
                return null;

            if (request.TriggerPriceType == SharedTriggerPriceType.LastPrice)
                return WorkingType.Contract;

            if (request.TriggerPriceType == SharedTriggerPriceType.MarkPrice)
                return WorkingType.Mark;

            return WorkingType.Contract;
        }

        EndpointOptions<GetOrderRequest, IFuturesTriggerOrderRestClient> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest, IFuturesTriggerOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedFuturesTriggerOrder>> IFuturesTriggerOrderRestClient.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTriggerOrderRestClient)this).GetFuturesTriggerOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    if (!long.TryParse(request.OrderId, out var id))
                        throw new ArgumentException($"Invalid order id");

                    var order = await Trading.GetOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        id,
                        ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedFuturesTriggerOrder>.Error(order);

                    var (orderType, orderDirection) = ParseTriggerDirections(order.Data.Type, order.Data.Side);
                    // Return
                    return SharedExecutionResult<SharedFuturesTriggerOrder>.Ok(order, new SharedFuturesTriggerOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
                        order.Data.Symbol,
                        order.Data.Id.ToString(),
                        orderType,
                        orderDirection,
                        ParseTriggerStatus(order.Data),
                        order.Data.StopPrice ?? 0,
                        order.Data.PositionSide == PositionSide.Both ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        order.Data.CreateTime
                        )
                    {
                        AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                        OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(order.Data.Quantity, contractQuantity: order.Data.Quantity),
                        QuantityFilled = new SharedOrderQuantity(order.Data.QuantityFilled, order.Data.QuoteQuantityFilled, contractQuantity: order.Data.QuantityFilled),
                        TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                        UpdateTime = order.Data.UpdateTime,
                        PositionSide = order.Data.PositionSide == PositionSide.Both ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                        PlacedOrderId = order.Data.Id.ToString(),
                        ClientOrderId = order.Data.ClientOrderId
                    });


                });
        }

        private SharedTriggerOrderStatus ParseTriggerStatus(BinanceUsdFuturesOrder data)
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

        EndpointOptions<CancelOrderRequest, IFuturesTriggerOrderRestClient> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest, IFuturesTriggerOrderRestClient>(_exchangeName, true);
        Task<ExchangeWebResult<SharedId>> IFuturesTriggerOrderRestClient.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTriggerOrderRestClient)this).CancelFuturesTriggerOrderOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<SharedId>.Error(new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id")));

                    var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
                    if (!order)
                        return SharedExecutionResult<SharedId>.Error(order);

                    return SharedExecutionResult<SharedId>.Ok(order, new SharedId(order.Data.Id.ToString()));


                });
        }


        private (FuturesOrderType, OrderSide) GetTriggerOrderParameters(SharedTriggerPriceDirection orderType, decimal? orderPrice, SharedTriggerOrderDirection direction)
        {
            if (orderType == SharedTriggerPriceDirection.PriceBelow)
            {
                if (direction == SharedTriggerOrderDirection.Enter)
                    // PriceBelow + Enter = TakeProfit Buy order
                    return (orderPrice == null ? FuturesOrderType.TakeProfitMarket : FuturesOrderType.TakeProfit, OrderSide.Buy);
                else
                    // PriceBelow + Exit = StopLoss Sell order
                    return (orderPrice == null ? FuturesOrderType.StopMarket : FuturesOrderType.Stop, OrderSide.Sell);
            }

            if (direction == SharedTriggerOrderDirection.Enter)
                // PriceAbove + Enter = StopLoss Buy order
                return (orderPrice == null ? FuturesOrderType.StopMarket : FuturesOrderType.Stop, OrderSide.Buy);
            else
                // PriceAbove + Exit = TakeProfit Sell order
                return (orderPrice == null ? FuturesOrderType.TakeProfitMarket : FuturesOrderType.TakeProfit, OrderSide.Sell);
        }

        private (SharedOrderType, SharedTriggerOrderDirection) ParseTriggerDirections(FuturesOrderType orderType, OrderSide side)
        {
            if (orderType == FuturesOrderType.TakeProfit || orderType == FuturesOrderType.TakeProfitMarket)
            {
                if (side == OrderSide.Buy)
                {
                    // TakeProfit + Buy = PriceBelow Enter
                    return (
                        orderType == FuturesOrderType.TakeProfitMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Enter);
                }
                else
                {
                    // TakeProfit + Sell = PriceAbove Exit
                    return (
                        orderType == FuturesOrderType.TakeProfitMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                        SharedTriggerOrderDirection.Exit);
                }
            }

            if (side == OrderSide.Buy)
            {
                // StopLoss + Buy = PriceAbove Enter
                return (
                    orderType == FuturesOrderType.StopMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Enter);
            }
            else
            {
                // StopLoss + Sell = PriceBelow Exit
                return (
                    orderType == FuturesOrderType.StopMarket ? SharedOrderType.Market : SharedOrderType.Limit,
                    SharedTriggerOrderDirection.Exit);
            }
        }
        #endregion

        #region Tp/SL Client
        EndpointOptions<SetTpSlRequest, IFuturesTpSlRestClient> IFuturesTpSlRestClient.SetFuturesTpSlOptions { get; } = new EndpointOptions<SetTpSlRequest, IFuturesTpSlRestClient>(_exchangeName, true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(SetTpSlRequest.PositionMode), typeof(SharedPositionMode), "Position mode the account is in", SharedPositionMode.OneWay)
            },
        };

        Task<ExchangeWebResult<SharedId>> IFuturesTpSlRestClient.SetFuturesTpSlAsync(SetTpSlRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTpSlRestClient)this).SetFuturesTpSlOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    var result = await Trading.PlaceOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                        request.TpSlSide == SharedTpSlSide.TakeProfit ? FuturesOrderType.TakeProfitMarket : FuturesOrderType.StopMarket,
                        null,
                        stopPrice: request.TriggerPrice,
                        closePosition: true,
                        positionSide: request.PositionMode == SharedPositionMode.OneWay ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                        ct: ct).ConfigureAwait(false);

                    if (!result)
                        return SharedExecutionResult<SharedId>.Error(result);

                    // Return
                    return SharedExecutionResult<SharedId>.Ok(result, new SharedId(result.Data.Id.ToString()));


                });
        }

        EndpointOptions<CancelTpSlRequest, IFuturesTpSlRestClient> IFuturesTpSlRestClient.CancelFuturesTpSlOptions { get; } = new EndpointOptions<CancelTpSlRequest, IFuturesTpSlRestClient>(_exchangeName, true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(CancelTpSlRequest.OrderId), typeof(string), "Id of the tp/sl order", "123123")
            }
        };

        Task<ExchangeWebResult<bool>> IFuturesTpSlRestClient.CancelFuturesTpSlAsync(CancelTpSlRequest request, CancellationToken ct)
        {
            return SharedUtils.ExecuteSharedAsync(
                ((IFuturesTpSlRestClient)this).CancelFuturesTpSlOptions,
                request,
                SupportedTradingModes,
                async () =>
                {

                    if (!long.TryParse(request.OrderId, out var orderId))
                        return SharedExecutionResult<bool>.Error(new ExchangeWebResult<bool>(Exchange, ArgumentError.Invalid(nameof(CancelTpSlRequest.OrderId), "Invalid order id")));

                    var result = await Trading.CancelOrderAsync(
                        request.Symbol!.GetSymbol(FormatSymbol),
                        orderId,
                        ct: ct).ConfigureAwait(false);
                    if (!result)
                        return SharedExecutionResult<bool>.Error(result);

                    // Return
                    return SharedExecutionResult<bool>.Ok(result, true);


                });
        }

        #endregion
    }
}
