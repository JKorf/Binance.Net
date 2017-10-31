using System.Collections.Generic;
using Newtonsoft.Json;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceStreamAccountInfo: BinanceStreamEvent
    {
        /// <summary>
        /// Commission percentage to pay when making trades
        /// </summary>
        [JsonProperty("m")]
        public double MakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to pay when taking trades
        /// </summary>
        [JsonProperty("t")]
        public double TakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when buying
        /// </summary>
        [JsonProperty("b")]
        public double BuyerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when selling
        /// </summary>
        [JsonProperty("s")]
        public double SellerCommission { get; set; }
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
        public List<BinanceStreamBalance> Balances { get; set; }
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceStreamBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        [JsonProperty("a")]
        public string Asset { get; set; }
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        [JsonProperty("f")]
        public double Free { get; set; }
        /// <summary>
        /// The amount that is currently locked in a trade
        /// </summary>
        [JsonProperty("l")]
        public double Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public double Total => Free + Locked;
    }
}
