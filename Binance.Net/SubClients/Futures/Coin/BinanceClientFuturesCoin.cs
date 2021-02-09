using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.Futures;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Futures.Coin
{
    /// <summary>
    /// Coin-M futures endpoints
    /// </summary>
    public class BinanceClientFuturesCoin: BinanceClientFutures, IBinanceClientFuturesCoin
    {
        internal BinanceFuturesCoinExchangeInfo? ExchangeInfo;
        private const string positionInformationEndpoint = "positionRisk";
        /// <summary>
        /// Api path
        /// </summary>
        protected override string Api { get; } = "dapi";

        /// <summary>
        /// Futures market endpoints
        /// </summary>
        public IBinanceClientFuturesCoinMarket Market { get; protected set; }
        /// <summary>
        /// Futures order endpoints
        /// </summary>
        public IBinanceClientFuturesCoinOrder Order { get; protected set; }
        /// <summary>
        /// Futures account endpoints
        /// </summary>
        public IBinanceClientFuturesCoinAccount Account { get; protected set; }
        /// <summary>
        /// Coin futures system endpoints
        /// </summary>
        public IBinanceClientFuturesCoinSystem System { get; protected set; }
        /// <summary>
        /// Coin futures user stream endpoints
        /// </summary>
        public override IBinanceClientUserStream UserStream { get; protected set; }

        internal BinanceClientFuturesCoin(Log log, BinanceClient baseClient) : base(log, baseClient)
        {
            System = new BinanceClientFuturesCoinSystem(log, baseClient, this);
            Account = new BinanceClientFuturesCoinAccount(baseClient, this);
            Market = new BinanceClientFuturesCoinMarket(BaseClient, this);
            Order = new BinanceClientFuturesCoinOrder(log, BaseClient, this);
            UserStream = new BinanceClientFuturesCoinUserStream(baseClient, this);
        }

        #region Position Information

        /// <summary>
        /// Gets account position information
        /// </summary>
        /// <param name="marginAsset">Filter by margin asset</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        public WebCallResult<IEnumerable<BinancePositionDetailsCoin>> GetPositionInformation(string? marginAsset = null, string? pair = null, long? receiveWindow = null, CancellationToken ct = default) => GetPositionInformationAsync(marginAsset, pair, receiveWindow, ct).Result;

        /// <summary>
        /// Gets account position information
        /// </summary>
        /// <param name="marginAsset">Filter by margin asset</param>
        /// <param name="pair">Filter by pair</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of Positions</returns>
        public async Task<WebCallResult<IEnumerable<BinancePositionDetailsCoin>>> GetPositionInformationAsync(string? marginAsset = null, string? pair = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var timestampResult = await BaseClient.CheckAutoTimestamp(ct).ConfigureAwait(false);
            if (!timestampResult)
                return new WebCallResult<IEnumerable<BinancePositionDetailsCoin>>(timestampResult.ResponseStatusCode, timestampResult.ResponseHeaders, null, timestampResult.Error);

            var parameters = new Dictionary<string, object>
            {
                { "timestamp", BaseClient.GetTimestamp() }
            };

            parameters.AddOptionalParameter("marginAsset", marginAsset);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? BaseClient.DefaultReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            return await BaseClient.SendRequestInternal<IEnumerable<BinancePositionDetailsCoin>>(GetUrl(positionInformationEndpoint, Api, "1"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        internal override async Task<BinanceTradeRuleResult> CheckTradeRules(string symbol, decimal? quantity, decimal? price, decimal? stopPrice, OrderType type, CancellationToken ct)
        {
            var outputQuantity = quantity;
            var outputPrice = price;
            var outputStopPrice = stopPrice;

            if (BaseClient.TradeRulesBehaviour == TradeRulesBehaviour.None)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);

            if (ExchangeInfo == null || LastExchangeInfoUpdate == null || (DateTime.UtcNow - LastExchangeInfoUpdate.Value).TotalMinutes > BaseClient.TradeRulesUpdateInterval.TotalMinutes)
                await System.GetExchangeInfoAsync(ct).ConfigureAwait(false);

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
                        if (BaseClient.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");
                        }

                        _log.Write(LogVerbosity.Info, $"Quantity clamped from {quantity} to {outputQuantity}");
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
                        if (BaseClient.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter max/min failed. Original price: {price}, Closest allowed: {outputPrice}");

                        _log.Write(LogVerbosity.Info, $"price clamped from {price} to {outputPrice}");
                    }

                    if (stopPrice != null)
                    {
                        outputStopPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice,
                            symbolData.PriceFilter.MaxPrice, stopPrice.Value);
                        if (outputStopPrice != stopPrice)
                        {
                            if (BaseClient.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter max/min failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");

                            _log.Write(LogVerbosity.Info,
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
                        if (BaseClient.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter tick failed. Original price: {price}, Closest allowed: {outputPrice}");

                        _log.Write(LogVerbosity.Info, $"price rounded from {beforePrice} to {outputPrice}");
                    }

                    if (stopPrice != null)
                    {
                        var beforeStopPrice = outputStopPrice;
                        outputStopPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, stopPrice.Value);
                        if (outputStopPrice != beforeStopPrice)
                        {
                            if (BaseClient.TradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter tick failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");

                            _log.Write(LogVerbosity.Info,
                                $"Stop price floored from {beforeStopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputPrice, outputStopPrice);
        }

        internal override Uri GetUrl(string endpoint, string api, string? version = null)
            => BaseClient.GetUrlCoinFutures(endpoint, api, version);
    }
}
