using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Objects.Models.Spot.Loans
{
    /// <summary>
    /// Repay record
    /// </summary>
    public class BinanceCryptoLoanRepayRecord
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
        /// Borrow order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Repay timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime RepayTime { get; set; }
        /// <summary>
        /// Status of the repay
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BorrowStatus RepayStatus { get; set; }
        /// <summary>
        /// Collateral return
        /// </summary>
        public decimal CollateralReturn { get; set; }
        /// <summary>
        /// Collateral used
        /// </summary>
        public decimal CollateralUsed { get; set; }
        /// <summary>
        /// Repay quantity
        /// </summary>
        [JsonProperty("repayAmount")]
        public decimal RepayQuantity { get; set; }
        /// <summary>
        /// 1 for "repay with borrowed asset", 2 for "repay with collateral"
        /// </summary>
        public int RepayType { get; set; }
    }
}
