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

namespace Binance.Net
{
    /// <summary>
    /// Client providing access to the Binance REST Api
    /// </summary>
    public class BinanceClient : RestClient, IBinanceClient
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
        public BinanceClient(BinanceClientOptions options) : base(options, options.ApiCredentials == null ? null : new BinanceAuthenticationProvider(options.ApiCredentials))
        {
            AutoTimestamp = options.AutoTimestamp;
            TradeRulesBehaviour = options.TradeRulesBehaviour;
            TradeRulesUpdateInterval = options.TradeRulesUpdateInterval;
            AutoTimestampRecalculationInterval = options.AutoTimestampRecalculationInterval;
            TimestampOffset = options.TimestampOffset;
            DefaultReceiveWindow = options.ReceiveWindow;
            _baseAddressFuturesUsdt = options.FuturesUsdtBaseAddress;
            _baseAddressFuturesCoin = options.FuturesCoinBaseAddress;

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

            var rulesCheck = await CheckTradeRules(symbol, quantity, price, type, ct).ConfigureAwait(false);
            if (!rulesCheck.Passed)
            {
                log.Write(LogVerbosity.Warning, rulesCheck.ErrorMessage!);
                return new WebCallResult<BinancePlacedOrder>(null, null, null, new ArgumentError(rulesCheck.ErrorMessage!));
            }

            quantity = rulesCheck.Quantity;
            price = rulesCheck.Price;

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
                return new ServerError((string)error["msg"]);

            var err = new ServerError((int)error["code"], (string)error["msg"]);
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
            var result = $"{BaseAddress}/{api}/";

            if (!string.IsNullOrEmpty(version))
                result += $"v{version}/";

            result += endpoint;
            return new Uri(result);
        }

        internal Uri GetUrlUsdtFutures(string endpoint, string api, string? version = null)
        {
            var result = $"{_baseAddressFuturesUsdt}/{api}/";

            if (!string.IsNullOrEmpty(version))
                result += $"v{version}/";

            result += endpoint;
            return new Uri(result);
        }

        internal Uri GetUrlCoinFutures(string endpoint, string api, string? version = null)
        {
            var result = $"{_baseAddressFuturesCoin}/{api}/";

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

        internal async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? price, OrderType? type, CancellationToken ct)
        {
            var outputQuantity = quantity;
            var outputPrice = price;

            if (TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);

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
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, null);

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
                }
            }

            if (symbolData.MinNotionalFilter == null || quantity == null || outputPrice == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);

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

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice);
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, bool checkResult = true, PostParameters? postPosition = null, ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            return base.SendRequest<T>(uri, method, cancellationToken, parameters, signed, checkResult, postPosition);
        }

        #endregion
    }
}
