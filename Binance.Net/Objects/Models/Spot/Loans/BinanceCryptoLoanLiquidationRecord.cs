using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow record
    /// </summary>
    public record BinanceCryptoLoanLiquidationRecord
    {
        /// <summary>
        /// The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The liquidation dept
        /// </summary>
        [JsonPropertyName("liquidationDebt")]
        public decimal Debt { get; set; }
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// The liquidation collateral quantity
        /// </summary>
        [JsonPropertyName("liquidationCollateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// The return collateral quantity
        /// </summary>
        [JsonPropertyName("returnCollateralAmount")]
        public decimal returnCollateralQuantity { get; set; }
        /// <summary>
        /// The liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The liquidation starting price
        /// </summary>
        [JsonPropertyName("liquidationStartingPrice")]
        public decimal StartingPrice { get; set; }
        /// <summary>
        /// The liquidation starting time
        /// </summary>
        [JsonPropertyName("liquidationStartingTime")]
        public DateTime StartingTime { get; set; }
        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("status")]
        public LoanLiquidationStatus Status { get; set; }
    }
}
