namespace Binance.Net.Objects.Internal
{
    internal class BinanceSnapshotWrapper<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")] 
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("snapshotVos")]
        public T SnapshotData { get; set; } = default!;
    }
}
