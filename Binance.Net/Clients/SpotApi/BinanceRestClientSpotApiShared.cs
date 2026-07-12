using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.SharedApis.Models;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotApi : IBinanceRestClientSpotApiShared
    {
        private const string _exchangeName = "Binance";
        private const string _topicId = "BinanceSpot";
        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticExchangeParameters(Exchange);
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(BinanceExchange.Metadata, this);

        #region Klines Client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, true, true, 1000, false);
        async Task<HttpResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return HttpResult.Fail<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var direction = request.Direction ?? DataDirection.Ascending;
            var symbol = request.SymbolName(FormatSymbol);
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
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

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
            return HttpResult.Ok(result,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume))
                    .ToArray(), nextPageRequest);

        }

        #endregion

        #region Spot Symbol client
        GetSpotSymbolsOptions ISpotSymbolRestClient.GetSpotSymbolsOptions { get; }
            = new GetSpotSymbolsOptions(_exchangeName, false);

        async Task<HttpResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var resultEx = ExchangeData.GetExchangeInfoAsync(false, SymbolStatus.Trading, ct: ct);
            var resultPr = ExchangeData.GetProductsAsync(ct: ct);
            await Task.WhenAll(resultEx, resultPr).ConfigureAwait(false);
            if (!resultEx.Result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(resultEx.Result);

            BinanceExchange._products = resultPr.Result.Data;
            BinanceExchange._spotExchangeInfo = resultEx.Result.Data;

            var exchangeInfo = await BinanceExchange.Cache.GetAsync<SharedExchangeInfo>("Binance.Spot.live.ExchangeInfo").ConfigureAwait(false);

            var resultData = resultEx.Result.Data.Symbols.Select(x => ParseSymbol(x, exchangeInfo));
            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, EnvironmentName, null, resultData.ToArray());
            if (request.BaseAssetType != null)
                resultData = resultData.Where(x => x.BaseAssetType == request.BaseAssetType);
            if (request.QuoteAssetType != null)
                resultData = resultData.Where(x => x.QuoteAssetType == request.QuoteAssetType);
            if (request.BaseAssetSubType != null)
                resultData = resultData.Where(x => x.BaseAssetSubType == request.BaseAssetSubType);
            if (request.QuoteAssetSubType != null)
                resultData = resultData.Where(x => x.QuoteAssetSubType == request.QuoteAssetSubType);

            return HttpResult.Ok(resultEx.Result, resultData.ToArray());

        }

        private SharedSpotSymbol ParseSymbol(BinanceSymbol symbol, SharedExchangeInfo exchangeInfo)
        {
            exchangeInfo.Assets.TryGetValue(symbol.BaseAsset, out var baseAssetInfo);
            exchangeInfo.Assets.TryGetValue(symbol.QuoteAsset, out var quoteAssetInfo);
            return new SharedSpotSymbol(symbol.BaseAsset, symbol.QuoteAsset, symbol.Name, symbol.Status == SymbolStatus.Trading && symbol.IsSpotTradingAllowed)
            {
                MinTradeQuantity = symbol.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = symbol.LotSizeFilter?.MaxQuantity,
                MinNotionalValue = symbol.MinNotionalFilter?.MinNotional ?? symbol.NotionalFilter?.MinNotional,
                QuantityStep = symbol.LotSizeFilter?.StepSize,
                PriceStep = symbol.PriceFilter?.TickSize,
                DisplayName = symbol.Name,
                BaseAssetType = baseAssetInfo?.Type ?? SharedAssetType.Unspecified,
                BaseAssetSubType = baseAssetInfo?.SubType,
                QuoteAssetType = quoteAssetInfo?.Type ?? SharedAssetType.Unspecified,
                QuoteAssetSubType = quoteAssetInfo?.SubType
            };
        }

        async Task<ExchangeCallResult<SharedSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, EnvironmentName, null))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicId, EnvironmentName, null, baseAsset));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicId, EnvironmentName, null))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, EnvironmentName, null, symbol));
        }

        async Task<ExchangeCallResult<bool>> ISpotSymbolRestClient.SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicId, EnvironmentName, null))
            {
                var symbols = await ((ISpotSymbolRestClient)this).GetSpotSymbolsAsync(new GetSymbolsRequest()).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicId, EnvironmentName, null, symbolName));
        }
        #endregion

        #region Ticker client

        GetSpotTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetSpotTickerOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetTickerAsync(request.SymbolName(FormatSymbol), ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker>(result);

            return HttpResult.Ok(result, new SharedSpotTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, result.Data!.Symbol),
                    result.Data.Symbol,
                    result.Data.LastPrice,
                    result.Data.HighPrice,
                    result.Data.LowPrice,
                    result.Data.Volume,
                    result.Data.PriceChangePercent)
            {
                QuoteVolume = result.Data.QuoteVolume
            });

        }

        GetSpotTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetSpotTickersOptions(_exchangeName);
        async Task<HttpResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTicker[]>(result);

            return HttpResult.Ok(result, result.Data!.Select(x =>
                    new SharedSpotTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.LastPrice,
                        x.HighPrice,
                        x.LowPrice,
                        x.Volume,
                        x.PriceChangePercent)
                    {
                        QuoteVolume = x.QuoteVolume
                    }).ToArray());

        }

        #endregion

        #region Book Ticker client

        GetBookTickerOptions IBookTickerRestClient.GetBookTickerOptions { get; }
            = new GetBookTickerOptions(_exchangeName, false);
        async Task<HttpResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetBookPriceAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedBookTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, resultTicker.Data!.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice,
                resultTicker.Data.BestAskQuantity,
                resultTicker.Data.BestBidPrice,
                resultTicker.Data.BestBidQuantity));

        }

        #endregion

        #region Recent Trades client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(_exchangeName, 1000, false);

        async Task<HttpResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetRecentTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetRecentTradesAsync(
                symbol,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

            // Return
            return HttpResult.Ok(result, result.Data!.Select(x =>
                new SharedTrade(request.Symbol, symbol, x.BaseQuantity, x.Price, x.TradeTime)
                {
                    Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                }).ToArray());

        }
        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(_exchangeName, true, true, true, 1000, false);
        async Task<HttpResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetTradeHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedTrade[]>(Exchange, validationError);

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

            if (!result.Success)
                return HttpResult.Fail<SharedTrade[]>(result);

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
            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.TradeTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.TradeTime)
                        {
                            Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                        }).ToArray(), nextPageRequest);

        }
        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(_exchangeName, 1, 5000, false);
        async Task<HttpResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedOrderBook>(result);

            return HttpResult.Ok(result, new SharedOrderBook(result.Data!.Asks, result.Data.Bids));

        }

        #endregion

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Funding, AccountTypeFilter.Spot);

        async Task<HttpResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Funding)
            {
                var result = await Account.GetFundingWalletAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data!.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.Available, 
                        x.Available + x.Freeze + x.Locked)).ToArray());
            }
            else
            {
                var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data!.Select(x => 
                    new SharedBalance(
                        SupportedTradingModes, 
                        x.Asset,
                        x.Available, 
                        x.Total)).ToArray());
            }

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
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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

            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            return HttpResult.Ok(result, new SharedId(result.Data!.Id.ToString()));

        }

        GetSpotOrderOptions ISpotOrderRestClient.GetSpotOrderOptions { get; } = new GetSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                    ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, order.Data!.Symbol),
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

        }

        GetOpenSpotOrdersOptions ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; }
            = new GetOpenSpotOrdersOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetOpenSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(orders);

            return HttpResult.Ok(orders, orders.Data!.Select(x => new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol),
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

        }

        GetSpotClosedOrdersOptions ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new GetSpotClosedOrdersOptions(_exchangeName, true, true, true, 1000);
        async Task<HttpResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetClosedSpotOrdersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder[]>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedSpotOrder[]>(result);

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

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled || x.Status == OrderStatus.Expired)
                    .Select(x => new SharedSpotOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol),
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

        }

        GetSpotOrderTradesOptions ISpotOrderRestClient.GetSpotOrderTradesOptions { get; }
            = new GetSpotOrderTradesOptions(_exchangeName, true);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedUserTrade[]>(orders);

            return HttpResult.Ok(orders, orders.Data!.Select(x => new SharedUserTrade(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol),
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

        }

        GetSpotUserTradesOptions ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new GetSpotUserTradesOptions(_exchangeName, true, true, true, 1000);
        async Task<HttpResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotUserTradesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedUserTrade[]>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedUserTrade[]>(result);

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

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedUserTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, x.Symbol),
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

        }

        CancelSpotOrderOptions ISpotOrderRestClient.CancelSpotOrderOptions { get; }
            = new CancelSpotOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data!.Id.ToString()));

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

        GetSpotOrderByClientOrderIdOptions ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; }
            = new GetSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedSpotOrder>(order);

            return HttpResult.Ok(order, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, order.Data!.Symbol),
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

        }

        CancelSpotOrderByClientOrderIdOptions ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; }
            = new CancelSpotOrderByClientOrderIdOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotOrderByClientOrderIdOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data!.Id.ToString()));

        }
        #endregion

        #region Asset client
        GetAssetsOptions IAssetsRestClient.GetAssetsOptions { get; }
            = new GetAssetsOptions(_exchangeName, true);

        async Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data!.Select(x => new SharedAsset(x.Asset)
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

        }

        GetAssetOptions IAssetsRestClient.GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, true);
        async Task<HttpResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            var asset = assets.Data!.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return HttpResult.Fail<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, false, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(asset.Asset)
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

        }

        #endregion

        #region Deposit client

        GetDepositAddressesOptions IDepositRestClient.GetDepositAddressesOptions { get; }
            = new GetDepositAddressesOptions(_exchangeName, true);
        async Task<HttpResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, new[] {
                    new SharedDepositAddress(depositAddresses.Data!.Asset, depositAddresses.Data.Address)
                    {
                        TagOrMemo = depositAddresses.Data.Tag
                    }
                });

        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(_exchangeName, false, true, true, 1000)
        {
            OptionalExchangeParameters = new List<ParameterDescription>
            {
                new ParameterDescription("TravelRuleEndpoint", typeof(bool), "Whether to use the TravelRule endpoint (true) or not (false, default)", true)
            }
        };
        async Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetDepositsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

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
                if (!result.Success)
                    return HttpResult.Fail<SharedDeposit[]>(result);

                var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data!.Min(x => x.InsertTime), false),
                    result.Data!.Length,
                    result.Data.Select(x => x.InsertTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams,
                    TimeSpan.FromDays(90));

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
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
                if (!result.Success)
                    return HttpResult.Fail<SharedDeposit[]>(result);

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

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
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

        }

        private SharedTransferStatus ParseTransferStatus(DepositStatus status)
        {
            if (status == DepositStatus.Success)
                return SharedTransferStatus.Completed;
            if (status == DepositStatus.Pending || status == DepositStatus.Credited || status == DepositStatus.WaitingUserConfirm)
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
        async Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = SharedClient.GetWithdrawalsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

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
                if (!result.Success)
                    return HttpResult.Fail<SharedWithdrawal[]>(result);

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

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(
                            x.Asset, 
                            x.Address, 
                            x.Quantity, 
                            x.Status == WithdrawalStatus.Completed,
                            x.ApplyTime,
                            GetWithdrawalStatus(x.Status))
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
                if (!result.Success)
                    return HttpResult.Fail<SharedWithdrawal[]>(result);

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

                return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedWithdrawal(
                            x.Asset, 
                            x.Address,
                            x.Quantity,
                            x.Status == WithdrawalStatus.Completed,
                            x.ApplyTime,
                            GetWithdrawalStatus(x.Status))
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

        }

        private SharedTransferStatus GetWithdrawalStatus(WithdrawalStatus x)
        {
            if (x == WithdrawalStatus.Canceled || x == WithdrawalStatus.Rejected || x == WithdrawalStatus.Failure)
                return SharedTransferStatus.Failed;

            if (x == WithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;

            if (x == WithdrawalStatus.AwaitingApproval || x == WithdrawalStatus.EmailSend || x == WithdrawalStatus.Processing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
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
        async Task<HttpResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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
                if (!withdrawal.Success)
                    return HttpResult.Fail<SharedId>(withdrawal);

                return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data!.Id));
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
                if (!withdrawal.Success)
                    return HttpResult.Fail<SharedId>(withdrawal);

                return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data!.TravelRuleId.ToString()));
            }

        }

        #endregion

        #region Fee Client
        GetFeeOptions IFeeRestClient.GetFeeOptions { get; } = new GetFeeOptions(_exchangeName, true);

        async Task<HttpResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetFeeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradeFeeAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFee>(result);

            var symbol = result.Data!.SingleOrDefault();
            if (symbol == null)
                return HttpResult.Fail<SharedFee>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol not found")));

            // Return
            return HttpResult.Ok(result, new SharedFee(symbol.MakerFee * 100, symbol.TakerFee * 100));

        }
        #endregion

        #region Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.PlaceSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

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
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data!.Id.ToString()));

        }

        GetSpotTriggerOrderOptions ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; }
            = new GetSpotTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.GetSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var result = await Trading.GetOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                id,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotTriggerOrder>(result);

            var (orderType, orderDirection) = ParseTriggerDirections(result.Data!.Type, result.Data.Side);
            // Return
            return HttpResult.Ok(result, new SharedSpotTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, EnvironmentName, null, result.Data.Symbol),
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

        CancelSpotTriggerOrderOptions ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; }
            = new CancelSpotTriggerOrderOptions(_exchangeName, true);
        async Task<HttpResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.CancelSpotTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(order.Data!.Id.ToString()));

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
        async Task<HttpResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = SharedClient.TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var transferType = GetTransferType(request);
            if (transferType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await Account.TransferAsync(
                transferType.Value,
                request.Asset,
                request.Quantity,
                request.FromSymbol,
                request.ToSymbol,
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(transfer.Data!.TransactionId.ToString()));

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
