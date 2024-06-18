namespace Binance.Net.Objects.Models.Spot
{
    internal record BinanceCheckTime
    {
        [JsonProperty("serverTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ServerTime { get; set; }
    }
}
