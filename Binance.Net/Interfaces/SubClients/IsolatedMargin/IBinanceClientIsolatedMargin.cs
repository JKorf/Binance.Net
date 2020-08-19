using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.IsolatedMarginData;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.IsolatedMargin
{
    /// <summary>
    /// Isolated margin interface
    /// </summary>
    public interface IBinanceClientIsolatedMargin
    {
        /// <summary>
        /// Isolated margin user stream endpoints
        /// </summary>
        IBinanceClientIsolatedMarginUserStream UserStream { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAsset">The base asset</param>
        /// <param name="quoteAsset">The quote asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CreateIsolatedMarginAccountResult>> CreateIsolatedMarginAccountAsync(
            string baseAsset, string quoteAsset, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer from or to isolated margin account
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="symbol">Isolated symbol</param>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceMarginTransaction>> IsolatedMarginAccountTransfer(string asset,
            string symbol, IsolatedMarginTransferDirection from, IsolatedMarginTransferDirection to, decimal amount,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfer to and from the isolated margin account
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="from">Filter by direction</param>
        /// <param name="to">Filter by direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="current">Current page</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceIsolatedMarginTransfer>>>
            GetIsolatedMarginAccountTransferHistory(string symbol, string? asset = null,
                IsolatedMarginTransferDirection? from = null, IsolatedMarginTransferDirection? to = null,
                DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10,
                int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin account info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIsolatedMarginAccount>> GetIsolatedMarginAccountAsync(
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceIsolatedMarginSymbol>> GetIsolatedMarginSymbol(string symbol,
            int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Isolated margin symbol info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceIsolatedMarginSymbol>>> GetIsolatedMarginSymbols(int? receiveWindow =
            null, CancellationToken ct = default);
    }
}