namespace Binance.Net.Objects.Models.Spot
{
    internal record BinanceCheckTime
    {
        [JsonPropertyName("serverTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ServerTime { get; set; }
    }
}
