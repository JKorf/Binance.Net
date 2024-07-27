namespace Binance.Net.Objects.Models.Spot
{
    internal record BinanceListenKey
    {
        [JsonPropertyName("listenKey")]
        public string ListenKey { get; set; } = string.Empty;
    }
}
