using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay info
    /// </summary>
    public class BinanceCryptoLoanRepay
    {
        /// <summary>
        /// The loaning asset
        /// </summary>
        [JsonProperty("loanCoin")]
        public string LoanAsset { get; set; } = string.Empty;
        /// <summary>
        /// The collateral asset
        /// </summary>
        [JsonProperty("collateralCoin")]
        public string CollateralAsset { get; set; } = string.Empty;
        /// <summary>
        /// Current LTV
        /// </summary>
        public decimal? CurrentLTV { get; set; }
        /// <summary>
        /// Remaining principal
        /// </summary>
        public decimal? RemainingPrincipal { get; set; }
        /// <summary>
        /// Repay status
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BorrowStatus RepayStatus { get; set; }
        /// <summary>
        /// Remaining collateral
        /// </summary>
        public decimal? RemainingCollateral { get; set; }
        /// <summary>
        /// Remaining interest
        /// </summary>
        public decimal? RemainingInterest { get; set; }
    }
}
