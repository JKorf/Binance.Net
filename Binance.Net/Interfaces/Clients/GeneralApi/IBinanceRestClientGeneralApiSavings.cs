using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot.Lending;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance Spot Savings endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiSavings
    {
        /// <summary>
        /// Get product list
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-list-user_data" /></para>
        /// </summary>
        /// <param name="status">Filter by status</param>
        /// <param name="featured">Filter by featured</param>
        /// <param name="page">Page to retrieve</param>
        /// <param name="pageSize">Page size to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of product</returns>
        Task<WebCallResult<IEnumerable<BinanceSavingsProduct>>> GetFlexibleProductListAsync(ProductStatus? status = null, string asset = null, bool? featured = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the purchase quota left for a product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-left-daily-purchase-quota-of-flexible-product-user_data" /></para>
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        Task<WebCallResult<BinancePurchaseQuotaLeft>> GetLeftDailyPurchaseQuotaOfFlexableProductAsync(string productId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Purchase flexible product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#purchase-flexible-product-user_data" /></para>
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="quantity">The quantity to purchase</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseFlexibleProductAsync(string productId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the redemption quota left for a product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-left-daily-redemption-quota-of-flexible-product-user_data" /></para>
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Quota left</returns>
        Task<WebCallResult<BinanceRedemptionQuotaLeft>> GetLeftDailyRedemptionQuotaOfFlexibleProductAsync(string productId, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem flexible product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-flexible-product-user_data" /></para>
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="type">Redeem type</param>
        /// <param name="quantity">The quantity to redeem</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<object>> RedeemFlexibleProductAsync(string productId, decimal quantity, RedeemType type, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product position
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-position-user_data" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="page">Page to retrieve</param>
        /// <param name="pageSize">Page size to return</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Flexible product position(s)</returns>
        Task<WebCallResult<IEnumerable<BinanceFlexibleProductPosition>>> GetFlexibleProductPositionAsync(string? asset = null, string? productId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get fixed and customized fixed project list
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-fixed-and-activity-project-list-user_data" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#purchase-fixed-activity-project-user_data" /></para>
        /// </summary>
        /// <param name="projectId">Id of the project</param>
        /// <param name="lot">The lot</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingPurchaseResult>> PurchaseCustomizedFixedProjectAsync(string projectId, int lot, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get customized fixed project position
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-fixed-activity-project-position-user_data" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#lending-account-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Lending account</returns>
        Task<WebCallResult<BinanceLendingAccount>> GetLendingAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get purchase records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-purchase-record-user_data" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-redemption-record-user_data" /></para>
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
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-interest-history-user_data-2" /></para>
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

        /// <summary>
        /// Changed fixed/activity position to daily position
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#change-fixed-activity-position-to-daily-position-user_data" /></para>
        /// </summary>
        /// <param name="projectId">Id of the project</param>
        /// <param name="lot">The lot</param>
        /// <param name="positionId">For fixed position</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Purchase id</returns>
        Task<WebCallResult<BinanceLendingChangeToDailyResult>> ChangeToDailyPositionAsync(string projectId, int lot,
            long? positionId = null,
            long? receiveWindow = null, CancellationToken ct = default);
    }
}
