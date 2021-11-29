using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot.BSwap;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.Rest.Spot
{
    /// <summary>
    /// Binance Spot LiquidSwap endpoints
    /// </summary>
    public interface IBinanceClientSpotMarketLiquidSwap
    {
        /// <summary>
        /// Get all swap pools
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#list-all-swap-pools-market_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBSwapPool>>> GetBSwapPoolsAsync(int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get liquidity info for a pool
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-liquidity-information-of-a-pool-user_data" /></para>
        /// </summary>
        /// <param name="poolId">Get a specific pool</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBSwapPoolLiquidity>>> GetPoolLiquidityInfoAsync(int? poolId = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Add liquidity to a pool
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#add-liquidity-trade" /></para>
        /// </summary>
        /// <param name="poolId">The pool</param>
        /// <param name="asset">The asset</param>
        /// <param name="type">Add type</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBSwapOperationResult>> AddLiquidityAsync(int poolId, string asset, decimal quantity, LiquidityType? type = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Remove liquidity from a pool
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#remove-liquidity-trade" /></para>
        /// </summary>
        /// <param name="poolId">The pool</param>
        /// <param name="asset">The asset</param>
        /// <param name="type">Remove type</param>
        /// <param name="shareQuantity">Quantity to remove</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBSwapOperationResult>> RemoveLiquidityAsync(int poolId, string asset, LiquidityType type, decimal shareQuantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get liquidity operation records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-liquidity-operation-record-user_data" /></para>
        /// </summary>
        /// <param name="operationId">Filter by operationId</param>
        /// <param name="poolId">Filter by poolId</param>
        /// <param name="operation">Filter by operation</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBSwapOperation>>> GetLiquidityOperationRecordsAsync(long? operationId = null, int? poolId = null, BSwapOperation? operation = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Request a quote for swap quote asset (selling asset) for base asset (buying asset), essentially price/exchange rates. quoteQty is quantity of quote asset(to sell).
        /// Please be noted the quote is for reference only, the actual price will change as the liquidity changes, it's recommended to swap immediate after request a quote for slippage prevention.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#request-quote-user_data" /></para>
        /// </summary>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBSwapQuote>> GetQuoteAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Swap quote asset for base asset
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#swap-trade" /></para>
        /// </summary>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteQuantity">Quote quantity</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBSwapResult>> SwapAsync(string quoteAsset, string baseAsset, decimal quoteQuantity, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get swap history records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-swap-history-user_data" /></para>
        /// </summary>
        /// <param name="swapId">Filter by swapId</param>
        /// <param name="status">Filter by status</param>
        /// <param name="quoteAsset">Filter by quote asset</param>
        /// <param name="baseAsset">Filter by base asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBSwapRecord>>> GetSwapHistoryAsync(long? swapId = null, BSwapStatus? status = null, string? quoteAsset = null, string? baseAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get pool config
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-pool-configure-user_data" /></para>
        /// </summary>
        /// <param name="poolId">Id of the pool</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceBSwapPoolConfig>>> GetBSwapPoolConfigureAsync(int poolId, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Calculate expected share quantity for adding liquidity in single or dual token.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#add-liquidity-preview-user_data" /></para>
        /// </summary>
        /// <param name="poolId">The pool</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="type">Add type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBSwapPreviewResult>> AddLiquidityPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Calculate expected share quantity for removing liquidity in single or dual token.
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#remove-liquidity-preview-user_data" /></para>
        /// </summary>
        /// <param name="poolId">The pool</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">Quantity to add</param>
        /// <param name="type">Add type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceBSwapPreviewResult>> RemoveLiquidityPreviewAsync(int poolId, string asset, decimal quantity, LiquidityType type, int? receiveWindow = null, CancellationToken ct = default);
    }
}
