namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Info on a product
    /// </summary>
    [SerializationModel]
    public record BinanceProduct
    {
        /// <summary>
        /// ["<c>s</c>"] Name of the symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>st</c>"] Status of the symbol
        /// </summary>
        [JsonPropertyName("st")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>b</c>"] Name of the base asset
        /// </summary>
        [JsonPropertyName("b")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>q</c>"] Name of the quote asset
        /// </summary>
        [JsonPropertyName("q")]
        public string QuoteAsset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>ba</c>"] Char of the base asset
        /// </summary>
        [JsonPropertyName("ba")]
        public string? BaseAssetChar { get; set; }
        /// <summary>
        /// ["<c>qa</c>"] Char of the quote asset
        /// </summary>
        [JsonPropertyName("qa")]
        public string? QuoteAssetChar { get; set; }

        /// <summary>
        /// ["<c>an</c>"] Base asset name
        /// </summary>
        [JsonPropertyName("an")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>qn</c>"] Quote asset name
        /// </summary>
        [JsonPropertyName("qn")]
        public string QuoteAssetName { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Base volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>qv</c>"] Quote volume
        /// </summary>
        [JsonPropertyName("qv")]
        public decimal QuoteVolume { get; set; }

        /// <summary>
        /// ["<c>cs</c>"] Amount of coins in circulation
        /// </summary>
        [JsonPropertyName("cs")]
        public decimal? CirculatingSupply { get; set; }
        /// <summary>
        /// ["<c>tags</c>"] Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>etf</c>"] Is Etf enabled
        /// </summary>
        [JsonPropertyName("etf")]
        public bool? LeveragedTokenTrading { get; set; }

        /// <summary>
        /// ["<c>i</c>"]
        /// </summary>
        [JsonPropertyName("i")]
        public decimal? I { get; set; }
        /// <summary>
        /// ["<c>ts</c>"]
        /// </summary>
        [JsonPropertyName("ts")]
        public decimal? Ts { get; set; }
        /// <summary>
        /// ["<c>y</c>"]
        /// </summary>
        [JsonPropertyName("y")]
        public decimal? Y { get; set; }
        /// <summary>
        /// ["<c>as</c>"]
        /// </summary>
        [JsonPropertyName("as")]
        public decimal? As { get; set; }
        /// <summary>
        /// ["<c>pn</c>"]
        /// </summary>
        [JsonPropertyName("pn")]
        public string? Pn { get; set; }
        /// <summary>
        /// ["<c>pm</c>"]
        /// </summary>
        [JsonPropertyName("pm")]
        public string? Pm { get; set; }
        /// <summary>
        /// ["<c>pom</c>"]
        /// </summary>
        [JsonPropertyName("pom")]
        public bool? Pom { get; set; }
        /// <summary>
        /// ["<c>pomt</c>"]
        /// </summary>
        [JsonPropertyName("pomt")]
        public DateTime? Pomt { get; set; }
        /// <summary>
        /// ["<c>lc</c>"]
        /// </summary>
        [JsonPropertyName("lc")]
        public bool? Lc { get; set; }
        /// <summary>
        /// ["<c>g</c>"]
        /// </summary>
        [JsonPropertyName("g")]
        public bool? G { get; set; }
        /// <summary>
        /// ["<c>sd</c>"]
        /// </summary>
        [JsonPropertyName("sd")]
        public bool? Sd { get; set; }
        /// <summary>
        /// ["<c>r</c>"]
        /// </summary>
        [JsonPropertyName("r")]
        public bool? R { get; set; }
        /// <summary>
        /// ["<c>hd</c>"]
        /// </summary>
        [JsonPropertyName("hd")]
        public bool? Hd { get; set; }
        /// <summary>
        /// ["<c>rb</c>"]
        /// </summary>
        [JsonPropertyName("rb")]
        public bool? Rb { get; set; }
        /// <summary>
        /// ["<c>ks</c>"]
        /// </summary>
        [JsonPropertyName("ks")]
        public bool? Ks { get; set; }
    }
}

