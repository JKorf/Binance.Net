using System;
using System.Collections.Generic;
using System.Text;
using Binance.Net.Objects.Shared;

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
        IBinanceKline Data { get; set; }
    }
}
