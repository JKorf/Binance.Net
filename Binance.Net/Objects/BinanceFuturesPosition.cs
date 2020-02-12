using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesPosition
    {
        /// <summary>
        /// The entry price of the position
        /// </summary>
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// The current initial leverage of the position
        /// </summary>
        public int Leverage { get; set; }
        /// <summary>
        /// The notional value limit of current initial leverage
        /// </summary>
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// The Liquidation price of the position
        /// </summary>
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// The Market price of the position
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The quantity of the position
        /// </summary>
        [JsonProperty("positionAmt")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The symbol the position is for
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The price of the unrealized PnL
        /// </summary>
        public decimal unRealizedProfit { get; set; }
    }

}
