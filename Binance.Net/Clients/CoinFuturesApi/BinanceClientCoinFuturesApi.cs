using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Logging;
using Binance.Net.Interfaces.Clients.CoinFuturesApi;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Clients.CoinFuturesApi
{
    /// <inheritdoc cref="IBinanceClientCoinFuturesApi" />
    public class BinanceClientCoinFuturesApi : RestApiClient, IBinanceClientCoinFuturesApi, IFuturesClient
    {
        #region fields 
        private readonly BinanceClient _baseClient;
        internal new readonly BinanceClientOptions Options;

        internal readonly TradeRulesBehaviour TradeRulesBehaviour;
        internal BinanceFuturesCoinExchangeInfo? ExchangeInfo;
        internal DateTime? LastExchangeInfoUpdate;

        internal static TimeSyncState TimeSyncState = new TimeSyncState();

        private readonly Log _log;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceClientCoinFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBinanceClientCoinFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceClientCoinFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Binance";
        #endregion

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        #region constructor/destructor
        internal BinanceClientCoinFuturesApi(Log log, BinanceClient baseClient, BinanceClientOptions options) :
            base(options, options.CoinFuturesApiOptions)
        {
            _log = log;
            Options = options;
            _baseClient = baseClient;

            Account = new BinanceClientCoinFuturesApiAccount(this);
            ExchangeData = new BinanceClientCoinFuturesApiExchangeData(log, this);
            Trading = new BinanceClientCoinFuturesApiTrading(log, this);
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result = result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? price, decimal? stopPrice, FuturesOrderType type, CancellationToken ct)
        {
            var outputQuantity = quantity;
            var outputPrice = price;
            var outputStopPrice = stopPrice;

            if (TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);

            if (ExchangeInfo == null || LastExchangeInfoUpdate == null || (DateTime.UtcNow - LastExchangeInfoUpdate.Value).TotalMinutes > Options.CoinFuturesApiOptions.TradeRulesUpdateInterval.TotalMinutes)
                await ExchangeData.GetExchangeInfoAsync(ct).ConfigureAwait(false);

            if (ExchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");

            var symbolData = ExchangeInfo.Symbols.SingleOrDefault(s => string.Equals(s.Name, symbol, StringComparison.CurrentCultureIgnoreCase));
            if (symbolData == null)
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Symbol {symbol} not found");

            if (!symbolData.OrderTypes.Contains(type))
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: {type} order type not allowed for {symbol}");

            if (symbolData.LotSizeFilter != null || symbolData.MarketLotSizeFilter != null && type == Enums.FuturesOrderType.Market)
            {
                var minQty = symbolData.LotSizeFilter?.MinQuantity;
                var maxQty = symbolData.LotSizeFilter?.MaxQuantity;
                var stepSize = symbolData.LotSizeFilter?.StepSize;
                if (type == Enums.FuturesOrderType.Market && symbolData.MarketLotSizeFilter != null)
                {
                    minQty = symbolData.MarketLotSizeFilter.MinQuantity;
                    if (symbolData.MarketLotSizeFilter.MaxQuantity != 0)
                        maxQty = symbolData.MarketLotSizeFilter.MaxQuantity;

                    if (symbolData.MarketLotSizeFilter.StepSize != 0)
                        stepSize = symbolData.MarketLotSizeFilter.StepSize;
                }

                if (minQty.HasValue && quantity.HasValue)
                {
                    outputQuantity = BinanceHelpers.ClampQuantity(minQty.Value, maxQty!.Value, stepSize!.Value, quantity.Value);
                    if (outputQuantity != quantity.Value)
                    {
                        if (TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");
                        }

                        _log.Write(LogLevel.Information, $"Quantity clamped from {quantity} to {outputQuantity}");
                    }
                }
            }

            if (price == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, null, outputStopPrice);

            if (symbolData.PriceFilter != null)
            {
                if (symbolData.PriceFilter.MaxPrice != 0 && symbolData.PriceFilter.MinPrice != 0)
                {
                    outputPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice, symbolData.PriceFilter.MaxPrice, price.Value);
                    if (outputPrice != price)
                    {
                        if (TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter max/min failed. Original price: {price}, Closest allowed: {outputPrice}");

                        _log.Write(LogLevel.Information, $"price clamped from {price} to {outputPrice}");
                    }

                    if (stopPrice != null)
                    {
                        outputStopPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice,
                            symbolData.PriceFilter.MaxPrice, stopPrice.Value);
                        if (outputStopPrice != stopPrice)
                        {
                            if (TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter max/min failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");

                            _log.Write(LogLevel.Information,
                                $"Stop price clamped from {stopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }

                if (symbolData.PriceFilter.TickSize != 0)
                {
                    var beforePrice = outputPrice;
                    outputPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, price.Value);
                    if (outputPrice != beforePrice)
                    {
                        if (TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter tick failed. Original price: {price}, Closest allowed: {outputPrice}");

                        _log.Write(LogLevel.Information, $"price rounded from {beforePrice} to {outputPrice}");
                    }

                    if (stopPrice != null)
                    {
                        var beforeStopPrice = outputStopPrice;
                        outputStopPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, stopPrice.Value);
                        if (outputStopPrice != beforeStopPrice)
                        {
                            if (TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter tick failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");

                            _log.Write(LogLevel.Information,
                                $"Stop price floored from {beforeStopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);
        }

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1) where T : class
        {
            return await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, Options.CoinFuturesApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        /// <inheritdoc />
        public IFuturesClient CommonFuturesClient => this;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            (baseAsset + quoteAsset).ToUpper(CultureInfo.InvariantCulture);

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        async Task<WebCallResult<OrderId>> IFuturesClient.PlaceOrderAsync(string symbol, CryptoExchange.Net.CommonObjects.OrderSide side, OrderType type, decimal quantity, decimal? price, int? leverage, string? accountId)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.PlaceOrderAsync), nameof(symbol));

            var order = await Trading.PlaceOrderAsync(symbol, GetOrderSide(side), GetOrderType(type), quantity, price: price, timeInForce: type == OrderType.Limit ? TimeInForce.GoodTillCanceled : (TimeInForce?)null).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<IEnumerable<Position>>> IFuturesClient.GetPositionsAsync()
        {
            var positions = await Account.GetPositionInformationAsync().ConfigureAwait(false);
            if (!positions)
                return positions.As<IEnumerable<Position>>(null);

            return positions.As(positions.Data.Select(p =>
                new Position
                {
                    SourceObject = p,
                    Symbol = p.Symbol,
                    AutoMargin = p.IsAutoAddMargin,
                    EntryPrice = p.EntryPrice,
                    Isolated = p.MarginType == FuturesMarginType.Isolated,
                    Leverage = p.Leverage,
                    LiquidationPrice = p.LiquidationPrice,
                    MarkPrice = p.MarkPrice,
                    Quantity = p.Quantity,
                    UnrealizedPnl = p.UnrealizedPnl,
                    Side = p.PositionSide == Enums.PositionSide.Long ? CryptoExchange.Net.CommonObjects.PositionSide.Long : p.PositionSide == Enums.PositionSide.Short ? CryptoExchange.Net.CommonObjects.PositionSide.Short : CryptoExchange.Net.CommonObjects.PositionSide.Both
                }
            ));
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Order id invalid", nameof(orderId));

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetOrderAsync), nameof(symbol));

            var order = await Trading.GetOrderAsync(symbol!, id).ConfigureAwait(false);
            if (!order)
                return order.As<Order>(null);

            return order.As(new Order
            {
                SourceObject = order,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture),
                Symbol = order.Data.Symbol,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                Side = order.Data.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.CommonObjects.OrderSide.Buy : CryptoExchange.Net.CommonObjects.OrderSide.Sell,
                Type = GetOrderType(order.Data.Type),
                Status = GetOrderStatus(order.Data.Status),
                Timestamp = order.Data.CreateTime
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Order id invalid", nameof(orderId));

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetOrderTradesAsync), nameof(symbol));

            var trades = await Trading.GetUserTradesAsync(symbol!).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<UserTrade>>(null);

            return trades.As(trades.Data.Where(t => t.OrderId == id).Select(t =>
                new UserTrade
                {
                    SourceObject = t,
                    Id = t.Id.ToString(CultureInfo.InvariantCulture),
                    OrderId = t.OrderId.ToString(CultureInfo.InvariantCulture),
                    Symbol = t.Symbol,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    Fee = t.Fee,
                    FeeAsset = t.FeeAsset,
                    Timestamp = t.Timestamp
                }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol)
        {
            var orderInfo = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            if (!orderInfo)
                return orderInfo.As<IEnumerable<Order>>(null);

            return orderInfo.As(orderInfo.Data.Select(s =>
                new Order
                {
                    SourceObject = s,
                    Id = s.Id.ToString(CultureInfo.InvariantCulture),
                    Symbol = s.Symbol,
                    Side = s.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.CommonObjects.OrderSide.Buy : CryptoExchange.Net.CommonObjects.OrderSide.Sell,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    QuantityFilled = s.QuantityFilled,
                    Type = GetOrderType(s.Type),
                    Status = GetOrderStatus(s.Status),
                    Timestamp = s.CreateTime
                }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetClosedOrdersAsync), nameof(symbol));

            var orderInfo = await Trading.GetOrdersAsync(symbol!).ConfigureAwait(false);
            if (!orderInfo)
                return orderInfo.As<IEnumerable<Order>>(null);

            return orderInfo.As(orderInfo.Data.Where(o => o.Status == Enums.OrderStatus.Canceled || o.Status == Enums.OrderStatus.Filled).Select(s =>
                new Order
                {
                    SourceObject = s,
                    Id = s.Id.ToString(CultureInfo.InvariantCulture),
                    Symbol = s.Symbol,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    QuantityFilled = s.QuantityFilled,
                    Side = s.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.CommonObjects.OrderSide.Buy : CryptoExchange.Net.CommonObjects.OrderSide.Sell,
                    Type = GetOrderType(s.Type),
                    Status = GetOrderStatus(s.Status),
                    Timestamp = s.CreateTime
                }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol)
        {
            if (!long.TryParse(orderId, out var id))
                throw new ArgumentException("Order id invalid", nameof(orderId));

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.CancelOrderAsync), nameof(symbol));

            var order = await Trading.CancelOrderAsync(symbol!, id).ConfigureAwait(false);
            if (!order)
                return order.As<OrderId>(null);

            return order.As(new OrderId
            {
                SourceObject = order,
                Id = order.Data.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync()
        {
            var exchangeInfo = await ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);
            if (!exchangeInfo)
                return exchangeInfo.As<IEnumerable<Symbol>>(null);

            return exchangeInfo.As(exchangeInfo.Data.Symbols.Select(s =>
                new Symbol
                {
                    SourceObject = s,
                    Name = s.Name,
                    MinTradeQuantity = s.LotSizeFilter?.MinQuantity,
                    QuantityStep = s.LotSizeFilter?.StepSize,
                    PriceStep = s.PriceFilter?.TickSize
                }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetTickerAsync), nameof(symbol));

            var tickers = await ExchangeData.GetTickersAsync(symbol).ConfigureAwait(false);
            if (!tickers)
                return tickers.As<Ticker>(null);

            var ticker = tickers.Data.First();
            return tickers.As(new Ticker
            {
                SourceObject = tickers.Data,
                Symbol = ticker.Symbol,
                HighPrice = ticker.HighPrice,
                LowPrice = ticker.LowPrice,
                Price24H = ticker.OpenPrice,
                LastPrice = ticker.LastPrice,
                Volume = ticker.Volume
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync()
        {
            var tickers = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            if (!tickers)
                return tickers.As<IEnumerable<Ticker>>(null);

            return tickers.As(tickers.Data.Select(t => new Ticker
            {
                SourceObject = t,
                Symbol = t.Symbol,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,
                Price24H = t.OpenPrice,
                LastPrice = t.LastPrice,
                Volume = t.Volume
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetKlinesAsync), nameof(symbol));

            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime, limit).ConfigureAwait(false);
            if (!klines)
                return klines.As<IEnumerable<Kline>>(null);

            return klines.As(klines.Data.Select(t => new Kline
            {
                SourceObject = t,
                HighPrice = t.HighPrice,
                LowPrice = t.LowPrice,
                OpenTime = t.OpenTime,
                ClosePrice = t.ClosePrice,
                OpenPrice = t.OpenPrice,
                Volume = t.Volume
            }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetOrderBookAsync), nameof(symbol));

            var orderbook = await ExchangeData.GetOrderBookAsync(symbol).ConfigureAwait(false);
            if (!orderbook)
                return orderbook.As<OrderBook>(null);

            return orderbook.As(new OrderBook
            {
                SourceObject = orderbook.Data,
                Asks = orderbook.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
                Bids = orderbook.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            }); 
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for Binance " + nameof(IFuturesClient.GetRecentTradesAsync), nameof(symbol));

            var trades = await ExchangeData.GetRecentTradesAsync(symbol).ConfigureAwait(false);
            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.Select(t => new Trade
            {
                SourceObject = t,
                Symbol = symbol,
                Price = t.Price,
                Quantity = t.BaseQuantity,
                Timestamp = t.TradeTime
            }));
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId)
        {
            var balances = await Account.GetAccountInfoAsync().ConfigureAwait(false);
            if (!balances)
                return balances.As<IEnumerable<Balance>>(null);

            return balances.As(balances.Data.Assets.Select(t => new Balance
            {
                SourceObject = t,
                Asset = t.Asset,
                Available = t.AvailableBalance,
                Total = t.WalletBalance
            }));
        }

        private static OrderType GetOrderType(FuturesOrderType orderType)
        {
            if (orderType == FuturesOrderType.Limit)
                return OrderType.Limit;
            if (orderType == FuturesOrderType.Market)
                return OrderType.Market;
            return OrderType.Other;
        }

        private static CryptoExchange.Net.CommonObjects.OrderStatus GetOrderStatus(Enums.OrderStatus orderStatus)
        {
            if (orderStatus == Enums.OrderStatus.New || orderStatus == Enums.OrderStatus.PartiallyFilled)
                return CryptoExchange.Net.CommonObjects.OrderStatus.Active;
            if (orderStatus == Enums.OrderStatus.Filled)
                return CryptoExchange.Net.CommonObjects.OrderStatus.Filled;
            return CryptoExchange.Net.CommonObjects.OrderStatus.Canceled;
        }

        private static Enums.OrderSide GetOrderSide(CryptoExchange.Net.CommonObjects.OrderSide side)
        {
            if (side == CryptoExchange.Net.CommonObjects.OrderSide.Sell) return Enums.OrderSide.Sell;
            if (side == CryptoExchange.Net.CommonObjects.OrderSide.Buy) return Enums.OrderSide.Buy;

            throw new ArgumentException("Unsupported order side for Binance order: " + side);
        }

        private static FuturesOrderType GetOrderType(OrderType type)
        {
            if (type == OrderType.Limit) return FuturesOrderType.Limit;
            if (type == OrderType.Market) return FuturesOrderType.Market;

            throw new ArgumentException("Unsupported order type for Binance order: " + type);
        }

        private static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(3)) return KlineInterval.ThreeMinutes;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(2)) return KlineInterval.TwoHour;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHour;
            if (timeSpan == TimeSpan.FromHours(6)) return KlineInterval.SixHour;
            if (timeSpan == TimeSpan.FromHours(8)) return KlineInterval.EightHour;
            if (timeSpan == TimeSpan.FromHours(12)) return KlineInterval.TwelveHour;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(3)) return KlineInterval.ThreeDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return KlineInterval.OneMonth;

            throw new ArgumentException("Unsupported timespan for Binance Klines, check supported intervals using Binance.Net.Enums.KlineInterval");
        }
    }
}
