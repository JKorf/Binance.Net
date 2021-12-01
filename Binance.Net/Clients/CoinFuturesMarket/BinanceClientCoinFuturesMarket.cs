using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.ExchangeInterfaces;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Interfaces.Clients.Rest.CoinFutures;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Futures;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using Binance.Net.Clients.Rest.Spot;
using CryptoExchange.Net.Logging;

namespace Binance.Net.Clients.Rest.CoinFutures
{
    /// <inheritdoc cref="IBinanceClientCoinFutures" />
    public class BinanceClientCoinFuturesMarket : RestApiClient, IBinanceClientCoinFuturesMarket, IExchangeClient
    {
        #region fields 
        private readonly BinanceClient _baseClient;
        internal readonly BinanceClientOptions Options;

        internal static double CalculatedTimeOffset;
        internal static bool TimeSynced;
        internal static DateTime LastTimeSync;

        internal readonly TradeRulesBehaviour TradeRulesBehaviour;
        internal BinanceFuturesCoinExchangeInfo? ExchangeInfo;
        internal DateTime? LastExchangeInfoUpdate;

        private Log _log;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBinanceClientCoinFuturesMarketAccount Account { get; }
        /// <inheritdoc />
        public IBinanceClientCoinFuturesMarketExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBinanceClientCoinFuturesMarketTrading Trading { get; }
        #endregion

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<ICommonOrderId>? OnOrderCanceled;

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClientCoinFuturesMarket(Log log, BinanceClient baseClient, BinanceClientOptions options) :
            base(options, options.CoinFuturesApiOptions)
        {
            _log = log;
            Options = options;
            _baseClient = baseClient;

            Account = new BinanceClientCoinFuturesMarketAccount(this);
            ExchangeData = new BinanceClientCoinFuturesMarketExchangeData(log, this);
            Trading = new BinanceClientCoinFuturesMarketTrading(log, this);
        }
        #endregion

        public override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BinanceAuthenticationProvider(credentials);

        internal string GetTimestamp()
        {
            var offset = Options.AutoTimestamp ? CalculatedTimeOffset : 0;
            offset += Options.CoinFuturesApiOptions.TimestampOffset.TotalMilliseconds;
            return DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow.AddMilliseconds(offset))!.Value.ToString(CultureInfo.InvariantCulture);
        }

        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal async Task<WebCallResult<DateTime>> CheckAutoTimestamp(CancellationToken ct)
        {
            if (Options.AutoTimestamp && (!TimeSynced || DateTime.UtcNow - LastTimeSync > Options.AutoTimestampRecalculationInterval))
                return await ExchangeData.GetServerTimeAsync(TimeSynced, ct).ConfigureAwait(false);

            return new WebCallResult<DateTime>(null, null, default, null);
        }

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? price, decimal? stopPrice, OrderType type, CancellationToken ct)
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

            if (symbolData.LotSizeFilter != null || (symbolData.MarketLotSizeFilter != null && type == OrderType.Market))
            {
                var minQty = symbolData.LotSizeFilter?.MinQuantity;
                var maxQty = symbolData.LotSizeFilter?.MaxQuantity;
                var stepSize = symbolData.LotSizeFilter?.StepSize;
                if (type == OrderType.Market && symbolData.MarketLotSizeFilter != null)
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
            if (signed)
            {
                var timestampResult = await CheckAutoTimestamp(cancellationToken).ConfigureAwait(false);
                if (!timestampResult)
                    return new WebCallResult<T>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

                if (parameters == null)
                    parameters = new Dictionary<string, object>();

                parameters.Add("timestamp", GetTimestamp());
            }

            return await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonSymbol>>> GetSymbolsAsync()
        {
            var exchangeInfo = await ExchangeData.GetExchangeInfoAsync().ConfigureAwait(false);
            return exchangeInfo.As<IEnumerable<ICommonSymbol>>(exchangeInfo.Data?.Symbols);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonTicker>>> GetTickersAsync()
        {
            var tickers = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            return tickers.As<IEnumerable<ICommonTicker>>(tickers.Data?.Select(d => (BinanceFuturesCoin24HPrice)d));
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ICommonTicker>> GetTickerAsync(string symbol)
        {
            var tickers = await ExchangeData.GetTickersAsync(symbol).ConfigureAwait(false);
            return tickers.As<ICommonTicker>((BinanceFuturesCoin24HPrice?)tickers.Data?.FirstOrDefault());
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonKline>>> GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var klines = await ExchangeData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime, limit).ConfigureAwait(false);
            return klines.As<IEnumerable<ICommonKline>>(klines.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ICommonOrderBook>> GetOrderBookAsync(string symbol)
        {
            var orderBookResult = await ExchangeData.GetOrderBookAsync(symbol).ConfigureAwait(false);
            return orderBookResult.As<ICommonOrderBook>(orderBookResult.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> GetRecentTradesAsync(string symbol)
        {
            var tradesResult = await ExchangeData.GetRecentTradesAsync(symbol).ConfigureAwait(false);
            return tradesResult.As<IEnumerable<ICommonRecentTrade>>(tradesResult.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ICommonOrderId>> PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price = null, string? accountId = null)
        {
            var result = await Trading.PlaceOrderAsync(symbol, GetOrderSide(side), GetOrderType(type), quantity, price: price, timeInForce: type == IExchangeClient.OrderType.Limit ? TimeInForce.GoodTillCancel : (TimeInForce?)null).ConfigureAwait(false);
            return result.As<ICommonOrderId>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ICommonOrder>> GetOrderAsync(string orderId, string? symbol = null)
        {
            if (string.IsNullOrEmpty(symbol))
                return WebCallResult<ICommonOrder>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.GetOrderAsync)));

            var result = await Trading.GetOrderAsync(symbol!, long.Parse(orderId)).ConfigureAwait(false);
            return result.As<ICommonOrder>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonTrade>>> GetTradesAsync(string orderId, string? symbol = null)
        {
            if (string.IsNullOrEmpty(symbol))
                return WebCallResult<IEnumerable<ICommonTrade>>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.GetTradesAsync)));

            var result = await Trading.GetUserTradesAsync(symbol!).ConfigureAwait(false);
            return result.As(result.Data.Where(t => t.OrderId == long.Parse(orderId)).Select(t => (ICommonTrade)t));
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonOrder>>> GetOpenOrdersAsync(string? symbol = null)
        {
            var result = await Trading.GetOpenOrdersAsync().ConfigureAwait(false);
            return result.As<IEnumerable<ICommonOrder>>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonOrder>>> GetClosedOrdersAsync(string? symbol = null)
        {
            if (symbol == null)
                return WebCallResult<IEnumerable<ICommonOrder>>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.GetClosedOrdersAsync)));

            var result = await Trading.GetOrdersAsync(symbol).ConfigureAwait(false);
            return result.As<IEnumerable<ICommonOrder>>(result.Data.Where(r => r.Status != OrderStatus.New && r.Status != OrderStatus.PartiallyFilled));
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ICommonOrderId>> CancelOrderAsync(string orderId, string? symbol = null)
        {
            if (symbol == null)
                return WebCallResult<ICommonOrderId>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.CancelOrderAsync)));

            var result = await Trading.CancelOrderAsync(symbol, orderId: long.Parse(orderId)).ConfigureAwait(false);
            return result.As<ICommonOrderId>(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ICommonBalance>>> GetBalancesAsync(string? accountId = null)
        {
            var result = await Account.GetAccountInfoAsync().ConfigureAwait(false);
            return result.As<IEnumerable<ICommonBalance>>(result.Data?.Assets.Select(b => (ICommonBalance)b));
        }

        internal void InvokeOrderPlaced(ICommonOrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(ICommonOrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        protected static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
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

        /// <summary>
        /// Get the symbol name for sending requests to the Binance server based on a base and quote asset
        /// </summary>
        /// <param name="baseAsset">The base asset name</param>
        /// <param name="quoteAsset">The quote asset name</param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            (baseAsset + quoteAsset).ToUpper(CultureInfo.InvariantCulture);

        protected static OrderSide GetOrderSide(IExchangeClient.OrderSide side)
        {
            if (side == IExchangeClient.OrderSide.Sell) return OrderSide.Sell;
            if (side == IExchangeClient.OrderSide.Buy) return OrderSide.Buy;

            throw new ArgumentException("Unsupported order side for Binance order: " + side);
        }

        protected static OrderType GetOrderType(IExchangeClient.OrderType type)
        {
            if (type == IExchangeClient.OrderType.Limit) return OrderType.Limit;
            if (type == IExchangeClient.OrderType.Market) return OrderType.Market;

            throw new ArgumentException("Unsupported order type for Binance order: " + type);
        }

    }
}
