namespace Binance.Net.Objects.Models.Spot
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
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonProperty("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonProperty("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonProperty("c")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// Base volume
        /// </summary>
        [JsonProperty("v")]
        public decimal Volume { get; set; }
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
        /// <summary>
        /// Tags
        /// </summary>
        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Is Etf enabled
        /// </summary>
        [JsonProperty("etf")]
        public bool? LeveragedTokenTrading { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("i")]
        public decimal? I { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ts")]
        public decimal? Ts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("y")]
        public decimal? Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("as")]
        public decimal? As { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pn")]
        public string? Pn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pm")]
        public string? Pm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pom")]
        public bool? Pom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pomt")]
        public bool? Pomt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("lc")]
        public bool? Lc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("g")]
        public bool? G { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sd")]
        public bool? Sd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("r")]
        public bool? R { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("hd")]
        public bool? Hd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("rb")]
        public bool? Rb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ks")]
        public bool? Ks { get; set; }
    }
}
