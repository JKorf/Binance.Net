using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{//TODO
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceStreamMarginAccountInfo: BinanceStreamEvent
    {
        /// <summary>
        /// Time of last account update
        /// </summary>
        [JsonProperty("u"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Commission percentage to pay when making trades
        /// </summary>
        [JsonProperty("m")]
        public decimal MakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to pay when taking trades
        /// </summary>
        [JsonProperty("t")]
        public decimal TakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when buying
        /// </summary>
        [JsonProperty("b")]
        public decimal BuyerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when selling
        /// </summary>
        [JsonProperty("s")]
        public decimal SellerCommission { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        [JsonProperty("T")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        [JsonProperty("W")]
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        [JsonProperty("D")]
        public bool CanDeposit { get; set; }
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        [JsonProperty("B")]
        public IEnumerable<BinanceStreamBalance> Balances { get; set; } = new List<BinanceStreamBalance>();
    }

}
