using Newtonsoft.Json;

namespace Binance.Net.Objects.Other
{
    /// <summary>
    /// Info on a product
    /// </summary>
    public class BinanceProduct
    {
        /// <summary>
        /// Name of the symbol
        /// </summary>
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Status of the symbol
        /// </summary>
        [JsonProperty("st")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Name of the base asset
        /// </summary>
        [JsonProperty("b")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Name of the quote asset
        /// </summary>
        [JsonProperty("q")]
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// Char of the base asset
        /// </summary>
        [JsonProperty("ba")]
        public string? BaseAssetChar { get; set; }
        /// <summary>
        /// Char of the quote asset
        /// </summary>
        [JsonProperty("qa")]
        public string? QuoteAssetChar { get; set; }

        /// <summary>
        /// Base asset name
        /// </summary>
        [JsonProperty("an")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        [JsonProperty("qn")]
        public string QuoteAssetName { get; set; } = string.Empty;

        /// <summary>
        /// Open price
        /// </summary>
        [JsonProperty("o")]
        public decimal? Open { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonProperty("h")]
        public decimal? High { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonProperty("l")]
        public decimal? Low { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonProperty("c")]
        public decimal? Close { get; set; }
        /// <summary>
        /// Base volume
        /// </summary>
        [JsonProperty("v")]
        public decimal BaseVolume { get; set; }
        /// <summary>
        /// Quote volume
        /// </summary>
        [JsonProperty("qv")]
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// Amount of coins in circulation
        /// </summary>
        [JsonProperty("cs")]
        public decimal? CirculatingSupply { get; set; }
    }
}
