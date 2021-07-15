using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Interfaces.SubClients.Spot
{
    /// <summary>
    /// Spot interface
    /// </summary>
    public interface IBinanceClientSpot
    {
        /// <summary>
        /// Spot system endpoints
        /// </summary>
        IBinanceClientSpotSystem System { get; }

        /// <summary>
        /// Spot market endpoints
        /// </summary>
        IBinanceClientSpotMarket Market { get; }

        /// <summary>
        /// Spot order endpoints
        /// </summary>
        IBinanceClientSpotOrder Order { get; }

        /// <summary>
        /// Spot user stream endpoints
        /// </summary>
        IBinanceClientUserStream UserStream { get; }
        /// <summary>
        /// Spot/futures endpoints
        /// </summary>
        IBinanceClientSpotFuturesInteraction Futures { get; }

        /// <summary>
        /// Gets the trading status for the current account
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The trading status of the account</returns>
        Task<WebCallResult<BinanceTradingStatus>> GetTradingStatusAsync(int? receiveWindow = null, CancellationToken ct = default);
    }
}