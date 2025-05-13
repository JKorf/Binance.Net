using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Index price constituents info
    /// </summary>
    public record BinanceConstituents
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Constituents
        /// </summary>
        [JsonPropertyName("constituents")]
        public BinanceConstituent[] Constituents { get; set; } = [];
    }

    /// <summary>
    /// Constituent info
    /// </summary>
    public record BinanceConstituent
    {
        /// <summary>
        /// Exchange
        /// </summary>
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal? Weight { get; set; }
    }
}
