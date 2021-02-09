using Binance.Net.Converters;
using Binance.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using Binance.Net.Objects.Spot;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Interfaces.SubClients.Spot;
using Binance.Net.SubClients;
using Binance.Net.SubClients.Futures.Coin;
using Binance.Net.SubClients.Futures.Usdt;
using Binance.Net.SubClients.Margin;
using Binance.Net.SubClients.Spot;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance REST Api
    /// </summary>
    public class BinanceClient : RestClient, IBinanceClient, IExchangeClient
    {
        #region fields 
        private static BinanceClientOptions _defaultOptions = new BinanceClientOptions();
        private static BinanceClientOptions DefaultOptions => _defaultOptions.Copy();

        internal readonly bool AutoTimestamp;
        internal readonly TimeSpan AutoTimestampRecalculationInterval;
        internal readonly TimeSpan TimestampOffset;
        internal readonly TradeRulesBehaviour TradeRulesBehaviour;
        internal readonly TimeSpan TradeRulesUpdateInterval;
        internal readonly TimeSpan DefaultReceiveWindow;

        internal double CalculatedTimeOffset;
        internal bool TimeSynced;
        internal DateTime LastTimeSync;

        internal BinanceExchangeInfo? ExchangeInfo;
        internal DateTime? LastExchangeInfoUpdate;

        private readonly string _baseAddressFuturesUsdt;
        private readonly string _baseAddressFuturesCoin;

        #endregion

        #region Subclients
        /// <summary>
        /// General endpoints
        /// </summary>
        public IBinanceClientGeneral General { get; }

        /// <summary>
        /// Sub account endpoints
        /// </summary>
        public IBinanceClientSubAccount SubAccount { get; }

        /// <summary>
        /// (Isolated) Margin endpoints
        /// </summary>
        public IBinanceClientMargin Margin { get; }

        /// <summary>
        /// Spot endpoints
        /// </summary>
        public IBinanceClientSpot Spot { get; }

        /// <summary>
        /// Lending endpoints
        /// </summary>
        public IBinanceClientLending Lending { get; }
        
        /// <summary>
        /// Mining endpoints
        /// </summary>
        public IBinanceClientMining Mining { get; }
        
        /// <summary>
        /// Withdraw/deposit endpoints
        /// </summary>
        public IBinanceClientWithdrawDeposit WithdrawDeposit { get; }

        /// <summary>
        /// Brokerage endpoints
        /// </summary>
        public IBinanceClientBrokerage Brokerage { get; }

        /// <summary>
        /// USDT-M futures endpoints
        /// </summary>
        public IBinanceClientFuturesUsdt FuturesUsdt { get; }

        /// <summary>
        /// Coin-M futures endpoints
        /// </summary>
        public IBinanceClientFuturesCoin FuturesCoin { get; }

        /// <summary>
        /// Leveraged tokens endpoints
        /// </summary>
        public IBinanceClientLeveragedTokens Blvt { get; set; }

        /// <summary>
        /// Liquidity swap endpoints
        /// </summary>
        public IBinanceClientLiquidSwap BSwap { get; set; }
        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BinanceClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BinanceClient(BinanceClientOptions options) : base("Binance", options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            AutoTimestamp = options.AutoTimestamp;
            TradeRulesBehaviour = options.TradeRulesBehaviour;
            TradeRulesUpdateInterval = options.TradeRulesUpdateInterval;
            AutoTimestampRecalculationInterval = options.AutoTimestampRecalculationInterval;
            TimestampOffset = options.TimestampOffset;
            DefaultReceiveWindow = options.ReceiveWindow;
            _baseAddressFuturesUsdt = options.BaseAddressUsdtFutures;
            _baseAddressFuturesCoin = options.BaseAddressCoinFutures;

            arraySerialization = ArrayParametersSerialization.MultipleValues;
            postParametersPosition = PostParameters.InBody;
            requestBodyFormat = RequestBodyFormat.FormData;
            requestBodyEmptyContent = "";

            Spot = new BinanceClientSpot(log, this);
            Brokerage = new BinanceClientBrokerage(this);
            FuturesCoin = new BinanceClientFuturesCoin(log, this);
            FuturesUsdt = new BinanceClientFuturesUsdt(log, this);

            General = new BinanceClientGeneral(this);
            Margin = new BinanceClientMargin(this);
            Lending = new BinanceClientLending(this);
            Mining = new BinanceClientMining(this);
            
            SubAccount = new BinanceClientSubAccount(this);
            WithdrawDeposit = new BinanceClientWithdrawDeposit(this);
            Blvt = new BinanceClientLeveragedTokens(this);
            BSwap = new BinanceClientLiquidSwap(this);
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options"></param>
        public static void SetDefaultOptions(BinanceClientOptions options)
        {
            _defaultOptions = options;
        }

        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new BinanceAuthenticationProvider(new ApiCredentials(apiKey, apiSecret)));
        }
        
        #region helpers

        internal async Task<WebCallResult<BinancePlacedOrder>> PlaceOrderInternal(Uri uri, 
            string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteOrderQuantity = null,
            string? newClientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            decimal? icebergQty = null,
            SideEffectType? sideEffectType = null,
            bool? isIsolated = null,
            OrderResponseType? orderResponseType = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            symbol.ValidateBinanceSymbol();

            if(quoteOrderQuantity != null && type != OrderType.Market)
                throw new ArgumentException("quoteOrderQuantity is only valid for market orders");

            if ((quantity == null && quoteOrderQuantity == null) || (quantity != null && quoteOrderQuantity != null))
                throw new ArgumentException("1 of either should be specified, quantity or quoteOrderQuantity");


            var timestampResult = await CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<BinancePlacedOrder>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, stopPrice, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinancePlacedOrder>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;
            stopPrice = rulesCheck.StopPrice;

            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)) },
                { "type", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)) },
                { "timestamp", GetTimestamp() }
            };
            parameters.AddOptionalParameter("quantity", quantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("quoteOrderQty", quoteOrderQuantity?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", newClientOrderId);
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("timeInForce", timeInForce == null ? null : JsonConvert.SerializeObject(timeInForce, new TimeInForceConverter(false)));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("icebergQty", icebergQty?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("sideEffectType", sideEffectType == null ? null : JsonConvert.SerializeObject(sideEffectType, new SideEffectTypeConverter(false)));
            parameters.AddOptionalParameter("isIsolated", isIsolated);
            parameters.AddOptionalParameter("newOrderRespType", orderResponseType == null ? null : JsonConvert.SerializeObject(orderResponseType, new OrderResponseTypeConverter(false)));
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await SendRequest<BinancePlacedOrder>(uri, HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
                return new ServerError(error.ToString());

            if (error["msg"] == null && error["code"] == null)
                return new ServerError(error.ToString());

            if (error["msg"] != null && error["code"] == null)
                return new ServerError((string)error["msg"]!);

            var err = new ServerError((int)error["code"]!, (string)error["msg"]!);
            if (err.Code == -1021)
            {
                if (AutoTimestamp)
                    Spot.System.GetServerTime(true);
            }
            return err;
        }

        internal Error ParseErrorResponseInternal(JToken error) => ParseErrorResponse(error);

        internal Uri GetUrlSpot(string endpoint, string api, string? version = null)
        {
            var result = $"{BaseAddress}{api}/";

            if (!string.IsNullOrEmpty(version))
                result += $"v{version}/";

            result += endpoint;
            return new Uri(result);
        }

        internal Uri GetUrlUsdtFutures(string endpoint, string api, string? version = null)
        {
            var result = $"{_baseAddressFuturesUsdt}{api}/";

            if (!string.IsNullOrEmpty(version))
                result += $"v{version}/";

            result += endpoint;
            return new Uri(result);
        }

        internal Uri GetUrlCoinFutures(string endpoint, string api, string? version = null)
        {
            var result = $"{_baseAddressFuturesCoin}{api}/";

            if (!string.IsNullOrEmpty(version))
                result += $"v{version}/";

            result += endpoint;
            return new Uri(result);
        }

        internal static long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        internal string GetTimestamp()
        {
            var offset = AutoTimestamp ? CalculatedTimeOffset : 0;
            offset += TimestampOffset.TotalMilliseconds;
            return ToUnixTimestamp(DateTime.UtcNow.AddMilliseconds(offset)).ToString(CultureInfo.InvariantCulture);
        }

        internal async Task<WebCallResult<DateTime>> CheckAutoTimestamp(CancellationToken ct)
        {
            if (AutoTimestamp && (!TimeSynced || DateTime.UtcNow - LastTimeSync > AutoTimestampRecalculationInterval))
                return await Spot.System.GetServerTimeAsync(TimeSynced, ct).ConfigureAwait(false);

            return new WebCallResult<DateTime>(null, null, default, null);
        }

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? price, decimal? stopPrice, OrderType? type, CancellationToken ct)
        {
            var outputQuantity = quantity;
            var outputPrice = price;
            var outputStopPrice = stopPrice;

            if (TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);

            if (ExchangeInfo == null || LastExchangeInfoUpdate == null || (DateTime.UtcNow - LastExchangeInfoUpdate.Value).TotalMinutes > TradeRulesUpdateInterval.TotalMinutes)
                await Spot.System.GetExchangeInfoAsync(ct).ConfigureAwait(false);

            if (ExchangeInfo == null)
                return BinanceTradeRuleResult.CreateFailed("Unable to retrieve trading rules, validation failed");

            var symbolData = ExchangeInfo.Symbols.SingleOrDefault(s => string.Equals(s.Name, symbol, StringComparison.CurrentCultureIgnoreCase));
            if (symbolData == null)
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Symbol {symbol} not found");

            if (type != null)
            {
                if (!symbolData.OrderTypes.Contains(type.Value))
                    return BinanceTradeRuleResult.CreateFailed(
                        $"Trade rules check failed: {type} order type not allowed for {symbol}");
            }
            
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

                        log.Write(LogVerbosity.Info, $"Quantity clamped from {quantity} to {outputQuantity} based on lot size filter");
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

                        log.Write(LogVerbosity.Info, $"price clamped from {price} to {outputPrice} based on price filter");
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

                            log.Write(LogVerbosity.Info,
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

                        log.Write(LogVerbosity.Info, $"price floored from {beforePrice} to {outputPrice} based on price filter");
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

                            log.Write(LogVerbosity.Info,
                                $"Stop price floored from {beforeStopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }
            }

            if (symbolData.MinNotionalFilter == null || quantity == null || outputPrice == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);

            var currentQuantity = (outputQuantity.HasValue ? outputQuantity.Value : quantity.Value);
            var notional = currentQuantity * outputPrice.Value;
            if (notional < symbolData.MinNotionalFilter.MinNotional)
            {
                if(TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                    return BinanceTradeRuleResult.CreateFailed(
                        $"Trade rules check failed: MinNotional filter failed. Order size: {notional}, minimal order size: {symbolData.MinNotionalFilter.MinNotional}");

                if (symbolData.LotSizeFilter == null)
                    return BinanceTradeRuleResult.CreateFailed("Trade rules check failed: MinNotional filter failed. Unable to auto comply because LotSizeFilter not present");
                
                var minQuantity = symbolData.MinNotionalFilter.MinNotional / outputPrice.Value;
                var stepSize = symbolData.LotSizeFilter!.StepSize;
                outputQuantity = BinanceHelpers.Floor(minQuantity + (stepSize - (minQuantity % stepSize)));
                log.Write(LogVerbosity.Info, $"Quantity clamped from {currentQuantity} to {outputQuantity} based on min notional filter");
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            return base.SendRequest<T>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition);
        }

        #endregion
        
        async Task<WebCallResult<IEnumerable<ICommonSymbol>>> IExchangeClient.GetSymbolsAsync()
        {
            var exchangeInfo = await Spot.System.GetExchangeInfoAsync();
            return new WebCallResult<IEnumerable<ICommonSymbol>>(exchangeInfo.ResponseStatusCode,
                exchangeInfo.ResponseHeaders, exchangeInfo.Data?.Symbols, exchangeInfo.Error);
        }

        async Task<WebCallResult<ICommonTicker>> IExchangeClient.GetTickerAsync(string symbol)
        {
            var tickers = await Spot.Market.Get24HPriceAsync(symbol);
            return new WebCallResult<ICommonTicker>(tickers.ResponseStatusCode, tickers.ResponseHeaders, (Binance24HPrice)tickers.Data, tickers.Error);
        }

        async Task<WebCallResult<IEnumerable<ICommonTicker>>> IExchangeClient.GetTickersAsync()
        {
            var tickers = await Spot.Market.Get24HPricesAsync();
            return new WebCallResult<IEnumerable<ICommonTicker>>(tickers.ResponseStatusCode, tickers.ResponseHeaders, tickers.Data?.Select(d => (Binance24HPrice)d), tickers.Error);
        }

        async Task<WebCallResult<IEnumerable<ICommonKline>>> IExchangeClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null, int? limit = null)
        {
            var klines = await Spot.Market.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), startTime, endTime, limit);
            return WebCallResult<IEnumerable<ICommonKline>>.CreateFrom(klines);
        }

        async Task<WebCallResult<ICommonOrderBook>> IExchangeClient.GetOrderBookAsync(string symbol)
        {
            var orderBookResult = await Spot.Market.GetOrderBookAsync(symbol);
            return WebCallResult<ICommonOrderBook>.CreateFrom(orderBookResult);
        }

        async Task<WebCallResult<IEnumerable<ICommonRecentTrade>>> IExchangeClient.GetRecentTradesAsync(string symbol)
        {
            var tradesResult = await Spot.Market.GetSymbolTradesAsync(symbol);
            return WebCallResult<IEnumerable<ICommonRecentTrade>>.CreateFrom(tradesResult);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.PlaceOrderAsync(string symbol, IExchangeClient.OrderSide side, IExchangeClient.OrderType type, decimal quantity, decimal? price, string? accountId)
        {
            var result = await Spot.Order.PlaceOrderAsync(symbol, GetOrderSide(side), GetOrderType(type), quantity, price: price, timeInForce: TimeInForce.GoodTillCancel);
            return WebCallResult<ICommonOrderId>.CreateFrom(result);
        }

        async Task<WebCallResult<ICommonOrder>> IExchangeClient.GetOrderAsync(string orderId, string? symbol)
        {
            var result = await Spot.Order.GetOrderAsync(orderId);
            return WebCallResult<ICommonOrder>.CreateFrom(result);
        }

        async Task<WebCallResult<IEnumerable<ICommonTrade>>> IExchangeClient.GetTradesAsync(string orderId, string? symbol)
        {
            if(string.IsNullOrEmpty(symbol))
                return WebCallResult<IEnumerable<ICommonTrade>>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.GetTradesAsync)));
               
            var result = await Spot.Order.GetMyTradesAsync(symbol!);
            if(!result)
                return WebCallResult<IEnumerable<ICommonTrade>>.CreateErrorResult(result.ResponseStatusCode, result.ResponseHeaders, result.Error!);

            return new WebCallResult<IEnumerable<ICommonTrade>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data.Where(d => d.OrderId.ToString() == orderId), result.Error);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetOpenOrdersAsync(string? symbol)
        {
            var result = await Spot.Order.GetOpenOrdersAsync();
            return WebCallResult<IEnumerable<ICommonOrder>>.CreateFrom(result);
        }

        async Task<WebCallResult<IEnumerable<ICommonOrder>>> IExchangeClient.GetClosedOrdersAsync(string? symbol)
        {
            if (symbol == null)
                return WebCallResult<IEnumerable<ICommonOrder>>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.GetClosedOrdersAsync)));

            var result = await Spot.Order.GetAllOrdersAsync(symbol);
            return WebCallResult<IEnumerable<ICommonOrder>>.CreateFrom(result);
        }

        async Task<WebCallResult<ICommonOrderId>> IExchangeClient.CancelOrderAsync(string orderId, string? symbol)
        {
            if (symbol == null)
                return WebCallResult<ICommonOrderId>.CreateErrorResult(new ArgumentError(nameof(symbol) + " required for Binance " + nameof(IExchangeClient.CancelOrderAsync)));

            var result = await Spot.Order.CancelOrderAsync(symbol, orderId: long.Parse(orderId));
            return WebCallResult<ICommonOrderId>.CreateFrom(result);
        }

        async Task<WebCallResult<IEnumerable<ICommonBalance>>> IExchangeClient.GetBalancesAsync(string? accountId = null)
        {
            var result = await General.GetAccountInfoAsync();
            return new WebCallResult<IEnumerable<ICommonBalance>>(result.ResponseStatusCode, result.ResponseHeaders, result.Data?.Balances.Select(b => (ICommonBalance)b), result.Error);
        }

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            (baseAsset + quoteAsset).ToUpper(CultureInfo.InvariantCulture);

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

        private static OrderSide GetOrderSide(IExchangeClient.OrderSide side)
        {
            if (side == IExchangeClient.OrderSide.Sell) return OrderSide.Sell;
            if (side == IExchangeClient.OrderSide.Buy) return OrderSide.Buy;

            throw new ArgumentException("Unsupported order side for Binance order: " + side);
        }

        private static OrderType GetOrderType(IExchangeClient.OrderType type)
        {
            if (type == IExchangeClient.OrderType.Limit) return OrderType.Limit;
            if (type == IExchangeClient.OrderType.Market) return OrderType.Market;

            throw new ArgumentException("Unsupported order type for Binance order: " + type);
        }
    }
}
