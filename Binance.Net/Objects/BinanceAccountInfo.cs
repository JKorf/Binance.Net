using System.Collections.Generic;

namespace Binance.Net.Objects
{
    /// <summary>
    /// Information about an account
    /// </summary>
    public class BinanceAccountInfo
    {
        /// <summary>
        /// Commission percentage to pay when making trades
        /// </summary>
        public decimal MakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to pay when taking trades
        /// </summary>
        public decimal TakerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when buying
        /// </summary>
        public decimal BuyerCommission { get; set; }
        /// <summary>
        /// Commission percentage to buy when selling
        /// </summary>
        public decimal SellerCommission { get; set; }
        /// <summary>
        /// Boolean indicating if this account can trade
        /// </summary>
        public bool CanTrade { get; set; }
        /// <summary>
        /// Boolean indicating if this account can withdraw
        /// </summary>
        public bool CanWithdraw { get; set; }
        /// <summary>
        /// Boolean indicating if this account can deposit
        /// </summary>
        public bool CanDeposit { get; set; }
        /// <summary>
        /// List of assets with their current balances
        /// </summary>
        public List<BinanceBalance> Balances { get; set; }
    }

    /// <summary>
    /// Information about an asset balance
    /// </summary>
    public class BinanceBalance
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string Asset { get; set; }
        /// <summary>
        /// The amount that isn't locked in a trade
        /// </summary>
        public decimal Free { get; set; }
        /// <summary>
        /// The amount that is currently locked in a trade
        /// </summary>
        public decimal Locked { get; set; }
        /// <summary>
        /// The total balance of this asset (Free + Locked)
        /// </summary>
        public decimal Total => Free + Locked;
    }
}
