namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo order result
    /// </summary>
    public record BinanceAlgoOrderResult : BinanceResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        [JsonConverterCtor(typeof(ReplaceConverter),
            $"{BinanceExchange.ClientOrderIdPrefixSpot}->",
            $"{BinanceExchange.ClientOrderIdPrefixFutures}->")]
        public string ClientAlgoId { get; set; } = string.Empty;
        /// <summary>
        /// Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
