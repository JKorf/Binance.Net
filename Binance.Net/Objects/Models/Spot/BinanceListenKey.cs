namespace Binance.Net.Objects.Models.Spot
{
    [SerializationModel]
    internal record BinanceListenKey
    {
        /// <summary>
        /// The listen key.
        /// </summary>
        [JsonPropertyName("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
