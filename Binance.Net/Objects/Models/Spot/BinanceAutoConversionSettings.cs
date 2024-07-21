﻿namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// User auto conversion settings
    /// </summary>
    public record BinanceAutoConversionSettings
    {
        /// <summary>
        /// Is auto convert enabled
        /// </summary>
        [JsonPropertyName("convertEnabled")]
        public bool ConvertEnabled { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("coins")]
        public IEnumerable<string> Assets { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Exchange rates
        /// </summary>
        [JsonPropertyName("exchangeRates")]
        public Dictionary<string, decimal> ExchangeRates { get; set; } = new Dictionary<string, decimal>();
    }
}
