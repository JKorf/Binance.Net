using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.SpotApi;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotApi : IBinanceRestClientSpotApiShared
    {
        private const string _topicId = "BinanceSpot";
        public string Exchange => BinanceExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Klines Client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(false, true, true, 1000, false);
        async Task<ExchangeWebResult<SharedKline[]>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<SharedKline[]>(Exchange, ArgumentError.Invalid(nameof(GetKlinesRequest.Interval), "Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedKline[]>(Exchange, validationError);

            var direction = request.Direction ?? DataDirection.Ascending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;

            var paginationParameters = ExchangeHelpers.ApplyPaginationParameters(
                direction,
                pageRequest,
                ExchangeHelpers.PaginationFilterType.Time,
                ExchangeHelpers.PaginationFilterType.Time,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                request.StartTime,
                request.EndTime);

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                symbol,
                interval,
                paginationParameters.StartTime,
                paginationParameters.EndTime,
                limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return new ExchangeWebResult<SharedKline[]>(Exchange, TradingMode.Spot, result.As<SharedKline[]>(default));

            var nextPageRequest = ExchangeHelpers.GetNextPageRequest(
                () =>
                {
                    if (direction == DataDirection.Ascending)
                        return PageRequest.NextStartTimeAsc(result.Data.Select(x => x.OpenTime));
                    else
                        return PageRequest.NextEndTimeDesc(result.Data.Select(x => x.OpenTime));
                },
                result.Data.Length,
                result.Data.Select(x => x.OpenTime),
                limit,
                pageRequest,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                paginationParameters.StartTime,
                direction,
                request.StartTime,
                request.EndTime);

            // Return
            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                new SharedKline(request.Symbol, symbol, x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextPageRequest);
        }

        #endregion

        #region Spot Symbol client
        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);

        async Task<ExchangeWebResult<SharedSpotSymbol[]>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await ExchangeData.GetExchangeInfoAsync(false, SymbolStatus.Trading, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotSymbol[]>(Exchange, null, default);

            var resultData = result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data.Symbols.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.Status == SymbolStatus.Trading && s.IsSpotTradingAllowed)
            {
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                MinNotionalValue = s.MinNotionalFilter?.MinNotional ?? s.NotionalFilter?.MinNotional,
                QuantityStep = s.LotSizeFilter?.StepSize,
                PriceStep = s.PriceFilter?.TickSize
            }).ToArray());

            ExchangeSymbolCache.UpdateSymbolInfo(_topicId, resultData.Data);
            return resultData;
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

        GetTickerOptions ISpotTickerRestClient.GetSpotTickerOptions { get; } = new GetTickerOptions();
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetTickerAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, result.Data.Symbol), result.Data.Symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice, result.Data.Volume, result.Data.PriceChangePercent) 
            { 
                QuoteVolume = result.Data.QuoteVolume
            });
        }

        GetTickersOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new GetTickersOptions();
        async Task<ExchangeWebResult<SharedSpotTicker[]>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker[]>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker[]>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data.Select(x =>
                new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume, x.PriceChangePercent)
                {
                    QuoteVolume = x.QuoteVolume
                }).ToArray());
        }

        #endregion

        #region Book Ticker client

        EndpointOptions<GetBookTickerRequest> IBookTickerRestClient.GetBookTickerOptions { get; } = new EndpointOptions<GetBookTickerRequest>(false);
        async Task<ExchangeWebResult<SharedBookTicker>> IBookTickerRestClient.GetBookTickerAsync(GetBookTickerRequest request, CancellationToken ct)
        {
            var validationError = ((IBookTickerRestClient)this).GetBookTickerOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBookTicker>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetBookPriceAsync(request.Symbol!.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<SharedBookTicker>(Exchange, null, default);

            return resultTicker.AsExchangeResult(Exchange, request.Symbol!.TradingMode, new SharedBookTicker(
                ExchangeSymbolCache.ParseSymbol(_topicId, resultTicker.Data.Symbol),
                resultTicker.Data.Symbol,
                resultTicker.Data.BestAskPrice,
                resultTicker.Data.BestAskQuantity,
                resultTicker.Data.BestBidPrice,
                resultTicker.Data.BestBidQuantity));
        }

        #endregion

        #region Recent Trades client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);

        async Task<ExchangeWebResult<SharedTrade[]>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            // Get data
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var result = await ExchangeData.GetRecentTradesAsync(
                symbol,
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data.Select(x => 
                new SharedTrade(request.Symbol, symbol, x.BaseQuantity, x.Price, x.TradeTime)
                {
                    Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                }).ToArray());
        }
        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(true, true, true, 1000, false);
        async Task<ExchangeWebResult<SharedTrade[]>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ITradeHistoryRestClient)this).GetTradeHistoryOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedTrade[]>(Exchange, validationError);

            var direction = request.Direction ?? DataDirection.Ascending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 1000;

            var paginationParameters = ExchangeHelpers.ApplyPaginationParameters(
                direction,
                pageRequest,
                ExchangeHelpers.PaginationFilterType.FromId,
                ExchangeHelpers.PaginationFilterType.Time,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                request.StartTime,
                request.EndTime);

            var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
                symbol,
                startTime: paginationParameters.StartTime,
                endTime: paginationParameters.EndTime,
                fromId: paginationParameters.FromId != null ? long.Parse(paginationParameters.FromId) : null,
                limit: limit,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedTrade[]>(Exchange, null, default);

            var nextPageRequest = ExchangeHelpers.GetNextPageRequest(
                () =>
                {
                    if (direction == DataDirection.Ascending)
                        return PageRequest.NextFromIdAsc(result.Data.Select(x => x.Id));
                    else
                        return PageRequest.NextEndTimeDesc(result.Data.Select(x => x.TradeTime));                    
                },
                result.Data.Length,
                result.Data.Select(x => x.TradeTime),
                limit,
                pageRequest,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                paginationParameters.StartTime,
                direction,
                request.StartTime,
                request.EndTime);

            // Return
            return result.AsExchangeResult(
                Exchange, 
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.TradeTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedTrade(request.Symbol, symbol, x.Quantity, x.Price, x.TradeTime)
                        {
                            Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                        }).ToArray(),
                nextPageRequest);
        }
        #endregion
                
        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 5000, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Balance Client
        GetBalancesOptions IBalanceRestClient.GetBalancesOptions { get; } = new GetBalancesOptions(AccountTypeFilter.Funding, AccountTypeFilter.Spot);

        async Task<ExchangeWebResult<SharedBalance[]>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Funding)
            {
                var result = await Account.GetFundingWalletAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Available + x.Freeze + x.Locked)).ToArray());
            }
            else
            {
                var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
                if (!result)
                    return result.AsExchangeResult<SharedBalance[]>(Exchange, null, default);

                return result.AsExchangeResult(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)).ToArray());
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

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol!.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

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
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(result.Data.Id.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
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

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            return orders.AsExchangeResult(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedSpotOrder(
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
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(true, true, true, 1000, true);
        async Task<ExchangeWebResult<SharedSpotOrder[]>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder[]>(Exchange, validationError);

            var direction = request.Direction ?? DataDirection.Ascending;
            var limit = request.Limit ?? 1000;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var paginationParameters = ExchangeHelpers.ApplyPaginationParameters(
                direction,
                pageRequest,
                ExchangeHelpers.PaginationFilterType.FromId,
                ExchangeHelpers.PaginationFilterType.Time,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                request.StartTime,
                request.EndTime);

            // Get data
            var result = await Trading.GetOrdersAsync(
                symbol,
                startTime: paginationParameters.StartTime,
                endTime: paginationParameters.EndTime,
                limit: limit,
                orderId: paginationParameters.FromId == null ? null : long.Parse(paginationParameters.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotOrder[]>(Exchange, null, default);

            var nextPageRequest = ExchangeHelpers.GetNextPageRequest(
                () =>
                {
                    if (direction == DataDirection.Ascending)
                        return PageRequest.NextFromIdAsc(result.Data.Select(x => x.Id));
                    else
                        return PageRequest.NextEndTimeDesc(result.Data.Select(x => x.CreateTime));
                },
                result.Data.Length,
                result.Data.Select(x => x.CreateTime),
                limit,
                pageRequest,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                paginationParameters.StartTime,
                direction,
                request.StartTime,
                request.EndTime);

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
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
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, ArgumentError.Invalid(nameof(GetOrderTradesRequest.OrderId), "Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId: orderId, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            return orders.AsExchangeResult(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedUserTrade(
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
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(true, true, true, 1000, true);
        async Task<ExchangeWebResult<SharedUserTrade[]>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedUserTrade[]>(Exchange, validationError);

            var direction = request.Direction ?? DataDirection.Ascending;
            var limit = request.Limit ?? 1000;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var paginationParameters = ExchangeHelpers.ApplyPaginationParameters(
                direction,
                pageRequest,
                ExchangeHelpers.PaginationFilterType.FromId,
                ExchangeHelpers.PaginationFilterType.Time,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                request.StartTime,
                request.EndTime);

            // Get data
            var result = await Trading.GetUserTradesAsync(request.Symbol!.GetSymbol(FormatSymbol),
                startTime: paginationParameters.StartTime,
                endTime: paginationParameters.EndTime,
                fromId: paginationParameters.FromId == null ? null : long.Parse(paginationParameters.FromId),
                limit: limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedUserTrade[]>(Exchange, null, default);

            var nextPageRequest = ExchangeHelpers.GetNextPageRequest(
                () =>
                {
                    if (direction == DataDirection.Ascending)
                        return PageRequest.NextFromIdAsc(result.Data.Select(x => x.Id));
                    else
                        return PageRequest.NextEndTimeDesc(result.Data.Select(x => x.Timestamp));
                },
                result.Data.Length,
                result.Data.Select(x => x.Timestamp),
                limit,
                pageRequest,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                paginationParameters.StartTime,
                direction,
                request.StartTime,
                request.EndTime);

            return result.AsExchangeResult(
                    Exchange,
                    TradingMode.Spot,
                    ExchangeHelpers.ApplyFilter(result.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
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
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(order.Data.Id.ToString()));
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
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            if (status == OrderStatus.PartiallyFilled
                || status == OrderStatus.New
                || status == OrderStatus.PendingNew
                || status == OrderStatus.PendingCancel)
            {
                return SharedOrderStatus.Open;
            }

            return SharedOrderStatus.Canceled;
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

        EndpointOptions<GetOrderRequest> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderClientIdRestClient.GetSpotOrderByClientOrderIdAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, order.Data.Symbol),
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

        EndpointOptions<CancelOrderRequest> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderClientIdRestClient.CancelSpotOrderByClientOrderIdAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), origClientOrderId: request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(order.Data.Id.ToString()));
        }
        #endregion

        #region Asset client
        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(true);

        async Task<ExchangeWebResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset[]>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset[]>(Exchange, null, default);

            return assets.AsExchangeResult(Exchange, TradingMode.Spot, assets.Data.Select(x => new SharedAsset(x.Asset)
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

        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            var asset = assets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, false, "Asset not found")));

            return assets.AsExchangeResult(Exchange, TradingMode.Spot, new SharedAsset(asset.Asset)
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

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<SharedDepositAddress[]>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, request.Network, ct: ct).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<SharedDepositAddress[]>(Exchange, null, default);

            return depositAddresses.AsExchangeResult(Exchange, TradingMode.Spot, new[] { new SharedDepositAddress(depositAddresses.Data.Asset, depositAddresses.Data.Address)
            {
                TagOrMemo = depositAddresses.Data.Tag
            }
            });
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(false, true, true, 1000);
        async Task<ExchangeWebResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedDeposit[]>(Exchange, validationError);

            var limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var paginationParameters = ExchangeHelpers.ApplyPaginationParameters(
                direction,
                pageRequest,
                null,
                ExchangeHelpers.PaginationFilterType.Offset,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                request.StartTime,
                request.EndTime,
                TimeSpan.FromDays(90));

            // Get data
            var result = await Account.GetDepositHistoryAsync(
                request.Asset,
                startTime: paginationParameters.StartTime,
                endTime: paginationParameters.EndTime,
                limit: limit,
                offset: paginationParameters.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedDeposit[]>(Exchange, null, default);

            var nextPageRequest = ExchangeHelpers.GetNextPageRequest(
                () => PageRequest.NextOffset((paginationParameters.Offset ?? 0) + result.Data.Length),
                result.Data.Length,
                result.Data.Select(x => x.InsertTime),
                limit,
                pageRequest,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                paginationParameters.StartTime,
                direction,
                request.StartTime,
                request.EndTime,
                TimeSpan.FromDays(90));

            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.InsertTime, request.StartTime, request.EndTime, direction)
                .Select(x =>
                    new SharedDeposit(
                        x.Asset,
                        x.Quantity,
                        x.Status == DepositStatus.Success,
                        x.InsertTime,
                        x.Status == DepositStatus.Success ? SharedTransferStatus.Completed 
                                : x.Status == DepositStatus.Pending || x.Status == DepositStatus.Completed ? SharedTransferStatus.InProgress
                                : SharedTransferStatus.Failed)
                    {
                        Confirmations = x.Confirmations.Contains("/") ? int.Parse(x.Confirmations.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0]) : null,
                        Network = x.Network,
                        TransactionId = x.TransactionId,
                        Tag = x.AddressTag,
                        Id = x.Id
                    }).ToArray(), nextPageRequest);
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(false, true, true, 1000);
        async Task<ExchangeWebResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedWithdrawal[]>(Exchange, validationError);

#warning validate allowed directions

            var limit = request.Limit ?? 100; 
            var direction = DataDirection.Descending;
            var paginationParameters = ExchangeHelpers.ApplyPaginationParameters(
                direction,
                pageRequest,
                null,
                ExchangeHelpers.PaginationFilterType.Offset,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                request.StartTime,
                request.EndTime,
                TimeSpan.FromDays(90));

            // Get data
            var result = await Account.GetWithdrawalHistoryAsync(
                request.Asset,
                startTime: paginationParameters.StartTime,
                endTime: paginationParameters.EndTime,
                limit: limit,
                offset: paginationParameters.Offset,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedWithdrawal[]>(Exchange, null, default);

            var nextPageRequest = ExchangeHelpers.GetNextPageRequest(
                () => PageRequest.NextOffset((paginationParameters.Offset ?? 0) + result.Data.Length),
                result.Data.Length,
                result.Data.Select(x => x.ApplyTime),
                limit,
                pageRequest,
                ExchangeHelpers.TimeParameterSetType.OnlyMatchingDirection,
                paginationParameters.StartTime,
                direction,
                request.StartTime,
                request.EndTime,
                TimeSpan.FromDays(90));

            return result.AsExchangeResult(
                Exchange,
                TradingMode.Spot,
                ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
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

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();
        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                request.Asset,
                request.Address,
                request.Quantity,
                network: request.Network,
                addressTag: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.Id));
        }

        #endregion

        #region Fee Client
        EndpointOptions<GetFeeRequest> IFeeRestClient.GetFeeOptions { get; } = new EndpointOptions<GetFeeRequest>(true);

        async Task<ExchangeWebResult<SharedFee>> IFeeRestClient.GetFeesAsync(GetFeeRequest request, CancellationToken ct)
        {
            var validationError = ((IFeeRestClient)this).GetFeeOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFee>(Exchange, validationError);

            // Get data
            var result = await Account.GetTradeFeeAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedFee>(Exchange, null, default);

            var symbol = result.Data.SingleOrDefault();
            if (symbol == null)
                return result.AsExchangeError<SharedFee>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol not found")));

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedFee(symbol.MakerFee * 100, symbol.TakerFee * 100));
        }
        #endregion

        #region Trigger Order Client
        PlaceSpotTriggerOrderOptions ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderOptions { get; } = new PlaceSpotTriggerOrderOptions(true);
        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.PlaceSpotTriggerOrderAsync(PlaceSpotTriggerOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).PlaceSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes, ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

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
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(result.Data.Id.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotTriggerOrderRestClient.GetSpotTriggerOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotTriggerOrder>> ISpotTriggerOrderRestClient.GetSpotTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).GetSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var id))
                throw new ArgumentException($"Invalid order id");

            var result = await Trading.GetOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                id,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTriggerOrder>(Exchange, null, default);

            var (orderType, orderDirection) = ParseTriggerDirections(result.Data.Type, result.Data.Side);
            // Return
            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTriggerOrder(
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
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(BinanceOrder data)
        {
            if (data.Status == OrderStatus.Filled)
                return SharedTriggerOrderStatus.Filled;

            if (data.Status == OrderStatus.Canceled || data.Status == OrderStatus.Rejected || data.Status == OrderStatus.Expired)
                return SharedTriggerOrderStatus.CanceledOrRejected;

            return SharedTriggerOrderStatus.Active;
        }

        EndpointOptions<CancelOrderRequest> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotTriggerOrderRestClient.CancelSpotTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTriggerOrderRestClient)this).CancelSpotTriggerOrderOptions.ValidateRequest(Exchange, request, request.Symbol!.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol!.GetSymbol(FormatSymbol), orderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(order.Data.Id.ToString()));
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

        TransferOptions ITransferRestClient.TransferOptions { get; } = new TransferOptions([
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
        async Task<ExchangeWebResult<SharedId>> ITransferRestClient.TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = ((ITransferRestClient)this).TransferOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var transferType = GetTransferType(request);
            if (transferType == null)
                return new ExchangeWebResult<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await Account.TransferAsync(
                transferType.Value,
                request.Asset,
                request.Quantity,
                request.FromSymbol,
                request.ToSymbol,
                ct: ct).ConfigureAwait(false);
            if (!transfer)
                return transfer.AsExchangeResult<SharedId>(Exchange, null, default);

            return transfer.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(transfer.Data.TransactionId.ToString()));
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
