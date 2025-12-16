using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// TradFi perps trading schedule
    /// </summary>
    public record BinanceTradingSchedule
    {
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Schedules
        /// </summary>
        [JsonPropertyName("marketSchedules")]
        public BinanceMarketSchedule Schedules { get; set; } = null!;

    }

    /// <summary>
    /// Schedule info
    /// </summary>
    public record BinanceMarketSchedule
    {
        /// <summary>
        /// Equity sessions
        /// </summary>
        [JsonPropertyName("EQUITY")]
        public BinanceSessionSchedules Equity { get; set; } = null!;
        /// <summary>
        /// Commodity sessions
        /// </summary>
        [JsonPropertyName("COMMODITY")]
        public BinanceSessionSchedules Commodity { get; set; } = null!;
    }

    /// <summary>
    /// Sessions schedule
    /// </summary>
    public record BinanceSessionSchedules
    {
        /// <summary>
        /// Sessions
        /// </summary>
        [JsonPropertyName("sessions")]
        public BinanceSchedule[] Sessions { get; set; } = [];
    }

    /// <summary>
    /// Schedule
    /// </summary>
    public record BinanceSchedule
    {
        /// <summary>
        /// Start time
        /// </summary>
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Session type
        /// </summary>
        [JsonPropertyName("type")]
        public SessionType Type { get; set; }
    }
}
