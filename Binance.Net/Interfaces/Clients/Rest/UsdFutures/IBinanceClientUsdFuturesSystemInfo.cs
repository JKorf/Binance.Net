using Binance.Net.Objects.Futures.FuturesData;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Interfaces.Clients.Rest.UsdFutures
{
    public interface IBinanceClientUsdFuturesSystemInfo
    {
        /// <summary>
        /// Pings the Binance Futures API
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
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceFuturesUsdtExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the current status of the trading rules for the account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of trading rules status per symbol</returns>
        Task<WebCallResult<BinanceFuturesTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);

    }
}
