using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Binance.Net
{
    /// <summary>
    /// Helper methods for the Binance API
    /// </summary>
    public static class BinanceHelpers
    {
        /// <summary>
        /// Get the used weight from the response headers
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static int? UsedWeight(this IEnumerable<KeyValuePair<string, IEnumerable<string>>>? headers)
        {
            if (headers == null)
                return null;

            var headerValues = headers.SingleOrDefault(s => s.Key.StartsWith("X-MBX-USED-WEIGHT-", StringComparison.InvariantCultureIgnoreCase)).Value;
            if (headerValues != null && int.TryParse(headerValues.First(), out var value))
                return value;
            return null;
        }

        /// <summary>
        /// Get the used weight from the response headers
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static int? UsedOrderCount(this IEnumerable<KeyValuePair<string, IEnumerable<string>>>? headers)
        {
            if (headers == null)
                return null;

            var headerValues = headers.SingleOrDefault(s => s.Key.StartsWith("X-MBX-ORDER-COUNT-", StringComparison.InvariantCultureIgnoreCase)).Value;
            if (headerValues != null && int.TryParse(headerValues.First(), out var value))
                return value;
            return null;
        }

        /// <summary>
        /// Clamp a quantity between a min and max quantity and floor to the closest step
        /// </summary>
        /// <param name="minQuantity"></param>
        /// <param name="maxQuantity"></param>
        /// <param name="stepSize"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
        {
            quantity = Math.Min(maxQuantity, quantity);
            quantity = Math.Max(minQuantity, quantity);
            if (stepSize == 0)
                return quantity;
            quantity -= quantity % stepSize;
            quantity = Floor(quantity);
            return quantity;
        }

        /// <summary>
        /// Clamp a price between a min and max price
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal ClampPrice(decimal minPrice, decimal maxPrice, decimal price)
        {
            price = Math.Min(maxPrice, price);
            price = Math.Max(minPrice, price);
            return price;
        }

        /// <summary>
        /// Floor a price to the closest tick
        /// </summary>
        /// <param name="tickSize"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal FloorPrice(decimal tickSize, decimal price)
        {
            price -= price % tickSize;
            price = Floor(price);
            return price;
        }

        /// <summary>
        /// Floor
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static decimal Floor(decimal number)
        {
            return Math.Floor(number * 100000000) / 100000000;
        }

        /// <summary>
        /// Add the IBinanceClient and IBinanceSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultOptionsCallback">Set default options for the client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IBinanceSocketClient for the service collection. Defaults to Scoped.</param>
        /// <returns></returns>
        public static IServiceCollection AddBinance(
            this IServiceCollection services, 
            Action<BinanceClientOptions, BinanceSocketClientOptions>? defaultOptionsCallback = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            if (defaultOptionsCallback != null)
            {
                var options = new BinanceClientOptions();
                var socketOptions = new BinanceSocketClientOptions();
                defaultOptionsCallback?.Invoke(options, socketOptions);

                BinanceClient.SetDefaultOptions(options);
                BinanceSocketClient.SetDefaultOptions(socketOptions);
            }

            services.AddTransient<IBinanceClient, BinanceClient>();
            if (socketClientLifeTime == null)
                services.AddScoped<IBinanceSocketClient, BinanceSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IBinanceSocketClient), typeof(BinanceSocketClient), socketClientLifeTime.Value));
            return services;
        }

        /// <summary>
        /// Validate the string is a valid Binance symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static void ValidateBinanceSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");

            if(!Regex.IsMatch(symbolString, "^([A-Z|a-z|0-9]{5,})$"))
                throw new ArgumentException($"{symbolString} is not a valid Binance symbol. Should be [BaseAsset][QuoteAsset], e.g. BTCUSDT");
        }

        internal static BinanceTradeRuleResult ValidateTradeRules(Log log, TradeRulesBehaviour tradeRulesBehaviour, BinanceExchangeInfo exchangeInfo, string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, SpotOrderType? type)
        {
            var outputQuantity = quantity;
            var outputQuoteQuantity = quoteQuantity;
            var outputPrice = price;
            var outputStopPrice = stopPrice;

            var symbolData = exchangeInfo.Symbols.SingleOrDefault(s => string.Equals(s.Name, symbol, StringComparison.CurrentCultureIgnoreCase));
            if (symbolData == null)
                return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Symbol {symbol} not found");

            if (type != null)
            {
                if (!symbolData.OrderTypes.Contains(type.Value))
                {
                    return BinanceTradeRuleResult.CreateFailed(
                        $"Trade rules check failed: {type} order type not allowed for {symbol}");
                }
            }

            if (symbolData.LotSizeFilter != null || symbolData.MarketLotSizeFilter != null && type == SpotOrderType.Market)
            {
                var minQty = symbolData.LotSizeFilter?.MinQuantity;
                var maxQty = symbolData.LotSizeFilter?.MaxQuantity;
                var stepSize = symbolData.LotSizeFilter?.StepSize;
                if (type == SpotOrderType.Market && symbolData.MarketLotSizeFilter != null)
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
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");
                        }

                        log.Write(LogLevel.Information, $"Quantity clamped from {quantity} to {outputQuantity} based on lot size filter");
                    }
                }
            }

            if (symbolData.MinNotionalFilter != null && outputQuoteQuantity != null)
            {
                if (quoteQuantity < symbolData.MinNotionalFilter.MinNotional)
                {
                    if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                    {
                        return BinanceTradeRuleResult.CreateFailed(
                            $"Trade rules check failed: MinNotional filter failed. Order value: {quoteQuantity}, minimal order value: {symbolData.MinNotionalFilter.MinNotional}");
                    }

                    outputQuoteQuantity = symbolData.MinNotionalFilter.MinNotional;
                    log.Write(LogLevel.Information, $"QuoteQuantity adjusted from {quoteQuantity} to {outputQuoteQuantity} based on min notional filter");
                }
            }

            if (price == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, null, outputStopPrice);

            if (symbolData.PriceFilter != null)
            {
                if (symbolData.PriceFilter.MaxPrice != 0 && symbolData.PriceFilter.MinPrice != 0)
                {
                    outputPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice, symbolData.PriceFilter.MaxPrice, price.Value);
                    if (outputPrice != price)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter max/min failed. Original price: {price}, Closest allowed: {outputPrice}");

                        log.Write(LogLevel.Information, $"price clamped from {price} to {outputPrice} based on price filter");
                    }

                    if (stopPrice != null)
                    {
                        outputStopPrice = BinanceHelpers.ClampPrice(symbolData.PriceFilter.MinPrice,
                            symbolData.PriceFilter.MaxPrice, stopPrice.Value);
                        if (outputStopPrice != stopPrice)
                        {
                            if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            {
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter max/min failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");
                            }

                            log.Write(LogLevel.Information,
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
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter tick failed. Original price: {price}, Closest allowed: {outputPrice}");

                        log.Write(LogLevel.Information, $"price floored from {beforePrice} to {outputPrice} based on price filter");
                    }

                    if (stopPrice != null)
                    {
                        var beforeStopPrice = outputStopPrice;
                        outputStopPrice = BinanceHelpers.FloorPrice(symbolData.PriceFilter.TickSize, stopPrice.Value);
                        if (outputStopPrice != beforeStopPrice)
                        {
                            if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            {
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter tick failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");
                            }

                            log.Write(LogLevel.Information,
                                $"Stop price floored from {beforeStopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }
            }

            if (symbolData.MinNotionalFilter == null || quantity == null || outputPrice == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, outputPrice, outputStopPrice);

            var currentQuantity = outputQuantity ?? quantity.Value;
            var notional = currentQuantity * outputPrice.Value;
            if (notional < symbolData.MinNotionalFilter.MinNotional)
            {
                if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                {
                    return BinanceTradeRuleResult.CreateFailed(
                        $"Trade rules check failed: MinNotional filter failed. Order quantity: {notional}, minimal order quantity: {symbolData.MinNotionalFilter.MinNotional}");
                }

                if (symbolData.LotSizeFilter == null)
                    return BinanceTradeRuleResult.CreateFailed("Trade rules check failed: MinNotional filter failed. Unable to auto comply because LotSizeFilter not present");

                var minQuantity = symbolData.MinNotionalFilter.MinNotional / outputPrice.Value;
                var stepSize = symbolData.LotSizeFilter!.StepSize;
                outputQuantity = BinanceHelpers.Floor(minQuantity + (stepSize - minQuantity % stepSize));
                log.Write(LogLevel.Information, $"Quantity clamped from {currentQuantity} to {outputQuantity} based on min notional filter");
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, outputPrice, outputStopPrice);
        }
    }
}
