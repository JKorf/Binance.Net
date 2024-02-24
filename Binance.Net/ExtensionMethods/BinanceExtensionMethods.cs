using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Binance.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the Binance API
    /// </summary>
    public static class BinanceExtensionMethods
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
        /// Validate the string is a valid Binance symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param> 
        public static void ValidateBinanceSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");

            if (!Regex.IsMatch(symbolString, "^([A-Z|a-z|0-9]{5,})$"))
                throw new ArgumentException($"{symbolString} is not a valid Binance symbol. Should be [BaseAsset][QuoteAsset], e.g. BTCUSDT");
        }

    }
}
