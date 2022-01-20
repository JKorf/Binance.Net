using Binance.Net.Enums;

namespace Binance.Net.Interfaces
{
    /// <summary>
    /// Stream kline data
    /// </summary>
    public interface IBinanceStreamKlineData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        string Symbol { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        IBinanceStreamKline Data { get; set; }
    }

    /// <summary>
    /// Stream kline data
    /// </summary>
    public interface IBinanceStreamKline: IBinanceKline
    {
        /// <summary>
        /// Interval
        /// </summary>
        KlineInterval Interval { get; set; }
        /// <summary>
        /// Is this kline final
        /// </summary>
        bool Final { get; set; }
        /// <summary>
        /// Id of the first trade in this kline
        /// </summary>
        long FirstTrade { get; set; }
        /// <summary>
        /// Id of the last trade in this kline
        /// </summary>
        long LastTrade { get; set; }
    }
}
