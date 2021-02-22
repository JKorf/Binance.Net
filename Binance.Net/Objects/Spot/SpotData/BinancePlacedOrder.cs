using Binance.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Binance.Net.Enums;
using Binance.Net.Objects.Shared;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Objects.Spot.SpotData
{
    /// <summary>
    /// The result of placing a new order
    /// </summary>
    public class BinancePlacedOrder: BinanceOrderBase, ICommonOrderId
    {
        /// <summary>
        /// The time the order was placed
        /// </summary>
        [JsonOptionalProperty]
        [JsonProperty("transactTime"), JsonConverter(typeof(TimestampConverter))]
        public new DateTime CreateTime { get; set; }
        
        /// <summary>
        /// Fills for the order
        /// </summary>
        [JsonOptionalProperty]
        public IEnumerable<BinanceOrderTrade>? Fills { get; set; }
        
        /// <summary>
        /// Only present if a margin trade happened
        /// </summary>
        [JsonOptionalProperty]
        public decimal? MarginBuyBorrowAmount { get; set; }
        /// <summary>
        /// Only present if a margin trade happened
        /// </summary>
        [JsonOptionalProperty]
        public string? MarginBuyBorrowAsset { get; set; }
        
        string ICommonOrderId.CommonId => OrderId.ToString();
    }
}
