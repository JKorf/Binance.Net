namespace Binance.Net.Objects.Models.Spot
{
    [SerializationModel]
    internal record BinanceCheckTime
    {
        [JsonPropertyName("serverTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ServerTime { get; set; }
    }
}
