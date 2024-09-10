using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using Binance.Net.Enums;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using CryptoExchange.Net.SharedApis.Models.EndpointOptions;

namespace Binance.Net.Clients.CoinFuturesApi
{
    internal partial class BinanceRestClientCoinFuturesApi : IBinanceRestClientCoinFuturesApiShared
    {
        public string Exchange => BinanceExchange.ExchangeName;

        public ApiType[] SupportedApiTypes => new[] { ApiType.DeliveryInverse, ApiType.PerpetualInverse };

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(true, false)
        {
            MaxRequestDataPoints = 1000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                interval,
                fromTimestamp ?? request.StartTime,
                request.EndTime,
                request.Limit ?? 1000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (request.StartTime != null && result.Data.Any())
            {
                var maxOpenTime = result.Data.Max(x => x.OpenTime);
                if (maxOpenTime < request.EndTime!.Value.AddSeconds(-(int)request.Interval))
                    nextToken = new DateTimeToken(maxOpenTime.AddSeconds((int)interval));
            }

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)), nextToken);
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions("GetFuturesSymbolsRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(ApiType apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, default);

            var data = result.Data.Symbols.Where(x => apiType == ApiType.PerpetualInverse ? x.ContractType == ContractType.Perpetual : x.ContractType != ContractType.Perpetual);
            return result.AsExchangeResult(Exchange, data.Select(s => new SharedFuturesSymbol(s.ContractType == ContractType.Perpetual ? SharedSymbolType.PerpetualInverse : SharedSymbolType.DeliveryInverse, s.BaseAsset, s.QuoteAsset, s.Name, s.Status == SymbolStatus.Trading)
            {
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                QuantityStep = s.LotSizeFilter?.StepSize,
                PriceStep = s.PriceFilter?.TickSize,
                ContractSize = 1,
                DeliveryTime = s.DeliveryDate
            }));
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = ExchangeData.GetTickersAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), ct: ct);
            var resultMarkPrice = ExchangeData.GetMarkPricesAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), ct: ct);
            await Task.WhenAll(resultTicker, resultMarkPrice).ConfigureAwait(false);
            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);
            if (!resultMarkPrice.Result)
                return resultMarkPrice.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);

            var ticker = resultTicker.Result.Data.SingleOrDefault();
            var mark = resultMarkPrice.Result.Data.SingleOrDefault();

            if (ticker == null || mark == null)
                return resultTicker.Result.AsExchangeError<SharedFuturesTicker>(Exchange, new ServerError("Not found"));

            return resultTicker.Result.AsExchangeResult(Exchange, new SharedFuturesTicker(ticker.Symbol, ticker.LastPrice, ticker.HighPrice, ticker.LowPrice, ticker.Volume)
            {
                IndexPrice = mark.IndexPrice,
                MarkPrice = mark.MarkPrice,
                FundingRate = mark.FundingRate,
                NextFundingTime = mark.NextFundingTime
            });
        }

        EndpointOptions IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions("GetTickersRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesTicker>>> IFuturesTickerRestClient.GetFuturesTickersAsync(ApiType apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesTicker>>(Exchange, validationError);

            var resultTickers = ExchangeData.GetTickersAsync(ct: ct);
            var resultMarkPrices = ExchangeData.GetMarkPricesAsync(ct: ct);
            await Task.WhenAll(resultTickers, resultMarkPrices).ConfigureAwait(false);
            if (!resultTickers.Result)
                return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);
            if (!resultMarkPrices.Result)
                return resultMarkPrices.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);

            return resultTickers.Result.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, resultTickers.Result.Data.Select(x =>
            {
                var markPrice = resultMarkPrices.Result.Data.Single(p => p.Symbol == x.Symbol);
                return new SharedFuturesTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume)
                {
                    IndexPrice = markPrice.IndexPrice,
                    MarkPrice = markPrice.MarkPrice,
                    FundingRate = markPrice.FundingRate,
                    NextFundingTime = markPrice.NextFundingTime
                };
            }));
        }

        #endregion

        #region Recent Trade client

        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);
        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetRecentTradesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedTrade(x.BaseQuantity, x.Price, x.TradeTime)));
        }

        #endregion

        #region Futures Order Client

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity));

        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Limit ? Enums.FuturesOrderType.Limit : Enums.FuturesOrderType.Market,
                quantity: request.Quantity,
                price: request.Price,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Long ? PositionSide.Long: PositionSide.Short,
                reduceOnly: request.ReduceOnly,
                newClientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.Id.ToString()));
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedFuturesOrder(
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime,
                PositionSide = order.Data.PositionSide == PositionSide.Both ? null : order.Data.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = order.Data.ReduceOnly
            });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate));
            var orders = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedFuturesOrder(
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime,
                PositionSide = x.PositionSide == PositionSide.Both ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = x.ReduceOnly
            }));
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var orders = await Trading.GetOrdersAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                startTime: fromTimestamp ?? request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 1000).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new DateTimeToken(orders.Data.Max(o => o.CreateTime));

            return orders.AsExchangeResult(Exchange, orders.Data.Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled || x.Status == OrderStatus.Expired).Select(x => new SharedFuturesOrder(
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime,
                PositionSide = x.PositionSide == PositionSide.Both ? null : x.PositionSide == PositionSide.Long ? SharedPositionSide.Long : SharedPositionSide.Short,
                ReduceOnly = x.ReduceOnly
            }));
        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), orderId: orderId).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Maker ? SharedRole.Maker : SharedRole.Taker
            }));
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            long? fromId = null;
            if (pageToken is FromIdToken fromIdToken)
                fromId = long.Parse(fromIdToken.FromToken);

            // Get data
            var orders = await Trading.GetUserTradesAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 500,
                fromId: fromId
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 500))
                nextToken = new FromIdToken(orders.Data.Max(o => o.Id).ToString());

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.Id.ToString(),
                x.Quantity,
                x.Price,
                x.Timestamp)
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.Maker ? SharedRole.Maker : SharedRole.Taker
            }), nextToken);
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedId(order.Data.Id.ToString()));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedPosition>>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPosition>>(Exchange, validationError);

            var result = await Account.GetPositionInformationAsync(pair: request.Symbol?.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, result.Data.Select(x => new SharedPosition(x.Symbol, Math.Abs(x.Quantity), x.UpdateTime)
            {
                UnrealizedPnl = x.UnrealizedPnl,
                LiquidationPrice = x.LiquidationPrice,
                Leverage = x.Leverage,
                AverageEntryPrice = x.EntryPrice,
                PositionSide = x.PositionSide == PositionSide.Both ? (x.Quantity > 0 ? SharedPositionSide.Long : SharedPositionSide.Short) : x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long
            }).ToList());
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                request.PositionSide == SharedPositionSide.Long ? OrderSide.Sell : OrderSide.Buy,
                FuturesOrderType.Market,
                request.Quantity,
                positionSide: request.PositionSide == null ? null : request.PositionSide == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long,
                reduceOnly: true,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.Id.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.PendingNew || status == OrderStatus.New || status == OrderStatus.PartiallyFilled || status == OrderStatus.PendingCancel) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.Rejected || status == OrderStatus.Expired) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
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
            if (tif == TimeInForce.GoodTillDate) return SharedTimeInForce.GoodTillDate;

            return null;
        }

        #endregion

        #region Leverage client

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true);
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.GetPositionInformationAsync(pair: request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, default);

            if (!result.Data.Any())
                return result.AsExchangeError<SharedLeverage>(Exchange, new ServerError("Not found"));

            var sideData =
                request.Side == SharedPositionSide.Short ? result.Data.SingleOrDefault(x => x.PositionSide == PositionSide.Short) :
                request.Side == SharedPositionSide.Long ? result.Data.SingleOrDefault(x => x.PositionSide == PositionSide.Long) :
                result.Data.SingleOrDefault(x => x.PositionSide == PositionSide.Both);

            return result.AsExchangeResult(Exchange, new SharedLeverage(sideData.Leverage)
            {
                Side = request.Side
            });
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions(false);
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.ChangeInitialLeverageAsync(symbol: request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), (int)request.Leverage ,ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedLeverage(result.Data.Leverage));
        }
        #endregion

        #region Mark Klines client

        GetKlinesOptions IMarkPriceKlineRestClient.GetMarkPriceKlinesOptions { get; } = new GetKlinesOptions(true, false)
        {
            MaxRequestDataPoints = 1000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IMarkPriceKlineRestClient.GetMarkPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IMarkPriceKlineRestClient)this).GetMarkPriceKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                interval,
                request.Limit ?? 1000,
                fromTimestamp ?? request.StartTime,
                request.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (request.StartTime != null && result.Data.Any())
            {
                var maxOpenTime = result.Data.Max(x => x.OpenTime);
                if (maxOpenTime < request.EndTime!.Value.AddSeconds(-(int)request.Interval))
                    nextToken = new DateTimeToken(maxOpenTime.AddSeconds((int)interval));
            }

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedMarkKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)), nextToken);
        }

        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 5, 10, 20, 50, 100, 500, 1000 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Trade History client
        GetTradeHistoryOptions ITradeHistoryRestClient.GetTradeHistoryOptions { get; } = new GetTradeHistoryOptions(true, false);

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> ITradeHistoryRestClient.GetTradeHistoryAsync(GetTradeHistoryRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeHistoryRestClient)this).GetTradeHistoryOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            long? fromId = null;
            if (pageToken is FromIdToken token)
                fromId = long.Parse(token.FromToken);

            // Get data
            var result = await ExchangeData.GetAggregatedTradeHistoryAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                startTime: fromId != null ? null : request.StartTime,
                endTime: fromId != null ? null : request.EndTime,
                limit: 1000,
                fromId: fromId,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            FromIdToken? nextToken = null;
            if (result.Data.Any() && result.Data.Last().TradeTime < request.EndTime)
                nextToken = new FromIdToken(result.Data.Max(x => x.Id).ToString());

            // Return
            return result.AsExchangeResult(Exchange, result.Data.Where(x => x.TradeTime < request.EndTime).Select(x => new SharedTrade(x.Quantity, x.Price, x.TradeTime)), nextToken);
        }
        #endregion

        #region Index Klines client

        GetKlinesOptions IIndexPriceKlineRestClient.GetIndexPriceKlinesOptions { get; } = new GetKlinesOptions(true, false)
        {
            MaxRequestDataPoints = 1000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedMarkKline>>> IIndexPriceKlineRestClient.GetIndexPriceKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IIndexPriceKlineRestClient)this).GetIndexPriceKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedMarkKline>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var result = await ExchangeData.GetMarkPriceKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                interval,
                request.Limit ?? 1000,
                fromTimestamp ?? request.StartTime,
                request.EndTime,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedMarkKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (request.StartTime != null && result.Data.Any())
            {
                var maxOpenTime = result.Data.Max(x => x.OpenTime);
                if (maxOpenTime < request.EndTime!.Value.AddSeconds(-(int)request.Interval))
                    nextToken = new DateTimeToken(maxOpenTime.AddSeconds((int)interval));
            }

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedMarkKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice)), nextToken);
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetOpenInterestAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOpenInterest(result.Data.OpenInterest));
        }

        #endregion

        #region Funding Rate client
        GetFundingRateHistoryOptions IFundingRateRestClient.GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(true, false);

        async Task<ExchangeWebResult<IEnumerable<SharedFundingRate>>> IFundingRateRestClient.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFundingRateRestClient)this).GetFundingRateHistoryOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFundingRate>>(Exchange, validationError);

            DateTime? fromTime = null;
            if (pageToken is DateTimeToken token)
                fromTime = token.LastTime;

            // Get data
            var result = await ExchangeData.GetFundingRatesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                startTime: fromTime ?? request.StartTime,
                endTime: request.EndTime,
                limit: 1000,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFundingRate>>(Exchange, default);

            DateTimeToken? nextToken = null;
            if (result.Data.Count() == 1000)
                nextToken = new DateTimeToken(result.Data.Max(x => x.FundingTime));

            // Return
            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedFundingRate(x.FundingRate, x.FundingTime)), nextToken);
        }
        #endregion

        #region Balance Client
        EndpointOptions IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions("GetBalancesRequest", true);

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(ApiType apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedBalance(x.Asset, x.AvailableBalance, x.WalletBalance)));
        }

        #endregion

        #region Position Mode client

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions(false);
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).GetPositionModeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.GetPositionModeAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedPositionModeResult(result.Data.IsHedgeMode ? SharedPositionMode.LongShort : SharedPositionMode.OneWay));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions(true, true, false);
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IPositionModeRestClient)this).SetPositionModeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedPositionModeResult>(Exchange, validationError);

            var result = await Account.ModifyPositionModeAsync(request.Mode == SharedPositionMode.LongShort, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedPositionModeResult>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedPositionModeResult(request.Mode));
        }
        #endregion

    }
}
