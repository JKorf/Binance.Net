using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Borrow info
    /// </summary>
    public class BinanceCryptoLoanBorrow
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
        /// The loan quantity
        /// </summary>
        [JsonProperty("loanAmount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// The collateral quantity
        /// </summary>
        [JsonProperty("collateralAmount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Hourly interest rate
        /// </summary>
        public decimal HourlyInterestRate { get; set; }
        /// <summary>
        /// Borrow order id
        /// </summary>
        public long OrderId { get; set; }
    }
}
