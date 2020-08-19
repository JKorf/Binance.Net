using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Futures
{
    /// <summary>
    /// Futures account interface
    /// </summary>
    public interface IBinanceClientFuturesAccount
    {
        /// <summary>
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        WebCallResult<IEnumerable<BinanceFuturesAccountBalance>> GetFuturesAccountBalance(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>.
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesAccountBalance>>> GetFuturesAccountBalanceAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        WebCallResult<BinanceFuturesAccountInfo> GetAccountInfo(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);
    }
}