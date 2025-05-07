namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Extension to be able to deserialize an error response as well
    /// </summary>
    [SerializationModel]
    internal record BinanceFuturesMultipleOrderPlaceResult : BinanceFuturesOrder
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Extension to be able to deserialize an error response as well
    /// </summary>
    [SerializationModel]
    internal record BinanceUsdFuturesMultipleOrderPlaceResult : BinanceUsdFuturesOrder
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
    }
}
