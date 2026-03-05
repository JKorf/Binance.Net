namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest ROI
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRoi
    {
        /// <summary>
        /// The ROI date.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Simulated return on investment.
        /// </summary>
        [JsonPropertyName("simulateRoi")]
        public decimal SimulateRoi { get; set; }
    }
}
