using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Adjust info
    /// </summary>
    [SerializationModel]
    public record BinanceCryptoLoanLtvAdjust
    {
        /// <summary>
        /// ["<c>loanCoin</c>"] The loaning asset
        /// </summary>
        [JsonPropertyName("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateralCoin</c>"] The collateral asset
        /// </summary>
        [JsonPropertyName("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public string Direction { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>adjustmentAmount</c>"] Adjustment amount
        /// </summary>
        [JsonPropertyName("adjustmentAmount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>currentLTV</c>"] Current LTV.
        /// </summary>
        [JsonPropertyName("currentLTV")]
        public decimal CurrentLtv { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public FlexibleBorrowStatus Status { get; set; }
    }
}

