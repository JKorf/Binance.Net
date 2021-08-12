using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Futures.MarketData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Futures
{
    /// <summary>
    /// Futures system interface
    /// </summary>
    public interface IBinanceClientFuturesCoinSystem
    {
        /// <summary>
        /// Get's information about the exchange including rate limits and symbol list
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Exchange info</returns>
        Task<WebCallResult<BinanceFuturesCoinExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);
    }
}
