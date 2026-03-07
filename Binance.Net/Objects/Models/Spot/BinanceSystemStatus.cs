using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// The status of Binance
    /// </summary>
    [SerializationModel]
    public record BinanceSystemStatus
    {
        /// <summary>
        /// ["<c>status</c>"] The system status.
        /// </summary>
        [JsonPropertyName("status")]
        public SystemStatus Status { get; set; }
        /// <summary>
        /// ["<c>msg</c>"] Additional info
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }
    }
}

