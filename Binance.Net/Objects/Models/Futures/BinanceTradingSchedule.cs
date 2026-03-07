using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// TradFi perps trading schedule
    /// </summary>
    public record BinanceTradingSchedule
    {
        /// <summary>
        /// ["<c>updateTime</c>"] The last update time.
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>marketSchedules</c>"] Schedules
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
        /// ["<c>EQUITY</c>"] Equity sessions
        /// </summary>
        [JsonPropertyName("EQUITY")]
        public BinanceSessionSchedules Equity { get; set; } = null!;
        /// <summary>
        /// ["<c>COMMODITY</c>"] Commodity sessions
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
        /// ["<c>sessions</c>"] Sessions
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
        /// ["<c>startTime</c>"] The session start time.
        /// </summary>
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// ["<c>endTime</c>"] The session end time.
        /// </summary>
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Session type
        /// </summary>
        [JsonPropertyName("type")]
        public SessionType Type { get; set; }
    }
}

