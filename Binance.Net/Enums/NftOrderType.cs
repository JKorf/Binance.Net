using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Order type for a NFT order
    /// </summary>
    public enum NftOrderType
    {
        /// <summary>
        /// Purchase order made by the user to buy assets.
        /// </summary>
        [Map("0")]
        PurchaseOrder,
        /// <summary>
        /// Sell order made by the user to sell assets.
        /// </summary>
        [Map("1")]
        SellOrder,
        /// <summary>
        /// Income received as royalty from assets.
        /// </summary>
        [Map("2")]
        RoyaltyIncome,
        /// <summary>
        /// Order placed during the primary market sale.
        /// </summary>
        [Map("3")]
        PrimaryMarketOrder,
        /// <summary>
        /// Fee paid for minting assets.
        /// </summary>
        [Map("4")]
        MintFee
    }
}
