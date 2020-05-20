using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Spot.SubAccountData
{
    /// <summary>
    /// Sub accounts margin summary
    /// </summary>
    public class BinanceSubAccountsMarginSummary
    {
        /// <summary>
        /// Total btc asset
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Sub account details
        /// </summary>
        [JsonProperty("subAccountList")]
        public IEnumerable<BinanceSubAccountMarginInfo> SubAccounts { get; set; } = new List<BinanceSubAccountMarginInfo>();
    }

    /// <summary>
    /// Sub account margin info
    /// </summary>
    public class BinanceSubAccountMarginInfo
    {
        /// <summary>
        /// Sub account email
        /// </summary>
        public string Email { get; set; } = "";
        /// <summary>
        /// Total btc asset
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Total liability
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Total net btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
    }
}
