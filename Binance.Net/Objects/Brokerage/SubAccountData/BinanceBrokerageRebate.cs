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
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; } = "";

        /// <summary>
        /// Income
        /// </summary>
        [JsonProperty("income")]
        public decimal Income { get; set; }
        
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; } = "";
        
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";
        
        /// <summary>
        /// Trade Id
        /// </summary>
        [JsonProperty("tradeId")]
        public string TradeId { get; set; } = "";

        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Date { get; set; }
    }
}