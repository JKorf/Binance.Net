namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Extension to be able to deserialize an error response as well
    /// </summary>
    internal class BinanceFuturesMultipleOrderPlaceResult: BinanceFuturesOrder
    {
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Extension to be able to deserialize an error response as well
    /// </summary>
    internal class BinanceUsdFuturesMultipleOrderPlaceResult : BinanceUsdFuturesOrder
    {
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
