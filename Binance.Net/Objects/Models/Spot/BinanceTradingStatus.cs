using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade status
    /// </summary>
    [SerializationModel]
    public record BinanceTradingStatus
    {
        /// <summary>
        /// ["<c>isLocked</c>"] Whether trading is locked.
        /// </summary>
        [JsonPropertyName("isLocked")]
        public bool IsLocked { get; set; }
        /// <summary>
        /// ["<c>plannedRecoverTime</c>"] Planned time of recovery
        /// </summary>
        [JsonPropertyName("plannedRecoverTime")]
        public int PlannedRecoverTime { get; set; }

        /// <summary>
        /// ["<c>triggerCondition</c>"] Trigger conditions.
        /// </summary>
        [JsonPropertyName("triggerCondition")]
        public Dictionary<string, int> TriggerConditions { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// ["<c>indicators</c>"] Dictionary of indicator lists for symbols
        /// </summary>
        [JsonPropertyName("indicators")]
        public Dictionary<string, BinanceIndicator[]> Indicators { get; set; } = new Dictionary<string, BinanceIndicator[]>();
        /// <summary>
        /// ["<c>updateTime</c>"] Last update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// Indicator info
    /// </summary>
    public record BinanceIndicator
    {
        /// <summary>
        /// ["<c>i</c>"] Indicator name
        /// </summary>
        [JsonPropertyName("i")]
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// ["<c>c</c>"] Count
        /// </summary>
        [JsonPropertyName("c")]
        public int Count { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Current value
        /// </summary>
        [JsonPropertyName("v")]
        public decimal CurrentValue { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Trigger value
        /// </summary>
        [JsonPropertyName("t")]
        public decimal TriggerValue { get; set; }
    }
}

