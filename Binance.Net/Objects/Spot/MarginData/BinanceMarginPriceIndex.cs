using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Price index for a symbol
    /// </summary>
    public class BinanceMarginPriceIndex
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Time of calculation
        /// </summary>
        [JsonProperty("calcTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CalculationTime { get; set; }
    }
}
