namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class BinanceOrderTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonProperty("tradeId")]
        public long Id { get; set; }
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
        /// Fee paid over this trade
        /// </summary>
        [JsonProperty("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee is paid in
        /// </summary>
        [JsonProperty("commissionAsset")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
