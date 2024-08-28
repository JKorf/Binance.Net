using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Redemption result
    /// </summary>
    public record BinanceAutoInvestRedemptionResult
    {
        /// <summary>
        /// Redemption id
        /// </summary>
        [JsonPropertyName("redemptionId")]
        public long RedemptionId { get; set; }
    }

}
