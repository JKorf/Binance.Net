using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow record
    /// </summary>
    public record BinanceCryptoLoanLiquidationRecord
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>liquidationDebt</c>"] The liquidation debt.
        /// </summary>
        [JsonPropertyName("liquidationDebt")]
        public decimal Debt { get; set; }
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>liquidationCollateralAmount</c>"] The liquidation collateral quantity
        /// </summary>
        [JsonPropertyName("liquidationCollateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>returnCollateralAmount</c>"] The return collateral quantity
        /// </summary>
        [JsonPropertyName("returnCollateralAmount")]
        public decimal returnCollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>liquidationFee</c>"] The liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>liquidationStartingPrice</c>"] The liquidation starting price
        /// </summary>
        [JsonPropertyName("liquidationStartingPrice")]
        public decimal StartingPrice { get; set; }
        /// <summary>
        /// ["<c>liquidationStartingTime</c>"] The liquidation starting time
        /// </summary>
        [JsonPropertyName("liquidationStartingTime")]
        public DateTime StartingTime { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public LoanLiquidationStatus Status { get; set; }
    }
}

