using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// User auto conversion settings
    /// </summary>
    public class BinanceAutoConversionSettings
    {
        /// <summary>
        /// Is auto convert enabled
        /// </summary>
        public bool ConvertEnabled { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonProperty("coins")]
        public IEnumerable<string> Assets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Exchange rates
        /// </summary>
        public Dictionary<string, decimal> ExchangeRates { get; set; } = new Dictionary<string, decimal>();
    }
}
