using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
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
        /// Aggregate total balace as BTC
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        /// <summary>
        /// Aggregate total alalible balace as BTC
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        /// <summary>
        /// Aggregate total net balace as BTC
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
    
        [JsonProperty("userAssets")]
        public List<BinanceMarginBalance> Balances { get; set; }
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceMarginBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The amount that was borrowed
        /// </summary>
        public decimal Borrowed { get; set; }
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// TODO: wite description
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// The amount that is currently locked in a trade
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Free + Locked;
    }
}
