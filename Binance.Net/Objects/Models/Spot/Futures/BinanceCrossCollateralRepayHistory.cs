using Binance.Net.Converters;
using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Futures
{
    /// <summary>
    /// Repay history
    /// </summary>
    public record BinanceCrossCollateralRepayHistory
    {
        /// <summary>
        /// Id
        /// </summary>
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// Time of confirmation
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ConfirmedTime { get; set; }

        /// <summary>
        /// Time of last update
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Released collateral quantity
        /// </summary>
        public decimal ReleasedCollateral { get; set; }
        /// <summary>
        /// The status of the transfer
        /// </summary>
        [JsonConverter(typeof(FuturesTransferStatusConverter))]
        public FuturesTransferStatus Status { get; set; }
        /// <summary>
        /// Repay type
        /// </summary>
        public string RepayType { get; set; } = string.Empty;
        /// <summary>
        /// Collateral repayment
        /// </summary>
        public decimal? RepayCollateral { get; set; }
        /// <summary>
        /// Loan/collateral exchange rate
        /// </summary>
        public decimal Price { get; set; }
    }
}
