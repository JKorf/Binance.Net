using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Extension to be able to deserialize an error response as well
    /// </summary>
    internal class BinanceFuturesMultipleOrderCancelResult : BinanceFuturesCancelOrder
    {
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
