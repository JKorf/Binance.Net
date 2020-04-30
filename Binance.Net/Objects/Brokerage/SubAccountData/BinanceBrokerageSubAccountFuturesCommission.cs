using Newtonsoft.Json;

namespace Binance.Net.Objects.Brokerage.SubAccountData
{
    public class BinanceBrokerageSubAccountFuturesCommission
    {
        [JsonProperty("subaccountId")]
        public string Id { get; set; }
        
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        /// <summary>
        /// Futures commission adjustment for maker </summary>
        [JsonProperty("makerAdjustment")]
        public int MakerAdjustment { get; set; }
        
        /// <summary>
        /// Futures commission adjustment for taker
        /// </summary>
        [JsonProperty("takerAdjustment")]
        public int TakerAdjustment { get; set; }
        
        /// <summary>
        /// Futures commission (after adjusted) for maker
        /// </summary>
        [JsonProperty("makerCommission")]
        public decimal MakerCommission { get; set; }
        
        /// <summary>
        /// Futures commission (after adjusted) for taker
        /// </summary>
        [JsonProperty("takerCommission")]
        public decimal TakerCommission { get; set; }
    }
}