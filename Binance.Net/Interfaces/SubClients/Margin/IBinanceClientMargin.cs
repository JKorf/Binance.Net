using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Interfaces.SubClients;
using Binance.Net.Interfaces.SubClients.IsolatedMargin;
using Binance.Net.Interfaces.SubClients.Margin;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.IsolatedMarginData;
using Binance.Net.Objects.Spot.MarginData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.SubClients.Margin
{
    /// <summary>
    /// Margin interface
    /// </summary>
    public interface IBinanceClientMargin
    {
        /// <summary>
        /// Margin market endpoints
        /// </summary>
        IBinanceClientMarginMarket Market { get; }

        /// <summary>
        /// Margin order endpoints
        /// </summary>
        IBinanceClientMarginOrders Order { get; }

        /// <summary>
        /// Margin user stream endpoints
        /// </summary>
        IBinanceClientUserStream UserStream { get; }

        /// <summary>
        /// Isolated margin user stream endpoints
        /// </summary>
        IBinanceClientIsolatedMarginUserStream IsolatedUserStream { get; }

        /// <summary>
        /// Execute transfer between spot account and margin account.
        /// </summary>
        /// <param name="asset">The asset being transferred, e.g., BTC</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="type">TransferDirection (MainToMargin/MarginToMain)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        WebCallResult<BinanceTransaction> Transfer(string asset, decimal amount, TransferDirectionType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Execute transfer between spot account and margin account.
        /// </summary>
        /// <param name="asset">The asset being transferred, e.g., BTC</param>
        /// <param name="amount">The amount to be transferred</param>
        /// <param name="type">TransferDirection (MainToMargin/MarginToMain)</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceTransaction>> TransferAsync(string asset, decimal amount, TransferDirectionType type, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// </summary>
        /// <param name="asset">The asset being borrow, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        WebCallResult<BinanceTransaction> Borrow(string asset, decimal amount, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow. Apply for a loan. 
        /// </summary>
        /// <param name="asset">The asset being borrow, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceTransaction>> BorrowAsync(string asset, decimal amount, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay loan for margin account.
        /// </summary>
        /// <param name="asset">The asset being repay, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        WebCallResult<BinanceTransaction> Repay(string asset, decimal amount, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay loan for margin account.
        /// </summary>
        /// <param name="asset">The asset being repay, e.g., BTC</param>
        /// <param name="amount">The amount to be borrow</param>
        /// <param name="isIsolated">For isolated margin or not</param>
        /// <param name="symbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transaction Id</returns>
        Task<WebCallResult<BinanceTransaction>> RepayAsync(string asset, decimal amount, bool? isIsolated = null, string? symbol = null, int? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers
        /// </summary>
        /// <param name="direction">The direction of the the transfers to retrieve</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        WebCallResult<BinanceQueryRecords<BinanceTransferHistory>> GetTransferHistory(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers
        /// </summary>
        /// <param name="direction">The direction of the the transfers to retrieve</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of transfers</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceTransferHistory>>> GetTransferHistoryAsync(TransferDirection direction, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get loan records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of loan transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">The records count size need show</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Loan records</returns>
        WebCallResult<BinanceQueryRecords<BinanceLoan>> GetLoans(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query loan records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of loan transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">The records count size need show</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Loan records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceLoan>>> GetLoansAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = 1, int? limit = 10, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query repay records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of repay transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Number of page records</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="size">The records count size need show</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay records</returns>
        WebCallResult<BinanceQueryRecords<BinanceRepay>> GetRepays(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query repay records
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="transactionId">The id of repay transaction</param>
        /// <param name="startTime">Time to start getting records from</param>
        /// <param name="endTime">Time to stop getting records to</param>
        /// <param name="current">Filter by number</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="size">The records count size need show</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay records</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceRepay>>> GetRepaysAsync(string asset, long? transactionId = null, DateTime? startTime = null, DateTime? endTime = null, int? current = null, int? size = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest events</returns>
        WebCallResult<BinanceQueryRecords<BinanceInterestHistory>> GetInterestHistory(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="archived">Set to true for archived data from 6 months ago</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest events</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceInterestHistory>>> GetInterestHistoryAsync(string? asset = null, int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, bool? archived = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest rate
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="vipLevel">Vip level</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest rate</returns>
        WebCallResult<IEnumerable<BinanceInterestRateHistory>> GetInterestRateHistory(string asset, string? vipLevel = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of interest rate
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="vipLevel">Vip level</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of interest rate</returns>
        Task<WebCallResult<IEnumerable<BinanceInterestRateHistory>>> GetInterestRateHistoryAsync(string asset, string? vipLevel = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of forced liquidations
        /// </summary>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced liquidations</returns>
        WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>> GetForceLiquidationHistory(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of forced liquidations
        /// </summary>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="isolatedSymbol">Filter by isolated symbol</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of forced liquidations</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceForcedLiquidation>>> GetForceLiquidationHistoryAsync(int? page = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query margin account details
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The margin account information</returns>
        WebCallResult<BinanceMarginAccount> GetMarginAccountInfo(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query margin account details
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The margin account information</returns>
        Task<WebCallResult<BinanceMarginAccount>> GetMarginAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max borrow amount
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="isolatedSymbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        WebCallResult<BinanceMarginAmount> GetMaxBorrowAmount(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max borrow amount
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="isolatedSymbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        Task<WebCallResult<BinanceMarginAmount>> GetMaxBorrowAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max transfer-out amount 
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="isolatedSymbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        WebCallResult<decimal> GetMaxTransferAmount(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Query max transfer-out amount 
        /// </summary>
        /// <param name="asset">The records asset</param>
        /// <param name="isolatedSymbol">The isolated symbol</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Return max amount</returns>
        Task<WebCallResult<decimal>> GetMaxTransferAmountAsync(string asset, string? isolatedSymbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Create isolated margin account
        /// </summary>
        /// <param name="baseAsset">The base asset</param>
        /// <param name="quoteAsset">The quote asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CreateIsolatedMarginAccountResult>> CreateIsolatedMarginAccountAsync(
            string baseAsset, string quoteAsset, int? receiveWindow = null, CancellationToken ct = default);

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
            GetIsolatedMarginAccountTransferHistoryAsync(string symbol, string? asset = null,
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
        Task<WebCallResult<BinanceTransaction>> IsolatedMarginAccountTransferAsync(string asset,
            string symbol, IsolatedMarginTransferDirection from, IsolatedMarginTransferDirection to, decimal amount,
            int? receiveWindow = null, CancellationToken ct = default);
    }
}