﻿namespace Binance.Net.Objects.Models.Spot
{
    internal class BinanceCheckTime
    {
        [JsonProperty("serverTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ServerTime { get; set; }
    }
}
