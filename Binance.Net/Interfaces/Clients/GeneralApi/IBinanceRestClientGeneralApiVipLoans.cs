using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.VipLoans;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance VIP Loan endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiVipLoans
    {
        #region Market Data

        /// <summary>
        /// Get VIP Loan borrow interest rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/market-data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/request/interestRate
        /// </para>
        /// </summary>
        /// <param name="loanAsset">Filler by loan asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BinanceVipLoanBorrowInterestRate>>> GetBorrowInterestRateAsync(string loanAsset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get VIP Loan interest rate History
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/market-data/Get-VIP-Loan-Interest-Rate-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/interestRateHistory
        /// </para>
        /// </summary>
        /// <param name="loanAsset">Filler by loan asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="limit">Page size limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanInterestRate>>> GetVipLoanInterestaRateHistoryAsync(string loanAsset, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get loanable asset data
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/market-data/Get-Loanable-Assets-Data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/loanable/data
        /// </para>
        /// </summary>
        /// <param name="loanAsset">Filler by loan asset</param>
        /// <param name="vipLevel">User's vip level</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanAsset>>> GetVipLoanAssetDataAsync(string? loanAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get collateral asset data
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/market-data/Get-Collateral-Asset-Data" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/collateral/data
        /// </para>
        /// </summary>
        /// <param name="collateralAsset">Filler by collateral asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanCollateralAsset>>> GetVipLoanCollateralAssetDataAsync(string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default);

        #endregion

        #region User Data

        /// <summary>
        /// Get VIP Loan ongoing orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/user-information" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/ongoing/orders
        /// </para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="collateralAccountId">Collateral account id</param>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="page">Current page</param>
        /// <param name="limit">Page size limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanOngoingOrderData>>> GetVipLoanOngoinOrdersAsync(long? orderId = null, long? collateralAccountId = null, string? loanAsset = null, string? collateralAsset = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get VIP Loan repayment history
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/user-information/Get-VIP-Loan-Repayment-History" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/repay/history
        /// </para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="limit">Page size limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanRepayHistoryData>>> GetVipLoanRepaymentHistoryAsync(long? orderId = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get VIP Loan accrued interest
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/user-information/Get-VIP-Loan-Accrued-Interest" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/accruedInterest
        /// </para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="limit">Page size limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanAccuredInterest>>> GetVipLoanAccuredInterestAsync(long? orderId = null, string? loanAsset = null, DateTime? startTime = null, DateTime? endTime = null, long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get VIP Loan Collateral Account
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/user-information/Check-Locked-Value-of-VIP-Collateral-Account" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/collateral/account
        /// </para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="collateralAccountId">Collateral account id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanCollateralAccountLockedValue>>> GetVipLoanCollateralAccountAsync(long? orderId = null, long? collateralAccountId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get VIP Loan application status
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/user-information/Query-Application-Status" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/request/data
        /// </para>
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="limit">Page size limit</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceVipLoanApplicationStatus>>> GetApplicationStatusAsync(long? page = null, long? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        #endregion

        #region Trade

        /// <summary>
        /// Renew VIP Loan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/trade" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/renew
        /// </para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="loanTerm">Loan term in days, 30/60</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceVipLoanRenewData>> RenewVipLoanAsync(long orderId, int loanTerm, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay VIP Loan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/trade/VIP-Loan-Repay" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/repay
        /// </para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceVipLoanRepayData>> RepayVipLoanAsync(long orderId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow VIP Loan
        /// <para>
        /// Docs:<br />
        /// <a href="https://developers.binance.com/docs/vip_loan/trade/VIP-Loan-Borrow" /><br />
        /// Endpoint:<br />
        /// GET /sapi/v1/loan/vip/borrow
        /// </para>
        /// </summary>
        /// <param name="loanAccountId">Loan account id</param>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="loanQuantity">Loan quantity</param>
        /// <param name="collateralAccountId">Collateral account id</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="flexibleRate">Is flexible rate</param>
        /// <param name="loanTerm">Loan term in days, 30/60</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceVipLoanBorrowData>> BorrowVipLoanAsync(long loanAccountId, string loanAsset, decimal loanQuantity, string collateralAccountId, string collateralAsset, bool flexibleRate, int? loanTerm = null, long? receiveWindow = null, CancellationToken ct = default);

        #endregion

    }
}
