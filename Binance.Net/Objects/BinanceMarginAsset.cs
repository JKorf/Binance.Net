using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Margin asset info
    /// </summary>
    public class BinanceMarginAsset
    {
        /// <summary>
        /// Full name of the asset
        /// </summary>
        [JsonProperty("assetFullName")]
        public string FullName { get; set; } = "";
        /// <summary>
        /// Short name of the asset
        /// </summary>
        [JsonProperty("assetName")]
        public string Name { get; set; } = "";
        /// <summary>
        /// Is borrowable
        /// </summary>
        public bool IsBorrowable { get; set; }
        /// <summary>
        /// Is mortgageable
        /// </summary>
        public bool IsMortgageable { get; set; }
        /// <summary>
        /// Minimal amount which can be borrowed
        /// </summary>
        [JsonProperty("userMinBorrow")]
        public decimal MinimalBorrowAmount { get; set; }
        /// <summary>
        /// Minimal amount which can be repaid
        /// </summary>
        [JsonProperty("userMinRepay")]
        public decimal MinimalRepayAmount { get; set; }
    }
}
