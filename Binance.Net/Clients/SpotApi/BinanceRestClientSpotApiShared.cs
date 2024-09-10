using Binance.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using Binance.Net.Enums;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;

namespace Binance.Net.Clients.SpotApi
{
    internal partial class BinanceRestClientSpotApi : IBinanceRestClientSpotApiShared
    {
        public string Exchange => BinanceExchange.ExchangeName;
        public ApiType[] SupportedApiTypes => new[] { ApiType.Spot };

        #region Klines Client

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

            // Get data
            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                interval,
                fromTimestamp ?? request.StartTime,
                request.EndTime?.AddSeconds(-1),
                request.Limit ?? 1000,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, result.As<IEnumerable<SharedKline>>(default));

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

        #region Spot Symbol client
        EndpointOptions ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions("GetSpotSymbolsRequest", false);

        async Task<ExchangeWebResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSpotSymbolsAsync(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Symbols.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name, s.Status == SymbolStatus.Trading)
            {
                MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                MaxTradeQuantity = s.LotSizeFilter?.MaxQuantity,
                QuantityStep = s.LotSizeFilter?.StepSize,
                PriceStep = s.PriceFilter?.TickSize
            }));
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetTickerAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedSpotTicker(result.Data.Symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice, result.Data.Volume));
        }

        EndpointOptions ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions("GetSpotTickersRequest", false);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotTicker>>> ISpotTickerRestClient.GetSpotTickersAsync(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotTicker>>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotTicker>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedSpotTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume)));
        }

        #endregion

        #region Recent Trades client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(1000, false);

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            // Get data
            var result = await ExchangeData.GetRecentTradesAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            // Return
            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedTrade(x.BaseQuantity, x.Price, x.TradeTime)));
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
                limit:  1000,
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

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 5000, false);
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

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedBalance(x.Asset, x.Available, x.Total)));
        }

        #endregion

        #region Spot Order Client

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market,
                SharedOrderType.LimitMaker
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
                SharedQuantityType.Both,
                SharedQuantityType.Both));

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Limit ? Enums.SpotOrderType.Limit : request.OrderType == SharedOrderType.Market ? Enums.SpotOrderType.Market: Enums.SpotOrderType.LimitMaker,
                quantity: request.Quantity,
                quoteQuantity: request.QuoteQuantity,
                price: request.Price,
                newClientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.Id.ToString()));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AverageFillPrice,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.QuoteQuantity,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(order.Data.TimeInForce),
                UpdateTime = order.Data.UpdateTime
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request,exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol((baseAsset, quoteAsset, deliveryDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliveryDate));
            var orders = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime
            }));
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

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
                return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 1000))
                nextToken = new DateTimeToken(orders.Data.Max(o => o.CreateTime));

            return orders.AsExchangeResult(Exchange, orders.Data.Where(x => x.Status == OrderStatus.Filled || x.Status == OrderStatus.Canceled || x.Status == OrderStatus.Expired).Select(x => new SharedSpotOrder(
                x.Symbol,
                x.Id.ToString(),
                ParseOrderType(x.Type),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AverageFillPrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(x.TimeInForce),
                UpdateTime = x.UpdateTime
            }));
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
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
                Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
            }));
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
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
                Role = x.IsMaker ? SharedRole.Maker : SharedRole.Taker
            }), nextToken);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<SharedId>(Exchange, new ArgumentError("Invalid order id"));

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol((baseAsset, quoteAsset, deliverDate) => FormatSymbol(baseAsset, quoteAsset, request.ApiType, deliverDate)), orderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedId(order.Data.Id.ToString()));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.PendingNew || status == OrderStatus.New || status == OrderStatus.PartiallyFilled || status == OrderStatus.PendingCancel) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.Rejected || status == OrderStatus.Expired) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
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
            if (tif == TimeInForce.GoodTillDate) return SharedTimeInForce.GoodTillDate;

            return null;
        }

        #endregion

        #region Asset client
        EndpointOptions IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions("GetAssetsRequest", true);

        async Task<ExchangeWebResult<IEnumerable<SharedAsset>>> IAssetsRestClient.GetAssetsAsync(ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedAsset>>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, default);

            return assets.AsExchangeResult(Exchange, assets.Data.Select(x => new SharedAsset(x.Asset)
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
                    WithdrawFee = x.WithdrawFee
                })
            }));
        }

        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await Account.GetUserAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, default);

            var asset = assets.Data.SingleOrDefault(x => x.Asset.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset == null)
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError("Asset not found"));

            return assets.AsExchangeResult(Exchange,new SharedAsset(asset.Asset)
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
                    WithdrawFee = x.WithdrawFee
                })
            });
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedDepositAddress>>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDepositAddress>>(Exchange, validationError);

            var depositAddresses = await Account.GetDepositAddressAsync(request.Asset, request.Network).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, default);

            return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, new[] { new SharedDepositAddress(depositAddresses.Data.Asset, depositAddresses.Data.Address) 
            { 
                Tag = depositAddresses.Data.Tag
            } 
            });
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedDeposit>>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDeposit>>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var deposits = await Account.GetDepositHistoryAsync(
                request.Asset,
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, default);

            // Determine next token
            OffsetToken? nextToken = null;
            if (deposits.Data.Count() == (request.Limit ?? 100))
                nextToken = new OffsetToken((offset ?? 0) + deposits.Data.Count());

            return deposits.AsExchangeResult(Exchange, deposits.Data.Select(x => new SharedDeposit(x.Asset, x.Quantity, x.Status == DepositStatus.Success, x.InsertTime)
            {
                Confirmations = x.Confirmations.Contains("/") ? int.Parse(x.Confirmations.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[0]) : null,
                Network = x.Network,
                TransactionId = x.TransactionId,
                Tag = x.AddressTag
            }), nextToken);
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(true, true);
        async Task<ExchangeWebResult<IEnumerable<SharedWithdrawal>>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, exchangeParameters, ApiType.Spot, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedWithdrawal>>(Exchange, validationError);

            // Determine page token
            int? offset = null;
            if (pageToken is OffsetToken offsetToken)
                offset = offsetToken.Offset;

            // Get data
            var withdrawals = await Account.GetWithdrawalHistoryAsync(
                request.Asset,
                startTime: request.StartTime,
                endTime: request.EndTime,
                limit: request.Limit ?? 100,
                offset: offset,
                ct: ct).ConfigureAwait(false);
            if (!withdrawals)
                return withdrawals.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, default);

            // Determine next token
            OffsetToken nextToken;
            if (withdrawals.Data.Count() == (request.Limit ?? 100))
                nextToken = new OffsetToken((offset ?? 0) + withdrawals.Data.Count());

            return withdrawals.AsExchangeResult(Exchange, withdrawals.Data.Select(x => new SharedWithdrawal(x.Asset, x.Address, x.Quantity, x.Status == WithdrawalStatus.Completed, x.ApplyTime)
            {
                Confirmations = x.ConfirmTimes,
                Network = x.Network,
                Tag = x.AddressTag,
                TransactionId = x.TransactionId,
                Fee = x.TransactionFee
            }));
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task <ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, exchangeParameters, ApiType.Spot, SupportedApiTypes);
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
                return withdrawal.AsExchangeResult<SharedId>(Exchange, default);

            return withdrawal.AsExchangeResult(Exchange, new SharedId(withdrawal.Data.Id));
        }

        #endregion
    }
}
