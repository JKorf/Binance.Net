using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects;
using Binance.Net.Objects.Spot.Futures;
using Binance.Net.Objects.Spot.MarginData;
using Binance.Net.Objects.Spot.WalletData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients.Spot
{
    /// <summary>
    /// Spot futures endpoints
    /// </summary>
    public interface IBinanceClientSpotFuturesInteraction
    {
        /// <summary>
        /// Execute a transfer between the spot account and a futures account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="transferType">The transfer direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The transaction id</returns>
        WebCallResult<BinanceTransaction> TransferFuturesAccount(string asset, decimal amount, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Execute a transfer between the spot account and a futures account
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="amount">Amount to transfer</param>
        /// <param name="transferType">The transfer direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The transaction id</returns>
        Task<WebCallResult<BinanceTransaction>> TransferFuturesAccountAsync(string asset, decimal amount, FuturesTransferType transferType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers between spot and futures account
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to return</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>> GetFuturesTransferHistory(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get history of transfers between spot and futures account
        /// </summary>
        /// <param name="asset">The asset to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">The page to return</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSpotFuturesTransfer>>> GetFuturesTransferHistoryAsync(string asset, DateTime startTime, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow for cross-collateral
        /// </summary>
        /// <param name="coin">The coin to borrow</param>
        /// <param name="amount">The amount to borrow</param>
        /// <param name="collateralCoin">The coin to use as collateral</param>
        /// <param name="collateralAmount">The amount of collateral coin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow result</returns>
        WebCallResult<BinanceCrossCollateralBorrowResult> BorrowForCrossCollateral(string coin, string collateralCoin, decimal? amount = null, decimal? collateralAmount = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow for cross-collateral
        /// </summary>
        /// <param name="coin">The coin to borrow</param>
        /// <param name="amount">The amount to borrow</param>
        /// <param name="collateralCoin">The coin to use as collateral</param>
        /// <param name="collateralAmount">The amount of collateral coin to use</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Borrow result</returns>
        Task<WebCallResult<BinanceCrossCollateralBorrowResult>> BorrowForCrossCollateralAsync(string coin, string collateralCoin, decimal? amount = null, decimal? collateralAmount = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral borrow history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        WebCallResult<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>> GetCrossCollateralBorrowHistory(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral borrow history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralBorrowHistory>>> GetCrossCollateralBorrowHistoryAsync(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay for cross-collateral
        /// </summary>
        /// <param name="coin">The coin</param>
        /// <param name="amount">The amount to repay</param>
        /// <param name="collateralCoin">The collateral coin to repay</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay result</returns>
        WebCallResult<BinanceCrossCollateralRepayResult> RepayForCrossCollateral(string coin, string collateralCoin, decimal amount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay for cross-collateral
        /// </summary>
        /// <param name="coin">The coin</param>
        /// <param name="amount">The amount to repay</param>
        /// <param name="collateralCoin">The collateral coin to repay</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Repay result</returns>
        Task<WebCallResult<BinanceCrossCollateralRepayResult>> RepayForCrossCollateralAsync(string coin, string collateralCoin, decimal amount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral repay history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        WebCallResult<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>> GetCrossCollateralRepayHistory(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral borrow history
        /// </summary>
        /// <param name="coin">The coin to get history for</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>History</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralRepayHistory>>> GetCrossCollateralRepayHistoryAsync(string? coin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross-collateral wallet info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Wallet</returns>
        WebCallResult<BinanceCrossCollateralWallet> GetCrossCollateralWallet(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross-collateral wallet info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Wallet</returns>
        Task<WebCallResult<BinanceCrossCollateralWallet>> GetCrossCollateralWalletAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross-collateral info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info</returns>
        WebCallResult<IEnumerable<BinanceCrossCollateralInformation>> GetCrossCollateralInformation(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross-collateral info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Info</returns>
        Task<WebCallResult<IEnumerable<BinanceCrossCollateralInformation>>> GetCrossCollateralInformationAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Calculate rate after adjust cross-collateral loan to value
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>After collateral rate</returns>
        WebCallResult<BinanceCrossCollateralAfterAdjust> GetRateAfterAdjustLoanToValue(string collateralCoin, string loanCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Calculate rate after adjust cross-collateral loan to value
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>After collateral rate</returns>
        Task<WebCallResult<BinanceCrossCollateralAfterAdjust>> GetRateAfterAdjustLoanToValueAsync(string collateralCoin, string loanCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get max amount for adjust cross-collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max amounts</returns>
        WebCallResult<BinanceCrossCollateralAdjustMaxAmounts> GetMaxAmountForAdjustCrossCollateralLoanToValue(string collateralCoin, string loanCoin, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get max amount for adjust cross-collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Max amounts</returns>
        Task<WebCallResult<BinanceCrossCollateralAdjustMaxAmounts>> GetMaxAmountForAdjustCrossCollateralLoanToValueAsync(string collateralCoin, string loanCoin, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust cross collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjust result</returns>
        WebCallResult<BinanceCrossCollateralAdjustLtvResult> AdjustCrossCollateralLoanToValue(string collateralCoin, string loanCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust cross collateral LTV
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="amount">The amount</param>
        /// <param name="direction">The direction</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjust result</returns>
        Task<WebCallResult<BinanceCrossCollateralAdjustLtvResult>> AdjustCrossCollateralLoanToValueAsync(string collateralCoin, string loanCoin, decimal amount, AdjustRateDirection direction, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral LTV adjustment history
        /// </summary>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjustment history</returns>
        WebCallResult<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>> GetAdjustCrossCollateralLoanToValueHistory(string collateralCoin, string loanCoin, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral LTV adjustment history
        /// </summary>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Adjustment history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralAdjustLtvHistory>>> GetAdjustCrossCollateralLoanToValueHistoryAsync(string? collateralCoin = null, string? loanCoin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral liquidation history
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Liquidation history</returns>
        WebCallResult<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>> GetCrossCollateralLiquidationHistory(string? collateralCoin = null, string? loanCoin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get cross collateral liquidation history
        /// </summary>
        /// <param name="collateralCoin">The collateral coin</param>
        /// <param name="loanCoin">The loan coin</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">The page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Liquidation history</returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCrossCollateralLiquidationHistory>>> GetCrossCollateralLiquidationHistoryAsync(string? collateralCoin = null, string? loanCoin = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}