using Binance.Net.Objects.Models.Spot;

namespace Binance.Net.Objects.Models.Futures
{
    /// <summary>
    /// Book price
    /// </summary>
    [SerializationModel]
    public record BinanceFuturesBookPrice : BinanceBookPrice
    {
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }
}

