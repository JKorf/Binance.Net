using Binance.Net.Interfaces;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarketStream
{
    /// <summary>
    /// MiniTick info
    /// </summary>
    public class BinanceStreamMiniTick : BinanceStreamEvent, IBinanceMiniTick
    {
        /// <summary>
        /// The symbol this data is for
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = "";

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
        [JsonProperty("v")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Total traded volume in the alternate asset
        /// </summary>
        [JsonProperty("q")]
        public decimal TotalTradedAlternateAssetVolume { get; set; }
    }
}
