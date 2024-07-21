using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Adjust history
    /// </summary>
    public record BinanceCrossCollateralAdjustLtvHistory
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;

        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Pre adjustment rate
        /// </summary>
        public decimal PreCollateralRate { get; set; }
        /// <summary>
        /// After adjustment rate
        /// </summary>
        public decimal AfterCollateralRate { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        public AdjustRateDirection Direction { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Time of adjustment
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime AdjustTime { get; set; }
    }
}
