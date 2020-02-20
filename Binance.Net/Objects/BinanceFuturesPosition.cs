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
        /// Type of margin used for the position
        /// </summary>
        public FuturesMarginType MarginType { get; set; }
        /// <summary>
        /// Does the position add margin automatically?
        /// </summary>
        public bool IsAutoAddMargin { get; set; }
        /// <summary>
        /// Amount of isolated margin
        /// </summary>
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// The current initial leverage of the position
        /// </summary>
        public int Leverage { get; set; }
        /// <summary>
        /// The Liquidation price of the position
        /// </summary>
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// The Market price of the position
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// The notional value limit of current initial leverage
        /// </summary>
        public decimal MaxNotionalValue { get; set; }
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
        [JsonProperty("unRealizedProfit")]
        public decimal UnrealizedPnL { get; set; }
    }

}
