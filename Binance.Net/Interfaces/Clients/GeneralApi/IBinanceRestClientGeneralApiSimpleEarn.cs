using System;
using System.Threading;
using System.Threading.Tasks;
using Binance.Net.Enums;
using Binance.Net.Objects.Models;
using Binance.Net.Objects.Models.Spot.SimpleEarn;
using CryptoExchange.Net.Objects;

namespace Binance.Net.Interfaces.Clients.GeneralApi
{
    /// <summary>
    /// Binance simple earn endpoints
    /// </summary>
    public interface IBinanceRestClientGeneralApiSimpleEarn
    {
        /// <summary>
        /// Get a list of simple earn flexible products
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-simple-earn-flexible-product-list-user_data" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleProduct>>> GetFlexibleProductsAsync(string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get a list of simple earn locked products
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-simple-earn-locked-product-list-user_data" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedProduct>>> GetLockedProductsAsync(string? asset = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to flexible product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#subscribe-flexible-product-trade" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="autoSubscribe">Auto subscribe, default true</param>
        /// <param name="sourceAccount">Source account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnPurchase>> SubscribeFlexibleProductAsync(string productId, decimal quantity, bool? autoSubscribe = null, AccountSource? sourceAccount = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to locked product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#subscribe-locked-product-trade" /></para>
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="autoSubscribe">Auto subscribe, default true</param>
        /// <param name="sourceAccount">Source account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnPurchase>> SubscribeLockedProductAsync(string projectId, decimal quantity, bool? autoSubscribe = null, AccountSource? sourceAccount = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem flexible product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-flexible-product-trade" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="redeemAll">Whether to redeem all. If not then quantity should be specified</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="destinationAccount">Destination account</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnRedemption>> RedeemFlexibleProductAsync(string productId, bool? redeemAll = null, decimal? quantity = null, AccountSource? destinationAccount = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Redeem locked product
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#redeem-locked-product-trade" /></para>
        /// </summary>
        /// <param name="positionId">Position id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnRedemption>> RedeemLockedProductAsync(string positionId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product position info
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-product-position-user_data" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="productId">Filter by product id</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexiblePosition>>> GetFlexibleProductPositionsAsync(string? asset = null, string? productId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get locked product position info
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-locked-product-position-user_data" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="positionId">Filter by position id</param>
        /// <param name="projectId">Filter by project id</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedPosition>>> GetLockedProductPositionsAsync(string? asset = null, string? positionId = null, string? projectId = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn account info
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#simple-account-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnAccount>> GetAccountAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn flexible product subscription records 
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-subscription-record-user_data" /></para>
        /// </summary>
        /// <param name="productId">Filter by product id</param>
        /// <param name="purchaseId">Filter by purchase id</param>
        /// <param name="asset">Filler by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRecord>>> GetFlexibleSubscriptionRecordsAsync(string? productId = null, string? purchaseId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn locked product subscription records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-locked-subscription-record-user_data" /></para>
        /// </summary>
        /// <param name="purchaseId">Filter by purchase id</param>
        /// <param name="asset">Filler by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRecord>>> GetLockedSubscriptionRecordsAsync(string? purchaseId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn flexible product redemption records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-redemption-record-user_data" /></para>
        /// </summary>
        /// <param name="productId">Filter by product id</param>
        /// <param name="redeemId">Filler by redeem id</param>
        /// <param name="asset">Filler by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRedemptionRecord>>> GetFlexibleRedemptionRecordsAsync(string? productId = null, string? redeemId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn locked product redemption records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-locked-redemption-record-user_data" /></para>
        /// </summary>
        /// <param name="positionId">Filter by position id</param>
        /// <param name="redeemId">Filler by redeem id</param>
        /// <param name="asset">Filler by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRedemptionRecord>>> GetLockedRedemptionRecordsAsync(string? positionId = null, string? redeemId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn flexible product reward records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-rewards-history-user_data" /></para>
        /// </summary>
        /// <param name="type">Type or rewards</param>
        /// <param name="productId">Filter by product id</param>
        /// <param name="asset">Filler by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnFlexibleRewardRecord>>> GetFlexibleRewardRecordsAsync(RewardType type, string? productId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get Simple Earn locked product reward records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-locked-rewards-history-user_data" /></para>
        /// </summary>
        /// <param name="positionId">Position id</param>
        /// <param name="asset">Filler by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnLockedRewardRecord>>> GetLockedRewardRecordsAsync(string? positionId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set flexible product auto subscribe toggle
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#set-flexible-auto-subscribe-user_data" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="autoSubscribe">Auto subscribe enabled or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnResult>> SetFlexibleAutoSubscribeAsync(string productId, bool autoSubscribe, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set locked product auto subscribe toggle
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#set-locked-auto-subscribe-user_data" /></para>
        /// </summary>
        /// <param name="positionId">Position id</param>
        /// <param name="autoSubscribe">Auto subscribe enabled or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnResult>> SetLockedAutoSubscribeAsync(string positionId, bool autoSubscribe, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible product personal quota left
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-personal-left-quota-user_data" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnPersonalQuotaLeft>> GetFlexiblePersonalQuotaLeftAsync(string productId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get locked product personal quota left
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-locked-personal-left-quota-user_data" /></para>
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnPersonalQuotaLeft>> GetLockedPersonalQuotaLeftAsync(string projectId, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get flexible subscription preview
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-flexible-subscription-preview-user_data" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnFlexiblePreview>> GetFlexibleSubscriptionPreviewAsync(string productId, decimal quantity, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get locked subscription preview
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-locked-subscription-preview-user_data" /></para>
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="autoSubscribe">Auto subscribe</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceSimpleEarnLockedPreview>> GetLockedSubscriptionPreviewAsync(string projectId, decimal quantity, bool? autoSubscribe = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get rate history
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-rate-history-user_data" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnRateRecord>>> GetRateHistoryAsync(string productId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get collateral records
        /// <para><a href="https://binance-docs.github.io/apidocs/spot/en/#get-rate-history-user_data" /></para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BinanceQueryRecords<BinanceSimpleEarnCollateralRecord>>> GetCollateralRecordsAsync(string productId, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
