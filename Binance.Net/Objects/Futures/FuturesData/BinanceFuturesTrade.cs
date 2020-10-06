using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Converters;
using Binance.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.FuturesData
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class BinanceFuturesTrade
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = "";

        /// <summary>
        /// Is buyer
        /// </summary>
        public bool Buyer { get; set; }
        /// <summary>
        /// Paid commission
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Asset the commission is paid in
        /// </summary>
        public string CommissionAsset { get; set; } = "";
        /// <summary>
        /// Trade id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Is maker
        /// </summary>
        public bool Maker { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Realized pnl
        /// </summary>
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonConverter(typeof(PositionSideConverter))]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Trade details
    /// </summary>
    public class BinanceFuturesUsdtTrade: BinanceFuturesTrade
    {
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonProperty("quoteQty")]
        public decimal QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Trade details
    /// </summary>
    public class BinanceFuturesCoinTrade : BinanceFuturesTrade
    {
        /// <summary>
        /// The pair
        /// </summary>
        public string Pair { get; set; } = "";
        /// <summary>
        /// Base quantity
        /// </summary>
        [JsonProperty("baseQty")]
        public decimal BaseQuantity { get; set; }
    }
}
