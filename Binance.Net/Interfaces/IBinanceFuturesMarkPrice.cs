using System;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Mark Price and Funding Rate
    /// </summary>
    public interface IBinanceFuturesMarkPrice
    {
        /// <summary>
        /// The symbol the information is about
        /// </summary>
        string Symbol { get; set; }
        /// <summary>
        /// The current market price
        /// </summary>
        decimal MarkPrice { get; set; }
        /// <summary>
        /// The last funding rate
        /// </summary>
        decimal? FundingRate { get; set; }
        /// <summary>
        /// The time the funding rate is applied
        /// </summary>
        DateTime NextFundingTime { get; set; }
    }
}
