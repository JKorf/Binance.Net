namespace Binance.Net.Interfaces
{
    /// <summary>
    /// 24 hour price stats
    /// </summary>
    public interface IBinance24HPrice : IBinanceMiniTick
    {
        /// <summary>
        /// The actual price change in the last 24 hours
        /// </summary>
        decimal PriceChange { get; set; }

        /// <summary>
        /// The price change in percentage in the last 24 hours
        /// </summary>
        decimal PriceChangePercent { get; set; }

        /// <summary>
        /// The weighted average price in the last 24 hours
        /// </summary>
        decimal WeightedAveragePrice { get; set; }

        /// <summary>
        /// The most recent trade quantity
        /// </summary>
        decimal LastQuantity { get; set; }

        /// <summary>
        /// Time at which this 24 hours opened
        /// </summary>
        DateTime OpenTime { get; set; }

        /// <summary>
        /// Time at which this 24 hours closed
        /// </summary>
        DateTime CloseTime { get; set; }

        /// <summary>
        /// The first trade ID in the last 24 hours
        /// </summary>
        long FirstTradeId { get; set; }

        /// <summary>
        /// The last trade ID in the last 24 hours
        /// </summary>
        long LastTradeId { get; set; }

        /// <summary>
        /// The amount of trades made in the last 24 hours
        /// </summary>
        long TotalTrades { get; set; }
    }
}