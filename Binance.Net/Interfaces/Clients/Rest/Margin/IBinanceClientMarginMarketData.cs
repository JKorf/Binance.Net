using Binance.Net.Objects.Spot.IsolatedMarginData;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Binance.Net.Interfaces.Clients.Rest.Margin
{
    public interface IBinanceClientMarginMarketData
    {
        /// <summary>
        /// Get a margin asset
        /// </summary>
        /// <param name="asset">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<BinanceMarginAsset>> GetMarginAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get a margin pair
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<BinanceMarginPair>> GetMarginSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get all assets available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginAsset>>> GetMarginAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all asset pairs available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIsolatedMarginSymbol>> GetIsolatedMarginSymbolAsync(string symbol,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbolsAsync(int? receiveWindow =
            null, CancellationToken ct = default);
    }
}
