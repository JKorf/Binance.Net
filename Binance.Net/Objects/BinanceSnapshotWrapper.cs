using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    internal class BinanceSnapshotWrapper<T>
    {
        public int Code { get; set; }
        [JsonProperty("msg")] 
        public string Message { get; set; } = string.Empty;
        [JsonProperty("snapshotVos")]
        public T SnapshotData { get; set; } = default!;
    }
}
