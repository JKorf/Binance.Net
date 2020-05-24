using Newtonsoft.Json;
using System;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Information about a trade
    /// </summary>
    public class BinanceFuturesTrade
    {
        /// <summary>
        /// The symbol the trade is for
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The order id the trade belongs to
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Side of the order 
        /// </summary>
        public string Side { get; set; } = "";
        /// <summary>
        /// Position Side of the order 
        /// </summary>
        public string PositionSide { get; set; } = "";
        /// <summary>
        /// Realized PNLof the order 
        /// </summary>
        public decimal RealizedPNL { get; set; }
        /// <summary>
        /// The price of the trade
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity of the trade
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The quote quantity of the trade
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// The commission paid for the trade
        /// </summary>
        public decimal Commission { get; set; }
        /// <summary>
        /// The asset the commission is paid in
        /// </summary>
        public string CommissionAsset { get; set; } = "";
        /// <summary>
        /// The time the trade was made
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Whether account was the buyer in the trade
        /// </summary>
        [JsonProperty("buyer")]
        public bool IsBuyer { get; set; }
        /// <summary>
        /// Whether account was the maker in the trade
        /// </summary>
        [JsonProperty("maker")] 
        public bool IsMaker { get; set; }
    }
}
