namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Result of placing a withdrawal
    /// </summary>
    public record BinanceWithdrawalPlaced
    {
        /// <summary>
        /// The id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
