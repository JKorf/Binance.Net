using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceFuturesStreamAccountInfo: BinanceStreamEvent
    {
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        [JsonProperty("B")]
        public List<BinanceFuturesStreamBalance>? Balances { get; set; }
        /// <summary>
        /// List of assets with their current positions
        /// </summary>
        [JsonProperty("P")]
        public List<BinanceFuturesStreamPosition>? Positions { get; set; }
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceFuturesStreamBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonProperty("a")]
        public string? Asset { get; set; }
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        [JsonProperty("wb")]
        public decimal WalletBalance { get; set; }
    }

    /// <summary>
    /// Information about an asset position
    /// </summary>
    public class BinanceFuturesStreamPosition
    {
        /// <summary>
        /// The symbol this balance is for
        /// </summary>
        [JsonProperty("s")]
        public string? Symbol { get; set; }
        /// <summary>
        /// The amount of the position
        /// </summary>
        [JsonProperty("pa")]
        public decimal PositionAmount { get; set; }
        /// <summary>
        /// The entry price
        /// </summary>
        [JsonProperty("ep")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// The accumulated realized PnL
        /// </summary>
        [JsonProperty("cr")]
        public decimal RealizedPnL { get; set; }
    }
}
