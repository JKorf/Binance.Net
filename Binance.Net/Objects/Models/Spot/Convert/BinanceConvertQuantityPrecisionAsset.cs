namespace Binance.Net.Objects.Models.Spot.Convert
{
    /// <summary>
    /// Precision per asset
    /// </summary>
    public record BinanceConvertQuantityPrecisionAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fraction
        /// </summary>
        public int Fraction { get; set; }
    }
}
