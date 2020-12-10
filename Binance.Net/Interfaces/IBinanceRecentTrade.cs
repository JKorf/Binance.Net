using System;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Recent trade data
    /// </summary>
    public interface IBinanceRecentTrade: ICommonRecentTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        long OrderId { get; set; }

        /// <summary>
        /// The price of the trade
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// The base quantity of the trade
        /// </summary>
        decimal BaseQuantity { get; set; }
        /// <summary>
        /// The quote quantity of the trade
        /// </summary>
        decimal QuoteQuantity { get; set; }

        /// <summary>
        /// The timestamp of the trade
        /// </summary>
        DateTime TradeTime { get; set; }

        /// <summary>
        /// Whether the buyer is maker
        /// </summary>
        bool BuyerIsMaker { get; set; }

        /// <summary>
        /// Whether the trade was made at the best match
        /// </summary>
        bool IsBestMatch { get; set; }
    }
}