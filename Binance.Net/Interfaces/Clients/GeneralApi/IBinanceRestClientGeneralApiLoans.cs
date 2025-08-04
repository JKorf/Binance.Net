using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.Loans;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Spot Crypto loans endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiLoans
    {
        /// <summary>
        /// Get LTV information and collateral limit of collateral assets. The collateral limit is shown in USD value.
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/market-data" /></para>
        /// </summary>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanCollateralAsset>>> GetCollateralAssetsAsync(string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get interest rate and borrow limit of loanable assets. The borrow limit is shown in USD value.
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/market-data/Get-Flexible-Loan-Assets-Data" /></para>
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanAsset>>> GetLoanableAssetsAsync(string? loanAsset = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Borrow flexible loan
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/trade" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="loanQuantity">Quantity to loan in loan asset</param>
        /// <param name="collateralQuantity">Quantity to loan in collateral asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanBorrow>> BorrowAsync(string loanAsset, string collateralAsset, decimal? loanQuantity = null, decimal? collateralQuantity = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay flexible loan
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/trade/Flexible-Loan-Repay" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="quantity">Quantity to loan in loan asset</param>
        /// <param name="collateralReturn">Return extra collateral to spot account</param>
        /// <param name="fullRepayment">Is fully repaid</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanRepay>> RepayAsync(string loanAsset, string collateralAsset, decimal quantity, bool? collateralReturn = null, bool? fullRepayment = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Repay collateral flexible loan
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/trade/Flexible-Loan-Collateral-Repay" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="quantity">Quantity to loan in loan asset</param>
        /// <param name="collateralReturn">Return extra collateral to spot account</param>
        /// <param name="fullRepayment">RIs fully repaid</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanRepay>> RepayCollateralAsync(string loanAsset, string collateralAsset, decimal quantity, bool? collateralReturn = null, bool? fullRepayment = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Adjust LTV for a loan
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/trade/Flexible-Loan-Adjust-LTV" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="quantity">Adjustment quantity</param>
        /// <param name="addOrRemove">True for add, false to reduce</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanLtvAdjust>> AdjustLTVAsync(string loanAsset, string collateralAsset, decimal quantity, bool addOrRemove, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible loan LTV adjustment history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/user-information" /></para>
        /// </summary>
        /// <param name="loanAsset">Asset to loan</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanFlexibleLtvAdjustRecord>>> GetFlexibleLtvAdjustHistoryAsync(string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the latest rate of collateral coin/loan coin when using collateral repay.
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/user-information/Check-Collateral-Repay-Rate" /></para>
        /// </summary>
        /// <param name="loanAsset">Loan asset</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanRepayRate>> GetCollateralRepayRateAsync(string loanAsset, string collateralAsset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible borrow order history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/user-information/Get-Flexible-Loan-Borrow-History" /></para>
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanFlexibleBorrowRecord>>> GetFlexibleBorrowHistoryAsync(string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get ongoing flexible loan orders
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/user-information/Get-Flexible-Loan-Ongoing-Orders" /></para>
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanOpenBorrowOrder>>> GetOpenBorrowOrdersAsync(string? loanAsset = null, string? collateralAsset = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible loan liquidation history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/user-information/Get-Flexible-Loan-Liquidation-History" /></para>
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanLiquidationRecord>>> GetLiquidationHistoryAsync(string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible loan repayment history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/flexible-rate/user-information/Get-Flexible-Loan-Repayment-History" /></para>
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanFlexibleRepayRecord>>> GetFlexibleRepayHistoryAsync(string? loanAsset = null, string? collateralAsset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get income history from stable crypto loans
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/stable-rate/market-data" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="type">Filter by type of incoming</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanIncome[]>> GetIncomeHistoryAsync(string asset, LoanIncomeType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get stable borrow order history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/stable-rate/user-information" /></para>
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
        /// Get LTV adjustment history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/stable-rate/user-information/Get-Loan-LTV-Adjustment-History" /></para>
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

        /// <summary>
        /// Get stable loan repayment history
        /// <para><a href="https://developers.binance.com/docs/crypto_loan/stable-rate/user-information/Get-Loan-Repayment-History" /></para>
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
        /// Get interest rate and borrow limit of loanable assets.
        /// </summary>
        /// <param name="loanAsset">Filter by loan asset</param>
        /// <param name="vipLevel">VIP level filter</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanAsset>>> GetLoanableAssetsAsync(string? loanAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get LTV information and collateral limit of collateral assets.
        /// </summary>
        /// <param name="collateralAsset">Filter by collateral asset</param>
        /// <param name="vipLevel">VIP level filter</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanCollateralAsset>>> GetCollateralAssetsAsync(string? collateralAsset = null, int? vipLevel = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the latest rate of collateral coin/loan coin when using collateral repay.
        /// </summary>
        /// <param name="loanAsset">Loan asset</param>
        /// <param name="collateralAsset">Collateral asset</param>
        /// <param name="quantity">Repay amount</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceCryptoLoanRepayRate>> GetCollateralRepayRateAsync(string loanAsset, string collateralAsset, decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Customize margin call (FULLY RETIRED)
        /// </summary>
        /// <param name="marginCall">Margin call value</param>
        /// <param name="orderId">Order ID filter</param>
        /// <param name="collateralAsset">Collateral asset filter</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceCryptoLoanMarginCallResult>>> CustomizeMarginCallAsync(decimal marginCall, string? orderId = null, string? collateralAsset = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
