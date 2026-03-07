namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Precision per asset
    /// </summary>
    [SerializationModel]
    public record BinanceConvertQuantityPrecisionAsset
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fraction</c>"] Fraction
        /// </summary>
        [JsonPropertyName("fraction")]
        public int Fraction { get; set; }
    }
}

