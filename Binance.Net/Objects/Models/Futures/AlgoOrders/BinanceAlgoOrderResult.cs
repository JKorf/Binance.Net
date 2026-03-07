using Binance.Net.Converters;

namespace Binance.Net.Objects.Models.Futures.AlgoOrders
{
    /// <summary>
    /// Algo order result
    /// </summary>
    [SerializationModel]
    public record BinanceAlgoOrderResult : BinanceResult
    {
        /// <summary>
        /// ["<c>clientAlgoId</c>"] Order id
        /// </summary>
        [JsonPropertyName("clientAlgoId")]
        [JsonConverter(typeof(ClientOrderIdReplaceConverter))]
        public string ClientAlgoId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>success</c>"] Successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}

