using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Brokerage Rebate
    /// </summary>
    public class BinanceBrokerageRebate
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Income
        /// </summary>
        public decimal Income { get; set; }
        
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// Trade Id
        /// </summary>
        public string TradeId { get; set; } = string.Empty;

        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}