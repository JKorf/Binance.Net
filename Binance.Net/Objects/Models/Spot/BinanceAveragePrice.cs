namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Current average price details for a symbol.
    /// </summary>
    public class BinanceAveragePrice
    {
        /// <summary>
        /// Duration in minutes
        /// </summary>
        [JsonProperty("mins")]
        public int Minutes { get; set; }
        /// <summary>
        /// The average price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
