using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Trading rules status
    /// </summary>
    public class BinanceFuturesTradingStatus
    {
        /// <summary>
        /// The trading rule indicators
        /// </summary>
        public Dictionary<string, BinanceFuturesTradingStatusIndicator> Indicators { get; set; } = new Dictionary<string, BinanceFuturesTradingStatusIndicator>();
        /// <summary>
        /// Last update time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Indicator details
    /// </summary>
    public class BinanceFuturesTradingStatusIndicator
    {
        /// <summary>
        /// Locked
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// Planned time when indicator is unlocked
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime? PlannedRecoveryTime { get; set; }
        /// <summary>
        /// The indicator name
        /// </summary>
        public string Indicator { get; set; } = "";
        /// <summary>
        /// Current value of the indicator
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// The trigger value of the indicator
        /// </summary>
        public decimal TriggerValue { get; set; }
    }
}
