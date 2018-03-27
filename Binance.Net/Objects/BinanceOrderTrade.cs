using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    public class BinanceOrderTrade
    {
        /// <summary>
        /// Price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the trade
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Commission paid over this trade
        /// </summary>
        public decimal Commission { get; set; }
        /// <summary>
        /// The asset the commission is paid in
        /// </summary>
        public string CommissionAsset { get; set; }
    }
}
