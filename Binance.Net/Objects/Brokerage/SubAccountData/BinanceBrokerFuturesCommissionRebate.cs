using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Futures Commission Rebate
    /// </summary>
    public class BinanceBrokerFuturesCommissionRebate
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        public string SubAccountId { get; set; } = "";

        /// <summary>
        /// Income
        /// </summary>
        public decimal Income { get; set; }
        
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = "";
        
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = "";
        
        /// <summary>
        /// TradeId
        /// </summary>
        public long TradeId { get; set; }
        
        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}