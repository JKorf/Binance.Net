using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.Staking
{
    /// <summary>
    /// SOL rewards history
    /// </summary>
    public record BinanceBnsolRewardHistory
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("token")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// BN SOL holding
        /// </summary>
        [JsonPropertyName("bnsolHolding")]
        public decimal BnSolHolding { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
    }
}
