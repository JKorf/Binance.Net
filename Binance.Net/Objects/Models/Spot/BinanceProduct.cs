namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Info on a product
    /// </summary>
    [SerializationModel]
    public record BinanceProduct
    {
        /// <summary>
        /// Name of the symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Status of the symbol
        /// </summary>
        [JsonPropertyName("st")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Name of the base asset
        /// </summary>
        [JsonPropertyName("b")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Name of the quote asset
        /// </summary>
        [JsonPropertyName("q")]
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// Char of the base asset
        /// </summary>
        [JsonPropertyName("ba")]
        public string? BaseAssetChar { get; set; }
        /// <summary>
        /// Char of the quote asset
        /// </summary>
        [JsonPropertyName("qa")]
        public string? QuoteAssetChar { get; set; }

        /// <summary>
        /// Base asset name
        /// </summary>
        [JsonPropertyName("an")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        [JsonPropertyName("qn")]
        public string QuoteAssetName { get; set; } = string.Empty;

        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// Base volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Quote volume
        /// </summary>
        [JsonPropertyName("qv")]
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// Amount of coins in circulation
        /// </summary>
        [JsonPropertyName("cs")]
        public decimal? CirculatingSupply { get; set; }
        /// <summary>
        /// Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Is Etf enabled
        /// </summary>
        [JsonPropertyName("etf")]
        public bool? LeveragedTokenTrading { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("i")]
        public decimal? I { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ts")]
        public decimal? Ts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("y")]
        public decimal? Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("as")]
        public decimal? As { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pn")]
        public string? Pn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pm")]
        public string? Pm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pom")]
        public bool? Pom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pomt")]
        public DateTime? Pomt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("lc")]
        public bool? Lc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("g")]
        public bool? G { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("sd")]
        public bool? Sd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("r")]
        public bool? R { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("hd")]
        public bool? Hd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("rb")]
        public bool? Rb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ks")]
        public bool? Ks { get; set; }
    }
}
