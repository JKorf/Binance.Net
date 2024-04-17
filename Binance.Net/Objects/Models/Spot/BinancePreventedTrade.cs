using Binance.Net.Enums;

namespace Binance.Net.Objects.Models.Spot
{
    /// <summary>
    /// Prevented order info
    /// </summary>
    public class BinancePreventedTrade
    {
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Match id
        /// </summary>
        public long PreventedMatchId { get; set; }
        /// <summary>
        /// Taker order id
        /// </summary>
        public long TakerOrderId { get; set; }
        /// <summary>
        /// Maker order id
        /// </summary>
        public long MakerOrderId { get; set; }
        /// <summary>
        /// Trade group id
        /// </summary>
        public long TradeGroupId { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public SelfTradePreventionMode SelfTradePreventionMode { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Prevented quantity
        /// </summary>
        public decimal MakerPreventedQuantity { get; set; }
        /// <summary>
        /// Transaction time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactTime { get; set; }
    }
}
