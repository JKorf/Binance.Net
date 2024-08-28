using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Edit result
    /// </summary>
    public record BinanceAutoInvestEditResult
    {
        /// <summary>
        /// Plan id
        /// </summary>
        [JsonPropertyName("planId")]
        public long PlanId { get; set; }
        /// <summary>
        /// Next execution date time
        /// </summary>
        [JsonPropertyName("nextExecutionDateTime")]
        public DateTime? NextExecutionTime { get; set; }
    }


}
