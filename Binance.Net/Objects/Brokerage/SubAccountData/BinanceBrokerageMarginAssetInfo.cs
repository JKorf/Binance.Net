using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Margin Asset Info
    /// </summary>
    public class BinanceBrokerageMarginAssetInfo
    {
        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<BinanceBrokerageSubAccountMarginAssetInfo>? Data { get; set; }
        
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Account Margin Asset Info
    /// </summary>
    public class BinanceBrokerageSubAccountMarginAssetInfo
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Margin enable
        /// </summary>
        public bool MarginEnable { get; set; }
        
        /// <summary>
        /// Total Asset Of Btc
        /// </summary>
        public decimal TotalAssetOfBtc { get; set; }
        
        /// <summary>
        /// Total Liability Of Btc
        /// </summary>
        public decimal TotalLiabilityOfBtc { get; set; }
        
        /// <summary>
        /// Total Net Asset Of Btc
        /// </summary>
        public decimal TotalNetAssetOfBtc { get; set; }
        
        /// <summary>
        /// Margin level
        /// </summary>
        public decimal MarginLevel { get; set; }
    }
}