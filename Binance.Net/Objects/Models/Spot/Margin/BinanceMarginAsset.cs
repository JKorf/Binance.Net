using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.MarginData
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
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Short name of the asset
        /// </summary>
        [JsonProperty("assetName")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Is borrowable
        /// </summary>
        public bool IsBorrowable { get; set; }
        /// <summary>
        /// Is mortgageable
        /// </summary>
        public bool IsMortgageable { get; set; }
        /// <summary>
        /// Minimal quantity which can be borrowed
        /// </summary>
        [JsonProperty("userMinBorrow")]
        public decimal MinimalBorrowQuantity { get; set; }
        /// <summary>
        /// Minimal quantity which can be repaid
        /// </summary>
        [JsonProperty("userMinRepay")]
        public decimal MinimalRepayQuanitty { get; set; }
    }
}
