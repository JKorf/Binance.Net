using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// MiniTick info
    /// </summary>
    [SerializationModel]
    public abstract record BinanceStreamMiniTickBase : BinanceStreamEvent, IBinanceMiniTick
    {
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Todays open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Todays high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Todays low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Total traded volume
        /// </summary>
        public abstract decimal Volume { get; set; }

        /// <summary>
        /// Total traded quote volume
        /// </summary>
        public abstract decimal QuoteVolume { get; set; }
    }

    /// <summary>
    /// Stream mini tick
    /// </summary>
    [SerializationModel]
    public record BinanceStreamMiniTick : BinanceStreamMiniTickBase
    {
        /// <inheritdoc/>
        [JsonPropertyName("v")]
        public override decimal Volume { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("q")]
        public override decimal QuoteVolume { get; set; }
    }

    /// <summary>
    /// Stream mini tick
    /// </summary>
    [SerializationModel]
    public record BinanceStreamCoinMiniTick : BinanceStreamMiniTickBase
    {
        /// <inheritdoc/>
        [JsonPropertyName("q")]
        public override decimal Volume { get; set; }
        /// <inheritdoc/>
        [JsonPropertyName("v")]
        public override decimal QuoteVolume { get; set; }

        /// <summary>
        /// The pair
        /// </summary>
        [JsonPropertyName("ps")]
        public string Pair { get; set; } = string.Empty;
    }
}
