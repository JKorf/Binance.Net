using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    internal class BinanceSnapshotWrapper<T>
    {
        public int Code { get; set; }
        [JsonProperty("msg")] public string Message { get; set; } = "";
        [JsonProperty("snapshotVos")]
        public T SnapshotData { get; set; }
    }
}
