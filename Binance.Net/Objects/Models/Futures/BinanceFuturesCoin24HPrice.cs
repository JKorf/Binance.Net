namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Price statistics of the last 24 hours
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesCoin24HPrice : Binance24HPriceBase
    {
        /// <summary>
        /// The pair the price is for
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;

        /// <inheritdoc />
        [JsonPropertyName("baseVolume")]
        public override decimal Volume { get; set; }
        /// <inheritdoc />
        [JsonPropertyName("volume")]
        public override decimal QuoteVolume { get; set; }
    }
}
