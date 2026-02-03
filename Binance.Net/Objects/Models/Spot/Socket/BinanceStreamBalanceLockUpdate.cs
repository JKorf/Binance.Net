namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Update when asset lock is applied/removed
    /// </summary>
    [SerializationModel]
    public record BinanceStreamBalanceLockUpdate : BinanceStreamEvent
    {
        /// <summary>
        /// The asset which changed
        /// </summary>
        [JsonPropertyName("a")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The balance delta
        /// </summary>
        [JsonPropertyName("d")]
        public decimal BalanceDelta { get; set; }
        /// <summary>
        /// API key this update was for
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// The time the deposit/withdrawal was cleared
        /// </summary>
        [JsonPropertyName("T"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ClearTime { get; set; }
    }
}
