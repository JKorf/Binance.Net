using Newtonsoft.Json;
using System;
using System.Globalization;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// Information about a trade
    /// </summary>
    public class BinanceTrade: ICommonTrade
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
        /// Id of the order list this order belongs to
        /// </summary>
        public long? OrderListId { get; set; }
        
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
        public bool IsBuyer { get; set; }
        /// <summary>
        /// Whether account was the maker in the trade
        /// </summary>
        public bool IsMaker { get; set; }
        /// <summary>
        /// Whether trade was made with the best match
        /// </summary>
        public bool IsBestMatch { get; set; }

        string ICommonTrade.CommonId => Id.ToString(CultureInfo.InvariantCulture);
        decimal ICommonTrade.CommonPrice => Price;
        decimal ICommonTrade.CommonQuantity => Quantity;
        decimal ICommonTrade.CommonFee => Commission;
        string ICommonTrade.CommonFeeAsset => CommissionAsset;
    }
}
