namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest ROI
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRoi
    {
        /// <summary>
        /// Date
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Simulate roi
        /// </summary>
        [JsonPropertyName("simulateRoi")]
        public decimal SimulateRoi { get; set; }
    }
}
