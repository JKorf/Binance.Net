using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Spot.LendingData;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.SubClients
{
    /// <summary>
    /// Lending interface
    /// </summary>
    public interface IBinanceClientLending
    {
        /// <summary>
        /// Get product list
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="featured">Filter by featured</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of product</returns>
        WebCallResult<IEnumerable<BinanceSavingsProduct>> GetFlexibleProductList(ProductStatus? status = null,
            bool? featured = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get product list
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="featured">Filter by featured</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of product</returns>
        Task<WebCallResult<IEnumerable<BinanceSavingsProduct>>> GetFlexibleProductListAsync(ProductStatus? status = null, bool? featured = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the purchase quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        WebCallResult<BinancePurchaseQuotaLeft> GetLeftDailyPurchaseQuotaOfFlexableProduct(string productId,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the purchase quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        Task<WebCallResult<BinancePurchaseQuotaLeft>> GetLeftDailyPurchaseQuotaOfFlexableProductAsync(string productId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="amount">The amount to purchase</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        WebCallResult<BinanceLendingPurchaseResult> PurchaseFlexibleProduct(string productId,
            decimal amount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="amount">The amount to purchase</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseFlexibleProductAsync(string productId, decimal amount, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the redemption quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        WebCallResult<BinanceRedemptionQuotaLeft> GetLeftDailyRedemptionQuotaOfFlexibleProduct(string productId, RedeemType type,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the redemption quota left for a product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        Task<WebCallResult<BinanceRedemptionQuotaLeft>> GetLeftDailyRedemptionQuotaOfFlexibleProductAsync(string productId, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Redeem type</param>
        /// <param name="amount">The amount to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        WebCallResult<object> RedeemFlexibleProduct(string productId,
            decimal amount, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem flexible product
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Redeem type</param>
        /// <param name="amount">The amount to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> RedeemFlexibleProductAsync(string productId, decimal amount, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Flexible product position</returns>
        WebCallResult<IEnumerable<BinanceFlexibleProductPosition>> GetFlexibleProductPosition(
            string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Flexible product position</returns>
        Task<WebCallResult<IEnumerable<BinanceFlexibleProductPosition>>> GetFlexibleProductPositionAsync(string asset, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get fixed and customized fixed project list
        /// </summary>
        /// <param name="type">Type of project</param>
        /// <param name="asset">Asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="sortAscending">If should sort ascending</param>
        /// <param name="sortBy">Sort by. Valid values: "START_TIME", "LOT_SIZE", "INTEREST_RATE", "DURATION"; default "START_TIME"</param>
        /// <param name="currentPage">Result page</param>
        /// <param name="size">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Project list</returns>
        WebCallResult<IEnumerable<BinanceProject>> GetFixedAndCustomizedFixedProjectList(
            ProjectType type, string? asset = null, ProductStatus? status = null, bool? sortAscending = null,
            string? sortBy = null, int? currentPage = null, int? size = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get fixed and customized fixed project list
        /// </summary>
        /// <param name="type">Type of project</param>
        /// <param name="asset">Asset</param>
        /// <param name="status">Filter by status</param>
        /// <param name="sortAscending">If should sort ascending</param>
        /// <param name="sortBy">Sort by. Valid values: "START_TIME", "LOT_SIZE", "INTEREST_RATE", "DURATION"; default "START_TIME"</param>
        /// <param name="currentPage">Result page</param>
        /// <param name="size">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Project list</returns>
        Task<WebCallResult<IEnumerable<BinanceProject>>> GetFixedAndCustomizedFixedProjectListAsync(
            ProjectType type, string? asset = null, ProductStatus? status = null, bool? sortAscending = null, string? sortBy = null, int? currentPage = null, int? size = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase customized fixed project
        /// </summary>
        /// <param name="projectId">Id of the project</param>
        /// <param name="lot">The lot</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        WebCallResult<BinanceLendingPurchaseResult> PurchaseCustomizedFixedProject(string projectId, int lot,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase customized fixed project
        /// </summary>
        /// <param name="projectId">Id of the project</param>
        /// <param name="lot">The lot</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseCustomizedFixedProjectAsync(string projectId, int lot, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get customized fixed project position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="projectId">The project id</param>
        /// <param name="status">Filter by status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Customized fixed project position</returns>
        WebCallResult<IEnumerable<BinanceCustomizedFixedProjectPosition>> GetCustomizedFixedProjectPositions(
            string asset, string? projectId = null, ProjectStatus? status = null, long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get customized fixed project position
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="projectId">The project id</param>
        /// <param name="status">Filter by status</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Customized fixed project position</returns>
        Task<WebCallResult<IEnumerable<BinanceCustomizedFixedProjectPosition>>> GetCustomizedFixedProjectPositionsAsync(string asset, string? projectId = null, ProjectStatus? status = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get lending account info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Lending account</returns>
        WebCallResult<BinanceLendingAccount> GetLendingAccount(long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get lending account info
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Lending account</returns>
        Task<WebCallResult<BinanceLendingAccount>> GetLendingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get purchase records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The purchase records</returns>
        WebCallResult<IEnumerable<BinancePurchaseRecord>> GetPurchaseRecords(LendingType lendingType,
            string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get purchase records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The purchase records</returns>
        Task<WebCallResult<IEnumerable<BinancePurchaseRecord>>> GetPurchaseRecordsAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The redemption records</returns>
        WebCallResult<IEnumerable<BinanceRedemptionRecord>> GetRedemptionRecords(LendingType lendingType,
            string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get redemption records
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The redemption records</returns>
        Task<WebCallResult<IEnumerable<BinanceRedemptionRecord>>> GetRedemptionRecordsAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get interest history
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The interest history</returns>
        WebCallResult<IEnumerable<BinanceLendingInterestHistory>> GetLendingInterestHistory(LendingType lendingType,
            string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10,
            long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get interest history
        /// </summary>
        /// <param name="lendingType">Lending type</param>
        /// <param name="asset">Asset</param>
        /// <param name="page">Results page</param>
        /// <param name="startTime">Filter by startTime from</param>
        /// <param name="endTime">Filter by endTime from</param>
        /// <param name="limit">Limit of the amount of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The interest history</returns>
        Task<WebCallResult<IEnumerable<BinanceLendingInterestHistory>>> GetLendingInterestHistoryAsync(LendingType lendingType, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = 1, int? limit = 10, long? receiveWindow = null, CancellationToken ct = default);
    }
}