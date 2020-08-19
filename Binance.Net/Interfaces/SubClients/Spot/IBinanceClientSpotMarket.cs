using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Spot
{
    /// <summary>
    /// Spot market interface
    /// </summary>
    public interface IBinanceClientSpotMarket: IBinanceClientMarket
    {
        /// <summary>
        /// Gets current average price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<BinanceAveragePrice> GetCurrentAvgPrice(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets current average price for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceAveragePrice>> GetCurrentAvgPriceAsync(string symbol, CancellationToken ct = default);
        
        /// <summary>
        /// Gets the withdrawal fee for an symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        WebCallResult<IEnumerable<BinanceTradeFee>> GetTradeFee(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the trade fee for a symbol
        /// </summary>
        /// <param name="symbol">Symbol to get withdrawal fee for</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Trade fees</returns>
        Task<WebCallResult<IEnumerable<BinanceTradeFee>>> GetTradeFeeAsync(string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);
    }
}