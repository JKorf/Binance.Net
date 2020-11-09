using System;
using System.Collections.Generic;
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
        /// Validate the string is a valid Binance symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static void ValidateBinanceSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");

            if(!Regex.IsMatch(symbolString, "^([A-Z|a-z|0-9]{5,})$"))
                throw new ArgumentException($"{symbolString} is not a valid Binance symbol. Should be [BaseCurrency][QuoteCurrency], e.g. BTCUSDT");
        }
    }
}
