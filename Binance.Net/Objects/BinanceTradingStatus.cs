using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    internal class BinanceTradingStatusWrapper
    {
        public bool Success { get; set; }
        [JsonProperty("msg")]
        [JsonOptionalProperty]
        public string Message { get; set; }
        public BinanceTradingStatus Status { get; set; }
    }

    /// <summary>
    /// Trade status
    /// </summary>
    public class BinanceTradingStatus
    {
        /// <summary>
        /// Is locked
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// Planned time of recovery
        /// </summary>
        public int PlannedRecoverTime { get; set; }

        /// <summary>
        /// Conditions
        /// </summary>
        [JsonProperty("triggerCondition")]
        public Dictionary<string, int> TriggerConditions { get; set; }

        /// <summary>
        /// Dictionary of indicator lists for symbols
        /// </summary>
        public Dictionary<string, List<BinanceIndicator>> Indicators { get; set; }
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Indicator info
    /// </summary>
    public class BinanceIndicator
    {
        /// <summary>
        /// Indicator name
        /// </summary>
        [JsonProperty("i")]
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [JsonProperty("c")]
        public int Count { get; set; }
        /// <summary>
        /// Current value
        /// </summary>
        [JsonProperty("v")]
        public decimal CurrentValue { get; set; }
        /// <summary>
        /// Trigger value
        /// </summary>
        [JsonProperty("t")]
        public decimal TriggerValue { get; set; }
    }
}
