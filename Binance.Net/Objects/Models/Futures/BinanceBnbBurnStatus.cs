using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// BNB burn for fee reduction status
    /// </summary>
    public record BinanceBnbBurnStatus
    {
        /// <summary>
        /// Fee burn status
        /// </summary>
        [JsonPropertyName("feeBurn")]
        public bool FeeBurn { get; set; }
    }
}
