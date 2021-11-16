using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Liquidation history
    /// </summary>
    public class BinanceCrossCollateralLiquidationHistory
    {
        /// <summary>
        /// Quantity for liquidation
        /// </summary>
        [JsonProperty("collateralAmountForLiquidation")]
        public decimal CollateralQuantityForLiquidation { get; set; }

        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Start time of liquidation
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ForceLiquidationStartTime { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Rest collateral quantity after liquidation
        /// </summary>
        [JsonProperty("restCollateralAmountAfterLiquidation")]
        public decimal RestCollateralQuantityAfterLiquidation { get; set; }
        /// <summary>
        /// Rest loan quantity
        /// </summary>
        [JsonProperty("restLoanAmount")]
        public decimal RestLoanQuantity { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
