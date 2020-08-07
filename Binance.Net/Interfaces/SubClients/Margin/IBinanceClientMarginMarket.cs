using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Margin
{
    /// <summary>
    /// Margin market interface
    /// </summary>
    public interface IBinanceClientMarginMarket
    {
        /// <summary>
        /// Get a margin asset
        /// </summary>
        /// <param name="asset">The asset to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin asset</returns>
        WebCallResult<BinanceMarginAsset> GetMarginAsset(string asset, CancellationToken ct = default);

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
        /// <returns>Margin asset</returns>
        WebCallResult<BinanceMarginPair> GetMarginPair(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get a margin pair
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        Task<WebCallResult<BinanceMarginPair>> GetMarginPairAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get all assets available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin assets</returns>
        WebCallResult<IEnumerable<BinanceMarginAsset>> GetMarginAssets(CancellationToken ct = default);

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
        WebCallResult<IEnumerable<BinanceMarginPair>> GetMarginPairs(CancellationToken ct = default);

        /// <summary>
        /// Get all asset pairs available for margin trading
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of margin pairs</returns>
        Task<WebCallResult<IEnumerable<BinanceMarginPair>>> GetMarginPairsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        WebCallResult<BinanceMarginPriceIndex> GetMarginPriceIndex(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get margin price index
        /// </summary>
        /// <param name="symbol">The symbol to get</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Margin price index</returns>
        Task<WebCallResult<BinanceMarginPriceIndex>> GetMarginPriceIndexAsync(string symbol, CancellationToken ct = default);
    }
}