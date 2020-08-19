using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients
{
    /// <summary>
    /// Dust interface
    /// </summary>
    public interface IBinanceClientDust
    {
        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        WebCallResult<IEnumerable<BinanceDustLog>> GetDustLog(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the history of dust conversions
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The history of dust conversions</returns>
        Task<WebCallResult<IEnumerable<BinanceDustLog>>> GetDustLogAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        WebCallResult<BinanceDustTransferResult> DustTransfer(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Converts dust (small amounts of) assets to BNB 
        /// </summary>
        /// <param name="assets">The assets to convert to BNB</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Dust transfer result</returns>
        Task<WebCallResult<BinanceDustTransferResult>> DustTransferAsync(IEnumerable<string> assets, int? receiveWindow = null, CancellationToken ct = default);
    }
}