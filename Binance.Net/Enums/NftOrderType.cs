using CryptoExchange.Net.Attributes;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Order type for a NFT order
    /// </summary>
    public enum NftOrderType
    {
        /// <summary>
        /// ["<c>0</c>"] Purchase order made by the user to buy assets.
        /// </summary>
        [Map("0")]
        PurchaseOrder,
        /// <summary>
        /// ["<c>1</c>"] Sell order made by the user to sell assets.
        /// </summary>
        [Map("1")]
        SellOrder,
        /// <summary>
        /// ["<c>2</c>"] Income received as royalty from assets.
        /// </summary>
        [Map("2")]
        RoyaltyIncome,
        /// <summary>
        /// ["<c>3</c>"] Order placed during the primary market sale.
        /// </summary>
        [Map("3")]
        PrimaryMarketOrder,
        /// <summary>
        /// ["<c>4</c>"] Fee paid for minting assets.
        /// </summary>
        [Map("4")]
        MintFee
    }
}

