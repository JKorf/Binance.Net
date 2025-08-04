namespace Binance.Net.Objects.Models.Spot
{
    [SerializationModel]
    internal record BinanceListenKey
    {
        [JsonPropertyName("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
