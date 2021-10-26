using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.Futures
{
    /// <summary>
    /// Borrow result
    /// </summary>
    public class BinanceCrossCollateralBorrowResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public string BorrowId { get; set; } = string.Empty;
        /// <summary>
        /// The asset borrowed
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The asset used for collateral
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity borrowed
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonProperty("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
