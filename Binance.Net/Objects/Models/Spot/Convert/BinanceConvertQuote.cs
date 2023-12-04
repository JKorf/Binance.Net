using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Convert Quote
    /// </summary>
    public class BinanceConvertQuote
    {
        /// <summary>
        /// Quote id
        /// </summary>
        public string? QuoteId { get; set; }
        /// <summary>
        /// Price ratio
        /// </summary>
        public decimal Ratio { get; set; }
        /// <summary>
        /// Inverse price ratio
        /// </summary>
        public decimal InverseRatio { get; set; }
        /// <summary>
        /// Valid Timestamp
        /// </summary>
        public long ValidTimestamp { get; set; }
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("toAmount")]
        public decimal BaseQuantity { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("fromAmount")]
        public decimal QuoteQuantity { get; set; }
       
    }
}
