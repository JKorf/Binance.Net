namespace Binance.Net.Objects.Models.Spot.AutoInvest
{
    /// <summary>
    /// Auto invest ROI
    /// </summary>
    [SerializationModel]
    public record BinanceAutoInvestRoi
    {
        /// <summary>
        /// ["<c>date</c>"] The ROI date.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// ["<c>simulateRoi</c>"] Simulated return on investment.
        /// </summary>
        [JsonPropertyName("simulateRoi")]
        public decimal SimulateRoi { get; set; }
    }
}

