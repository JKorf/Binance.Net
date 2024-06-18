using Binance.Net.Interfaces;

namespace Binance.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// MiniTick info
    /// </summary>
    public abstract record BinanceStreamMiniTickBase : BinanceStreamEvent, IBinanceMiniTick
    {
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The current day close price. This is the latest price for this symbol.
        /// </summary>
        [JsonProperty("c")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Todays open price
        /// </summary>
        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Todays high price
        /// </summary>
        [JsonProperty("h")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Todays low price
        /// </summary>
        [JsonProperty("l")]
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
    public record BinanceStreamMiniTick: BinanceStreamMiniTickBase
    {
        /// <inheritdoc/>
        [JsonProperty("v")]
        public override decimal Volume { get; set; }
        /// <inheritdoc/>
        [JsonProperty("q")]
        public override decimal QuoteVolume { get; set; }
    }

    /// <summary>
    /// Stream mini tick
    /// </summary>
    public record BinanceStreamCoinMiniTick : BinanceStreamMiniTickBase
    {
        /// <inheritdoc/>
        [JsonProperty("q")]
        public override decimal Volume { get; set; }
        /// <inheritdoc/>
        [JsonProperty("v")]
        public override decimal QuoteVolume { get; set; }

        /// <summary>
        /// The pair
        /// </summary>
        [JsonProperty("ps")]
        public string Pair { get; set; } = string.Empty;
    }
}
