using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Interfaces.Clients.Rest.Margin
{
    public interface IBinanceClientMarginSystemInfo
    {
        /// <summary>
        /// Pings the Binance API
        /// </summary>
        /// <returns>True if successful ping, false if no response</returns>
        Task<WebCallResult<long>> PingAsync(CancellationToken ct = default);

        /// <summary>
        /// Requests the server for the local time. This function also determines the offset between server and local time and uses this for subsequent API calls
        /// </summary>
        /// <param name="resetAutoTimestamp">Whether the response should be used for a new auto timestamp calculation</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Server time</returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(bool resetAutoTimestamp = false, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and information on the provided symbol
        /// </summary>
        /// <param name="symbol">Symbol to get data for token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and information on the provided symbols
        /// </summary>
        /// <param name="symbols">Symbols to get data for token</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(IEnumerable<string> symbols, CancellationToken ct = default);

        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);
    }
}
