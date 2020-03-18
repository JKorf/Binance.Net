using System;
using System.Collections.Generic;
using Binance.Net.Converters;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects.Futures.UserStream
{
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
        /// <summary>
        /// The amount that is locked in a trade
        /// </summary>
        [JsonProperty("wb")]
        public decimal CrossBalance { get; set; }
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
        /// <summary>
        /// The Unrealized PnL
        /// </summary>
        [JsonProperty("up")]
        public decimal UnRealizedPnL { get; set; }

        /// <summary>
        /// The margin type
        /// </summary>
        [JsonProperty("mt"), JsonConverter(typeof(FuturesMarginTypeConverter))]
        public FuturesMarginType MarginType { get; set; }

        /// <summary>
        /// The isolated wallet (if isolated position)
        /// </summary>
        [JsonProperty("iw")]
        public decimal IsolatedWallet { get; set; }
    }
}
