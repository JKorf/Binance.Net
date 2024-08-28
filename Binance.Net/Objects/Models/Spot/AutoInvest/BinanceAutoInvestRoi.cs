using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest ROI
    /// </summary>
    public record BinanceAutoInvestRoi
    {
        /// <summary>
        /// Date
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Simulate roi
        /// </summary>
        [JsonPropertyName("simulateRoi")]
        public decimal SimulateRoi { get; set; }
    }
}
