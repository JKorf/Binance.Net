using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Spot Asset Info
    /// </summary>
    public class BinanceBrokerageSpotAssetInfo
    {
        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<BinanceBrokerageSubAccountSpotAssetInfo> Data { get; set; } = Array.Empty<BinanceBrokerageSubAccountSpotAssetInfo>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Account Spot Asset Info
    /// </summary>
    public class BinanceBrokerageSubAccountSpotAssetInfo
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;
        
        /// <summary>
        /// Total Balance Of Btc
        /// </summary>
        public decimal TotalBalanceOfBtc { get; set; }
    }
}