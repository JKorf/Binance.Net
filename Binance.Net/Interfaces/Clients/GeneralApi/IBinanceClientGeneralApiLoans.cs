using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Loans;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Spot Crypto loans endpoints
    /// </summary>
    public interface IBinanceClientGeneralApiCryptoLoans
    {
        /// <summary>
        /// Get income history from crypto loans
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-crypto-loans-income-history-user_data" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of incoming</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceCryptoLoanIncome>>> GetIncomeHistoryAsync(string asset, LoanIncomeType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Take a crypto loan
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#borrow-crypto-loan-borrow-trade" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="loanTerm">Loan term in days, 7/14/30/90/180</param>
        /// <param name="loanQuantity">Quantity to loan in loan asset</param>
        /// <param name="collateralQuantity">Quantity to loan in collateral asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanBorrow>> BorrowAsync(string loanAsset, string collateralAsset, int loanTerm, decimal? loanQuantity = null, decimal? collateralQuantity = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get borrow order history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#borrow-get-loan-borrow-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanBorrowRecord>>> GetBorrowHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ongoing loan orders
        /// </summary>
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#borrow-get-loan-ongoing-orders-user_data" /></para>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanOpenBorrowOrder>>> GetOpenBorrowOrdersAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay a loan
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#repay-crypto-loan-repay-trade" /></para>
        /// </summary>
        /// <param name="orderId">Order id to repay</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="repayWithBorrowedAsset">True to repay with the borrowed asset, false to repay with collateral asset</param>
        /// <param name="collateralReturn">Return extra colalteral to spot account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanRepay>> RepayAsync(long orderId, decimal quantity, bool? repayWithBorrowedAsset = null, bool? collateralReturn = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get loan repayment history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#repay-get-loan-repayment-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanRepayRecord>>> GetRepayHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust LTV for a loan
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#adjust-ltv-crypto-loan-adjust-ltv-trade" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="quantity">Adjustment quantity</param>
        /// <param name="addOrRmove">True for add, false to reduce</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanLtvAdjust>> AdjustLTVAsync(long orderId, decimal quantity, bool addOrRmove, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get LTV adjustment history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#adjust-ltv-get-loan-ltv-adjustment-history-user_data" /></para>
        /// </summary>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanLtvAdjustRecord>>> GetLtvAdjustHistoryAsync(long? orderId = null, string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
