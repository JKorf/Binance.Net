using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects;
using Binance.Net.Objects.Futures.FuturesData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Futures
{
    /// <summary>
    /// USDT-M futures account endpoints
    /// </summary>
    public interface IBinanceClientFuturesUsdtAccount
    {
        /// <summary>
        /// Gets account information, including balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<BinanceFuturesAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>.
        /// Gets account balances
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The account information</returns>
        Task<WebCallResult<IEnumerable<BinanceFuturesAccountBalance>>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>.
        /// Get user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Multi asset mode</returns>
        Task<WebCallResult<BinanceFuturesMultiAssetMode>> GetMultiAssetsModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>.
        /// Set user's Multi-Assets mode (Multi-Assets Mode or Single-Asset Mode) on Every symbol
        /// </summary>
        /// <param name="enabled">Enabled or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Succes</returns>
        Task<WebCallResult<BinanceResult>> SetMultiAssetsModeAsync(bool enabled, long? receiveWindow = null, CancellationToken ct = default);
    }
}