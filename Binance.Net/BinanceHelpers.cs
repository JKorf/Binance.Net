using Binance.Net.Enums;
using Binance.Net.Objects.Internal;
using Binance.Net.Objects.Models.Spot;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Binance.Net
{
    /// <summary>
    /// Helper methods for the Binance API
    /// </summary>
    public static class BinanceHelpers
    {
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

        internal static BinanceTradeRuleResult ValidateTradeRules(ILogger logger, TradeRulesBehaviour tradeRulesBehaviour, BinanceExchangeInfo exchangeInfo, string symbol, decimal? quantity, decimal? quoteQuantity, decimal? price, decimal? stopPrice, SpotOrderType? type)
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
                    outputQuantity = ClampQuantity(minQty.Value, maxQty!.Value, stepSize!.Value, quantity.Value);
                    if (outputQuantity != quantity.Value)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                        {
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: LotSize filter failed. Original quantity: {quantity}, Closest allowed: {outputQuantity}");
                        }

                        logger.Log(LogLevel.Information, $"Quantity clamped from {quantity} to {outputQuantity} based on lot size filter");
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
                    logger.Log(LogLevel.Information, $"QuoteQuantity adjusted from {quoteQuantity} to {outputQuoteQuantity} based on min notional filter");
                }
            }

            if (price == null)
                return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, null, outputStopPrice);

            if (symbolData.PriceFilter != null)
            {
                if (symbolData.PriceFilter.MaxPrice != 0 && symbolData.PriceFilter.MinPrice != 0)
                {
                    outputPrice = ClampPrice(symbolData.PriceFilter.MinPrice, symbolData.PriceFilter.MaxPrice, price.Value);
                    if (outputPrice != price)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter max/min failed. Original price: {price}, Closest allowed: {outputPrice}");

                        logger.Log(LogLevel.Information, $"price clamped from {price} to {outputPrice} based on price filter");
                    }

                    if (stopPrice != null)
                    {
                        outputStopPrice = ClampPrice(symbolData.PriceFilter.MinPrice,
                            symbolData.PriceFilter.MaxPrice, stopPrice.Value);
                        if (outputStopPrice != stopPrice)
                        {
                            if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            {
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter max/min failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");
                            }

                            logger.Log(LogLevel.Information,
                                $"Stop price clamped from {stopPrice} to {outputStopPrice} based on price filter");
                        }
                    }
                }

                if (symbolData.PriceFilter.TickSize != 0)
                {
                    var beforePrice = outputPrice;
                    outputPrice = FloorPrice(symbolData.PriceFilter.TickSize, price.Value);
                    if (outputPrice != beforePrice)
                    {
                        if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            return BinanceTradeRuleResult.CreateFailed($"Trade rules check failed: Price filter tick failed. Original price: {price}, Closest allowed: {outputPrice}");

                        logger.Log(LogLevel.Information, $"price floored from {beforePrice} to {outputPrice} based on price filter");
                    }

                    if (stopPrice != null)
                    {
                        var beforeStopPrice = outputStopPrice;
                        outputStopPrice = FloorPrice(symbolData.PriceFilter.TickSize, stopPrice.Value);
                        if (outputStopPrice != beforeStopPrice)
                        {
                            if (tradeRulesBehaviour == TradeRulesBehaviour.ThrowError)
                            {
                                return BinanceTradeRuleResult.CreateFailed(
                                    $"Trade rules check failed: Stop price filter tick failed. Original stop price: {stopPrice}, Closest allowed: {outputStopPrice}");
                            }

                            logger.Log(LogLevel.Information,
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
                outputQuantity = Floor(minQuantity + (stepSize - minQuantity % stepSize));
                logger.Log(LogLevel.Information, $"Quantity clamped from {currentQuantity} to {outputQuantity} based on min notional filter");
            }

            return BinanceTradeRuleResult.CreatePassed(outputQuantity, outputQuoteQuantity, outputPrice, outputStopPrice);
        }
    }
}
