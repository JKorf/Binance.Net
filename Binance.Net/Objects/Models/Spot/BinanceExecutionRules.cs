using Binance.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Net.Objects.Models.Spot
{
    internal record BinanceExecutionRulesWrapper
    {
        /// <summary>
        /// Symbol rules
        /// </summary>
        [JsonPropertyName("symbolRules")]
        public BinanceExecutionRules[] SymbolRules { get; set; } = [];
    }

    /// <summary>
    /// Execution rules
    /// </summary>
    public record BinanceExecutionRules
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Rules
        /// </summary>
        [JsonPropertyName("rules")]
        public BinanceExecutionRule[] Rules { get; set; } = [];
    }

    /// <summary>
    /// Execution rule
    /// </summary>
    public record BinanceExecutionRule
    {
        /// <summary>
        /// Rule type
        /// </summary>
        [JsonPropertyName("ruleType")]
        public RuleType RuleType { get; set; }
        /// <summary>
        /// Bid limit multiplier up
        /// </summary>
        [JsonPropertyName("bidLimitMultUp")]
        public decimal BidLimitMultiplierUp { get; set; }
        /// <summary>
        /// Bid limit multiplier down
        /// </summary>
        [JsonPropertyName("bidLimitMultDown")]
        public decimal BidLimitMultiplierDown { get; set; }
        /// <summary>
        /// Ask limit multiplier up
        /// </summary>
        [JsonPropertyName("askLimitMultUp")]
        public decimal AskLimitMultiplierUp { get; set; }
        /// <summary>
        /// Ask limit multiplier down
        /// </summary>
        [JsonPropertyName("askLimitMultDown")]
        public decimal AskLimitMultiplierDown { get; set; }
    }
}
