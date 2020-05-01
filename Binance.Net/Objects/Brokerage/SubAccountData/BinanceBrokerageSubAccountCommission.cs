using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    /// <summary>
    /// Sub Account Commission
    /// </summary>
    public class BinanceBrokerageSubAccountCommission
    {
        /// <summary>
        /// Sub Account Id
        /// </summary>
        [JsonProperty("subaccountId")]
        public string SubAccountId { get; set; } = "";
        
        /// <summary>
        /// Maker Commission
        /// </summary>
        [JsonProperty("makerCommission")]
        public decimal MakerCommission { get; set; }
        
        /// <summary>
        /// Taker Commission
        /// </summary>
        [JsonProperty("takerCommission")]
        public decimal TakerCommission { get; set; }
        
        /// <summary>
        /// Margin Maker Commission
        /// </summary>
        [JsonProperty("marginMakerCommission")]
        public decimal MarginMakerCommission { get; set; }
        
        /// <summary>
        /// Margin Taker Commission
        /// </summary>
        [JsonProperty("marginTakerCommission")]
        public decimal MarginTakerCommission { get; set; }
    }
}