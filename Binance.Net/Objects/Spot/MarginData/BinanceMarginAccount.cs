using Newtonsoft.Json;
using System.Collections.Generic;

namespace Binance.Net.Objects.Spot.MarginData
{
    /// <summary>
    /// Information about margin account
    /// </summary>
    public class BinanceMarginAccount
    {
        /// <summary>
        /// Boolean indicating if this account can borrow
        /// </summary>
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// Boolean indicating if this account can transfer
        /// </summary>
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// Aggregate level of margin
        /// </summary>
        public decimal MarginLevel { get; set; }
        /// <summary>
        /// Aggregate total balance as BTC
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Aggregate total liability balance of BTC
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Aggregate total available net balance of BTC
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        /// <summary>
        /// Balance list
        /// </summary>
        [JsonProperty("userAssets")]
        public IEnumerable<BinanceMarginBalance> Balances { get; set; } = new List<BinanceMarginBalance>();
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceMarginBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; } = "";
        /// <summary>
        /// The amount that was borrowed
        /// </summary>
        public decimal Borrowed { get; set; }
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// Commission to need pay by borrowed
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// The amount that is currently locked in a trade
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// The amount that is netAsset
        /// </summary>
        public decimal NetAsset { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Free + Locked;
    }
}
