using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Insurance fund balance info
    /// </summary>
    public record BinanceInsuranceFundBalance
    {
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = [];
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public BinanceInsuranceFundAsset[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Insurance fund asset balance
    /// </summary>
    public record BinanceInsuranceFundAsset
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
