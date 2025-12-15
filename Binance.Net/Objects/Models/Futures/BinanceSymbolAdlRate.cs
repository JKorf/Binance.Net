using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Symbol ADL risk rate
    /// </summary>
    public record BinanceSymbolAdlRate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ADL risk
        /// </summary>
        [JsonPropertyName("adlRisk")]
        public AdlRisk AdlRisk { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
